﻿using System;
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

        public Device(string device_HostName, string user_name, string password, int port = 22)
        {
            Device_Hostname = device_HostName;
            User_name = user_name;
            Password = password;
            Port = port;
        }

        public string Device_Hostname { get => Hostname; set => Hostname = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public string Password { get => password; set => password = value; }
        public int Port { get => port; set => port = value; }

        public override string ToString()
        {
            return $"{this.Hostname}^{this.User_name}^{this.Password}^{this.Port}";
        }
    }


}
