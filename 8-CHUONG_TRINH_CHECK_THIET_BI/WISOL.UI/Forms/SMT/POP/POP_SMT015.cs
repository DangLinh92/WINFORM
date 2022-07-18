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
    public partial class POP_SMT015 : FormType
    {

        public POP_SMT015()
        {
            InitializeComponent();
        }

        public POP_SMT015(string Year, string Month, string fileName)
            : this()
        {
            try
            {
                var excel = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                excel.LoadDocument(fileName);
                var worksheet = excel.Document.Worksheets[0];
                var source = new DataTable();
                source.Columns.Add("YEAR");
                source.Columns.Add("MONTH");
                source.Columns.Add("LINE");
                source.Columns.Add("MODEL");
                source.Columns.Add("POINT");
                source.Columns.Add("BLOCK");
                source.Columns.Add("CYCLE_TIME");
                source.Columns.Add("DAY_CAPA");
                source.Columns.Add("MONTH_CAPA");
                
               // source.Columns.Add("CREATE_USER");

                var range = worksheet.GetUsedRange();
                string line = string.Empty;
                for (var row = range.TopRowIndex + 1; row <= range.BottomRowIndex; row++)
                {
                    if(!string.IsNullOrWhiteSpace(worksheet[row, 1].DisplayText))
                    {
                        line = worksheet[row, 1].DisplayText;
                    }
                    //var a = worksheet[row, 1];
                    //var b = worksheet[row, 2];
                    //var c = worksheet[row, 3];
                    //if (!String.IsNullOrEmpty(a.DisplayText) && !String.IsNullOrEmpty(b.DisplayText) && !String.IsNullOrEmpty(c.DisplayText))
                    //{
                    //    if (a.DisplayText.ToUpper() == "NO") continue;
                    //    var @new = source.NewRow();
                    //    @new["LINE"] = worksheet[row, 1].DisplayText;
                    //    @new["MODEL"] = worksheet[row, 2].DisplayText;
                    //    @new["POINT"] = worksheet[row, 3].DisplayText;
                    //    @new["BLOCK"] = worksheet[row, 4].DisplayText;
                    //    @new["CYCLE_TIME"] = worksheet[row, 5].DisplayText;
                    //    @new["일_CAPA"] = worksheet[row, 6].DisplayText;
                    //    @new["월_CAPA"] = worksheet[row, 7].DisplayText;
                    //    source.Rows.Add(@new);
                    //}
                    var @new = source.NewRow();
                    @new["YEAR"] = Year;
                    @new["MONTH"] = Month;
                    @new["LINE"] = line;// worksheet[row, 1].DisplayText;
                    @new["MODEL"] = worksheet[row, 2].DisplayText;
                    @new["POINT"] = worksheet[row, 3].DisplayText;
                    @new["BLOCK"] = worksheet[row, 4].DisplayText;
                    @new["CYCLE_TIME"] = worksheet[row, 5].DisplayText;
                    @new["DAY_CAPA"] = worksheet[row, 6].DisplayText;
                    @new["MONTH_CAPA"] = worksheet[row, 7].DisplayText;
                   // @new["CREATE_USER"] = Consts.USER_INFO.Id;
                    source.Rows.Add(@new);
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
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT015.POP_PUT_ITEM",
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
