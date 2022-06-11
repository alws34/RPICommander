namespace RPICommander
{
    public partial class frmEditDBs : Form
    {
        private List<Command> commands = new List<Command>();
        private List<Device> devices = new List<Device>();
        string devicesDBpath, commandsDBpath, current_device;


        public event SendMessageToConsoleEventHandler SendMessageToConsole;

        protected virtual void SendMessageToConsoleEvent(string msg)
        {
            SendMessageToConsole?.Invoke(new SendMessageToConsoleEventArgs(msg));
        }

        public event SetPlaceHolderEventHandler SetPlaceHolder;
        protected virtual void OnTBFocusChange(TextBox tb)
        {
            SetPlaceHolder?.Invoke(new SetPlaceHolderEventArgs(tb));
        }

        public frmEditDBs(string devicesdb, string commandsdb)
        {
            InitializeComponent();
            if (!Init())
                return;
            devicesDBpath = devicesdb;
            commandsDBpath = commandsdb;
            CenterToScreen();
        }

        public frmEditDBs(string devicesdb, string commandsdb, bool isEmpty)
        {
            InitializeComponent();
            devicesDBpath = devicesdb;
            commandsDBpath = commandsdb;
            CenterToScreen();
        }

        public bool Init()
        {
            string dirpath = @"C:\ProgramData\RPI Commander";
            string commandsDB = dirpath + @"\commands.dat";
            string devicesDB = dirpath + @"\devices.dat";

            // read commands from DB
            if (!File.Exists(commandsDB) || !File.Exists(devicesDB))
                return false;
            if (!ReadCommandsFromDB(commandsDB) || !ReadDevicesFromDB(devicesDB) || !setcommandsControls(commands) || !setdevicesControls(devices))
                return false;
            setEvents();
            return true;
        }

        private bool setcommandsControls(List<Command> commands)
        {
            try
            {
                foreach (Command cmd in commands)
                    listBoxCommands.Items.Add(cmd);
            }
            catch (Exception e)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
                return false;
            }
            return true;
        }

        private bool setdevicesControls(List<Device> devices)
        {
            try
            {
                foreach (Device device in devices)
                    listBoxDevices.Items.Add(device.Device_Hostname);
            }
            catch (Exception e)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
                return false;
            }
            return true;
        }

        private void setEvents()
        {
            listBoxCommands.DoubleClick += new EventHandler(listBoxCommands_DoubleClick);
            listBoxDevices.DoubleClick += new EventHandler(listBoxDevices_DoubleClick);
        }

        private bool ReadCommandsFromDB(string db)
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    int counter = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] keyvalue = line.Split('^');
                        if (keyvalue.Length != 2)
                        {
                            SendMessageToConsoleEvent($"Error Reading Command from DB: {keyvalue}");
                            continue;
                        }
                        commands.Add(new Command(keyvalue[0], keyvalue[1]));
                        counter++;
                    }
                    SendMessageToConsoleEvent($"Read {counter} Commands from DB.");
                }
            }
            catch (Exception e)
            {
                SendMessageToConsoleEvent($"Error Reading Commands from DB\n\t{e}");
                return false;
            }
            return true;
        }

        private bool ReadDevicesFromDB(string db)
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    int counter = 0;
                    while ((!string.IsNullOrWhiteSpace(line = sr.ReadLine())))
                    {
                        string[] dev = line.Split('^');
                        if (dev.Length != 4)
                        {
                            SendMessageToConsoleEvent($"Error Reading from DB: {dev}");
                            continue;
                        }
                        counter++;
                        Device device = new Device(dev[0], dev[1], dev[2], int.Parse(dev[3]));
                        devices.Add(device);
                    }
                    SendMessageToConsoleEvent($"Read {counter} Devices from DB.");
                }
            }
            catch (Exception e)
            {
                SendMessageToConsoleEvent($"Error Reading Devices from DB\n\t{e}");
                return false;
            }
            return true;
        }

        private void saveChanges()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(devicesDBpath))
                    foreach (Device device in devices)
                        sw.WriteLine(device.ToString());

                using (StreamWriter sw = new StreamWriter(devicesDBpath))
                    foreach (Command cmd in commands)
                        sw.WriteLine(cmd.ToString());
            }
            catch (Exception e)
            {
                SendMessageToConsoleEvent($"Error Saving to DB!\n\t{e}");
                return;
            }
            SendMessageToConsoleEvent($"Changes Saved Successfully");
        }

        private void ResetFormFields()
        {
            listBoxDevices.Items.Clear();
            listBoxCommands.Items.Clear();
            //OnSendMessageToConsoleEvent($"Form Cleared!");
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ResetFormFields();
            setdevicesControls(devices);
            setcommandsControls(commands);
        }

        private void listBoxCommands_DoubleClick(object sender, EventArgs e)
        {
            Command current_command = (Command)listBoxCommands.SelectedItem;
            if (current_command != null)
            {
                listBoxCommands.Items.RemoveAt(listBoxCommands.SelectedIndex);
                frmAddCommand frmEditCommand = new frmAddCommand(commandsDBpath, current_command, commands);
                frmEditCommand.SendMessageToConsole += SendMessageToConsole;
                frmEditCommand.SetPlaceHolder += SetPlaceHolder;
                frmEditCommand.Show();
                SendMessageToConsoleEvent($"Editing command {current_command}...");
                //Dispose();
            }
        }

        private void listBoxDevices_DoubleClick(object sender, EventArgs e)
        {
            current_device = listBoxDevices.SelectedItem.ToString();
            //Device device = new Device();
            if (current_device != null)
            {
                //for (int i = devices.Count - 1; i > -1; i--)
                //    if (devices[i].Device_Hostname == current_device)
                //        device = devices[i];

                listBoxDevices.Items.RemoveAt(listBoxDevices.SelectedIndex);
                frmAddDevice frmEditDevice = new frmAddDevice(devicesDBpath, current_device);
                frmEditDevice.SendMessageToConsole += SendMessageToConsole;
                frmEditDevice.SetPlaceHolder += SetPlaceHolder;
                frmEditDevice.Text = $"Edit Device {current_device}";
                frmEditDevice.Show();
                SendMessageToConsoleEvent($"Editing device {current_device}...");
                //Dispose();
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // saveChanges();
            Dispose();
        }
    }
}
