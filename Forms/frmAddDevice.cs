namespace RPICommander
{
    public partial class frmAddDevice : Form
    {
        private const string DEFAULT_SSH_PORT = "22";
        string devicesDBpath;
        List<Device> deviceslst = new List<Device>();
        Device edit_device = new Device();

        public event SendMessageToConsoleEventHandler SendMessageToConsole;

        protected virtual void OnSendMessageToConsoleEvent(string msg)
        {
            SendMessageToConsole?.Invoke(new SendMessageToConsoleEventArgs(msg));
        }

        public event SetPlaceHolderEventHandler SetPlaceHolder;
        protected virtual void OnTBFocusChange(TextBox tb)
        {
            SetPlaceHolder?.Invoke(new SetPlaceHolderEventArgs(tb));
        }

        public frmAddDevice(string devicesDBpath)
        {
            InitializeComponent();
            SetEvents();
            SetTags();
            SetText();
            DevicesDBPath = devicesDBpath;
            btnSaveDevices.Enabled = false;
            CenterToScreen();
        }

        public frmAddDevice(string devicesDBpath, string device_name)//device edit mode
        {
            InitializeComponent();
            DevicesDBPath = devicesDBpath;
            btnRemoveDevice.Visible = true;
            SetEvents();
            SetTags();
            if (device_name == null)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"No device name"));
                Dispose();
            }
            if (!ReadDB(device_name))
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"Couldn't read Device from DB!"));
                Dispose();
            }
            SetEditGui(edit_device);
            CenterToScreen();
        }

        public string DevicesDBPath { get => devicesDBpath; set => devicesDBpath = value; }

        private void SetEditGui(Device device)
        {
            buttonAddDeviceToList.Enabled = false;
            btnSaveDevices.Enabled = false;
            listBoxDevices.Enabled = false;
            textBoxAddDeviceName.Text = device.Device_Hostname;
            textBoxdevicePassword.Text = device.Password;
            textBoxdeviceUsername.Text = device.User_name;
            textBoxDevicePort.Text = device.Port.ToString();
        }

        private void SetText()
        {
            textBoxAddDeviceName.Text = textBoxAddDeviceName.Tag.ToString();
        }

        private void SetEvents()
        {
            textBoxAddDeviceName.GotFocus += IsFocused;
            textBoxAddDeviceName.LostFocus += IsFocused;
        }

        private void SetTags()
        {
            textBoxAddDeviceName.Tag = "Enter device host name or ip(v4)";
        }

        private bool ReadDB(string device_name)
        {
            try
            {
                using (StreamReader sr = new StreamReader(DevicesDBPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] device_str = line.Split('^');
                        if (device_str.Length == 4)
                        {
                            if (device_str[0] != device_name)//add to list all devices from existing db except editable device
                                deviceslst.Add(new Device(device_str[0], device_str[1], device_str[2], int.Parse(device_str[3])));
                            else
                                edit_device = new Device(device_str[0], device_str[1], device_str[2], int.Parse(device_str[3]));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                OnSendMessageToConsoleEvent($"{e}");
                return false;
            }
            return true;
        }

        private void WriteToDeviceDB()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(DevicesDBPath))
                {
                    foreach (Device device in deviceslst)
                    {
                        sw.WriteLine(device.ToString());
                        OnSendMessageToConsoleEvent($"\rAdded device ''{device.Device_Hostname.ToString().Replace("^", ": ")}'' to deviced DB\r\n");
                    }
                }
            }catch(Exception e)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs($"{e}"));
            }
            
        }

        private void AddDeviceToDB()
        {
            deviceslst.Add(edit_device);
            WriteToDeviceDB();
            Dispose();
        }

        private Device GetCurrentDevice()
        {
            string devicename = GetDeviceName();

            if (!IsEmpty(devicename))
            {
                devicename = "Error - No Device Name Entered";
                OnSendMessageToConsoleEvent(devicename);
                return null;
            }
            return new Device(devicename, GetUserName(), GetPassword(), int.Parse(GetPort()));
        }

        private bool IsEmpty(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
                return true;
            return false;
        }

        private void AddToList()
        {
            if (!CheckAllFields())
            {
                ShowErrors($"Please fill all fields");
                return;
            }

            listBoxDevices.Items.Add(GetDeviceName());
            Device dev = GetCurrentDevice();

            if (dev == null)
                return;

            deviceslst.Add(dev);
            btnSaveDevices.Enabled = true;
            ResetFormFields(false);
        }

        private bool CheckAllFields()
        {
            return IsEmpty(GetDeviceName()) && IsEmpty(GetUserName()) && IsEmpty(GetPassword()) && IsEmpty(GetPort());
        }

        private void ShowErrors(string msg)
        {
            OnSendMessageToConsoleEvent(msg);
            ShowMessageBox(msg);
        }

        private string GetDeviceName()
        {
            if (textBoxAddDeviceName.Text != textBoxAddDeviceName.Tag.ToString())
                return textBoxAddDeviceName.Text;
            ShowErrors($"Please Fill Device Name!");
            return null;
        }

        private string GetUserName()
        {
            return textBoxdeviceUsername.Text;
        }

        private string GetPassword()
        {
            return textBoxdevicePassword.Text;
        }

        private string GetPort()
        {
            if (!IsEmpty(textBoxDevicePort.Text))
                return textBoxDevicePort.Text;
            return DEFAULT_SSH_PORT;

        }

        private void ShowMessageBox(string msg, string caption = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(msg, caption, buttons, icon);
        }

        private void ResetFormFields(bool clearlist)
        {
            textBoxAddDeviceName.Text = "";
            textBoxdevicePassword.Text = "";
            textBoxdeviceUsername.Text = "";
            textBoxDevicePort.Text = "";
            if (clearlist)
                listBoxDevices.Items.Clear();
        }

        private void buttonSaveDevices_Click(object sender, EventArgs e)
        {
            WriteToDeviceDB();
        }

        private void buttonSaveDevice_Click(object sender, EventArgs e)
        {
            Device dev = GetCurrentDevice();

            if (dev == null)
                return;

            edit_device = dev;
            AddDeviceToDB();
            Dispose();
        }

        private void buttonAddDeviceToList_Click(object sender, EventArgs e)
        {
            AddToList();
        }

        private void btnRemoveDevice_Click(object sender, EventArgs e)
        {
            Device dev = GetCurrentDevice();

            if (dev == null)
            {
                SendMessageToConsole(new SendMessageToConsoleEventArgs("NULL CURRENT DEVICE"));
                return;
            }

            for (int i = 0; i < deviceslst.Count; i++)
                if (deviceslst[i].Equals(dev))
                    deviceslst.RemoveAt(i);
            WriteToDeviceDB();
            Dispose();
        }

        private void IsFocused(object sender, EventArgs e)
        {
            string sender_name = sender.GetType().Name;
            switch (sender_name)
            {
                case "TextBox":
                    TextBox? tb = sender as TextBox;
                    if (tb != null)
                        OnTBFocusChange(tb);
                    break;
                default:
                    OnSendMessageToConsoleEvent($"Sender wasn't a TextBox @frmAddDevice - IsFocused: {sender_name}");
                    break;
            }
        }
    }
}
