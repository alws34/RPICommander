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
        private string DEFAULT_SSH_PORT = "22";
        string devicesDBpath;
        bool edit_mode = false;

        public string DevicesDBPath { get => devicesDBpath; set => devicesDBpath = value; }
        List<string> devices = new List<string>();

        public delegate void EventHandler_sendMessageToConsole(string msg);
        public event EventHandler_sendMessageToConsole sendMessageToConsole = delegate { };


        public frmAddDevice(string devicesDBpath)
        {
            InitializeComponent();
            DevicesDBPath = devicesDBpath;
            btnSaveDevice.Enabled = false;
        }

        public frmAddDevice(string devicesDBpath, string device_name)//edit mode
        {
            InitializeComponent();
            DevicesDBPath = devicesDBpath;

            edit_mode = true;

            buttonAddDeviceToList.Enabled = false;
            btnSaveDevice.Enabled = false;
            listBoxDevices.Enabled = false;
            textBoxDevicePort.Text = DEFAULT_SSH_PORT;
            ReadDB(device_name);
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
                            {
                                devices.Add(GetCurrentDevice());
                            }
                            else
                            {
                                textBoxAddDeviceName.Text = device_str[0];
                                textBoxdeviceUsername.Text = device_str[1];
                                textBoxdevicePassword.Text = device_str[2];
                                textBoxDevicePort.Text = device_str[3];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                sendMessageToConsole($"{e}");
            }
        }
        private void RewriteDB()
        {
            using (StreamWriter sw = new StreamWriter(DevicesDBPath))//Rewrite the file
                foreach (string device in devices)
                    sw.WriteLine(device);
        }

        private void AppendToDeviceDB(string device)
        {
            using (StreamWriter sw = File.AppendText(DevicesDBPath))
                sw.WriteLine(device);
            sendMessageToConsole($"Added device ''{device}'' to deviced DB");
        }


        private void AddDeviceToDB()
        {
            if (edit_mode)
                AppendToDeviceDB(GetCurrentDevice());
            else
            {
                if (listBoxDevices.Items.Count == 0)
                    AppendToDeviceDB(GetCurrentDevice());
                else
                    foreach (string device in devices)
                        AppendToDeviceDB(device);
            }
            Dispose();
        }

        private string GetCurrentDevice()
        {
            return $"{GetDeviceName()}^{GetUserName()}^{GetPassword()}^{GetPort()}";
        }

        private bool CheckIfEmptyString(string arg)
        {
            return string.IsNullOrWhiteSpace(arg);
        }
        private void AddToList()
        {
            if (!CheckIfEmptyString(GetDeviceName()) && !CheckIfEmptyString(GetUserName()) && !CheckIfEmptyString(GetPassword()))
            {
                listBoxDevices.Items.Add(GetDeviceName());
                devices.Add(GetCurrentDevice());
                btnSaveDevice.Enabled = true;
                ResetFormFields();
            }
            else
            {
                ShowMessageBox("Please fill all fields");
                sendMessageToConsole($"Not all field were filled");
            }
        }
        private string GetDeviceName()
        {
            return textBoxAddDeviceName.Text;
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
            if (String.IsNullOrWhiteSpace(textBoxDevicePort.Text))
                return DEFAULT_SSH_PORT;
            return textBoxDevicePort.Text;
        }

        private void ShowMessageBox(string msg, string caption = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(msg, caption, buttons, icon);
        }
        private void ResetFormFields()
        {
            textBoxAddDeviceName.Text = "";
            textBoxdevicePassword.Text = "";
            textBoxdeviceUsername.Text = "";
            textBoxDevicePort.Text = "";
        }


        private void buttonSaveDevices_Click(object sender, EventArgs e)
        {
            AddDeviceToDB();
            Dispose();
        }
        private void buttonAddDeviceToList_Click(object sender, EventArgs e)
        {
            AddToList();
        }
        private void btnsaveonedevice_Click(object sender, EventArgs e)
        {
            AddDeviceToDB();
        }
    }
}
