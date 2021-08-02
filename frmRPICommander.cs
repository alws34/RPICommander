using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPICommander
{
    public partial class frmRPICommander : Form
    {
        private Dictionary<string, string> commands = new Dictionary<string, string>() { };//dictionary of all available commands
        private List<string> devices = new List<string>(); // list of all available devices

        private List<string> selected_commands = new List<string>(); //list of selected commands to assign for the selected device

        private List<string> batches = new List<string>();//list of paths for created batch files - for parallel running and clening up at the end
        private List<string> commandsfiles = new List<string>();//list of paths for created text (commands) files - for clening up at the end

        private List<CheckBox> current_devices = new List<CheckBox>(); //current device
        CheckBox current_command = new CheckBox(); //current command

        string commandsDB; //commandsDB .dat path
        string devicesDB; //devicesDB .dat path

        private int lastX;
        private int lastY;

        public frmRPICommander()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            // flpCommands.Enabled = false;
            string dirpath = @"C:\ProgramData\RPI Commander";
            commandsDB = dirpath + @"\commands.dat";
            devicesDB = dirpath + @"\devices.dat";

            createDir(dirpath);

            // read commands from DB
            if (File.Exists(commandsDB))
            {
                readcommands(commandsDB);
            }
            else
            {
                createFile(commandsDB);//create commandsDB file if not exists;
                addCommand(commandsDB);
            }

            //read device list from DB
            if (File.Exists(devicesDB))
            {
                readdevices(devicesDB);
            }
            else
            {
                createFile(devicesDB);//create devicesDB file if not exists;
                newdevice(devicesDB); //add first device to db
            }

            setCommandsFileSystemWatcher();
            setDevicesFileSystemWatcher();
            commandsControls(commands);
            devicesControls(devices);
        }

        private void setCommandsFileSystemWatcher()//sets the commandsDB.dat File System Watcher
        {
            fileSystemWatcherCommandsWatch.Path = Directory.GetParent(commandsDB).ToString();//set file system watcher only for changes in the commandsDB file
            fileSystemWatcherDevicesDB.Filter = Path.GetFileName(commandsDB);
        }

        private void setDevicesFileSystemWatcher()//sets the devicesDB.dat File System Watcher
        {
            fileSystemWatcherDevicesDB.Path = Directory.GetParent(devicesDB).ToString();//set file system watcher only for changes in the devicesDB file
            fileSystemWatcherDevicesDB.Filter = Path.GetFileName(devicesDB);
        }

        private void commandsControls(Dictionary<string, string> commands)//creates the commands controls
        {
            //create commands FLP controls
            foreach (KeyValuePair<string, string> item in commands)
            {
                CheckBox chkbox = new CheckBox();
                chkbox.Name = item.Value;
                chkbox.Text = item.Key;
                chkbox.CheckedChanged += c_checkedchanged;
                chkbox.MouseMove += flpDevices_MouseMove;
                flpCommands.AutoScroll = true;
                flpCommands.Controls.Add(chkbox);
            }
        }

        private void devicesControls(List<string> devices)//creates the devices controls
        {
            //create devices FLP controls
            foreach (string item in devices)
            {
                string[] keyvalue = item.Split('^');
                CheckBox cb = new CheckBox();
                cb.Name = keyvalue[0];
                cb.Text = keyvalue[0];
                cb.CheckedChanged += cb_checkedchanged;
                cb.MouseMove += flpDevices_MouseMove;
                flpDevices.Controls.Add(cb);
            }
        }

        private void readcommands(string db) //read commands from DB
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] keyvalue = line.Split('^');
                        if (keyvalue.Length == 2)
                        {
                            commands.Add(keyvalue[0], keyvalue[1]);
                        }
                    }
                }
            }
            catch (IOException)
            {
                reset();
            }
            catch (ArgumentException e)
            {
                //showmessage(e.ToString());
            }
        }

        private void readdevices(string db)//read devices from DB
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] device = line.Split('^');
                        if (device.Length == 3)
                        {
                            string str = device[0] + "^" + device[1] + "^" + device[2];
                            devices.Add(str);
                        }
                        //else
                        //    showmessage("error reading device list! @frmRPICommander - readdevices");
                    }
                }
            }
            catch (IOException)
            {
                reset();
            }
            catch (ArgumentException)
            {
                //showmessage(e.ToString());
            }
        }

        private void addCommand(string commandsDB)//add commands to DB (frmAddCommand)
        {
            frmAddCommand addcommand = new frmAddCommand(commandsDB);
            addcommand.Show();
        }

        private void createDir(string path) //create directories
        {
            try
            {
                if (!(Directory.Exists(path)))//create directory if not exist
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                showmessage(e.ToString());
            }
        }

        private void createFile(string path)//create files
        {
            try
            {
                if (!(File.Exists(path))) //create file if not exist
                {
                    var f = File.Create(path);
                    f.Close();
                }
            }
            catch (Exception e)
            {
                showmessage(e.ToString());
            }
        }

        private void startInstall()//start batch
        {
            foreach (CheckBox c in current_devices)
            {
                string commandsfilepath = @"C:\ProgramData\RPI Commander\" + c.Text + "_commands.txt";//text file to run the commands from to hold the commands for specific device
                string batch = @"C:\ProgramData\RPI Commander\" + c.Text + "_commands.bat";

                createFile(commandsfilepath); // checks if device file doesnt exists, and creates it if needed
                createFile(batch);

                batches.Add(batch);
                commandsfiles.Add(commandsfilepath);

                try
                {
                    using (StreamWriter sr = new StreamWriter(commandsfilepath))//write commands to txt file
                    {
                        foreach (string command in selected_commands)
                        {
                            sr.WriteLine(command);
                        }
                    }

                    using (StreamWriter sr = new StreamWriter(batch))//write batch file
                    {
                        string device = "";
                        string username = "";
                        string password = "";

                        foreach (string d in devices)
                        {
                            string[] device_arr = d.Split('^');
                            if (device_arr.Length == 3)//make sure splitting done correctly
                            {
                                if (c.Text == device_arr[0])//make sure selected device is the same as intended
                                {
                                    device = device_arr[0];
                                    username = device_arr[1];
                                    password = device_arr[2];
                                }
                            }
                        }

                        string sshCommand = "putty.exe -ssh " + username + "@" + device + " -pw " + password + " -m " + '\u0022' + commandsfilepath + '\u0022';
                        sr.WriteLine(sshCommand); // write ssh command to file
                    }
                }
                catch (IOException)
                {
                    reset();
                }
                catch (ArgumentException e)
                {
                    //showmessage(e.ToString());
                }

                try
                {
                    foreach (string b in batches)
                        Process.Start(b); // run batch file to install
                }
                catch (Win32Exception e)
                {
                    showmessage(e.ToString());
                }
            }
        }

        private void newdevice(string devicesDB)//add device to db (frmAddDevice)
        {
            frmAddDevice add_device = new frmAddDevice(devicesDB);
            add_device.Show();
        }


        private void showmessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void reset()
        {
            flpDevices.Enabled = true;
            flpCommands.Controls.Clear();
            flpDevices.Controls.Clear();
            devices.Clear();
            commands.Clear();
            init();
        }

        private void clearfiles()//delete all txt and bat files at exit
        {
            foreach (string file in batches)//delete all batch files at exit
            {
                File.Delete(file);
            }
            foreach (string file in commandsfiles)//delete all command txt files at exit
            {
                File.Delete(file);
            }
        }

        private void c_checkedchanged(object sender, EventArgs e)//checkbox state change
        {
            CheckBox c = (CheckBox)sender;
            if (c.Checked)
            {
                selected_commands.Add(commands[c.Text]);//add command to selected commands
            }
            else
            {
                selected_commands.Remove(commands[c.Text]);//remove command from selected command
            }
        }

        private void cb_checkedchanged(object sender, EventArgs e)//CheckBox state changed
        {
            CheckBox c = (CheckBox)sender;
            current_devices.Add(c); // add device to ssh intended devices list
            flpCommands.Enabled = true;//be able to choose commands only after a device been chosen
            //flpDevices.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)//start install
        {
            startInstall();
            //flpDevices.Enabled = true;
        }

        protected void btnAddDevice_Click(object sender, EventArgs e)
        {
            newdevice(devicesDB);
        }

        protected void btnAddCommand_Click(object sender, EventArgs e)
        {
            addCommand(commandsDB);
        }

        protected void btnEditDB_Click(object sender, EventArgs e)
        {
            frmEditDBs editdb = new frmEditDBs(devicesDB, commandsDB);
            editdb.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void frmRPICommander_FormClosing(object sender, FormClosingEventArgs e)
        {
            clearfiles();
        }

        private void fileSystemWatcherDevicesDB_Changed(object sender, FileSystemEventArgs e)
        {
            foreach (string item in devices)
            {
                CheckBox cb = new CheckBox();
                cb.Name = item;
                cb.Text = item;
                cb.CheckedChanged += cb_checkedchanged;
                cb.MouseMove += flpDevices_MouseMove;
                if (!(flpDevices.Controls.Contains(cb)))
                {
                    reset();
                    return;
                }
            }
        }

        private void fileSystemWatcherCommandsWatch_Changed(object sender, FileSystemEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in commands)
            {
                CheckBox chkbox = new CheckBox();
                chkbox.Name = item.Value;
                chkbox.Text = item.Key;
                chkbox.CheckedChanged += c_checkedchanged;

                if (!(flpCommands.Controls.Contains(chkbox)))
                {
                    reset();
                    return;
                }

            }
        }

        private void flpDevices_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X != this.lastX || e.Y != this.lastY)
            {
                CheckBox c = (CheckBox)sender;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(c, c.Text + "\n" + c.Name);

                this.lastX = e.X;
                this.lastY = e.Y;
            }
        }
    }
}