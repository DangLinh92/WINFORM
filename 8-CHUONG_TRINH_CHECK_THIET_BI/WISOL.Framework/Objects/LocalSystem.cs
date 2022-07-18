namespace Wisol.Objects
{
    public class LocalSystem
    {
        private string name = string.Empty;
        private string userName = string.Empty;
        private string ipAddress = string.Empty;
        private string macAddress = string.Empty;
        private string serialPort = string.Empty;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        public string MacAddress
        {
            get
            {
                return macAddress;
            }
            set
            {
                macAddress = value;
            }
        }

        public string SerialPort
        {
            get
            {
                return serialPort;
            }
            set
            {
                serialPort = value;
            }
        }
    }
}
