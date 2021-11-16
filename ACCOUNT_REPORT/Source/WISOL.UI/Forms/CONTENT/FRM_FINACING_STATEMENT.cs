using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class FRM_FINACING_STATEMENT : PageType
    {
        public FRM_FINACING_STATEMENT()
        {
            InitializeComponent();
            this.Load += FRM_FINACING_STATEMENT_Load;
        }

        private void FRM_FINACING_STATEMENT_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_FINACING_STATEMENT");
        }

        private void btnLoadFileData_Click(object sender, EventArgs e)
        {
            LoadTemplate();
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {

        }

        private void LoadTemplate()
        {
            IWorkbook workbook = spreadsheetMain.Document;
            workbook.BeginUpdate();

            workbook.LoadDocument("FINANCING_STATEMENT.xlsx");
            Worksheet sheet1 = workbook.Worksheets[0];
            GetDataForPRRequest(sheet1);
            workbook.EndUpdate();
        }

        private void GetDataForPRRequest(Worksheet sheet)
        {
            try
            {
                string createDate = createDate = dateLoad.EditValue.NullString();

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_FINACING_STATEMENT.GET_DATE_REPORT",
                  new string[] { "A_DATE" },
                  new string[] { createDate });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection data = base.m_ResultDB.ReturnDataSet.Tables;

                    sheet.Cells["A3"].Value = DateTime.Parse(createDate).Year + "년  " + DateTime.Parse(createDate).Month + "월  " + DateTime.Parse(createDate).Day + "일";

                    // tien mat
                    if (data[0].Rows.Count > 0)
                    {
                        if(data[0].Rows.Count > 2)
                        {
                            for (int i = 2; i < data[0].Rows.Count; i++)
                            {
                                sheet.Rows[i + 9].Insert(InsertCellsMode.EntireRow);
                                //sheet.Rows[i + 9].CopyFrom(sheet.Rows[10], PasteSpecial.Formats);
                            }
                        }

                        sheet.DataBindings.BindToDataSource(data[0], 10, 0);
                    }

                    int startCashVND = 16;
                    if(data[0].Rows.Count > 2)
                    {
                        startCashVND = 16 + (data[0].Rows.Count - 2);
                    }

                    sheet.DataBindings.BindToDataSource(data[1], startCashVND, 0);
                    if (data[1].Rows.Count > 2)
                    {
                        for (int i = 2; i < data[1].Rows.Count; i++)
                        {
                            sheet.Rows[i + 9].Insert(InsertCellsMode.EntireRow);
                            //sheet.Rows[i + 9].CopyFrom(sheet.Rows[10], PasteSpecial.Formats);
                        }
                    }
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString, MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
