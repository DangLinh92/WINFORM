using System;
using System.Collections.Generic;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class FileList
    {
        Dictionary<string, byte[]> fileList = new Dictionary<string, byte[]>();
        Dictionary<string, string> fileCheck = new Dictionary<string, string>();

        public Dictionary<string, byte[]> FileData
        {
            get
            {
                return fileList;
            }
            set
            {
                fileList = value;
            }
        }
        public Dictionary<string, string> FileCheck
        {
            get
            {
                return fileCheck;
            }
            set
            {
                fileCheck = value;
            }
        }

    }
}
