using System;
using System.Data;

namespace PROJ_B_DLL.Objects
{
    [Serializable]
    public class ResultDB
    {
        private int returnInt = 0;
        private string returnString = string.Empty;
        private DataSet returnDataSet = new DataSet();

        public int ReturnInt
        {
            get
            {
                return returnInt;
            }
            set
            {
                returnInt = value;
            }
        }
        public string ReturnString
        {
            get
            {
                return returnString;
            }
            set
            {
                returnString = value;
            }
        }
        public DataSet ReturnDataSet
        {
            get
            {
                return returnDataSet;
            }
            set
            {
                returnDataSet = value.Copy();
            }
        }
    }
}
