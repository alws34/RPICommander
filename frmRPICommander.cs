using Renci.SshNet;
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
        private List<Device> devices_lst = new List<Device>() { }; // list of all available devices
        private List<Command> commands_lst = new List<Command>() { };//dictionary of all available commands
        private List<Command> selected_commands = new List<Command>(); //list of selected commands to assign for the selected device

        private List<CheckBox> selected_devices = new List<CheckBox>(); //current device


        string commandsDBPath; //commandsDB .dat path
        string devicesDBPath; //devicesDB .dat path

        private int lastX;
        private int lastY;

        public FrmRPICommander()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            string dirpath = @"C:\ProgramData\RPI Commander";
            commandsDBPath = dirpath + @"\commands.dat";
            devicesDBPath = dirpath + @"\devices.dat";
            flpCommands.AutoScroll = true;
            CreateDir(dirpath);

            // read commands from DB
            if (File.Exists(commandsDBPath))
            {
                ReadCommands(commandsDBPath);
            }
            else//create commandsDB file if not exists;
            {
                CreateFile(commandsDBPath);
                AddNewCommand(commandsDBPath);
            }

            //read device list from DB
            if (File.Exists(devicesDBPath))
                ReadDevices(devicesDBPath);
            else
            {
                CreateFile(devicesDBPath);//create devicesDB file if not exists;
                AddNewDevice(devicesDBPath); //add first device to db
            }

            CommandsControls(commands_lst);
            DevicesControls(devices_lst);
        }

        private void CommandsControls(List<Command> commands)//creates the commands controls
        {
            //create commands FLP controls
            foreach (Command command in commands)//name, [user,pass]
            {
                CheckBox chkbox = new CheckBox
                {
                    Name = command.Command_description,
                    Text = command.Command_name,
                    Tag = command
                };
                chkbox.CheckedChanged += C_CheckedChanged;
                chkbox.MouseMove += FlpDevices_MouseMove;

                flpCommands.Controls.Add(chkbox);
            }
        }

        private void DevicesControls(List<Device> devices)//creates the devices controls
        {
            //create devices FLP controls
            foreach (Device device in devices)
            {
                CheckBox cb = new CheckBox
                {
                    Name = $"{device.User_name}^{device.Password}",
                    Text = device.Device_Hostname,
                    Tag = device
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
                        {
                            Command command = new Command(keyvalue[0], keyvalue[1]);
                            commands_lst.Add(command);
                        }
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
                        string[] devic_creds = line.Split('^');
                        if (devic_creds.Length == 3)
                        {
                            Device device = new Device(devic_creds[0], devic_creds[1], devic_creds[2]);
                            devices_lst.Add(device);
                        }
                    }
                    devices_lst.Sort();
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
                    f.Close();
                }
            }
            catch (Exception e)
            {
                Showmessage(e.ToString());
            }
        }


        private void AddNewDevice(string devicesDB)//add device to db (frmAddDevice)
        {
            frmAddDevice add_device = new frmAddDevice(devicesDB);
            add_device.Show();
        }
        private void AddNewCommand(string commandsDB)//add commands to DB (frmAddCommand)
        {
            frmAddCommand addcommand = new frmAddCommand(commandsDB);
            addcommand.Show();
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
            devices_lst.Clear();
            commands_lst.Clear();
            Init();
        }


        private void C_CheckedChanged(object sender, EventArgs e)//checkbox state change
        {
            CheckBox c = (CheckBox)sender;
            if (c.Checked)
                selected_commands.Add((Command)c.Tag);//add command to selected commands
            else
                selected_commands.Remove((Command)c.Tag);//remove command from selected command
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)//CheckBox state changed
        {
            CheckBox c = (CheckBox)sender;
            selected_devices.Add(c);
            flpCommands.Enabled = true;
        }

        private void BtnStart_Click(object sender, EventArgs e)//start install
        {
            foreach (CheckBox c in selected_devices)
            {
                Device device = c.Tag as Device;
                using (var client = new SshClient(device.Device_Hostname, device.Port, device.User_name, device.Password))
                {
                    client.Connect();

                    //foreach (string command in device.Get_Commands())
                    //    client.RunCommand(command);
                    foreach (Command command in selected_commands)
                        client.RunCommand(command.Command_description);
                    
                    client.Disconnect();
                }
            }
        }

        protected void BtnAddDevice_Click(object sender, EventArgs e)
        {
            AddNewDevice(devicesDBPath);
        }

        protected void BtnAddCommand_Click(object sender, EventArgs e)
        {
            AddNewCommand(commandsDBPath);
        }

        protected void BtnEditDB_Click(object sender, EventArgs e)
        {
            frmEditDBs editdb = new frmEditDBs(devicesDBPath, commandsDBPath);
            editdb.Show();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Reset();
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