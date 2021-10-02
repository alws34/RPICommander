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
    public partial class FrmRPICommander : Form
    {
        private Dictionary<string, string> commands = new Dictionary<string, string>() { };//dictionary of all available commands
        private List<string> devices = new List<string>(); // list of all available devices

        private List<string> selected_commands = new List<string>(); //list of selected commands to assign for the selected device

        private List<string> batches = new List<string>();//list of paths for created batch files - for parallel running and clening up at the end
        private List<string> commandsfiles = new List<string>();//list of paths for created text (commands) files - for clening up at the end

        private List<CheckBox> current_devices = new List<CheckBox>(); //current device

        string commandsDB; //commandsDB .dat path
        string devicesDB; //devicesDB .dat path

        private int lastX;
        private int lastY;

        public FrmRPICommander()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            // flpCommands.Enabled = false;
            string dirpath = @"C:\ProgramData\RPI Commander";
            commandsDB = dirpath + @"\commands.dat";
            devicesDB = dirpath + @"\devices.dat";

            CreateDir(dirpath);

            // read commands from DB
            if (File.Exists(commandsDB))
                ReadCommands(commandsDB);
            else
            {
                CreateFile(commandsDB);//create commandsDB file if not exists;
                AddCommand(commandsDB);
            }

            //read device list from DB
            if (File.Exists(devicesDB))
                ReadDevices(devicesDB);
            else
            {
                CreateFile(devicesDB);//create devicesDB file if not exists;
                NewDevice(devicesDB); //add first device to db
            }

            SetCommandsFileSystemWatcher();
            SetDevicesFileSystemWatcher();
            CommandsControls(commands);
            DevicesControls(devices);
        }

        private void SetCommandsFileSystemWatcher()//sets the commandsDB.dat File System Watcher
        {
            fileSystemWatcherCommandsWatch.Path = Directory.GetParent(commandsDB).ToString();//set file system watcher only for changes in the commandsDB file
            fileSystemWatcherDevicesDB.Filter = Path.GetFileName(commandsDB);
        }

        private void SetDevicesFileSystemWatcher()//sets the devicesDB.dat File System Watcher
        {
            fileSystemWatcherDevicesDB.Path = Directory.GetParent(devicesDB).ToString();//set file system watcher only for changes in the devicesDB file
            fileSystemWatcherDevicesDB.Filter = Path.GetFileName(devicesDB);
        }

        private void CommandsControls(Dictionary<string, string> commands)//creates the commands controls
        {
            //create commands FLP controls
            foreach (KeyValuePair<string, string> item in commands)
            {
                CheckBox chkbox = new CheckBox
                {
                    Name = item.Value,
                    Text = item.Key
                };
                chkbox.CheckedChanged += C_CheckedChanged;
                chkbox.MouseMove += FlpDevices_MouseMove;
                flpCommands.AutoScroll = true;
                flpCommands.Controls.Add(chkbox);
            }
        }

        private void DevicesControls(List<string> devices)//creates the devices controls
        {
            //create devices FLP controls
            foreach (string item in devices)
            {
                string[] keyvalue = item.Split('^');
                CheckBox cb = new CheckBox
                {
                    Name = keyvalue[0],
                    Text = keyvalue[0]
                };
                cb.CheckedChanged += Cb_CheckedChanged;
                cb.MouseMove += FlpDevices_MouseMove;
                flpDevices.Controls.Add(cb);
            }
        }

        private void ReadCommands(string db) //read commands from DB
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
                            commands.Add(keyvalue[0], keyvalue[1]);
                    }
                }
            }
            catch (IOException)
            {
                Reset();
            }
            catch (ArgumentException)
            {
                //Showmessage(e.ToString());
                return;
            }
        }

        private void ReadDevices(string db)//read devices from DB
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
                        //    Showmessage("error reading device list! @FrmRPICommander - ReadDevices");
                    }
                    devices.Sort();//sort device list
                }
            }
            catch (IOException)
            {
                Reset();
            }
            catch (ArgumentException)
            {
                //Showmessage(e.ToString());
                return;
            }
        }

        private void AddCommand(string commandsDB)//add commands to DB (frmAddCommand)
        {
            frmAddCommand addcommand = new frmAddCommand(commandsDB);
            addcommand.Show();
        }

        private void CreateDir(string path) //create directories
        {
            try
            {
                if (!(Directory.Exists(path)))//create directory if not exist
                    Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Showmessage(e.ToString());
            }
        }

        private void CreateFile(string path)//create files
        {
            try
            {
                if (!(File.Exists(path))) //create file if not exist
                {
                    var f = File.Create(path);
                    f.Close();//make sure the file is closed for later use
                }
            }
            catch (Exception e)
            {
                Showmessage(e.ToString());
            }
        }

        private void StartInstall()//start batch
        {
            foreach (CheckBox c in current_devices)
            {
                string commandsfilepath = @"C:\ProgramData\RPI Commander\" + c.Text + "_commands.txt";//text file to run the commands from to hold the commands for specific device
                string batch = @"C:\ProgramData\RPI Commander\" + c.Text + "_commands.bat";

                CreateFile(commandsfilepath); // checks if device file doesnt exists, and creates it if needed
                CreateFile(batch);

                batches.Add(batch);
                commandsfiles.Add(commandsfilepath);

                try
                {
                    using (StreamWriter sr = new StreamWriter(commandsfilepath))//write commands to txt file
                        foreach (string command in selected_commands)
                            sr.WriteLine(command);

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
                    Reset();
                }
                catch (ArgumentException)
                {
                    //Showmessage(e.ToString());
                    return;
                }

                try
                {
                    foreach (string b in batches)
                        Process.Start(b); // run batch file to install
                }
                catch (Win32Exception e)
                {
                    Showmessage(e.ToString());
                }
            }
        }

        private void NewDevice(string devicesDB)//add device to db (frmAddDevice)
        {
            frmAddDevice add_device = new frmAddDevice(devicesDB);
            add_device.Show();
        }


        private void Showmessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void Reset()
        {
            flpDevices.Enabled = true;
            flpCommands.Controls.Clear();
            flpDevices.Controls.Clear();
            devices.Clear();
            commands.Clear();
            Init();
        }

        private void ClearFiles()//delete all txt and bat files at exit
        {
            foreach (string file in batches)//delete all batch files at exit
                File.Delete(file);
            foreach (string file in commandsfiles)//delete all command txt files at exit
                File.Delete(file);
        }

        private void C_CheckedChanged(object sender, EventArgs e)//checkbox state change
        {
            CheckBox c = (CheckBox)sender;
            if (c.Checked)
                selected_commands.Add(commands[c.Text]);//add command to selected commands
            else
                selected_commands.Remove(commands[c.Text]);//remove command from selected command
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)//CheckBox state changed
        {
            CheckBox c = (CheckBox)sender;
            current_devices.Add(c); // add device to ssh intended devices list
            flpCommands.Enabled = true;//be able to choose commands only after a device been chosen
            //flpDevices.Enabled = false;
        }

        private void BtnStart_Click(object sender, EventArgs e)//start install
        {
            StartInstall();
            //flpDevices.Enabled = true;
        }

        protected void BtnAddDevice_Click(object sender, EventArgs e)
        {
            NewDevice(devicesDB);
        }

        protected void BtnAddCommand_Click(object sender, EventArgs e)
        {
            AddCommand(commandsDB);
        }

        protected void BtnEditDB_Click(object sender, EventArgs e)
        {
            frmEditDBs editdb = new frmEditDBs(devicesDB, commandsDB);
            editdb.Show();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void FrmRPICommander_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearFiles();
        }

        private void FileSystemWatcherDevicesDB_Changed(object sender, FileSystemEventArgs e)
        {
            foreach (string item in devices)
            {
                CheckBox cb = new CheckBox
                {
                    Name = item,
                    Text = item
                };
                cb.CheckedChanged += Cb_CheckedChanged;
                cb.MouseMove += FlpDevices_MouseMove;
                if (!flpDevices.Controls.Contains(cb))
                {
                    Reset();
                    return;
                }
            }
        }

        private void FileSystemWatcherCommandsWatch_Changed(object sender, FileSystemEventArgs e)
        {
            foreach (KeyValuePair<string, string> item in commands)
            {
                CheckBox chkbox = new CheckBox
                {
                    Name = item.Value,
                    Text = item.Key
                };
                chkbox.CheckedChanged += C_CheckedChanged;

                if (!flpCommands.Controls.Contains(chkbox))
                {
                    Reset();
                    return;
                }

            }
        }

        private void FlpDevices_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X != lastX || e.Y != lastY)// fixes the tooltip flickering - remember the last mouse position and will not change the tooltip position (re-render it) unless the mouse moves again (changes position)
            {
                CheckBox c = (CheckBox)sender;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(c, c.Text + "\n" + c.Name);

                //update mouse position to new parameters
                lastX = e.X;
                lastY = e.Y;
            }
        }
    }
}