using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Wisol.Common;
using Wisol.Components;
using Wisol.Objects;

using Wisol.MES.Inherit;
using Wisol.MES.Classes;
using Wisol.MES.Dialog;
using System.Text.RegularExpressions;

namespace Wisol.MES.Forms.SMT.POP
{
    public partial class POP_SMT005 : FormType
    {

        public POP_SMT005()
        {
            InitializeComponent();
        }

        public POP_SMT005(string Year, string fileName, DateTime max_date) : this()
        {
            try
            {
                var excel = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                excel.LoadDocument(fileName);
                var worksheet = excel.Document.Worksheets[0];
                var source = new DataTable();
                source.Columns.Add("PLANT");
                source.Columns.Add("DATE_EVENT");
                source.Columns.Add("MODEL");
                source.Columns.Add("LOT_NO");
                source.Columns.Add("LAN");
                source.Columns.Add("INPUT");
                source.Columns.Add("CHUA_DAN");
                source.Columns.Add("SHORT");
                source.Columns.Add("THUA_THIEU_THIEC");
                source.Columns.Add("BONG");
                source.Columns.Add("SAW_SOLDER_OPEN");
                source.Columns.Add("LECH");
                source.Columns.Add("DI_HINH");
                source.Columns.Add("SW_CRACK");
                source.Columns.Add("SW_OPEN");
                source.Columns.Add("NGHI_NGO_SW");
                source.Columns.Add("LNA_CRACK");
                source.Columns.Add("LNA_OPEN");
                source.Columns.Add("NGHI_NGO_LNA");
                source.Columns.Add("NGHI_NGO_SW_LNA");
                source.Columns.Add("SAW_CRACK");
                source.Columns.Add("CREATE_USER");

                var range = worksheet.GetUsedRange();
                for (var row = range.BottomRowIndex; row > 1 ; row--)
                {
                    var a = worksheet[row, 0];
                    var b = worksheet[row, 1];
                    var c = worksheet[row, 2];
                    var t = worksheet[row, 4];
                    var d = worksheet[row, 5];
                    var e = worksheet[row, 22];
                    var f = worksheet[row, 38];
                    var g = worksheet[row, 44];
                    var h = worksheet[row, 48];
                    var i = worksheet[row, 54];
                    var j = worksheet[row, 56];
                    var k = worksheet[row, 62];
                    var l = worksheet[row, 72];
                    var m = worksheet[row, 75];
                    var n = worksheet[row, 78];
                    var o = worksheet[row, 80];
                    var p = worksheet[row, 83];
                    var q = worksheet[row, 86];
                    var r = worksheet[row, 88];
                    var s = worksheet[row, 90];
                    if (!String.IsNullOrEmpty(a.DisplayText) && !String.IsNullOrEmpty(b.DisplayText) && !String.IsNullOrEmpty(c.DisplayText))
                    {
                        if (t.DisplayText.ToUpper().Trim() != "DỰ BỊ") continue;
                        string date_event = a.DisplayText.Trim();
                        string day = date_event.Split('/').Last();
                        string month = date_event.Split('/').First();
                        DateTime dt = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(month), Convert.ToInt32(day));
                        if (DateTime.Compare(dt, max_date) < 1) continue;
                        if (day.Length == 1) day = "0" + day;
                        if (month.Length == 1) month = "0" + month;
                        var @new = source.NewRow();
                        @new["PLANT"] = Consts.PLANT;
                        @new["DATE_EVENT"] = Year + "-" + month + "-" + day;
                        @new["MODEL"] = b.DisplayText.Trim();
                        @new["LOT_NO"] = c.DisplayText.Trim();
                        @new["LAN"] = t.DisplayText.Trim();
                        @new["INPUT"] = d.DisplayText.Trim();
                        @new["CHUA_DAN"] = e.DisplayText.Trim();
                        @new["SHORT"] = f.DisplayText.Trim();
                        @new["THUA_THIEU_THIEC"] = g.DisplayText.Trim();
                        @new["BONG"] = h.DisplayText.Trim();
                        @new["SAW_SOLDER_OPEN"] = i.DisplayText.Trim();
                        @new["LECH"] = j.DisplayText.Trim();
                        @new["DI_HINH"] = k.DisplayText.Trim();
                        @new["SW_CRACK"] = l.DisplayText.Trim();
                        @new["SW_OPEN"] = m.DisplayText.Trim();
                        @new["NGHI_NGO_SW"] = n.DisplayText.Trim();
                        @new["LNA_CRACK"] = o.DisplayText.Trim();
                        @new["LNA_OPEN"] = p.DisplayText.Trim();
                        @new["NGHI_NGO_LNA"] = q.DisplayText.Trim();
                        @new["NGHI_NGO_SW_LNA"] = r.DisplayText.Trim();
                        @new["SAW_CRACK"] = s.DisplayText.Trim();
                        @new["CREATE_USER"] = Consts.USER_INFO.Id;
                        source.Rows.Add(@new);
                    }
                }
                base.mBindData.BindGridView(gcList, source);
                //gvList.Columns["UNIT_COST"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            try
            {
                string XML = Converter.GetDataTableToXml(gcList.DataSource as DataTable);
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT005.POP_PUT_ITEM",
                    new string[] 
                    {
                        "A_XML"
                    },
                    new string[] 
                    {
                        XML
                    }
                );
                if (base.mResultDB.ReturnInt == 0)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
