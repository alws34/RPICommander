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

        string commandsDBpath;
        public string CommandsDBpath { get => commandsDBpath; set => commandsDBpath = value; }
        List<string> commandslst = new List<string>();
        bool edit_mode = false;
        public frmAddCommand(string commandsDBpath)
        {
            InitializeComponent();
            CommandsDBpath = commandsDBpath;
        }
        public frmAddCommand(string path, string command)//edit commands constructor
        {
            InitializeComponent();
            commandsDBpath = path;
            edit_mode = true;
            try
            {
                using (StreamReader sr = new StreamReader(commandsDBpath))//read relevant command from db
                {
                    string[] lines;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines = line.Split('^');
                        if (lines[0] != command)//add to list all non relevant commands
                        {
                            commandslst.Add(line);
                        }
                        else//fill out the form with the relevant command and dont add it to the list(to enable changes with ease)
                        {
                            textBoxCommandName.Text = lines[0];
                            textBoxCommand.Text = lines[1];
                        }
                    }
                }
            }
            catch (IOException)
            {
                showmessage("error reading from db @ frmaddcommand constructor");
            }

        }

        private void showmessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void btnSaveCommand_Click(object sender, EventArgs e) //add command to db
        {
            string commandName = textBoxCommandName.Text;
            string command = textBoxCommand.Text;
            string line;

            if (!edit_mode)
                using (StreamReader sr = new StreamReader(commandsDBpath))
                    while ((line = sr.ReadLine()) != null)
                        commandslst.Add(line);

            if (!(String.IsNullOrEmpty(commandName)) && !(String.IsNullOrEmpty(command)))
            {
                commandslst.Add(commandName + "^" + command);
                using (StreamWriter sw = new StreamWriter(CommandsDBpath))
                    foreach (string c in commandslst)
                        sw.WriteLine(c);
            }
            Dispose();
        }
    }
}
