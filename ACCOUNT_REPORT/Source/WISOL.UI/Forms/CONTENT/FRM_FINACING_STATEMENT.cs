using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Classes;
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
            cheDaily.Checked = true;
        }

        private void btnLoadFileData_Click(object sender, EventArgs e)
        {
            if (dateLoad.EditValue.NullString() != "")
            {
                LoadTemplate();
            }
        }

        private const string SHEET_USD = "자금실적 및 계획(원)USD_VND";
        private const string SHEET_KRW = "자금실적 및 계획(원)KRW_VND";
        private const string VND_KRW = "0.05";
        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = "Financing Statement-" + DateTime.Parse(dateLoad.EditValue.NullString()).ToString("yyyyMMdd");

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;

                    // Save the modified document to a file.
                    IWorkbook workbook = spreadsheetMain.Document;
                    workbook.SaveDocument(fileName, DocumentFormat.Xlsx);
                    Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void LoadTemplate()
        {
            splashScreenManager1.ShowWaitForm();
            IWorkbook workbook = spreadsheetMain.Document;
            workbook.BeginUpdate();

            string fileName = "FINANCING_STATEMENT.xlsx";
            if (!cheDaily.Checked)
            {
                fileName = "MONTHLY_REPORT.xlsx";
            }

            workbook.LoadDocument(fileName);


            if (cheDaily.Checked)
            {
                Worksheet sheet1 = workbook.Worksheets[0];
                GetDataForReportDaily(sheet1);
            }
            else
            {
                string dateFrom = DateTime.Parse(dateLoad.EditValue.NullString()).ToString("yyyy-MM-dd");
                string dateto = DateTime.Parse(dateTo.EditValue.NullString()).ToString("yyyy-MM-dd");

                if (string.Compare(DateTime.Now.ToString("yyyyMM"), DateTime.Parse(dateto).ToString("yyyyMM")) >= 0 && string.Compare(dateFrom, dateto) <= 0)
                {
                    Worksheet sheet1 = workbook.Worksheets[SHEET_USD];// usd -> vnd 자금실적 및 계획(원)KRW_VND
                    Worksheet sheet2 = workbook.Worksheets[SHEET_KRW];
                    Worksheet sheet3 = workbook.Worksheets["자금요약"];
                    GetDataForReportMonthly(sheet1);
                    GetDataForReportMonthly(sheet2);
                    CreateFirstSheet(sheet3);
                }
                else
                {
                    MsgBox.Show("MONTH FROM < MONTH TO ", MsgType.Error);
                }
            }

            workbook.EndUpdate();
            splashScreenManager1.CloseWaitForm();
        }

        string[] arrHeader = { "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q" };
        private void CreateFirstSheet(Worksheet sheet)
        {
            try
            {
                // Từ năm 2022 trở đi sẽ cho from date luôn bắt đầu bằng tháng 1, kéo dài cho đến tháng to. Sẽ cho ẩn các tháng như excel

                string dateFrom = DateTime.Parse(dateLoad.EditValue.NullString()).ToString("yyyy-MM-dd");
                string dateto = DateTime.Parse(dateTo.EditValue.NullString()).ToString("yyyy-MM-dd");

                int monthDiff = Classes.Common.MonthDifference(DateTime.Parse(dateto), DateTime.Parse(dateFrom)) + 1;

                if(DateTime.Parse(dateto).ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                {
                    monthDiff -= 1;
                }

                if (monthDiff <= 1)
                {
                    sheet.Columns["G"].Delete();
                    sheet.Cells["F4"].SetValueFromText(DateTime.Parse(Header_Month_Dic.First().Key).ToString("yy.MM"));
                }
                else if (monthDiff > 2)
                {
                    sheet.Columns.Insert(7, monthDiff - 2);

                    for (int j = 0; j < monthDiff - 2; j++)
                    {
                        sheet.Columns[7 + j].CopyFrom(sheet.Columns[7 + j - 1], PasteSpecial.All);
                    }

                    int max = 0;
                    if (DateTime.Parse(Header_Month_Dic.Last().Key).ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                    {
                        max = Header_Month_Dic.Count - 1;
                    }
                    else
                    {
                        max = Header_Month_Dic.Count;
                    }

                    int i = 0;
                    foreach (var item in Header_Month_Dic)
                    {
                        if (i < max)
                        {
                            sheet.Cells[arrHeader[i] + "4"].SetValueFromText(DateTime.Parse(item.Key).ToString("yy.MM"));
                        }
                        i++;
                    }
                }
                else
                {
                    int max = 0;
                    if (DateTime.Parse(Header_Month_Dic.Last().Key).ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                    {
                        max = Header_Month_Dic.Count - 1;
                    }
                    else
                    {
                        max = Header_Month_Dic.Count;
                    }

                    int i = 0;
                    foreach (var item in Header_Month_Dic)
                    {
                        if(i < max)
                        {
                            sheet.Cells[arrHeader[i] + "4"].SetValueFromText(DateTime.Parse(item.Key).ToString("yy.MM"));
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private Dictionary<string, string> Header_Month_Dic;
        private void GetDataForReportMonthly(Worksheet sheet)
        {
            try
            {
                Header_Month_Dic = new Dictionary<string, string>();

                string dateFrom = DateTime.Parse(dateLoad.EditValue.NullString()).ToString("yyyy-MM-dd");
                string dateto = DateTime.Parse(dateTo.EditValue.NullString()).ToString("yyyy-MM-dd");

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_FINACING_STATEMENT.GET_DATA_REPORT_MONTHLY",
                 new string[] { "A_DATE_FROM", "A_DATE_TO" },
                 new string[] { dateFrom, dateto });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection data = base.m_ResultDB.ReturnDataSet.Tables;
                    List<string> months = new List<string>();
                    foreach (DataRow r in data[0].Rows)
                    {
                        if (!months.Contains(r["MONTH_DAY"].NullString()))
                        {
                            months.Add(r["MONTH_DAY"].NullString());
                        }
                    }

                    months = months.OrderBy(x => x).ToList();

                    if (months.Count > 0)
                    {
                        sheet.Cells["F2"].SetValueFromText(months.OrderBy(x => x).ToList()[0]);

                        if (sheet.Name == SHEET_USD)
                        {
                            sheet.Cells["G4"].SetValueFromText(data[1].Rows[0]["LOAN_AMT_BEGIN_VND"].NullString());// loan vnd
                            sheet.Cells["F5"].SetValueFromText(data[1].Rows[0]["CASH_BEGIN_USD"].NullString());// cash usd
                            sheet.Cells["G5"].SetValueFromText(data[1].Rows[0]["CASH_BEGIN_USD_VND"].NullString());// cash usd->VND
                            sheet.Cells["G6"].SetValueFromText(data[1].Rows[0]["CASH_BEGIN_VND"].NullString());// cash VND
                            sheet.Cells["G7"].SetValueFromText(data[1].Rows[0]["SAVE_MONEY_BEGIN"].NullString());// SAVE MONEY
                        }
                        else
                        {
                            sheet.Cells["G4"].SetValueFromText(data[1].Rows[0]["LOAN_AMT_BEGIN_KRW"].NullString());// loan KRW
                            sheet.Cells["F5"].SetValueFromText(data[1].Rows[0]["CASH_BEGIN_USD_VND"].NullString());// cash usd->VND
                            sheet.Cells["G5"].SetValueFromText(data[1].Rows[0]["CASH_BEGIN_VND_KRW"].NullString());// cash VND->KRW
                            sheet.Cells["G6"].SetValueFromText(data[1].Rows[0]["CASH_BEGIN_KRW"].NullString());// cash VND
                            sheet.Cells["G7"].SetValueFromText(data[1].Rows[0]["SAVE_MONEY_BEGIN"].NullString());// SAVE MONEY
                        }

                        if (!Header_Month_Dic.ContainsKey(months[0]))
                        {
                            Header_Month_Dic.Add(months[0], "F-G");
                        }

                        if (months.Count > 1)
                        {
                            sheet.Cells["H2"].SetValueFromText(months.OrderBy(x => x).ToList()[1]);

                            if (!Header_Month_Dic.ContainsKey(months[1]))
                            {
                                Header_Month_Dic.Add(months[1], "H-I");
                            }
                        }
                    }

                    var column = sheet.Columns[0].Heading;
                    if (months.Count > 2)
                    {
                        int j = 0;
                        for (int i = 0; i < months.Count - 2; i++)
                        {
                            CellRange sourceCell = sheet.Range["H2:I44"];
                            sheet.Cells[sheet.Columns[9 + j].Heading + "2"].CopyFrom(sourceCell, PasteSpecial.All);
                            sheet.Cells[sheet.Columns[9 + j].Heading + "2"].SetValueFromText(months.OrderBy(x => x).ToList()[i + 2]);

                            if (!Header_Month_Dic.ContainsKey(months[i + 2]))
                            {
                                Header_Month_Dic.Add(months[i + 2], sheet.Columns[9 + j].Heading + "-" + sheet.Columns[10 + j].Heading);
                            }

                            j += 2;
                        }
                    }

                    int k = 0;
                    DataRow[] rows;
                    string heading = "";
                    string heading2 = "";
                    for (int i = 0; i < months.Count; i++)
                    {
                        heading = sheet.Columns[5 + k].Heading;
                        heading2 = sheet.Columns[6 + k].Heading;

                        // month 1st
                        rows = data[0].Select("MONTH_DAY = '" + DateTime.Parse(sheet.Cells[heading + "2"].Value.NullString()).ToString("MM/dd/yyyy") + "'");

                        foreach (DataRow row in rows)
                        {
                            // 영업수금($)WTC 
                            if (row["REMARK1"].NullString() == sheet.Cells["B9"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "9"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "9"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "9"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "9"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "9"].Formula = "=" + (heading + "9") + "*" + VND_KRW;
                                //}
                            }

                            // 영업수금($)HQ
                            if (row["REMARK1"].NullString() == sheet.Cells["B10"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "10"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "10"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "10"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "10"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "10"].Formula = "=" + (heading + "10") + "*" + VND_KRW;
                                //}
                            }

                            // 영업수금($)기타
                            if (row["REMARK1"].NullString() == sheet.Cells["B11"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "11"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "11"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "11"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "11"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "11"].Formula = "=" + (heading + "11") + "*" + VND_KRW;
                                //}
                            }

                            //영업수금(VND)
                            if (row["REMARK1"].NullString() == sheet.Cells["B12"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "12"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "12"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "12"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "12"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "12"].Formula = "=" + (heading + "12") + "*" + VND_KRW;
                                //}
                            }

                            // 차입실행
                            if (row["REMARK1"].NullString() == sheet.Cells["B13"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "13"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "13"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "13"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "13"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "13"].Formula = "=" + (heading + "13") + "*" + VND_KRW;
                                //}
                            }

                            // 예금만기
                            if (row["REMARK1"].NullString() == sheet.Cells["B14"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "14"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "14"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "14"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "14"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "14"].Formula = "=" + (heading + "14") + "*" + VND_KRW;
                                //}
                            }

                            // 스크랩매각대
                            if (row["REMARK1"].NullString() == sheet.Cells["B15"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "15"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "15"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "15"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "15"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "15"].Formula = "=" + (heading + "15") + "*" + VND_KRW;
                                //}
                            }

                            // 클레임 보상금
                            if (row["REMARK1"].NullString() == sheet.Cells["B16"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "16"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "16"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "16"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "16"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "16"].Formula = "=" + (heading + "16") + "*" + VND_KRW;
                                //}
                            }

                            // 이자수익
                            if (row["REMARK1"].NullString() == sheet.Cells["B17"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "17"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "17"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "17"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "17"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "17"].Formula = "=" + (heading + "17") + "*" + VND_KRW;
                                //}
                            }

                            // Internal in
                            if (row["REMARK1"].NullString() == sheet.Cells["B18"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "18"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "18"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "18"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "18"].Formula = "=" + (heading + "18") + "*" + VND_KRW;
                                //}
                            }

                            // 기타입금 
                            if (row["REMARK1"].NullString() == sheet.Cells["B19"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "19"].SetValueFromText(row["DEBIT_AMT"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "19"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "19"].SetValueFromText(row["DEBIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "19"].Formula = "=" + (heading + "19") + "*" + VND_KRW;
                                //}
                            }

                            // 매입대금(WTC)
                            if (row["REMARK1"].NullString() == sheet.Cells["C21"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "21"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "21"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "21"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "21"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "21"].Formula = "=" + (heading + "21") + "*" + VND_KRW;
                                //}
                            }

                            // 매입대금(HQ)
                            if (row["REMARK1"].NullString() == sheet.Cells["C22"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "22"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "22"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "22"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "22"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "22"].Formula = "=" + (heading + "22") + "*" + VND_KRW;
                                //}
                            }

                            // 매입대금(기타)
                            if (row["REMARK1"].NullString() == sheet.Cells["C23"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "23"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "23"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "23"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "23"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "23"].Formula = "=" + (heading + "23") + "*" + VND_KRW;
                                //}
                            }

                            // 매입대금(VND)
                            if (row["REMARK1"].NullString() == sheet.Cells["C24"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "24"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "24"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "24"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "24"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "24"].Formula = "=" + (heading + "24") + "*" + VND_KRW;
                                //}
                            }

                            // 인건비
                            if (row["REMARK1"].NullString() == sheet.Cells["C25"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "25"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "25"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    if (row["CURRENCY"].NullString() == "USD")
                                //    {
                                //        sheet.Cells[heading + "25"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //        sheet.Cells[heading2 + "25"].Formula = "=" + (heading + "25") + "*" + VND_KRW;
                                //    }
                                //}
                            }

                            // 인건비
                            if (row["REMARK1"].NullString() == sheet.Cells["C26"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "VND")
                                    {
                                        sheet.Cells[heading2 + "26"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    if (row["CURRENCY"].NullString() == "VND")
                                //    {
                                //        sheet.Cells[heading + "26"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //        sheet.Cells[heading2 + "26"].Formula = "=" + (heading + "26") + "*" + VND_KRW;
                                //    }
                                //}
                            }

                            // 설비투자 (other)
                            if (row["REMARK1"].NullString() == sheet.Cells["C27"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "27"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "27"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    if (row["CURRENCY"].NullString() == "USD")
                                //    {
                                //        sheet.Cells[heading + "27"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //        sheet.Cells[heading2 + "27"].Formula = "=" + (heading + "27") + "*" + VND_KRW;
                                //    }
                                //}
                            }

                            // 설비투자 (other)
                            if (row["REMARK1"].NullString() == sheet.Cells["C28"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "VND")
                                    {
                                        sheet.Cells[heading2 + "28"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    if (row["CURRENCY"].NullString() == "VND")
                                //    {
                                //        sheet.Cells[heading + "28"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //        sheet.Cells[heading2 + "28"].Formula = "=" + (heading + "28") + "*" + VND_KRW;
                                //    }
                                //}
                            }

                            // 투자(HQ)
                            if (row["REMARK1"].NullString() == sheet.Cells["C29"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "29"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "29"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "29"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "29"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "29"].Formula = "=" + (heading + "29") + "*" + VND_KRW;
                                //}
                            }

                            // 법인카드
                            if (row["REMARK1"].NullString() == sheet.Cells["C30"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "30"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "30"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "30"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "30"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "30"].Formula = "=" + (heading + "30") + "*" + VND_KRW;
                                //}
                            }

                            // 세금
                            if (row["REMARK1"].NullString() == sheet.Cells["C31"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "31"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "31"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "31"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "31"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "31"].Formula = "=" + (heading + "31") + "*" + VND_KRW;
                                //}
                            }

                            //이자비용
                            if (row["REMARK1"].NullString() == sheet.Cells["C32"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "32"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "32"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "32"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "32"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "32"].Formula = "=" + (heading + "32") + "*" + VND_KRW;
                                //}
                            }

                            // Internal out
                            if (row["REMARK1"].NullString() == sheet.Cells["C33"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "33"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "33"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "33"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "33"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "33"].Formula = "=" + (heading + "33") + "*" + VND_KRW;
                                //}
                            }

                            // 기타
                            if (row["REMARK1"].NullString() == sheet.Cells["C34"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "34"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "34"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    if (row["CURRENCY"].NullString() == "USD")
                                //    {
                                //        sheet.Cells[heading + "34"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //        sheet.Cells[heading2 + "34"].Formula = "=" + (heading + "34") + "*" + VND_KRW;
                                //    }
                                //}
                            }

                            // 기타
                            if (row["REMARK1"].NullString() == sheet.Cells["C35"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "VND")
                                    {
                                        sheet.Cells[heading2 + "35"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    if (row["CURRENCY"].NullString() == "VND")
                                //    {
                                //        sheet.Cells[heading + "35"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //        sheet.Cells[heading2 + "35"].Formula = "=" + (heading + "35") + "*" + VND_KRW;
                                //    }
                                //}
                            }

                            // 차입금상환
                            if (row["REMARK1"].NullString() == sheet.Cells["C38"].Value.NullString())
                            {
                                if (sheet.Name == SHEET_USD)
                                {
                                    if (row["CURRENCY"].NullString() == "USD")
                                    {
                                        sheet.Cells[heading + "38"].SetValueFromText(row["CREDIT_AMT"].NullString());
                                        sheet.Cells[heading2 + "38"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                    else
                                    {
                                        sheet.Cells[heading2 + "38"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                    }
                                }
                                //else if (sheet.Name == SHEET_KRW)
                                //{
                                //    sheet.Cells[heading + "38"].SetValueFromText(row["CREDIT_AMT_LOCAL"].NullString());
                                //    sheet.Cells[heading2 + "38"].Formula = "=" + (heading + "35") + "*" + VND_KRW;
                                //}
                            }

                        }

                        k += 2;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private async void GetDataForReportDaily(Worksheet sheet)
        {
            try
            {
                string createDate = dateLoad.EditValue.NullString();

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_FINACING_STATEMENT.GET_DATE_REPORT",
                  new string[] { "A_DATE" },
                  new string[] { createDate });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection data = base.m_ResultDB.ReturnDataSet.Tables;

                    sheet.Cells["A3"].SetValueFromText(DateTime.Parse(createDate).Year + "년  " + DateTime.Parse(createDate).Month + "월  " + DateTime.Parse(createDate).Day + "일");

                    int startOnHand = 10;
                    int endOnHand = startOnHand + 2;

                    // tien mat
                    if (data[0].Rows.Count > 0)
                    {
                        if (data[0].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(startOnHand + 1, data[0].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[0].Rows.Count == 1)
                        {
                            sheet.Rows[startOnHand + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[0], startOnHand, 0);
                        endOnHand = startOnHand + data[0].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[startOnHand + 1].Delete();
                        endOnHand = startOnHand + 1;
                    }

                    // tien gui ngan hang vnd
                    int startCashVND = endOnHand + 4;
                    int endCashVND = startCashVND + 2;

                    if (data[1].Rows.Count > 0)
                    {
                        if (data[1].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(startCashVND + 1, data[1].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[1].Rows.Count == 1)
                        {
                            sheet.Rows[startCashVND + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[1], startCashVND, 0);
                        endCashVND = startCashVND + data[1].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[startCashVND + 1].Delete();
                        endCashVND = startCashVND + 1;
                    }

                    sheet.Cells["H" + (endCashVND + 2)].SetValueFromText(await GetExchangeRate(createDate, "USD", "VND"));

                    // tien gui ngan hang usd
                    int startCashUsd = endCashVND + 4;
                    int endCashUsd = startCashUsd + 2;

                    if (data[2].Rows.Count > 0)
                    {
                        if (data[2].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(startCashUsd + 1, data[2].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[2].Rows.Count == 1)
                        {
                            sheet.Rows[startCashUsd + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[2], startCashUsd, 0);

                        endCashUsd = startCashUsd + data[2].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[startCashUsd + 1].Delete();
                        endCashUsd = startCashUsd + 1;
                    }

                    // Bao Lanh Dien
                    int startElectric = endCashUsd + 4;
                    int endElectric = startElectric + 2;

                    if (data[3].Rows.Count > 0)
                    {
                        if (data[3].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(startElectric + 1, data[3].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[3].Rows.Count == 1)
                        {
                            sheet.Rows[startElectric + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[3], startElectric, 0);

                        endElectric = startElectric + data[3].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[startElectric + 1].Delete();
                        endElectric = startElectric + 1;
                    }

                    sheet.Cells["H" + (endElectric + 2)].SetValueFromText(await GetExchangeRate(createDate, "USD", "VND"));

                    // Bao cao vay
                    int startLoan = endElectric + 4;
                    int endLoan = startLoan + 2;

                    if (data[4].Rows.Count > 0)
                    {
                        if (data[4].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(startLoan + 1, data[4].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[4].Rows.Count == 1)
                        {
                            sheet.Rows[startLoan + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[4], startLoan, 0);
                        endLoan = startLoan + data[4].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[startLoan + 1].Delete();
                        endLoan = startLoan + 1;
                    }

                    // Chi tiet receive
                    int starDetail = endLoan + 4;
                    int endDetailReceive = starDetail + 2;

                    if (data[5].Rows.Count > 0)
                    {
                        if (data[5].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(starDetail + 1, data[5].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[5].Rows.Count == 1)
                        {
                            sheet.Rows[starDetail + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[5], starDetail, 0);
                        endDetailReceive = starDetail + data[5].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[starDetail + 1].Delete();
                        endDetailReceive = starDetail + 1;
                    }

                    // chi tiet payment
                    int starDetailPay = endDetailReceive + 2;
                    int endDetailPay = starDetailPay + 2;

                    if (data[6].Rows.Count > 0)
                    {
                        if (data[6].Rows.Count > 2)
                        {
                            sheet.Rows.Insert(starDetailPay + 1, data[6].Rows.Count - 2, RowFormatMode.FormatAsNext);
                        }
                        else if (data[6].Rows.Count == 1)
                        {
                            sheet.Rows[starDetailPay + 1].Delete();
                        }

                        sheet.DataBindings.BindToDataSource(data[6], starDetailPay, 0);
                        endDetailPay = starDetailPay + data[6].Rows.Count;
                    }
                    else
                    {
                        sheet.Rows[starDetailPay + 1].Delete();
                        endDetailPay = starDetailPay + 1;
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

        private async Task<string> GetExchangeRate(string date, string from, string to)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_EXCHANGE.GET", new string[] { "A_DATE", "A_FROM", "A_TO" }, new string[] { date, from, to });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];

                    if (data.Rows.Count > 0)
                    {
                        if (data.Rows[0]["USER_UPDATE"].NullString() == "WHC_FinaceService")
                        {
                            return await GetLatestExchange(DateTime.Parse(date).ToString("yyyyMMdd"));
                        }
                        else
                        {
                            return data.Rows[0]["RATE"].NullString();
                        }
                    }
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            return "";
        }

        private async Task<string> GetLatestExchange(string date)
        {
            try
            {
                var exchange = await ExchangeRateDownload.DownloadAsync(date);

                if (exchange != null && exchange.Contains("-"))
                {
                    return exchange.Split('-')[0].Split(' ')[0];
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            return "";
        }

        private void cheDaily_CheckedChanged(object sender, EventArgs e)
        {
            dateTo.Enabled = !cheDaily.Checked;

            if (cheDaily.Checked && DateTime.Now >= DateTime.Parse("2022-02-01"))
            {
                dateTo.EditValue = DateTime.Now.Year.ToString() + "-01-01";
                dateTo.Enabled = false;
            }
            else
            {
                dateTo.EditValue = null;
            }
        }

        private void btnCloseData_Click(object sender, EventArgs e)
        {
            try
            {
                if (cheDaily.Checked)
                {
                    return;
                }

                if (dateCloseMonth.EditValue.NullString() == "")
                {
                    MsgBox.Show("NHẬP THÁNG MUỐN CLOSE DATA!", MsgType.Warning);
                    return;
                }

                DialogResult dialogResult = MsgBox.Show("XÁC NHẬN CLOSE DATA CHO THÁNG :" + dateCloseMonth.EditValue.NullString(), MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    DateTime selectDate = DateTime.Parse(dateCloseMonth.EditValue.NullString());
                    //string month = selectDate.ToString("MM/dd/yyyy");
                    string month = (new DateTime(selectDate.Year, selectDate.Month, 1)).AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy");
                    string valueHeader = "";
                    if (Header_Month_Dic.ContainsKey(month))
                    {
                        valueHeader = Header_Month_Dic[month];
                    }

                    IWorkbook workbook = spreadsheetMain.Document;
                    workbook.BeginUpdate();

                    Worksheet sheet1 = workbook.Worksheets[SHEET_USD];
                    Worksheet sheet2 = workbook.Worksheets[SHEET_KRW];

                    string header1 = valueHeader.Split('-')[0];
                    string header2 = valueHeader.Split('-')[1];

                    string ZERO = "0";
                    string loan_usd = sheet1.Cells[header1 + "40"].Value.NullString().Replace(",", "");
                    string loan_vnd = sheet1.Cells[header2 + "40"].Value.NullString().Replace(",", "");
                    string loan_krw = sheet2.Cells[header2 + "40"].Value.NullString().Replace(",", "");

                    string cash_usd = sheet1.Cells[header1 + "41"].Value.NullString().Replace(",", "");
                    string cash_usd_vnd = sheet1.Cells[header2 + "41"].Value.NullString().Replace(",", "");
                    string cash_vnd = sheet1.Cells[header2 + "42"].Value.NullString().Replace(",", "");
                    string cash_vnd_krw = sheet2.Cells[header2 + "41"].Value.NullString().Replace(",", "");
                    string cash_krw = sheet2.Cells[header2 + "42"].Value.NullString().Replace(",", "");

                    string save_money = "0";

                    if (!double.TryParse(loan_usd, out _))
                    {
                        loan_usd = ZERO;
                    }

                    if (!double.TryParse(loan_vnd, out _))
                    {
                        loan_vnd = ZERO;
                    }

                    if (!double.TryParse(loan_krw, out _))
                    {
                        loan_krw = ZERO;
                    }

                    if (!double.TryParse(cash_usd, out _))
                    {
                        cash_usd = ZERO;
                    }

                    if (!double.TryParse(cash_usd_vnd, out _))
                    {
                        cash_usd_vnd = ZERO;
                    }

                    if (!double.TryParse(cash_vnd, out _))
                    {
                        cash_vnd = ZERO;
                    }

                    if (!double.TryParse(cash_vnd_krw, out _))
                    {
                        cash_vnd_krw = ZERO;
                    }

                    if (!double.TryParse(cash_krw, out _))
                    {
                        cash_krw = ZERO;
                    }

                    if (!double.TryParse(save_money, out _))
                    {
                        save_money = ZERO;
                    }

                    workbook.EndUpdate();

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_CLOSE_DATA.PUT",
                        new string[] { "A_LOAN_AMT_USD", "A_LOAN_ATM_VND", "A_LOAN_ATM_KRW", "A_CASH_USD", "A_CASH_USD_VND", "A_CASH_VND", "A_CASH_VND_KRW", "A_CASH_KRW", "A_SAVE_MONEY", "A_DATE", "A_USER" },
                        new string[]
                        {
                                loan_usd,
                                loan_vnd,
                                loan_krw,
                                cash_usd,
                                cash_usd_vnd,
                                cash_vnd,
                                cash_vnd_krw,
                                cash_krw,
                                save_money,
                                DateTime.Parse(dateCloseMonth.EditValue.NullString()).AddMonths(1).ToString("yyyy-MM-dd"),
                                Consts.USER_INFO.Id
                        });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
