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
    public partial class POP_SMT001 : FormType
    {

        public POP_SMT001()
        {
            InitializeComponent();
        }

        public POP_SMT001(string Year, string Month, string fileName)
            : this()
        {
            try
            {
                var excel = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                excel.LoadDocument(fileName);
                var worksheet = excel.Document.Worksheets[0];
                var source = new DataTable();
                source.Columns.Add("PLANT");
                source.Columns.Add("MATERIAL_CODE");
                source.Columns.Add("UNIT_COST");
                source.Columns.Add("TYPE");
                source.Columns.Add("YEAR");
                source.Columns.Add("MONTH");
                source.Columns.Add("CREATE_USER");

                var range = worksheet.GetUsedRange();
                for (var row = range.TopRowIndex + 1; row <= range.BottomRowIndex; row++)
                {
                    var a = worksheet[row, 0];
                    var b = worksheet[row, 1];
                    var c = worksheet[row, 2];
                    if (!String.IsNullOrEmpty(a.DisplayText) && !String.IsNullOrEmpty(b.DisplayText) && !String.IsNullOrEmpty(c.DisplayText))
                    {
                        if (a.DisplayText.ToUpper() == "NO") continue;
                        var @new = source.NewRow();
                        @new["PLANT"] = Consts.PLANT;
                        @new["MATERIAL_CODE"] = a.DisplayText;
                        @new["UNIT_COST"] = (b.DisplayText.Trim() != "-" ? b.DisplayText.Trim() : "1");
                        @new["TYPE"] = c.DisplayText;
                        @new["YEAR"] = Year;
                        @new["MONTH"] = Month;
                        @new["CREATE_USER"] = Consts.USER_INFO.Id;
                        source.Rows.Add(@new);
                    }
                }
                base.mBindData.BindGridView(gcList, source);
                gvList.Columns["UNIT_COST"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            try
            {
                string XML = Converter.GetDataTableToXml(gcList.DataSource as DataTable);
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT001.POP_PUT_ITEM",
                    new string[] 
                    {
                        "A_PLANT",
                        "A_XML"
                    },
                    new string[] 
                    {
                        Consts.PLANT,
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
