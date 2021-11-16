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

                    int startOnHand = 10;
                    int endOnHand = startOnHand + 2;

                    // tien mat
                    if (data[0].Rows.Count > 0)
                    {
                        if (data[0].Rows.Count > 2)
                        {
                            for (int i = 2; i < data[0].Rows.Count; i++)
                            {
                                sheet.Rows[i + startOnHand - 1].Insert(InsertCellsMode.EntireRow);
                            }
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
                            for (int i = 2; i < data[1].Rows.Count; i++)
                            {
                                sheet.Rows[startCashVND + i - 1].Insert(InsertCellsMode.EntireRow);
                            }
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

                    // tien gui ngan hang usd
                    int startCashUsd = endCashVND + 4;
                    int endCashUsd = startCashUsd + 2;

                    if (data[2].Rows.Count > 0)
                    {
                        if (data[2].Rows.Count > 2)
                        {
                            for (int i = 2; i < data[2].Rows.Count; i++)
                            {
                                sheet.Rows[startCashUsd + i - 1].Insert(InsertCellsMode.EntireRow);
                            }
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
                            for (int i = 2; i < data[3].Rows.Count; i++)
                            {
                                sheet.Rows[startElectric + i - 1].Insert(InsertCellsMode.EntireRow);
                            }
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

                    // Bao cao vay
                    int startLoan = endElectric + 4;
                    int endLoan = startLoan + 2;

                    if (data[4].Rows.Count > 0)
                    {
                        if (data[4].Rows.Count > 2)
                        {
                            for (int i = 2; i < data[4].Rows.Count; i++)
                            {
                                sheet.Rows[startLoan + i - 1].Insert(InsertCellsMode.EntireRow);
                            }
                        }
                        else if(data[4].Rows.Count == 1)
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
