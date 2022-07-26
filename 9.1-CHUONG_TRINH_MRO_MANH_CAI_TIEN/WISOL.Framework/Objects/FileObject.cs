using System;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class FileObject
    {
        string fileName = string.Empty;
        Byte[] fileContent = null;
        int result = 0;

        public FileObject()
        {

        }

        public FileObject(string fileName)
        {
            this.fileName = fileName;
        }
        public FileObject(string fileName, Byte[] fileContent)
        {
            this.fileName = fileName;
            this.fileContent = fileContent;
        }
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public Byte[] FileContent
        {
            get
            {
                return this.fileContent;
            }
            set
            {
                this.fileContent = value;
            }
        }

        public int Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }
    }
}
