using PROJ_B_DLL.DataAcess;
using System;
using System.Collections.Generic;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class SetterDB
    {
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        private string procName = string.Empty;
        private string query = string.Empty;
        private string serverID = string.Empty;
        private string serverPW = string.Empty;
        private string clientAddress = string.Empty;
        private string userId = string.Empty;
        private DBAccessType dbAccessType = DBAccessType.DB;
        private int rfcTableCount = 0;
        private string rfcDirection = string.Empty;



        public Dictionary<string, string> Parameter
        {
            get
            {
                return dic;
            }
            set
            {
                dic = value;
            }
        }
        public string ProcName
        {
            get
            {
                return procName;
            }
            set
            {
                procName = value;
            }
        }
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
            }
        }
        public string ServerID
        {
            get
            {
                return serverID;
            }
            set
            {
                serverID = value;
            }
        }
        public string ServerPW
        {
            get
            {
                return serverPW;
            }
            set
            {
                serverPW = value;
            }
        }
        public string ClientAddress
        {
            get
            {
                return clientAddress;
            }
            set
            {
                clientAddress = value;
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

        public DBAccessType DbAccessType
        {
            get
            {
                return dbAccessType;
            }
            set
            {
                dbAccessType = value;
            }
        }

        public int RfcTableCount
        {
            get
            {
                return rfcTableCount;
            }
            set
            {
                rfcTableCount = value;
            }
        }

        public string RfcDirection
        {
            get
            {
                return rfcDirection;
            }
            set
            {
                rfcDirection = value;
            }
        }
    }
}
