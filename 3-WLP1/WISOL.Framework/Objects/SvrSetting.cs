using System;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class SvrPortSetting
    {
        private string serverIp = string.Empty;
        private int updatePort = 0;
        private int pdaUpdatePort = 0;
        private int fileTransferPort = 0;
        private int resPort = 0;
        private string fileDirPath = string.Empty;
        private string updateDirPath = string.Empty;

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

        public int UpdatePort
        {
            get
            {
                return this.updatePort;
            }
            set
            {
                this.updatePort = value;
            }
        }

        public int PdaUpdatePort
        {
            get
            {
                return this.pdaUpdatePort;
            }
            set
            {
                this.pdaUpdatePort = value;
            }
        }

        public int FileTransferPort
        {
            get
            {
                return this.fileTransferPort;
            }
            set
            {
                this.fileTransferPort = value;
            }
        }
        public int ResPort
        {
            get
            {
                return this.resPort;
            }
            set
            {
                this.resPort = value;
            }
        }

        public string FileDirPath
        {
            get
            {
                return this.fileDirPath;
            }
            set
            {
                this.fileDirPath = value;
            }
        }

        public string UpdateDirPath
        {
            get
            {
                return this.updateDirPath;
            }
            set
            {
                this.updateDirPath = value;
            }
        }
    }
}
