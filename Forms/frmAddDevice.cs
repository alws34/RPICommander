using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPICommander;

namespace RPICommander
{
    public partial class frmAddDevice : Form
    {
        private const string DEFAULT_SSH_PORT = "22";
        string devicesDBpath;
        bool edit_mode = false;
        List<Device> deviceslst = new List<Device>();
        Device edit_device = new Device();

        public delegate void EventHandler_sendMessageToConsole(string msg);
        public event EventHandler_sendMessageToConsole sendMessageToConsole = delegate { };

        public delegate void EventHandler_SetPlaceHolder(TextBox tb);
        public event EventHandler_SetPlaceHolder SetPlaceHolder = delegate { };

        public frmAddDevice(string devicesDBpath)
        {
            InitializeComponent();
            SetEvents();
            DevicesDBPath = devicesDBpath;
            btnSaveDevices.Enabled = false;
            CenterToScreen();
        }

        public frmAddDevice(string devicesDBpath, string device_name)//device edit mode
        {
            InitializeComponent();
            DevicesDBPath = devicesDBpath;
            edit_mode = true;
            btnRemoveDevice.Visible = true;
            SetEvents();
            ReadDB(device_name);
            SetEditGui(edit_device);
            CenterToScreen();
            sendMessageToConsole($"Editing device {device_name}...");
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

        private void SetEvents()
        {
            textBoxAddDeviceName.GotFocus += IsFocused;
            textBoxAddDeviceName.LostFocus += IsFocused;
            textBoxAddDeviceName.Tag = "Enter device host name or ip(v4)";
        }

        private void IsFocused(object sender, EventArgs e)
        {
            string sendername = sender.GetType().Name;
            switch (sendername)
            {
                case "TextBox":
                    SetPlaceHolder((TextBox)sender);
                    break;
                default:
                    sendMessageToConsole($"Sender wasn't a TextBox @frmAddDevice - IsFocused: {sendername}");
                    break;
            }
        }

        private void ReadDB(string device_name)
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
                sendMessageToConsole($"{e}");
            }
        }

        private void WriteToDeviceDB()
        {
            using (StreamWriter sw = File.AppendText(DevicesDBPath))
            {
                foreach (Device device in deviceslst)
                {
                    sw.WriteLine(device.ToString());
                    sendMessageToConsole($"\rAdded device ''{device.Device_Hostname.ToString()}'' to deviced DB\r\n");
                }
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

            if (IsEmpty(devicename))
            {
                devicename = "Error - No Device Name Entered";
                sendMessageToConsole(devicename);
                return null;
            }
            return new Device(devicename, GetUserName(), GetPassword(), int.Parse(GetPort()));
        }

        private bool IsEmpty(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
                return false;
            return true;
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

        private string GetDeviceName()
        {
            if (textBoxAddDeviceName.Text != textBoxAddDeviceName.Tag.ToString())
                return textBoxAddDeviceName.Text;
            ShowErrors($"Please Fill Device Name!");
            return null;
        }

        private void ShowErrors(string msg)
        {
            sendMessageToConsole(msg);
            ShowMessageBox(msg);
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
            for (int i = 0; i < deviceslst.Count; i++)
                if (deviceslst[i].Equals(dev))
                    deviceslst.RemoveAt(i);
            WriteToDeviceDB();
            Dispose();
        }
    }
}
