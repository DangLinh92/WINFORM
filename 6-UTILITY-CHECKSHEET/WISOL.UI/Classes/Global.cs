using System;
using System.Data;
using System.Windows.Forms;
using Wisol.Objects;

namespace Wisol.MES
{
    public class Consts
    {
        public const string HOME_USER_FILE = "UserId.xml";
        public const string HOME_CONGIF = "Setting2.xml";
        public const string LABELPOINT = "LABEL_POINT.txt";
        public const string DEFAULT_SKIN_INFO = "SkinInfo.txt";
        public const string COMPORT_FILE = "COMInfo.txt";

        public static string DEFAULT_SKIN_SYTELE = "Office 2010 Blue";
        public static string MUNU_TYPE = "T";
        public static string MENU_EXPANDED = "Y";
        public static DataTable GLOSSARY = new DataTable();
        public static string HOME_FILE_PATH = Application.StartupPath + @"\XmlFiles\";
        public static string UPDATE_PROGRAM = string.Empty;
        public static bool ACCESS_INOUT_FLAG = false;
        public static DataTable CLIENT_CONFIG = null;
        public static DateTime LOCAL_SYSTEM_TIME = DateTime.Now;

        public static string PLANT = "WHC";
        public static string ACCESS_PLANT = "WHC";
        public static string INOUT_FLAG = string.Empty;
        public static string WHERE_HOUSE = string.Empty;
        public static string PROJECT_NAME = string.Empty;
        public static string VERSION = string.Empty;
        public static string ACCESS_MSG = string.Empty;
        public static string ORIGINAL_ACCESS_MSG = string.Empty;

        public static ServiceInfo SERVICE_INFO = new ServiceInfo();

        public static LocalSystem LOCAL_SYSTEM_INFO = new LocalSystem();

        public static UserInfo USER_INFO = new UserInfo();

        public static string ACCESS_TYPE = string.Empty;

        public static string UPDATE_IP = string.Empty;

        public static int UPDATE_PORT = 0;

        public static string STOCK_SET = string.Empty;

        public static int X_POINT = 0;
        public static int Y_POINT = 0;

        public static int COM_X = 0;
        public static int CON_Y = 0;

        public static bool CONTAIN_ROLE(string role)
        {
            try
            {
                if (USER_INFO.Role == null)
                {
                    return false;
                }
                else
                {
                    if (USER_INFO.Role.Select("USERROLE = '" + role + "'").Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
