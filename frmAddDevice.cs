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

        public frmAddDevice(string devicesDBpath) // constructor that gets path to devices .dat file
        {
            InitializeComponent();
            DevicesDBPath = devicesDBpath;
            btnSaveDevice.Enabled = false;
        }

        private void addDevice()
        {
            if (listBoxDevices.Items.Count == 0)//adding a single device
            {
               

                using (StreamWriter sw = File.AppendText(DevicesDBPath))
                {
                    sw.WriteLine(getdevice());
                }
            }
            else//adding multiple devices
            {
                using (StreamWriter sw = File.AppendText(DevicesDBPath))
                {
                    foreach (string device in devices)

                        sw.WriteLine(device);
                }
            }
        }

        private string getdevice()
        {
            string device;
            string devicename = deviceName();
            string userName = username();
            string password = passWord();

            return device = devicename + "^" + userName + "^" + password;
        }

        private void AddToList()
        {
            string devicename = deviceName();
            string userName = username();
            string password = passWord();

            if (!(String.IsNullOrEmpty(devicename)) && !(String.IsNullOrEmpty(userName)) && !(String.IsNullOrEmpty(password)))
            {
                listBoxDevices.Items.Add(devicename);
                devices.Add(devicename + "^" + userName + "^" + password);
                textBoxAddDeviceName.Text = "";
                textBoxdevicePassword.Text = "";
                textBoxdeviceUsername.Text = "";
                btnSaveDevice.Enabled = true;
            }
            else
            {
                MessageBox.Show("please fill all fields");
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

        /*
         * Events
         * **/
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
