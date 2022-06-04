namespace RPICommander
{
    public class Device
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

        public Device()
        {

        }

        public string Device_Hostname { get => Hostname; set => Hostname = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public string Password { get => password; set => password = value; }
        public int Port { get => port; set => port = value; }

        public override string ToString()
        {
            return $"{Hostname}^{User_name}^{Password}^{Port}";
        }
    }


}
