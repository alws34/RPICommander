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
    public partial class frmRPICommander : Form
    {
        private Dictionary<string, string> commands = new Dictionary<string, string>() { };
        private List<string> devices = new List<string>();

        private List<string> selected_commands = new List<string>();

        //List<string> deviceFiles = new List<string>();

        RadioButton current_device = new RadioButton();
        CheckBox current_command = new CheckBox();

        string commandsDB;
        string devicesDB;

        public Dictionary<string, string> Commands { get => commands; set => commands = value; }
        public List<string> Devices { get => devices; set => devices = value; }
        public List<string> Selected_commands { get => selected_commands; set => selected_commands = value; }

        public frmRPICommander()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            flpCommands.Enabled = false;
            string dirpath = @"C:\ProgramData\RPI Commander";
            commandsDB = dirpath + @"\Commands.dat";
            devicesDB = dirpath + @"\Devices.dat";

            createDir(dirpath);

            // read commands from DB
            if (File.Exists(commandsDB))
            {
                readcommands(commandsDB);
            }
            else
            {
                createFile(commandsDB);//create commandsDB file if not exists;
                addCommand(Commands, commandsDB);
            }

            //read device list from DB
            if (File.Exists(devicesDB))
            {
                readdevices(devicesDB);
            }
            else
            {
                createFile(devicesDB);//create devicesDB file if not exists;
                newdevice(devicesDB); //add first device to db
            }

            //create commands FLP controls
            foreach (var item in Commands)
            {
                CheckBox chkbox = new CheckBox();
                chkbox.Name = item.Value;
                chkbox.Text = item.Key;
                chkbox.CheckedChanged += c_checkedchanged;
                flpCommands.AutoScroll = true;
                flpCommands.Controls.Add(chkbox);
            }

            //create devices FLP controls
            foreach (string item in Devices)
            {
                RadioButton rb = new RadioButton();
                rb.Name = item;
                rb.Text = item;
                rb.CheckedChanged += rb_checkedchanged;
                flpDevices.Controls.Add(rb);
            }
        }

        //read commands from DB
        public void readcommands(string db)
        {
            using (StreamReader sr = new StreamReader(db))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] keyvalue = line.Split('^');
                    if (keyvalue.Length == 2)
                    {
                        Commands.Add(keyvalue[0], keyvalue[1]);
                    }
                }
            }
        }

        public void readdevices(string db)
        {
            using (StreamReader sr = new StreamReader(db))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Devices.Add(line);
                }
            }
        }

        //add commands to db
        private void addCommand(Dictionary<string, string> commands, string commandsDB)
        {
            frmAddCommand addcommand = new frmAddCommand(commands, commandsDB);
            addcommand.Show();
        }

        //create directories
        private void createDir(string path)
        {
            try
            {
                if (!(Directory.Exists(path)))//create directory if not exist
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                showmessage(e.ToString());
            }
        }

        //create files
        private void createFile(string path)
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
                showmessage(e.ToString());
            }
        }

        //start batch
        private void startInstall()
        {
            string commandsfilepath = @"C:\ProgramData\RPI Commander\" + current_device.Text + "_commands.txt";//text file to run the commands from to hold the commands for specific device
            string batch = @"C:\ProgramData\RPI Commander\" + current_device.Text + "_commands.bat";
            createFile(commandsfilepath); // checks if device file doesnt exists, and creates it if needed

            using (StreamWriter sr = new StreamWriter(commandsfilepath))
            {
                foreach (string command in Selected_commands)
                {
                    sr.WriteLine(command);
                }
            }
            using (StreamWriter sr = new StreamWriter(batch))
            {
                string username = "pi";
                string device = current_device.Text;
                string password = "320479";

                string sshCommand = "putty.exe -ssh " + username + "@" + device + " -pw " + password + " -m " + "'\u0022'" + commandsfilepath + "'\u0022'";

                "
                foreach (string command in Selected_commands)
                {
                    sr.WriteLine(command);
                }
            }
            //Process.Start(filepath); // run batch file to install;
        }

        //add device to db
        private void newdevice(string devicesDB)
        {
            frmAddDevice add_device = new frmAddDevice(devicesDB);
            add_device.Show();
        }

        //add command to db
        private void newcommand(Dictionary<string, string> commands, string commandsDB)
        {
            addCommand(commands, commandsDB);
        }

        private void showmessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void reset()
        {
            flpDevices.Enabled = true;
            foreach (CheckBox c in flpCommands.Controls)
            {
                c.Checked = false;
            }
            foreach (RadioButton rb in flpDevices.Controls)
            {
                rb.Checked = false;
            }
        }
        private void c_checkedchanged(object sender, EventArgs e)//checkbox state change
        {
            CheckBox c = (CheckBox)sender;
            if (c.Checked)
            {
                Selected_commands.Add(Commands[c.Text]);//add command to selected commands
            }
            else
            {
                Selected_commands.Remove(Commands[c.Text]);//remove command from selected command
            }
        }

        private void rb_checkedchanged(object sender, EventArgs e)//radio button state changed
        {
            RadioButton r = (RadioButton)sender;
            current_device = r;
            flpCommands.Enabled = true;//be able to choose commands only after a device been chosen
            flpDevices.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)//start install
        {
            startInstall();
            flpDevices.Enabled = true;
        }

        protected void btnAddDevice_Click(object sender, EventArgs e)
        {
            newdevice(devicesDB);
        }

        protected void btnAddCommand_Click(object sender, EventArgs e)
        {
            newcommand(commands, commandsDB);
        }

        protected void btnEditDB_Click(object sender, EventArgs e)
        {
            frmEditDBs editdb = new frmEditDBs();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}


/*
 string changeIP = "change ip:\n interface eth0\n" + "ip_address= 192.168.0.4 / 24 \n" + "routers= 192.168.0.254\n" + "domain_name_servers= 192.168.0.254 8.8.8.8 ";

 commands.Add("Update & Upgrade", "sudo apt-get update && sudo apt-get upgrade -y");
 commands.Add("Reboot", "sudo reboot");
 commands.Add("Motion Eye Install", @"sudo apt - y dist - upgrade && sudo apt update && sudo apt full - upgrade - y && sudo apt - get update && sudo apt - get upgrade - y && sudo  apt - get install ffmpeg libmariadb3 libpq5 libmicrohttpd12 - y && sudo  wget https://github.com/Motion-Project/motion/releases/download/release-4.2.2/pi_buster_motion_4.2.2-1_armhf.deb && sudo dpkg -i pi_buster_motion_4.2.2-1_armhf.deb && sudo  apt-get install python-pip python-dev libssl-dev libcurl4-openssl-dev libjpeg-dev libz-dev -y && sudo  pip install motioneye && sudo  mkdir -p /etc/motioneye && sudo  cp /usr/local/share/motioneye/extra/motioneye.conf.sample /etc/motioneye/motioneye.conf && sudo  mkdir -p /var/lib/motioneye && sudo  cp /usr/local/share/motioneye/extra/motioneye.systemd-unit-local /etc/systemd/system/motioneye.service && sudo systemctl daemon-reload && sudo systemctl enable motioneye && sudo systemctl start motioneye && sudo sudo apt-get install samba samba-common-bin -y && sudo nano /etc/samba/smb.conf &&sudo smbpasswd -a pi && sudo systemctl restart smbd && pip install psutil");
 commands.Add("Xscreenaver", "sudo apt-get install xscreensaver -y");
 commands.Add("Samba", "sudo apt-get install samba samba-common-bin -y && sudo nano /etc/samba/smb.conf");
 commands.Add("Pi-Hole", "curl -sSL https://install.pi-hole.net | bash");
 commands.Add("PiVPN", "curl -L https://install.pivpn.io | bash");
 commands.Add("Change Hostname", "sudo nano /etc/hostname");
 commands.Add("ip to static", "sudo nano /etc/dhcpcd.conf");
 commands.Add("ip to dhcp", "sudo nano /etc/dhcpcd.conf");

 devices.Add("rpi4adblock");
 devices.Add("rpi3");
 devices.Add("rpi3bplus");
 devices.Add("rpi4");

 using (StreamWriter sr = new StreamWriter(@"C:\ProgramData\RPI Commander\Commands.dat"))
 {
     foreach (KeyValuePair<string, string> kvp in commands)
     {
         sr.WriteLine("{0}^{1}", kvp.Key, kvp.Value);
     }
 }
 using (StreamWriter sr = new StreamWriter(@"C:\ProgramData\RPI Commander\Devices.dat"))
 {
     foreach (string item in devices)
     {
         sr.WriteLine(item);
     }
 }
 */
