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

namespace RPICommander
{
    public partial class frmAddDevice : Form
    {
        string devicesDB;
        public string DevicesDBPath { get => devicesDB; set => devicesDB = value; }
        List<string> devices = new List<string>();
        bool edit_mode = false;

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

            try
            {
                using (StreamReader sr = new StreamReader(DevicesDBPath))//read existing db without the edit designated device
                {
                    string[] lines;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines = line.Split('^');
                        if (lines.Length == 3)
                        {
                            if (lines[0] != device_name)//add to list all devices from existing db except editable device
                                devices.Add(line);
                            else
                            {
                                textBoxAddDeviceName.Text = lines[0];
                                textBoxdeviceUsername.Text = lines[1];
                                textBoxdevicePassword.Text = lines[2];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                showmessage(e.ToString());
            }
        }

        private void addDevice()//save devices
        {
            if (edit_mode)
            {
                devices.Add(getdevice());
                using (StreamWriter sw = new StreamWriter(DevicesDBPath))//rewrite the file
                    foreach (string device in devices)
                        sw.WriteLine(device);
            }
            else
            {
                if (listBoxDevices.Items.Count == 0)//adding a single device
                {
                    using (StreamWriter sw = File.AppendText(DevicesDBPath))
                        sw.WriteLine(getdevice());
                }
                else//adding multiple devices
                {
                    using (StreamWriter sw = File.AppendText(DevicesDBPath))
                        foreach (string device in devices)
                            sw.WriteLine(device);
                }
            }
            Dispose();
        }
        private string getdevice()
        {
            return deviceName() + "^" + username() + "^" + passWord();
        }

        private void AddToList()
        {
            if (!(String.IsNullOrEmpty(deviceName())) && !(String.IsNullOrEmpty(username())) && !(String.IsNullOrEmpty(passWord())))
            {
                listBoxDevices.Items.Add(deviceName()); //add device to listbox - only by name
                devices.Add(getdevice());//add device to devices list
                btnSaveDevice.Enabled = true;
                //reset the rest of the fields
                textBoxAddDeviceName.Text = "";
                textBoxdevicePassword.Text = "";
                textBoxdeviceUsername.Text = "";
            }
            else
            {
                showmessage("Please fill all fields");
            }
        }
        private string deviceName()//return device name
        {
            return textBoxAddDeviceName.Text;
        }
        private string username()//return username
        {
            return textBoxdeviceUsername.Text;
        }
        private string passWord()//return password
        {
            return textBoxdevicePassword.Text;
        }
        private void showmessage(string msg)//displays message
        {
            MessageBox.Show(msg);
        }

        /*
         * Events
         */
        private void buttonSaveDevices_Click(object sender, EventArgs e)
        {
            addDevice();
            Dispose();
        }
        private void buttonAddDeviceToList_Click(object sender, EventArgs e)
        {
            AddToList();
        }
        private void btnsaveonedevice_Click(object sender, EventArgs e)
        {
            addDevice();
        }
    }
}
