using System;
using System.Collections.Generic;
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

        public static string DEPARTMENT = string.Empty;

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

        public static string SPARE_PART_TYPE = "2";
        public static string CONSUMABLE_PART_TYPE = "1";
        public static string OTHER_PART_TYPE = "3";
        public static string NG = "NG";

        public static string INVENTORY_REPORT = "1";
        public static string INVENTORY_IN_OUT_REPORT = "2";
        public static string INVENTORY_IN_OUT_REPORT_BY_DAY = "3";

        public static string MODE_NEW = "NEW";
        public static string MODE_UPDATE = "UPDATE";
        public static string MODE_DELETE = "DELETE";
        public static string MODE_VIEW = "VIEW";

        public static string IN = "IN";
        public static string OUT = "OUT";

        public static string STATUS_COMPLETE = "COMPLETE";
        public static string STATUS_NEW = "NEW";
        public static string STATUS_INPROGRESS = "INPROGRESS";
        public static string STATUS_PUR_RECEIPT = "PUR_RECEIPT";
        public static string STATUS_ORDER = "ORDER";
        public static string STATUS_SHIPPING = "SHIPPING";
        public static string STATUS_RECEIVE = "RECEIVE";
        public static string STATUS_ACCEPT = "ACCEPT";
        public static string STATUS_WAIT_ACCEPT = "WAIT_ACCEPT";
        public static string STATUS_CANCEL = "CANCEL";

        public static char CHARACTER_SPILIT_ON_BARCODE = '$';
        public static string STR_SPILIT_ON_BARCODE = "$";
        public static string STR_SPILIT_WITH_QUANTITY = ":";
        public static char CHAR_SPILIT_WITH_QUANTITY = ':';
        public static string ZERO = "0";
        public static string SHEET_NAME_EWIP_SPAREPART_LOCATION = "EWIP_SPAREPART_LOCATION$";
        public static string IMPORT_TYPE_INVENTORY_REAL = "0";
        public static string IMPORT_TYPE_BUSINESS_LOCATION_SPAREPART_INSERT_BATCH = "1";
        public static string IMPORT_TYPE_PRICE = "2";
        public static string IMPORT_TYPE_INVOICE = "3";
        public static string IMPORT_TYPE_LOGISTICS_DAILY = "4";
        public static string IMPORT_TYPE_GOC_PLAN = "5";
        public static string IMPORT_TYPE_ACTUAL_PRODUCT = "6";
        public static string CONDITION_DEFAULT = "OK";
        public static string PACK_UNIT = "PACK";

        private static DataTable DataMemory;

        public static DataTable GetDataMemory()
        {
            if (DataMemory == null)
            {
                DataMemory = new DataTable();
                DataMemory.Clear();
                DataMemory.Columns.Add("CODE");
                DataMemory.Columns.Add("NAME_VI");
            }
            return DataMemory;
        }

        public static DataTable SPAREPART_TO_ID;
        public static string SMT_PRINTER_DEFAULT = "ZDesigner ZT410-600dpi ZPL (Copy 1)";
        public static string SMT_DEPT = "SMT";
        public static string CSP_DEPT = "CSP";

        public static List<string> lstUnicodeUnitErr = new List<string> { "RÊL", "RÊL", "PẢI", "PẢI", "BÕ", "BÕ", "MÊTR", "MÊTR" };
        public static List<string> lstUnicodeUnitOK = new List<string> { "REEL", "REEL", "PAIR", "PAIR", "BOX", "BOX", "METER", "METER" };

        public static MainForm mainForm { get; set; }
    }
}
