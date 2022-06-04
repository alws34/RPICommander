namespace RPICommander
{/*this form adds or edits commands*/
    public partial class frmAddCommand : Form
    {
        string commandsDBpath;
        List<Command> commandslst = new List<Command>();
        bool edit_mode = false;
        string delimiter = "^";
        Command edited_command = new Command();

        public string CommandsDBpath { get => commandsDBpath; set => commandsDBpath = value; }

        public event SendMessageToConsoleEventHandler SendMessageToConsole;
        public event SetPlaceHolderEventHandler SetPlaceHolder;
        protected virtual void OnTBFocusChange(TextBox tb)
        {
            SetPlaceHolder?.Invoke(new SetPlaceHolderEventArgs(tb));
        }
        protected virtual void OnSendMessageToConsoleEvent(string msg)
        {
            SendMessageToConsole?.Invoke(new SendMessageToConsoleEventArgs(msg));
        }


        public frmAddCommand(string commandsDBpath)
        {
            InitializeComponent();
            CommandsDBpath = commandsDBpath;
            SetTags();
            SetText();
            SetEvents();
            CenterToScreen();

        }

        public frmAddCommand(string path, Command command, List<Command> command_lst)
        {
            InitializeComponent();
            SetTags();
            SetEvents();
            commandsDBpath = path;
            edit_mode = true;
            commandslst = command_lst;
            SetGui(command);
            SetEdittedCommand(command);
            OnSendMessageToConsoleEvent($"Editing command: {command.Command_name}");
            OnSendMessageToConsoleEvent($"Read {commandslst.Count} commands from DB:\n");
            CenterToScreen();
            PrintCommands();
        }

        private void SetEvents()
        {
            textBoxCommand.GotFocus += IsFocused;
            textBoxCommand.LostFocus += IsFocused;
            textBoxCommandName.GotFocus += IsFocused;
            textBoxCommandName.LostFocus += IsFocused;
        }

        private void SetGui(Command command)
        {
            textBoxCommandName.Text = command.Command_name;
            textBoxCommand.Text = command.Command_description;

        }

        private void SetEdittedCommand(Command command)
        {
            edited_command.Command_description = command.Command_description;
            edited_command.Command_name = command.Command_name;

            for (int i = 0; i < commandslst.Count; i++)//remove editted command from commands list (removes all instances)
                if (commandslst[i].Command_name == command.Command_name)
                    commandslst.RemoveAt(i);
        }

        private void SetTags()
        {
            textBoxCommandName.Tag = "Enter Command name Here";
            textBoxCommand.Tag = "Enter Command Here";
        }
       
        private void SetText()
        {
            textBoxCommandName.Text = textBoxCommandName.Tag.ToString();
            textBoxCommand.Text = textBoxCommand.Tag.ToString();
        }
     
        private void PrintCommands()
        {
            foreach (Command cmd in commandslst)
                OnSendMessageToConsoleEvent($"\t{cmd.ToString().Replace(delimiter, ": ")}");
        }

        private void ReadCommandsFromDB(Command command)
        {
            try
            {
                using (StreamReader sr = new StreamReader(commandsDBpath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lines = line.Split(delimiter.ToCharArray());
                        if (edit_mode)
                        {
                            if (command.Command_name != lines[0])//add all commands except from edited command
                                commandslst.Add(new Command(lines[0], lines[1]));
                            else
                            {
                                textBoxCommandName.Text = lines[0];
                                textBoxCommand.Text = lines[1];
                                OnSendMessageToConsoleEvent($"Editing command: {lines[0]}");
                            }
                        }
                        else
                        {
                            commandslst.Add(new Command(lines[0], lines[1]));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string msg = $"Error Reading commands from DB\n\t\t{e.Message}";
                OnSendMessageToConsoleEvent(msg);
            }
        }

        private void ShowMessageBox(string msg, string caption = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            MessageBox.Show(msg, caption, buttons, icon);
        }

        private void btnSaveCommand_Click(object sender, EventArgs e)
        {
            string commandName = textBoxCommandName.Text;
            string command = textBoxCommand.Text;

            if (!(string.IsNullOrWhiteSpace(commandName)) && !(string.IsNullOrWhiteSpace(command)))
                commandslst.Add(new Command(commandName, command));

            using (StreamWriter sw = new StreamWriter(CommandsDBpath))
                foreach (Command cmd in commandslst)
                    sw.WriteLine(cmd.ToString());
            Dispose();
        }

        private void btnDeleteCommand_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < commandslst.Count; i++)
            {
                if (commandslst[i].Command_name == textBoxCommandName.Text && commandslst[i].Command_description == textBoxCommand.Text)
                {
                    commandslst.RemoveAt(i);
                    Dispose();
                }
            }
        }

        private void IsFocused(object sender, EventArgs e)
        {
            string sender_name = sender.GetType().Name;
            switch (sender_name)
            {
                case "TextBox":
                    TextBox? tb = sender as TextBox;
                    if (tb != null)
                        OnTBFocusChange(tb);
                    break;
                default:
                    OnSendMessageToConsoleEvent($"Sender wasn't a TextBox @frmAddDevice - IsFocused: {sender_name}");
                    break;
            }
        }
    }
}
