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
    public partial class frmAddCommand : Form
    {
        Dictionary<string, string> commands;
        string commandsDBpath;

        public Dictionary<string, string> Commands { get => commands; set => commands = value; }
        public string CommandsDBpath { get => commandsDBpath; set => commandsDBpath = value; }

        public frmAddCommand(Dictionary<string, string> commands, string commandsDBpath)
        {
            InitializeComponent();
            Commands = commands;
            CommandsDBpath = commandsDBpath;
        }
        public frmAddCommand(string path, string command)
        {
            InitializeComponent();
            commandsDBpath = path;
            try
            {
                using (StreamReader sr = new StreamReader(commandsDBpath))
                {
                    string[] lines;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines = line.Split('^');
                        if (lines[0] == command)
                        {
                            textBoxCommandName.Text = lines[0];
                            textBoxCommand.Text = lines[1];
                        }
                    }
                }
            }catch(ArgumentException)
            {
                showmessage("this command already exists in db");
            }
            
        }

        private void showmessage(string msg)
        {
            MessageBox.Show(msg);
        }
        //add command to db
        private void btnSaveCommand_Click(object sender, EventArgs e)
        {
            string commandName = textBoxCommandName.Text;
            string command = textBoxCommand.Text;
            if (!(String.IsNullOrEmpty(commandName)) || !(String.IsNullOrEmpty(command)))
            {
                using (StreamWriter sw = File.AppendText(CommandsDBpath))
                {
                    sw.WriteLine("{0}^{1}", commandName, command);
                }
            }
        }
    }
}
