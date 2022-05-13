using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPICommander
{
    internal class Device
    {
        string Hostname;
        string user_name;
        string password;
        int port;
        List<Command> commands;
        public Device(string device_HostName, string user_name, string password,int port = 22)
        {
            this.Device_Hostname = device_HostName;
            this.User_name = user_name;
            this.Password = password;
            this.Port = port;
        }

        public string Device_Hostname { get => Hostname; set => Hostname = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public string Password { get => password; set => password = value; }
        public int Port { get => port; set => port = value; }
        public List<Command> Commands { get => commands; set => commands = value; }

        public List<Command> Get_Commands()
        {
            return Commands;
        }
        public void AddCommand(Command command_name)
        {
            Commands.Add(command_name);
        }
    }
}
