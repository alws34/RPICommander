namespace RPICommander
{
    public class Command
    {
        string command_name;
        string command_description;

        public Command(string command_name, string command_description)
        {
            this.Command_name = command_name;
            this.Command_description = command_description;
        }
        public Command() { }

        public string Command_name { get => command_name; set => command_name = value; }
        public string Command_description { get => command_description; set => command_description = value; }

        public override string ToString()
        {
            return $"{Command_name}^{Command_description}";
        }
    }
}
