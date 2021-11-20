using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.BindDatas;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Classes
{
    public class Common
    {
        public static void ShowImge(string image, DevExpress.XtraEditors.PictureEdit img)
        {
            if (!string.IsNullOrWhiteSpace(image))
            {
                byte[] imagebytes = Convert.FromBase64String(image);
                using (var ms = new MemoryStream(imagebytes, 0, imagebytes.Length))
                {
                    img.Image = Image.FromStream(ms, true);
                }
                img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                img.Size = img.Image.Size;
            }
            else
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images/default-product-image.png");
                img.Image = Image.FromFile(path);
                img.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                img.Size = img.Image.Size;
            }
        }

        public static Image GetImage(string image)
        {
            if (!string.IsNullOrWhiteSpace(image))
            {
                byte[] imagebytes = Convert.FromBase64String(image);
                using (var ms = new MemoryStream(imagebytes, 0, imagebytes.Length))
                {
                    return Image.FromStream(ms, true);
                }
            }

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images/default-product-image.png");

            return Image.FromFile(path);
        }

        public static void SendMail(string title)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("danglevan.9199@gmail.com");
                mail.To.Add("whcpi1@wisol.co.kr");
                mail.Subject = title;

                mail.IsBodyHtml = true;
                string htmlBody;

                htmlBody = "Write some HTML code here";

                mail.Body = htmlBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        protected internal static float ConvertUnit(string unitFrom, string unitTo, string spareCode)
        {
            try
            {
                ResultDB m_ResultDB = Program.dbAccess.ExcuteProc("PKG_BUSINESS_ALL.CONVERT_UNIT",
                      new string[] { "A_UNIT_FROM", "A_UNIT_TO", "A_SPARE_PART_CODE", "A_DEPT_CODE" },
                      new string[] { unitFrom, unitTo, spareCode, Consts.DEPARTMENT });

                if (m_ResultDB.ReturnInt == 0)
                {
                    if (m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        return float.Parse(m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["RESULT"].NullString());
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            return 1;
        }

        public static DataTable GetDataTable(GridView view, string[] arrNoAdd)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn c in view.Columns)
            {
                if (!arrNoAdd.Contains(c.FieldName))
                {
                    dt.Columns.Add(c.FieldName);
                }
            }


            for (int r = 0; r < view.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                    rowValues[c] = view.GetRowCellValue(r, dt.Columns[c].ColumnName).NullString();
                dt.Rows.Add(rowValues);
            }
            return dt;
        }

        public static void SelectPrinter(ComboBoxEdit cboPrinter)
        {
            ComboBoxItemCollection coll = cboPrinter.Properties.Items;
            coll.BeginUpdate();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                coll.Add(printer);
            }
            coll.EndUpdate();

            PrinterSettings settings = new PrinterSettings();
            cboPrinter.Text = settings.PrinterName;
        }

        public static List<ButtonInfo> GetAllButton(PageType page)
        {
            List<ButtonInfo> buttonInfos = new List<ButtonInfo>();
            IEnumerable<Control> lst = GetAll(page, typeof(XSimpleButton), typeof(SimpleButton));
            ButtonInfo button;
            foreach (var item in lst)
            {
                button = new ButtonInfo();
                button.Name = item.Name;
                button.Text = item.Text;
                button.IsActive = true;
                buttonInfos.Add(button);
            }
            return buttonInfos;
        }

        public static IEnumerable<Control> GetAll(Control control, Type type, Type type1)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type, type1))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type || c.GetType() == type1);
        }

        public static DataTable ToDataTable<T>(List<T> items, DataTable tableTemp)

        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)

            {

                //Setting column names as Property names
                if (tableTemp.Columns.Contains(prop.Name))
                {
                    dataTable.Columns.Add(prop.Name, tableTemp.Columns[prop.Name].DataType);
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }
            }

            foreach (T item in items)

            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)

                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }

        public static void SetFormIdToButton(PageType page, string formId, FormType formType = null)
        {
            var controls = page != null ? GetAll(page, typeof(XSimpleButton), typeof(SimpleButton)) : GetAll(formType, typeof(XSimpleButton), typeof(SimpleButton));
            foreach (Control item in controls)
            {
                if (item.GetType() == typeof(XSimpleButton))
                {
                    ((XSimpleButton)item).FormId = formId;
                    ((XSimpleButton)item).isFormType = page == null;
                }
            }
        }

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }
    }

    public class ButtonInfo
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string FormId { get; set; }
        public bool IsActive { get; set; }
    }
}
