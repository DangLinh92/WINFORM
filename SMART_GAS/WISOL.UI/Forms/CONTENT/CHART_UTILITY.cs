using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
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
    public partial class CHART_UTILITY : PageType
    {
        public CHART_UTILITY()
        {
            InitializeComponent();
            Load += CHART_UTILITY_Load;
            Classes.Common.SetFormIdToButton(this, "CHART_UTILITY");
        }

        private void CHART_UTILITY_Load(object sender, EventArgs e)
        {
            LoadTemplate();
        }

        private void LoadTemplate()
        {
            IWorkbook workbook = spreadsheetChart.Document;
            workbook.BeginUpdate();

            workbook.LoadDocument("TemplateChartGasUti.xlsx");
            Worksheet sheet1 = workbook.Worksheets[0];
            GetDataForPRRequest(sheet1);
            workbook.EndUpdate();
        }

        private List<string> CELL = new List<string>() { "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };

        private void GetDataForPRRequest(Worksheet sheet)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@REPORT_UTILITY",
                  new string[] { },
                  new string[] { });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    sheet.Cells["C1"].Value = "wisol Ha noi식당 LPG사용내역 " + DateTime.Now.Year;
                    sheet.Cells["B2"].Value = DateTime.Now.Year;
                    sheet.Cells["B26"].Value = DateTime.Now.Year + "(VND)";

                    string strCell = "";
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            strCell = CELL[i] + (26 + j).ToString();

                            sheet.Cells[strCell].Value = float.Parse(data.Rows[i][j].IfNullIsZero());
                            sheet.Cells[strCell].NumberFormat = "#,#";
                        }
                    }

                    if (DateTime.Now.Year - 1 > 2021)
                    {
                        DataTable data1 = base.m_ResultDB.ReturnDataSet.Tables[1];
                        sheet.Cells["B30"].Value = (DateTime.Now.Year - 1) + "(VND)";

                        string strCell1 = "";
                        for (int i = 0; i < data1.Rows.Count; i++)
                        {
                            for (int j = 0; j < data1.Columns.Count; j++)
                            {
                                strCell1 = CELL[i] + (30 + j).ToString();

                                sheet.Cells[strCell1].Value = float.Parse(data1.Rows[i][j].IfNullIsZero());
                                sheet.Cells[strCell1].NumberFormat = "#,#";
                            }
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
