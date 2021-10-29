using System;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class RfcInfo
    {
        private string appServerHost = string.Empty;
        private string systemNumber = string.Empty;
        private string systemId = string.Empty;
        private string user = string.Empty;
        private string password = string.Empty;
        private string client = string.Empty;
        private string language = string.Empty;
        private string poolSize = string.Empty;


        public string AppServerHost
        {
            get
            {
                return appServerHost;
            }
            set
            {
                appServerHost = value;
            }
        }

        public string SystemNumber
        {
            get
            {
                return systemNumber;
            }
            set
            {
                systemNumber = value;
            }
        }

        public string SystemId
        {
            get
            {
                return systemId;
            }
            set
            {
                systemId = value;
            }
        }

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
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

        public string Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }

        public string PoolSize
        {
            get
            {
                return poolSize;
            }
            set
            {
                poolSize = value;
            }
        }
    }
}
