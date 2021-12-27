using System;
using System.Data;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_SETTING007 : FormType
    {
        string next_1 = "";
        string next_2 = "";
        string next_3 = "";
        
        public POP_SETTING007()
        {
            InitializeComponent();
        }
        public POP_SETTING007(string fileName)
            : this()
        {
            try
            {
                var excel = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                excel.LoadDocument(fileName);
                var worksheet = excel.Document.Worksheets[0];
                var source = new DataTable();
                source.Columns.Add(worksheet[0, 0].DisplayText.Trim());
                source.Columns.Add(worksheet[0, 1].DisplayText.Trim());
                source.Columns.Add(worksheet[0, 2].DisplayText.Trim());
                source.Columns.Add(worksheet[0, 3].DisplayText.Trim());

                next_1 = worksheet[0, 1].DisplayText.Trim();
                next_2 = worksheet[0, 2].DisplayText.Trim();
                next_3 = worksheet[0, 3].DisplayText.Trim();

                var range = worksheet.GetUsedRange();
                for (var row = range.TopRowIndex + 1; row <= range.BottomRowIndex; row++)
                {
                    var model = worksheet[row, 0];
                    var next_1 = worksheet[row, 1];
                    var next_2 = worksheet[row, 2];
                    var next_3 = worksheet[row, 3];

                    var @new = source.NewRow();
                    @new[worksheet[0, 0].DisplayText] = model.DisplayText.Trim();
                    @new[worksheet[0, 1].DisplayText] = next_1.DisplayText.Trim();
                    @new[worksheet[0, 2].DisplayText] = next_2.DisplayText.Trim();
                    @new[worksheet[0, 3].DisplayText] = next_3.DisplayText.Trim();
                    source.Rows.Add(@new);
                }
                base.mBindData.BindGridView(gcList, source);
                gvList.Columns[worksheet[0, 1].DisplayText].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gvList.Columns[worksheet[0, 2].DisplayText].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gvList.Columns[worksheet[0, 3].DisplayText].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            try
            {
                DataTable dt = (DataTable)gcList.DataSource;
                dt.Columns[1].ColumnName = "NEXT_1";
                dt.Columns[2].ColumnName = "NEXT_2";
                dt.Columns[3].ColumnName = "NEXT_3";
                dt.AcceptChanges();
                //string XML = Converter.GetDataTableToXml(gcList.DataSource as DataTable);
                string XML = Converter.GetDataTableToXml(dt);
                if (Consts.DEPARTMENT == "CSP")  // Gọi store procedure xử lý CSP
                { 
                    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.POP_PUT_ITEM",
                    new string[]
                    {
                        "A_DEPARTMENT",
                        "A_XML",
                        "A_NEXT_1",
                        "A_NEXT_2",
                        "A_NEXT_3"
                    },
                    new string[]
                    {
                        Consts.DEPARTMENT,
                        XML,
                        next_1,
                        next_2,
                        next_3
                    }
                );
                }

                if (Consts.DEPARTMENT == "WLP2") // Gọi store procedure xử lý WLP2
                {
                    string temp = SETTING009.plan_type;
                    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.POP_PUT_ITEM_01",
                    new string[]
                    {
                        "A_DEPARTMENT",
                        "A_XML",
                        "A_NEXT_1",
                        "A_NEXT_2",
                        "A_NEXT_3",
                        "V_OPTION_PLAN"
                    },
                    new string[]
                    {
                        Consts.DEPARTMENT,
                        XML,
                        next_1,
                        next_2,
                        next_3,
                        temp
                    }
                );
                }

                if (Consts.DEPARTMENT == "LFEM") // Gọi store procedure xử lý LFEM
                {
                    string temp = SETTING009.plan_type;
                    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.POP_PUT_ITEM_02",
                    new string[]
                    {
                        "A_DEPARTMENT",
                        "A_XML",
                        "A_NEXT_1",
                        "A_NEXT_2",
                        "A_NEXT_3",
                        "V_OPTION_PLAN"
                    },
                    new string[]
                    {
                        Consts.DEPARTMENT,
                        XML,
                        next_1,
                        next_2,
                        next_3,
                        temp
                    }
                );
                }


                if (base.mResultDB.ReturnInt == 0)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    // Copy vao bang KH tuan
                    //int i = base.m_DBaccess.ExcuteProcNoneQuery("PKG_SETTING009.COPY_TO_KEHOACH_TUAN");
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
