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
