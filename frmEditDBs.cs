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
    public partial class frmEditDBs : Form
    {
        private Dictionary<string, string> commands = new Dictionary<string, string>() { };//dictionary of all available commands
        private List<string> devices = new List<string>(); // list of all available devices
        string devicesDBpath, commandsDBpath;

        public frmEditDBs(string devicesdb, string commandsdb)
        {
            InitializeComponent();
            init();

            devicesDBpath = devicesdb;
            commandsDBpath = commandsdb;
        }

        private void init()
        {
            string dirpath = @"C:\ProgramData\RPI Commander";
            string commandsDB = dirpath + @"\commands.dat";
            string devicesDB = dirpath + @"\devices.dat";

            // read commands from DB
            if (File.Exists(commandsDB))
            {
                readcommands(commandsDB);
            }

            //read device list from DB
            if (File.Exists(devicesDB))
            {
                readdevices(devicesDB);
            }

            commandsControls(commands);
            devicesControls(devices);

            setEvents();
        }

        private void commandsControls(Dictionary<string, string> commands)//creates the commands controls
        {
            try
            {
                foreach (KeyValuePair<string, string> item in commands)
                {
                    listBoxCommands.Items.Add(item.Key);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void devicesControls(List<string> devices)//creates the devices controls
        {
            //create devices FLP controls
            foreach (string item in devices)
            {
                try
                {
                    listBoxDevices.Items.Add(item);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void setEvents()
        {
            listBoxCommands.DoubleClick += new EventHandler(listBoxCommands_DoubleClick);
            listBoxDevices.DoubleClick += new EventHandler(listBoxDevices_DoubleClick);
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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
                        string[] device_name = line.Split('^');
                        devices.Add(device_name[0]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void saveChanges()
        {
            using (StreamWriter sw = new StreamWriter(devicesDBpath))
            {
                foreach(string device in devices)
                {
                    sw.WriteLine(device);
                }
            }
            using (StreamWriter sw = new StreamWriter(commandsDBpath))
            {
                foreach(KeyValuePair<string,string> command in commands)
                {
                    sw.WriteLine("{0}^{1}", command.Key, command.Value);
                }
            }
        }

        private void listBoxCommands_DoubleClick(object sender, EventArgs e)//double click event on commands list item
        {
            if (listBoxCommands.SelectedItem != null)
            {
                Form edit_command = new frmAddCommand(commandsDBpath, listBoxCommands.SelectedItem.ToString());
                edit_command.Show();
                commands.Remove(listBoxCommands.SelectedItem.ToString());
                listBoxCommands.Items.RemoveAt(listBoxCommands.SelectedIndex);
            }
        }
       
        private void listBoxDevices_DoubleClick(object sender, EventArgs e)//double click event on devicess list item
        {
            if (listBoxDevices.SelectedItem != null )
            {
                Form edit_device = new frmAddDevice(devicesDBpath, listBoxDevices.SelectedItem.ToString());
                edit_device.Show();
                devices.Remove(listBoxDevices.SelectedItem.ToString());
                listBoxDevices.Items.RemoveAt(listBoxDevices.SelectedIndex);
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            saveChanges();
            Dispose();
        }
    }
}
