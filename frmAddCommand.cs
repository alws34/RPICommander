using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RPICommander
{
    public partial class frmAddCommand : Form
    {
        string commandsDBpath;
        public string CommandsDBpath { get => commandsDBpath; set => commandsDBpath = value; }
        List<string> commandslst = new List<string>();
        bool edit_mode = false;

        public delegate void EventHandler_sendMessageToConsole(string msg);
        public event EventHandler_sendMessageToConsole sendMessageToConsole = delegate { };

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

            ReadCommands(command);
            sendMessageToConsole($"Read {commandslst.Count} commands");
        }
        private void ReadCommands(string command)
        {
            try
            {
                using (StreamReader sr = new StreamReader(commandsDBpath))
                {
                    string[] lines;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines = line.Split('^');
                        if (lines[0] != command)
                        {
                            commandslst.Add(line);
                        }
                        else
                        {
                            textBoxCommandName.Text = lines[0];
                            textBoxCommand.Text = lines[1];
                            sendMessageToConsole($"Editing command {lines[0]}");
                        }
                    }
                }
            }
            catch (IOException)
            {
                string msg = "error reading from db @ frmaddcommand constructor";
                ShowMessageBox(msg);
                sendMessageToConsole(msg);
            }
        }
        private void ShowMessageBox(string msg, string caption = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(msg, caption, buttons, icon);
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
