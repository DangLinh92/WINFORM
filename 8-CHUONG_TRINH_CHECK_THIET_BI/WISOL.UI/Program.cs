using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraSplashScreen;
using PROJ_B_DLL.Objects;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;

namespace Wisol.MES
{
    static class Program
    {
        public static MainForm mainForm = null;
        public static DBAccess dbAccess = null;
        public static ResultDB result = new ResultDB();
        //public static string connectionString = "Data Source = 172.22.100.150\\SQLEXPRESS;Initial Catalog = WHNP1;Persist Security Info=True;User id = sa;Password = 123456@a;Connect Timeout=5";
        //public static string connectionString = "Data Source = 10.70.21.72\\SQLEXPRESS;Initial Catalog = WHNP_TEST;Persist Security Info=True;User id = sa;Password = 123456;Connect Timeout=3";
        //public static string connectionString = "Data Source = 10.70.10.97;Initial Catalog = WHNP1;User Id = sa;Password = Wisol@123;Connect Timeout=3";
        
        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                try
                {
                    string input = args[0].ToString();
                    if (!input.Equals("PASS"))
                    {
                        MsgBox.Show("Please Run Updater Program First!", MsgType.Error);
                        Application.ExitThread();
                        Environment.Exit(0);
                        Application.Exit();
                    }
                }
                catch
                {
                    MsgBox.Show("Please Run Updater Program First!", MsgType.Error);
                    Application.ExitThread();
                    Environment.Exit(0);
                    Application.Exit();
                }
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = SystemFonts.CaptionFont;
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = SystemFonts.MenuFont;
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = SystemFonts.CaptionFont;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DevExpress.UserSkins.BonusSkins.Register();
                SkinManager.EnableFormSkins();
                LoadSkin();
                DevExpress.Utils.Paint.XPaint.ForceTextRenderPaint();
                SplashScreenManager.ShowForm(typeof(Dialog.SplashScreen), true, false);
                SplashScreenManager.Default.SendCommand(FrmSplashScreen.SplashScreenCommand.Description, "System Stating...");

                if (!Settings.SettingUserFileCheck())
                {
                    Settings.LoadUser();
                }

                Settings.SettingCreate();

                SplashScreenManager.Default.SendCommand(FrmSplashScreen.SplashScreenCommand.Description, "Server Information Setting...");

                Consts.LOCAL_SYSTEM_INFO.IpAddress = GetIp();
                Consts.LOCAL_SYSTEM_INFO.Name = GetPCInfo();

                SplashScreenManager.Default.SendCommand(FrmSplashScreen.SplashScreenCommand.Description, "Version Check...");

                Settings.SettingRead();

                dbAccess = new DBAccess(Consts.LOCAL_SYSTEM_INFO.IpAddress, Consts.SERVICE_INFO.ServiceIp, Converter.ParseValue<int>(Consts.SERVICE_INFO.ServicePort), Consts.SERVICE_INFO.UserId, Consts.SERVICE_INFO.Password, string.Empty);
                //dbAccess = new DBAccess("Data Source = 10.70.21.72;Initial Catalog = WHNP1;Persist Security Info=True;User id = sa;Password = 123456;Connect Timeout=3");
                mainForm = new MainForm();

                SplashScreenManager.Default.SendCommand(FrmSplashScreen.SplashScreenCommand.Description, "Language Check...");
                GetGlsr();
                LoadLabelPoint();


                Screen screen = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));
                if (screen.Primary == false)
                {
                    mainForm.StartPosition = FormStartPosition.Manual;
                    mainForm.SetDesktopLocation(screen.WorkingArea.Left, 0);
                }
                Thread.Sleep(300);
                SplashScreenManager.CloseForm();

                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


        public static string GetIp()
        {
            try
            {
                string ip = string.Empty;

                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

                for (int i = 0; i < ipHostEntry.AddressList.Length; i++)
                {
                    if (ipHostEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ip = ipHostEntry.AddressList[i].ToString();
                        break;
                    }
                }

                return ip;
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string GetPCInfo()
        {
            try
            {
                string name = string.Empty;

                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

                if (ipHostEntry == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ipHostEntry.HostName.NullString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void GetGlsr()
        {
            try
            {
                result = dbAccess.ExcuteProc("PKG_COMM.GET_GLSR"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }

                    );

                if (result.ReturnInt == 0)
                {
                    Consts.GLOSSARY = result.ReturnDataSet.Tables[0].Copy();
                }
                else
                {
                    MessageBox.Show(result.ReturnString);
                    Application.ExitThread();
                    Environment.Exit(0);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        public static void LoadLabelPoint()
        {
            try
            {
                if (File.Exists(Consts.LABELPOINT))
                {
                    string[] point = File.ReadAllText(Consts.LABELPOINT).Split('/');
                    Consts.X_POINT = Converter.ParseValue<int>(point[0]);
                    Consts.Y_POINT = Converter.ParseValue<int>(point[1]);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
        public static void LoadSkin()
        {
            try
            {
                if (File.Exists(Consts.DEFAULT_SKIN_INFO))
                {
                    Consts.DEFAULT_SKIN_SYTELE = File.ReadAllText(Consts.DEFAULT_SKIN_INFO);
                }
                UserLookAndFeel.Default.SetSkinStyle(Consts.DEFAULT_SKIN_SYTELE);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
