using Renci.SshNet;
using RPICommander.Forms;

namespace RPICommander
{
    public partial class FrmRPICommander : Form
    {
        private const string APP_VERSION = "2.1.1";
        private const int DEFAULT_SSH_PORT = 22;
        private string commandsDBPath = "";
        private string devicesDBPath = "";
        private string logPath = "";
        private int lastX = 0;
        private int lastY = 0;
        private List<Device> devices_lst = new List<Device>() { };
        private List<Command> commands_lst = new List<Command>() { };
        private List<Command> selected_commands = new List<Command>();
        private List<CheckBox> selected_devices = new List<CheckBox>();

        public FrmRPICommander()
        {
            InitializeComponent();
            Init();
            SetCommandsFileSystemWatcher();
            SetDevicesFileSystemWatcher();
            CenterToScreen();
        }

        private void Init()
        {
            string dirpath = "C:\\ProgramData\\RPI Commander";
            commandsDBPath = dirpath + "\\commands.dat";
            devicesDBPath = dirpath + "\\devices.dat";
            logPath = dirpath + "\\Log.txt";
            flpCommands.AutoScroll = true;
            CreateDir(dirpath);

            if (!CreateFile(commandsDBPath))
                AddNewCommandForm();
            else
                ReadCommands(commandsDBPath);

            if (!CreateFile(devicesDBPath))
                AddNewDeviceForm(devicesDBPath);
            else
                ReadDevices(devicesDBPath);

            CreateCommandsControls(commands_lst);
            CreateDevicesControls(devices_lst);
        }

        private CheckBox CreateCheckBox(Color forecolor, Color backcolor, string name = "", string text = "")
        {
            CheckBox cb = new CheckBox
            {
                Name = name,
                Text = text,
                ForeColor = forecolor,
                BackColor = backcolor,
                Font = new Font(FontFamily.GenericSansSerif, (float)12.0, FontStyle.Bold),
            };
            cb.MouseMove += FlpDevices_MouseMove;
            return cb;
        }
        private void CreateCommandsControls(List<Command> commands)
        {
            foreach (Command command in commands)
            {

                CheckBox cb = CreateCheckBox(Color.White, Color.Black, command.Command_description, command.Command_name);
                cb.Tag = command;
                cb.CheckedChanged += CheckBoxCommand_CheckedChanged;
                flpCommands.Controls.Add(cb);

            }
        }

        private void CreateDevicesControls(List<Device> devices)
        {
            foreach (Device device in devices)
            {
                CheckBox cb = CreateCheckBox(Color.White, Color.Black, $"{device.User_name}^{device.Password}", device.Device_Hostname);
                cb.Tag = device;
                //cb.Font = new Font(cb.Font, FontStyle.Bold);
                cb.CheckedChanged += CheckBoxDevice_CheckedChanged;
                cb.MouseMove += FlpDevices_MouseMove;

                flpDevices.Controls.Add(cb);
            }
        }

        private void ReadCommands(string db)
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] keyvalue = line.Split('^');

                        if (keyvalue.Length != 2)
                            return;

                        Command command = new Command(keyvalue[0], keyvalue[1]);
                        commands_lst.Add(command);
                    }
                }
            }
            catch (IOException ioe)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{ioe}\nResetting Form"));
                ResetForm();
            }
            catch (ArgumentException e)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
                return;
            }
        }

        private void ReadDevices(string db)
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    while (!String.IsNullOrWhiteSpace(line = sr.ReadLine()))
                    {
                        string[] devic_creds = line.Split('^');
                        if (devic_creds.Length == 3 || devic_creds.Length == 4)
                        {
                            int port = DEFAULT_SSH_PORT;
                            if (devic_creds.Length == 4)
                            {
                                try
                                {
                                    port = int.Parse(devic_creds[3]);
                                }
                                catch (Exception)
                                {
                                    port = DEFAULT_SSH_PORT;
                                }
                            }
                            Device device = new Device(devic_creds[0], devic_creds[1], devic_creds[2], port);

                            //if (!devices_lst.Contains(device))
                            devices_lst.Add(device);
                        }
                    }
                }
            }
            catch (IOException)
            {
                ResetForm();
            }
            catch (ArgumentException e)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
                return;
            }
        }

        private void CreateDir(string path)
        {
            try
            {
                if (!(Directory.Exists(path)))
                    Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                //ShowMessageBox($"{e}");
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
            }
        }

        private bool CreateFile(string path)
        {
            try
            {
                if (!(File.Exists(path)))
                {
                    var f = File.Create(path);
                    f.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                //ShowMessageBox($"{e}");
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
                return false;
            }
            return true;
        }

        //private void ShowMessageBox(string msg, string caption = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        //{
        //    MessageBox.Show(msg, caption, buttons, icon);
        //}

        private void ResetForm(bool clearconsole = true)
        {
            flpDevices.Enabled = true;
            flpCommands.Controls.Clear();
            flpDevices.Controls.Clear();
            devices_lst.Clear();
            commands_lst.Clear();
            if (clearconsole)
                textBoxConsole.Clear();
            Init();
        }

        private void ChangesDetected()
        {
            flpDevices.Controls.Clear();
            flpCommands.Controls.Clear();
            devices_lst.Clear();
            commands_lst.Clear();
            selected_commands.Clear();
            selected_devices.Clear();
            Init();
        }

        private void SetCommandsFileSystemWatcher()
        {
            fileSystemWatcherCommandsWatch.Path = Directory.GetParent(commandsDBPath).ToString();
            fileSystemWatcherDevicesDB.Filter = Path.GetFileName(commandsDBPath);
            fileSystemWatcherDevicesDB.Changed += new System.IO.FileSystemEventHandler(fileSystemWatcherWatch_Changed);
            ResetForm();
        }

        private void SetDevicesFileSystemWatcher()
        {
            fileSystemWatcherDevicesDB.Path = Directory.GetParent(devicesDBPath).ToString();
            fileSystemWatcherDevicesDB.Filter = Path.GetFileName(devicesDBPath);
            fileSystemWatcherDevicesDB.Changed += new System.IO.FileSystemEventHandler(fileSystemWatcherWatch_Changed);
            ResetForm();
        }

        /*Form Creation*/
        private void AddNewDeviceForm(string devicesDB)
        {
            frmAddDevice frmAddDevice = new frmAddDevice(devicesDBPath);
            frmAddDevice.SendMessageToConsole += SendMessageToConsole;
            frmAddDevice.SetPlaceHolder += SetTBPlaceHolder;
            frmAddDevice.Show();
        }

        private void AddNewCommandForm()
        {
            frmAddCommand frmAddComand = new frmAddCommand(devicesDBPath);
            frmAddComand.SendMessageToConsole += SendMessageToConsole;
            frmAddComand.SetPlaceHolder += SetTBPlaceHolder;
            frmAddComand.Show();
        }

        private void StartEdit()
        {
            frmEditDBs frmEditDB = new frmEditDBs(devicesDBPath, commandsDBPath, true);
            frmEditDB.SendMessageToConsole += SendMessageToConsole;
            frmEditDB.SetPlaceHolder += SetTBPlaceHolder;
            if (!frmEditDB.Init())
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs("Couldn't Start Editing! see error above."));
                return;
            }
            frmEditDB.Show();
        }

        /*Events*/
        private void fileSystemWatcherWatch_Changed(object sender, FileSystemEventArgs e)
        {
            ChangesDetected();
        }

        private void StartSSH(object sender, EventArgs e)
        {
            if (!(selected_commands.Count > 0))
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"\r\nNo Commands Selected\r\n"));
                return;
            }

            foreach (CheckBox c in selected_devices)
            {
                Device? device = c.Tag as Device;
                if (device == null)
                {
                    SendMessageToConsole(new SendMessageToConsoleEventArgs($"Error Casting Device! @BtnStart_Click"));
                    continue;
                }
                using (var client = new SshClient(device.Device_Hostname, device.Port, device.User_name, device.Password))
                {
                    try
                    {
                        client.Connect();

                        if (!client.IsConnected)
                        {
                            SendMessageToConsole(new SendMessageToConsoleEventArgs($"couldn't connect to client {device.Device_Hostname} over port {device.Port}\n"));
                            continue;
                        }
                        SendMessageToConsole(new SendMessageToConsoleEventArgs($"\r\nconnected to client {device.Device_Hostname} over port {device.Port}\n"));

                        foreach (Command command in selected_commands)
                        {
                            SshCommand cmd = client.CreateCommand(command.Command_description);
                            string result = cmd.Execute();
                            StreamReader reader = new StreamReader(cmd.ExtendedOutputStream);
                            string stderr = reader.ReadToEnd();

                            SendMessageToConsole(new SendMessageToConsoleEventArgs($"\r\n\tExecuted command ''{command.Command_description}'' on {device.Device_Hostname}\r\n"));

                            if (!string.IsNullOrWhiteSpace(result))
                                SendMessageToConsole(new SendMessageToConsoleEventArgs($"\tCommand output: {result}\n"));
                            if (!string.IsNullOrWhiteSpace(stderr))
                                SendMessageToConsole(new SendMessageToConsoleEventArgs($"\t{stderr}\n"));
                        }
                    }
                    catch (Exception ex)
                    {
                        SendMessageToConsole(new SendMessageToConsoleEventArgs($"\r{ex.ToString()}"));
                        return;
                    }
                    client.Disconnect();
                }
            }
            ResetForm(false);
        }

        private void buttonAboutfrm_Click(object sender, EventArgs e)
        {
            frmAbout aboutfrm = new frmAbout(APP_VERSION);
            aboutfrm.Show();
        }

        private void CheckBoxCommand_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c != null)
            {
                if (c.Checked)
                    selected_commands.Add((Command)c.Tag);
                else
                    selected_commands.Remove((Command)c.Tag);
            }
            else
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"Sender was not a CheckBox! @CheckBox_CheckedChanged"));
            }

        }

        private void CheckBoxDevice_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            if (c.Checked)
                selected_devices.Add(c);
            else
                selected_devices.Remove(c);

        }

        protected void BtnAddDevice_Click(object sender, EventArgs e)
        {
            AddNewDeviceForm(devicesDBPath);
        }

        protected void BtnAddCommand_Click(object sender, EventArgs e)
        {
            AddNewCommandForm();
        }

        protected void BtnEditDB_Click(object sender, EventArgs e)
        {
            StartEdit();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void FlpDevices_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X != lastX || e.Y != lastY)
            {
                CheckBox c = (CheckBox)sender;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(c, $"{c.Tag.ToString().Replace("^", "\n")}");

                lastX = e.X;
                lastY = e.Y;
            }
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            textBoxConsole.Text = "";
        }

        /*Delegated Methods*/
        public void SetTBPlaceHolder(SetPlaceHolderEventArgs arg)
        {
            TextBox tb = arg.TB;
            if (!tb.Focused)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = tb.Tag.ToString();
                return;
            }
            if (tb.Text == tb.Tag.ToString())
                tb.Text = "";
            return;
        }

        delegate void SendMessageToConsoleCallback(SendMessageToConsoleEventArgs txt);
        public void SendMessageToConsole(SendMessageToConsoleEventArgs arg)
        {
            if (!textBoxConsole.InvokeRequired)
            {
                textBoxConsole.AppendText($"{arg.Message}{Environment.NewLine}");
                return;
            }
            else
            {
                SendMessageToConsoleCallback callback = new SendMessageToConsoleCallback(SendMessageToConsole);
                textBoxConsole.Invoke(callback, arg);
            }
        }
    }
}