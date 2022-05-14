using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPICommander
{
    internal class Command
    {
        string command_name;
        string command_description;

        public Command(string command_name, string command_description)
        {
            this.Command_name = command_name;
            this.Command_description = command_description;
        }


        public string Command_name { get => command_name; set => command_name = value; }
        public string Command_description { get => command_description; set => command_description = value; }

        public override string ToString()
        {
            return $"{this.Command_name}^{this.Command_description}";
        }
    }
}
