using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Wisol.Common;
using Wisol.Components;

namespace Wisol.MES
{
    class Settings
    {
        public static bool SettingFileCheck()
        {
            if (File.Exists(Consts.HOME_FILE_PATH + Consts.HOME_CONGIF) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SettingUserFileCheck()
        {
            if (File.Exists(Consts.HOME_FILE_PATH + Consts.HOME_USER_FILE) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void SettingCreate()
        {
            DataTable dtSetting = new DataTable();
            dtSetting.TableName = "Settings";
            dtSetting.Columns.Add("GROUP", typeof(string));
            dtSetting.Columns.Add("KEY", typeof(string));
            dtSetting.Columns.Add("VALUE", typeof(string));

            // USERINFO
            dtSetting.Rows.Add(new object[] { "USERINFO", "USER_ID", "" });
            dtSetting.Rows.Add(new object[] { "USERINFO", "USER_NAME", "" });
            dtSetting.Rows.Add(new object[] { "USERINFO", "USER_LANGUAGE", "VTN" });

            // PCINFO
            dtSetting.Rows.Add(new object[] { "PCINFO", "PC_NAME", "" });
            dtSetting.Rows.Add(new object[] { "PCINFO", "PC_USERNAME", "" });
            dtSetting.Rows.Add(new object[] { "PCINFO", "PC_IP", "" });
            dtSetting.Rows.Add(new object[] { "PCINFO", "SERIALPORT", "" });

            dtSetting.AcceptChanges();

            if (!Directory.Exists(Consts.HOME_FILE_PATH))
                Directory.CreateDirectory(Consts.HOME_FILE_PATH);

            dtSetting.WriteXml(Consts.HOME_FILE_PATH + Consts.HOME_CONGIF, XmlWriteMode.WriteSchema);
        }

        public static void SettingRead()
        {
            DataTable dtSetting = new DataTable();
            dtSetting.TableName = "Settings";
            dtSetting.ReadXml(Consts.HOME_FILE_PATH + Consts.HOME_CONGIF);

            Consts.CLIENT_CONFIG = dtSetting;

            //SERVERINFO
            IEnumerable<DataRow> rowsWcf = dtSetting.AsEnumerable().Where(n => n.Field<string>("KEYGROUP").Equals("SERVERINFO"));

            string inout = string.Empty;

            if (Consts.ACCESS_MSG == "PASS")
            {
                inout = "SERVERINFO_" + Consts.ACCESS_PLANT + "_IN";
            }
            else
            {
                inout = "SERVERINFO_" + Consts.ACCESS_PLANT + "_" + Consts.ACCESS_MSG.Replace("AUTO_UPDATE_", "");
            }

            DataRow[] drService = dtSetting.Select("GROUP = '" + inout + "'");

            for (int i = 0; i < drService.Length; i++)
            {
                string value = drService[i]["VALUE"].NullString();
                switch (drService[i]["KEY"].NullString())
                {
                    case "SERVICE_IP": Consts.SERVICE_INFO.ServiceIp = value; break;
                    case "SERVICE_PORT": Consts.SERVICE_INFO.ServicePort = value; break;
                    case "SERVICE_NAME": Consts.SERVICE_INFO.ServiceName = value; break;
                    case "SERVICE_ID": Consts.SERVICE_INFO.UserId = value; break;
                    case "SERVICE_PASSWORD": Consts.SERVICE_INFO.Password = value; break;
                    case "PROJECT_NAME": Consts.PROJECT_NAME = value; break;
                }
            }

            //USERINFO
            DataRow[] drUser = dtSetting.Select("GROUP = 'USERINFO'");

            for (int i = 0; i < drUser.Length; i++)
            {
                string value = drUser[i]["VALUE"].NullString();
                switch (drUser[i]["KEY"].NullString())
                {
                    //case "USER_ID": Global.userInfo.Id = value; break;
                    case "USER_NAME": Consts.USER_INFO.Name = value; break;
                    case "USER_LANGUAGE": Consts.USER_INFO.Language = value; break;
                }
            }

            //LOCALPC
            DataRow[] drPc = dtSetting.Select("GROUP = 'LOCALPC'");

            for (int i = 0; i < drPc.Length; i++)
            {
                string value = drPc[i]["VALUE"].NullString();
                switch (drPc[i]["KEY"].NullString())
                {
                    case "PC_NAME": Consts.LOCAL_SYSTEM_INFO.Name = value; break;
                    case "PC_USERNAME": Consts.LOCAL_SYSTEM_INFO.UserName = value; break;
                    case "PC_IP": Consts.LOCAL_SYSTEM_INFO.IpAddress = value; break;
                    case "SERIALPORT": Consts.LOCAL_SYSTEM_INFO.SerialPort = value; break;
                }
            }
        }

        public static void SettingWrite()
        {
            DataTable dtSetting = new DataTable();
            dtSetting.TableName = "Settings";
            dtSetting.Columns.Add("GROUP");
            dtSetting.Columns.Add("KEY");
            dtSetting.Columns.Add("VALUE");

            // USERINFO
            dtSetting.Rows.Add(new object[] { "USERINFO", "USER_ID", Consts.USER_INFO.Id });
            dtSetting.Rows.Add(new object[] { "USERINFO", "USER_NAME", Consts.USER_INFO.Name });
            dtSetting.Rows.Add(new object[] { "USERINFO", "USER_LANGUAGE", Consts.USER_INFO.Language });

            // LOCALPC
            dtSetting.Rows.Add(new object[] { "LOCALPC", "PC_NAME", Consts.LOCAL_SYSTEM_INFO.Name });
            dtSetting.Rows.Add(new object[] { "LOCALPC", "PC_USERNAME", Consts.LOCAL_SYSTEM_INFO.UserName });
            dtSetting.Rows.Add(new object[] { "LOCALPC", "PC_IP", Consts.LOCAL_SYSTEM_INFO.IpAddress });
            dtSetting.Rows.Add(new object[] { "LOCALPC", "SERIALPORT", Consts.LOCAL_SYSTEM_INFO.SerialPort });
            dtSetting.AcceptChanges();

            if (!Directory.Exists(Consts.HOME_FILE_PATH))
                Directory.CreateDirectory(Consts.HOME_FILE_PATH);

            dtSetting.WriteXml(Consts.HOME_FILE_PATH + Consts.HOME_CONGIF, XmlWriteMode.WriteSchema);
        }
        public static void SettingChange()
        {
            DataTable dtSetting = new DataTable();
            dtSetting.TableName = "Settings";
            dtSetting.ReadXml(Consts.HOME_FILE_PATH + Consts.HOME_CONGIF);
            string type = string.Empty;

            Consts.CLIENT_CONFIG = dtSetting;
            IEnumerable<DataRow> rowsWcf = dtSetting.AsEnumerable().Where(n => n.Field<string>("KEYGROUP").Equals("SERVERINFO"));
            string inout = string.Empty;

            if (Consts.ACCESS_MSG == "PASS")
            {
                inout = "SERVERINFO_" + Consts.ACCESS_PLANT + "_IN";
            }
            else
            {
                inout = "SERVERINFO_" + Consts.ACCESS_PLANT + "_" + Consts.ACCESS_MSG.Replace("AUTO_UPDATE_", "");
            }
            //inout = "SERVER_INFO_" + Global.Plant + Global.AccessMsg.Replace("AUTO_UPDATE_", "");

            DataRow[] drService = dtSetting.Select("GROUP = '" + inout + "'");

            if (Consts.ACCESS_MSG.Replace("AUTO_UPDATE_", "") == "OUT")
            {
                Consts.ACCESS_TYPE = "Y";
            }

            //SERVERINFO


            //DataRow[] drService = dtSetting.Select("GROUP = 'SERVERINFO_" + type + "'");

            for (int i = 0; i < drService.Length; i++)
            {
                string value = drService[i]["VALUE"].NullString();
                switch (drService[i]["KEY"].NullString())
                {
                    case "SERVICE_IP": Consts.SERVICE_INFO.ServiceIp = value; break;
                    case "SERVICE_PORT": Consts.SERVICE_INFO.ServicePort = value; break;
                    case "SERVICE_NAME": Consts.SERVICE_INFO.ServiceName = value; break;
                    case "SERVICE_ID": Consts.SERVICE_INFO.UserId = value; break;
                    case "SERVICE_PASSWORD": Consts.SERVICE_INFO.Password = value; break;
                    case "PROJECT_NAME": Consts.PROJECT_NAME = value; break;
                }
            }
        }
        public static void SaveUser()
        {
            try
            {
                DataTable dtSetting = new DataTable();
                dtSetting.TableName = "USER";
                dtSetting.Columns.Add("GROUP");
                dtSetting.Columns.Add("KEY");
                dtSetting.Columns.Add("VALUE");
                dtSetting.Columns.Add("DEPARTMENT");

                dtSetting.Rows.Add(new object[] { "USER", "USER_ID", Consts.USER_INFO.Id, Consts.DEPARTMENT });

                if (!Directory.Exists(Consts.HOME_FILE_PATH))
                    Directory.CreateDirectory(Consts.HOME_FILE_PATH);

                dtSetting.WriteXml(Consts.HOME_FILE_PATH + Consts.HOME_USER_FILE, XmlWriteMode.WriteSchema);

            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
        public static void LoadUser()
        {
            DataTable table = new DataTable();
            table.TableName = "USER";
            table.ReadXml(Consts.HOME_FILE_PATH + Consts.HOME_USER_FILE);

            Consts.CLIENT_CONFIG = table;

            //SERVERINFO
            IEnumerable<DataRow> rowsWcf = table.AsEnumerable().Where(n => n.Field<string>("KEYGROUP").Equals("USER"));

            DataRow[] drService = table.Select("GROUP = 'USER'");

            for (int i = 0; i < drService.Length; i++)
            {
                string value = drService[i]["VALUE"].NullString();
                string department = drService[i]["DEPARTMENT"].NullString();
                switch (drService[i]["KEY"].NullString())
                {
                    case "USER_ID": Consts.USER_INFO.Id = value; Consts.DEPARTMENT = department; break;
                }

            }
        }
        
    }
}
