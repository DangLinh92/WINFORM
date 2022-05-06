using System;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class ServerInfo
    {
        private string clientIp = string.Empty;
        private string serverIp = string.Empty;
        private string serviceName = string.Empty;        
        private int servicePort = 0;                     
        private string serviceID = string.Empty;          
        private string servicePassword = string.Empty;    
        private string serviceDBtype = string.Empty;         
        private string dbIp = string.Empty;                
        private int dbPort = 0;                            
        private string dbSID = string.Empty;               
        private string dbUserId = string.Empty;            
        private string dbPassword = string.Empty;          
        private bool startFlag;                          
        private bool saveSuccessLog;
        private bool saveErrorLog;
        private bool saveLogToDB;
        private RfcInfo rfcInfo;

        public ServerInfo()
        {

        }
        public ServerInfo(string serviceName, int servicePort, string serviceID, string servicePassword, string serviceDBtype,
            string dbIp, int dbPort, string dbSID, string dbUserId, string dbPassword)
        {
            this.serviceName = serviceName;
            this.servicePort = servicePort;
            this.serviceID = serviceID;
            this.servicePassword = servicePassword;
            this.serviceDBtype = serviceDBtype;
            this.dbIp = dbIp;
            this.dbPort = dbPort;
            this.dbSID = dbSID;
            this.dbUserId = dbUserId;
            this.dbPassword = dbPassword;
        }
        public ServerInfo(string clientIp,
                           string serverIp,
                           string serviceName,
                           int servicePort,
                           string serviceID,
                           string servicePassword,
                           string serviceDBtype,
                           string dbIp,
                           int dbPort,
                           string dbSID,
                           string dbUserId,
                           string dbPassword)
        {
            this.clientIp = clientIp;
            this.serverIp = serverIp;
            this.serviceName = serviceName;
            this.servicePort = servicePort;
            this.serviceID = serviceID;
            this.servicePassword = servicePassword;
            this.serviceDBtype = serviceDBtype;
            this.dbIp = dbIp;
            this.dbPort = dbPort;
            this.dbSID = dbSID;
            this.dbUserId = dbUserId;
            this.dbPassword = dbPassword;
        }

        public string ServiceName
        {
            get
            {
                return this.serviceName;
            }
            set
            {
                this.serviceName = value;
            }
        }
        public int ServicePort
        {
            get
            {
                return this.servicePort;
            }
            set
            {
                this.servicePort = value;
            }
        }
        public string ServiceID
        {
            get
            {
                return this.serviceID;
            }
            set
            {
                this.serviceID = value;
            }
        }
        public string ServicePassword
        {
            get
            {
                return this.servicePassword;
            }
            set
            {
                this.servicePassword = value;
            }
        }
        public string ServiceDBtype
        {
            get
            {
                return this.serviceDBtype;
            }
            set
            {
                this.serviceDBtype = value;
            }
        }
        public string DbIP
        {
            get
            {
                return this.dbIp;
            }
            set
            {
                this.dbIp = value;
            }
        }
        public int DbPort
        {
            get
            {
                return this.dbPort;
            }
            set
            {
                this.dbPort = value;
            }
        }
        public string DbSID
        {
            get
            {
                return this.dbSID;
            }
            set
            {
                this.dbSID = value;
            }
        }
        public string DbUserId
        {
            get
            {
                return this.dbUserId;
            }
            set
            {
                this.dbUserId = value;
            }
        }
        public string DbPassword
        {
            get
            {
                return this.dbPassword;
            }
            set
            {
                this.dbPassword = value;
            }
        }
        public bool StartFlag
        {
            get
            {
                return this.startFlag;
            }
            set
            {
                this.startFlag = value;
            }
        }

        public bool SaveSuccessLog
        {
            get
            {
                return this.saveSuccessLog;
            }
            set
            {
                this.saveSuccessLog = value;
            }
        }

        public bool SaveErrorLog
        {
            get
            {
                return this.saveErrorLog;
            }
            set
            {
                this.saveErrorLog = value;
            }
        }

        public bool SaveLogToDB
        {
            get
            {
                return this.saveLogToDB;
            }
            set
            {
                this.saveLogToDB = value;
            }
        }

        public string ClientIp
        {
            get
            {
                return this.clientIp;
            }
            set
            {
                this.clientIp = value;
            }
        }

        public string ServerIp
        {
            get
            {
                return this.serverIp;
            }
            set
            {
                this.serverIp = value;
            }
        }

        public RfcInfo RfcInfo
        {
            get
            {
                return this.rfcInfo;
            }
            set
            {
                this.rfcInfo = value;
            }
        }
    }
}
