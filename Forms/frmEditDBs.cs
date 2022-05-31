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
        private List<Command> commands = new List<Command>();
        private List<Device> devices = new List<Device>();
        string devicesDBpath, commandsDBpath, current_device, current_command_name;

        public delegate void EventHandler_sendMessageToConsole(string msg);
        public event EventHandler_sendMessageToConsole sendMessageToConsole = delegate { };

        public frmEditDBs(string devicesdb, string commandsdb)
        {
            InitializeComponent();
            init();
            devicesDBpath = devicesdb;
            commandsDBpath = commandsdb;
            CenterToScreen();
        }

        private void init()
        {
            string dirpath = @"C:\ProgramData\RPI Commander";
            string commandsDB = dirpath + @"\commands.dat";
            string devicesDB = dirpath + @"\devices.dat";

            // read commands from DB
            if (File.Exists(commandsDB))
            {
                ReadCommandsFromDB(commandsDB);
            }

            //read device list from DB
            if (File.Exists(devicesDB))
            {
                ReadDevicesFromDB(devicesDB);
            }

            commandsControls(commands);
            devicesControls(devices);

            setEvents();
        }

        private void commandsControls(List<Command> commands)//creates the commands controls
        {
            try
            {
                foreach (Command cmd in commands)
                    listBoxCommands.Items.Add(cmd);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void devicesControls(List<Device> devices)
        {
            foreach (Device device in devices)
                listBoxDevices.Items.Add(device.Device_Hostname);
        }

        private void setEvents()
        {
            listBoxCommands.DoubleClick += new EventHandler(listBoxCommands_DoubleClick);
            listBoxDevices.DoubleClick += new EventHandler(listBoxDevices_DoubleClick);
        }

        private void ReadCommandsFromDB(string db)
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
                            commands.Add(new Command(keyvalue[0], keyvalue[1]));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ReadDevicesFromDB(string db)
        {
            try
            {
                using (StreamReader sr = new StreamReader(db))
                {
                    string line;
                    while ((!string.IsNullOrWhiteSpace(line = sr.ReadLine())))
                    {
                        string[] dev = line.Split('^');
                        Device device = new Device(dev[0], dev[1], dev[2], int.Parse(dev[3]));
                        devices.Add(device);
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
                foreach (Device device in devices)
                    sw.WriteLine(device.ToString());

            using (StreamWriter sw = new StreamWriter(devicesDBpath))
                foreach (Command cmd in commands)
                    sw.WriteLine(cmd.ToString());
        }

        private void SetFileSystemWatcher()
        {
            fileSystemWatcherDevice.Path = Directory.GetParent(devicesDBpath).ToString();
            fileSystemWatcherDevice.Filter = Path.GetFileName(devicesDBpath);
            fileSystemWatcherCommands.Path = Directory.GetParent(commandsDBpath).ToString();
            fileSystemWatcherCommands.Filter = Path.GetFileName(commandsDBpath);
        }

        private void ResetFormFields()
        {
            listBoxDevices.Items.Clear();
            listBoxCommands.Items.Clear();
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ResetFormFields();
            devicesControls(devices);
            commandsControls(commands);
        }

        private void listBoxCommands_DoubleClick(object sender, EventArgs e)//double click event on commands list item
        {
            Command current_command = (Command)listBoxCommands.SelectedItem;
            if (current_command != null)
            {
                //for (int i = 0; i < commands.Count; i++)
                //    if (commands[i].Command_name == current_command.Command_name)
                //        commands.RemoveAt(i);

                listBoxCommands.Items.RemoveAt(listBoxCommands.SelectedIndex);
                frmAddCommand edit_command = new frmAddCommand(commandsDBpath, current_command, commands);
                edit_command.Show();
                Dispose();
            }
        }

        private void listBoxDevices_DoubleClick(object sender, EventArgs e)//double click event on devicess list item
        {
            current_device = listBoxDevices.SelectedItem.ToString();
            Device device = new Device();
            if (current_device != null)
            {
                for (int x = devices.Count - 1; x > -1; x--)
                {
                    if (devices[x].Device_Hostname == current_device)
                        device = devices[x];
                }

                listBoxDevices.Items.RemoveAt(listBoxDevices.SelectedIndex);
                frmAddDevice edit_device = new frmAddDevice(devicesDBpath, current_device);
                //edit_device.sendMessageToConsole += sendMessageToConsole;
                edit_device.Show();
                //Dispose();
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            saveChanges();
            Dispose();
        }
    }
}
