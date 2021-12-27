namespace Wisol.Objects
{
    public class ServiceInfo
    {
        private string serviceIp = string.Empty;
        private string servicePort = string.Empty;
        private string serviceName = string.Empty;
        private string userId = string.Empty;
        private string password = string.Empty;

        public string ServiceIp
        {
            get
            {
                return serviceIp;
            }
            set
            {
                serviceIp = value;
            }
        }

        public string ServicePort
        {
            get
            {
                return servicePort;
            }
            set
            {
                servicePort = value;
            }
        }

        public string ServiceName
        {
            get
            {
                return serviceName;
            }
            set
            {
                serviceName = value;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }
}
