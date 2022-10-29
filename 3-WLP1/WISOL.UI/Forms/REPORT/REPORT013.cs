using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraCharts;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Spreadsheet;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT013 : PageType
    {
        DataTable table1 = new DataTable("Table1");
        DataTable table2 = new DataTable("Table2");
        DataTable table3 = new DataTable("Table3");
        public REPORT013()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
        }

        public override void InitializePage()
        {
            txtNumberOfDays.Properties.MinValue = 5;
            txtNumberOfDays.Properties.MaxValue = 30;
            txtNumberOfDays.Properties.Mask.EditMask = "\\d+";
            txtNumberOfDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //txtNumberOfDays.EditValue = 14;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT013.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    txtNumberOfDays.EditValue = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            txtTarget.Text = "0.12";
            txtTarget.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTarget.Properties.Mask.EditMask = "n4";
            //ComboBoxItemCollection coll = cbLine.Properties.Items;
            //coll.BeginUpdate();
            //try
            //{
            //    coll.Add("LineC");
            //    coll.Add("LineD");
            //    coll.Add("LineE");
            //    coll.Add("LineF");
            //    coll.Add("LineG");
            //    coll.Add("LineH");
            //    coll.Add("LineI");
            //}
            //finally
            //{
            //    coll.EndUpdate();
            //}

            //cbLine.SelectedIndex = -1;

            //IWorkbook workbook;
            //Worksheet worksheet;
            ////spreadsheetControl1.WorksheetDisplayArea.SetSize(0, 44, 11);
            //spreadsheetControl1.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            //spreadsheetControl1.Options.Behavior.ShowPopupMenu = DevExpress.XtraSpreadsheet.DocumentCapability.Hidden;
            //workbook = spreadsheetControl1.Document;
            //worksheet = workbook.Worksheets[0];
            //worksheet.ActiveView.ShowGridlines = false;

            //this.layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }

        public override void SearchPage()
        {
            base.SearchPage();

            double target = Double.Parse(txtTarget.EditValue.ToString());
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT013.TREND_HEAD"
                    , new string[] { "A_DAYS" }
                    , new string[] { txtNumberOfDays.EditValue.ToString() }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    table1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                    table2 = base.m_ResultDB.ReturnDataSet.Tables[1];
                    table3 = base.m_ResultDB.ReturnDataSet.Tables[2];

                    table1.Columns.Add("Maintenance_Date", typeof(System.String));
                    table1.Columns.Add("Pickup_Count", typeof(System.Int32));
                    table1.Columns.Add("Next_Maintenance_Date", typeof(System.String));
                    table1.Columns.Add("Pickup_Count_Target", typeof(System.Int32));

                    table1 = SetColumnsOrder(table1, table2);

                    table1.Columns.Add("Ave", typeof(System.Double));
                    table1.Columns.Add("Next Day", typeof(System.Double));
                    table1.Columns.Add("Day over target", typeof(System.Double));

                    foreach(DataRow row in table1.Rows)
                    {
                        int c = 0;
                        float v = 0;
                        int g = 0;
                        List<PointF> list = new List<PointF>();
                        for (int i = 3; i < row.ItemArray.Length - 3; i++)
                        {
                            if(row[i] != DBNull.Value)
                            {
                                c += 1;
                                v += (float)Convert.ToDouble(row[i].ToString());
                                PointF f = new PointF(c, (float)Convert.ToDouble(row[i].ToString()));
                                list.Add(f);
                                if (Convert.ToDouble(row[i].ToString()) > target)
                                {
                                    g += 1;
                                }
                            }
                        }
                        float slope = SlopeOfPoints(list);
                        float yIntercept = YInterceptOfPoints(list, slope);
                        row["Ave"] = Math.Round(v / c, 4).ToString();
                        row["Next Day"] = Math.Round((slope * (c + 1)) + yIntercept, 4).ToString();
                        if (c == 1)
                        {
                            row["Next Day"] = Math.Round(v / c, 4).ToString();
                        }
                        //row["Trend"] = (Math.Round((slope * (c + 1)) + yIntercept, 4) - Math.Round(v / c, 4)).ToString();
                        row["Day over target"] = g.ToString();
                    }

                    for (int i = table1.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = table1.Rows[i];
                        if (Convert.ToDouble(dr["Next Day"].ToString()) < 0)
                            dr.Delete();
                    }
                    table1.AcceptChanges();

                    base.m_BindData.BindGridView(gcList,
                        //base.m_ResultDB.ReturnDataSet.Tables[0]
                        table1
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            for (int i = 0; i < gvList.RowCount; i++)
            {
                for (int j = 0; j < table3.Rows.Count; j++)
                {
                    if (gvList.GetDataRow(i)["Line"].ToString() == table3.Rows[j]["Line"].ToString()
                    && gvList.GetDataRow(i)["Machine_Name"].ToString() == table3.Rows[j]["Machine_Name"].ToString()
                    && gvList.GetDataRow(i)["Head"].ToString() == table3.Rows[j]["Head"].ToString()
                    )
                    {
                        gvList.SetRowCellValue(i, "Maintenance_Date", table3.Rows[j]["Maintenance_Date"].ToString());
                        gvList.SetRowCellValue(i, "Pickup_Count", table3.Rows[j]["Pickup_Count"].ToString());
                        gvList.SetRowCellValue(i, "Next_Maintenance_Date", table3.Rows[j]["Next_Maintenance_Date"].ToString());
                        gvList.SetRowCellValue(i, "Pickup_Count_Target", table3.Rows[j]["Pickup_Count_Target"].ToString());
                    }
                }
            }

            gvList.OptionsView.ShowFooter = false;
            gvList.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvList.Columns[0].AppearanceHeader.Options.UseTextOptions = true;
            gvList.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvList.Columns[1].AppearanceHeader.Options.UseTextOptions = true;
            gvList.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvList.Columns[2].AppearanceHeader.Options.UseTextOptions = true;
            //gvList.Columns["Trend"].Width = 120;
            gvList.Columns["Day over target"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Day over target"].DisplayFormat.FormatString = "n0";
            gvList.Columns["Pickup_Count"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Pickup_Count"].DisplayFormat.FormatString = "n0";
            gvList.Columns["Pickup_Count_Target"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Pickup_Count_Target"].DisplayFormat.FormatString = "n0";
            int count = gvList.Columns.Count;
            for (int i = 7; i < count - 1; i++)
            {
                gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[i].DisplayFormat.FormatString = "n4";
            }


            gvList.BeginSort();
            try
            {
                gvList.ClearSorting();
                gvList.Columns["Day over target"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
            finally
            {
                gvList.EndSort();
            }
            gvList.Columns[3].Width = 140;
            gvList.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[4].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[5].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[6].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns["Next_Maintenance_Date"].Visible = false;
            gvList.Columns["Pickup_Count_Target"].Visible = false;
            //IWorkbook workbook;
            //Worksheet worksheet;

            //workbook = spreadsheetControl1.Document;
            //worksheet = workbook.Worksheets[0];

            //worksheet.ClearFormats(worksheet.Range["A1:Z300"]);
            //worksheet.ClearContents(worksheet.Range["A1:Z300"]);

            ////worksheet.MergeCells(worksheet.Range["A1:A2"]);
            ////worksheet.MergeCells(worksheet.Range["B1:B2"]);

            //worksheet.Cells["A1"].Value = "Line";
            //worksheet.Cells["A2"].Value = "Line";
            //worksheet.Cells["B1"].Value = "Machine";
            //worksheet.Cells["B2"].Value = "Machine";
            //worksheet.Cells["C1"].Value = "Day";
            //worksheet.Cells["C2"].Value = "Head";

            //worksheet.Cells["D2"].Value = "Loss Rate";
            //worksheet.Cells["E2"].Value = "Loss Rate";
            //worksheet.Cells["F2"].Value = "Loss Rate";
            //worksheet.Cells["G2"].Value = "Loss Rate";
            //worksheet.Cells["H2"].Value = "Loss Rate";
            //worksheet.Cells["I2"].Value = "Loss Rate";
            //worksheet.Cells["J2"].Value = "Loss Rate";
            //worksheet.Cells["K2"].Value = "Loss Rate";
            //worksheet.Cells["L2"].Value = "Loss Rate";
            //worksheet.Cells["M2"].Value = "Loss Rate";
            //worksheet.Cells["N2"].Value = "Loss Rate";
            //worksheet.Cells["O2"].Value = "Loss Rate";
            //worksheet.Cells["P2"].Value = "Ave";
            //worksheet.Cells["Q2"].Value = "Next day";
            //worksheet.Cells["R1"].Value = "Trend";
            //worksheet.Cells["R2"].Value = "Trend";

            //////worksheet.Range["B3:B10"].Value = txtTarget.EditValue.ToString();

            //CellRange range = worksheet.Range["A1:R2"];
            //Formatting rangeFormatting = range.BeginUpdateFormatting();
            //rangeFormatting.Font.Color = Color.Black;
            //rangeFormatting.Font.Bold = true;
            //rangeFormatting.Fill.BackgroundColor = Color.FromArgb(204, 230, 255);
            //rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //range.EndUpdateFormatting(rangeFormatting);

            //worksheet.Columns["A"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //worksheet.Columns["A"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //worksheet.Columns["B"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //worksheet.Columns["B"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //worksheet.Columns["B"].Width = 400;
            //worksheet.Columns["C"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //worksheet.Columns["C"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //worksheet.Columns["D"].Width = 300;
            //worksheet.Columns["E"].Width = 300;
            //worksheet.Columns["F"].Width = 300;
            //worksheet.Columns["G"].Width = 300;
            //worksheet.Columns["H"].Width = 300;
            //worksheet.Columns["I"].Width = 300;
            //worksheet.Columns["J"].Width = 300;
            //worksheet.Columns["K"].Width = 300;
            //worksheet.Columns["L"].Width = 300;
            //worksheet.Columns["M"].Width = 300;
            //worksheet.Columns["N"].Width = 300;
            //worksheet.Columns["O"].Width = 300;
            //worksheet.Columns["P"].Width = 300;
            //worksheet.Columns["Q"].Width = 300;
            //worksheet.Columns["R"].Width = 300;
            //spreadsheetControl1.Options.Behavior.Group.Group = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled;
            //range = worksheet["P1:Q1"];
            //range.UnGroupColumns(true);
            //range.GroupColumns(true);

            //int count = table1.Rows.Count;

            //range = worksheet.Range["A1:R" + (count + 2)];
            //range.SetInsideBorders(Color.Black, BorderLineStyle.Thin);
            //range.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);

            //spreadsheetControl1.WorksheetDisplayArea.SetSize(0, 18, count + 2);
            //CellRange cellRange = worksheet.GetUsedRange();

            //int s = 0;
            //for (int i = 3; i < 15; i++)
            //{
            //    int j = 15 - i + 2;

            //    if (s == 1) j = j - 1;
            //    if (s == 2) j = j - 2;

            //    DateTime dt = DateTime.Now.AddDays(-j);

            //    if (dt.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        cellRange[0, i].SetValue(dt.AddDays(1).ToString("yyyy-MM-dd"));
            //        s += 1;
            //    }
            //    else if (dt.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        cellRange[0, i].SetValue(dt.ToString("yyyy-MM-dd"));
            //    }
            //    else
            //    {
            //        cellRange[0, i].SetValue(dt.ToString("yyyy-MM-dd"));
            //    }
            //}


            //for (int i = 2; i < cellRange.RowCount; i++)
            //{
            //    string Line = string.Empty;
            //    string Machine = string.Empty;
            //    string Head = string.Empty;
            //    cellRange[i, 0].SetValueFromText(table1.Rows[i - 2][0].ToString());
            //    cellRange[i, 1].SetValueFromText(table1.Rows[i - 2][1].ToString());
            //    cellRange[i, 2].SetValueFromText(table1.Rows[i - 2][2].ToString());
            //    Line = table1.Rows[i - 2][0].ToString();
            //    Machine = table1.Rows[i - 2][1].ToString();
            //    Head = table1.Rows[i - 2][2].ToString();
            //    int c = 0;
            //    float v = 0;
            //    List<PointF> list = new List<PointF>();
            //    for (int j = 3; j < 15; j++)
            //    {
            //        for (int z = 0; z < table2.Rows.Count; z++)
            //        {
            //            if (Line == table2.Rows[z][1].ToString() &&
            //               Machine == table2.Rows[z][2].ToString() &&
            //               Head == table2.Rows[z][3].ToString() &&
            //               cellRange[0, j].Value.ToString() == table2.Rows[z][0].ToString() 
            //               )
            //            {
            //                cellRange[i, j].SetValueFromText(table2.Rows[z][6].ToString());
            //                c += 1;
            //                v += (float)Convert.ToDouble(table2.Rows[z][6].ToString());
            //                PointF f = new PointF(c, (float)Convert.ToDouble(table2.Rows[z][6].ToString()));
            //                list.Add(f);
            //            }
            //        }
            //    }

            //    float slope = SlopeOfPoints(list);
            //    float yIntercept = YInterceptOfPoints(list, slope);

            //    cellRange[i, 15].SetValueFromText(Math.Round(v / c, 4).ToString());
            //    cellRange[i, 16].SetValueFromText(Math.Round((slope * (c + 1)) + yIntercept, 4).ToString());
            //    cellRange[i, 17].SetValueFromText((Math.Round((slope * (c + 1)) + yIntercept, 4) - Math.Round(v / c, 4)).ToString());
            //    if (Math.Round((slope * (c + 1)) + yIntercept, 4) > Math.Round(v / c, 4))
            //    {
            //        cellRange[i, 17].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
            //        cellRange[i, 17].Font.Color = Color.Red;// Color.FromArgb(156, 0, 6);
            //    }
            //    else
            //    {
            //        cellRange[i, 17].Fill.BackgroundColor = Color.FromArgb(255, 0x9F, 0xFB, 0x69);
            //        cellRange[i, 17].Font.Color = Color.BlueViolet;
            //    }
            //    //for (int j = 0; j < table2.Rows.Count; j++)
            //    //{
            //    //    if (Line == table2.Rows[j][1].ToString() && Machine == table2.Rows[j][2].ToString())
            //    //    {
            //    //        for (int z = 2; z < 14; z++)
            //    //        {
            //    //            if (table2.Rows[j][0].ToString() == cellRange[0, z].ToString())
            //    //            {
            //    //                cellRange[i, z].SetValueFromText(table2.Rows[j][5].ToString());
            //    //            }
            //    //        }
            //    //    }
            //    //}
            //}
            //Table table = worksheet.Tables.Add(worksheet["A2:Z300"], true);
        }

        private static DataTable SetColumnsOrder(DataTable table, DataTable columnNames)
        {
            table.Columns["Maintenance_Date"].SetOrdinal(3);
            table.Columns["Pickup_Count"].SetOrdinal(5);
            table.Columns["Next_Maintenance_Date"].SetOrdinal(4);
            table.Columns["Pickup_Count_Target"].SetOrdinal(6);
            int columnIndex = 7;

            for (int i = 0; i < columnNames.Rows.Count; i++)
            {
                table.Columns[columnNames.Rows[i][0].ToString()].SetOrdinal(columnIndex);
                columnIndex++;
            }
            return table;
        }
        private static float SlopeOfPoints(List<PointF> points)
        {
            float xBar = points.Average(p => p.X);
            float yBar = points.Average(p => p.Y);

            float dividend = points.Sum(p => (p.X - xBar) * (p.Y - yBar));
            float divisor = (float)points.Sum(p => Math.Pow(p.X - xBar, 2));

            return dividend / divisor;
        }

        private static float YInterceptOfPoints(List<PointF> points, float slope)
        {
            float xBar = points.Average(p => p.X);
            float yBar = points.Average(p => p.Y);

            return yBar - (slope * xBar);
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "Line" && e.Column.FieldName != "Machine_Name" && e.Column.FieldName != "Head" 
                && e.Column.FieldName != "Day over target" && e.Column.FieldName != "Maintenance_Date" && e.Column.FieldName != "Pickup_Count"
                && e.Column.FieldName != "Next_Maintenance_Date" && e.Column.FieldName != "Pickup_Count_Target"
                )
            {
                double target = Double.Parse(txtTarget.EditValue.ToString());
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (Convert.ToDouble(cellValue) > target)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 187, 153);
                    }
                }
            }

            if (e.Column.FieldName == "Maintenance_Date")
            {
                if (!string.IsNullOrWhiteSpace(gvList.GetRowCellDisplayText(e.RowHandle, "Next_Maintenance_Date")))
                {
                    DateTime dt;
                    DateTime.TryParse(gvList.GetRowCellDisplayText(e.RowHandle, "Next_Maintenance_Date"), out dt);
                    double _count = (dt - DateTime.Today).TotalDays;
                    if (_count < 7 && _count > 0)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    if(_count <= 0)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 102, 102);
                    }
                }
            }

            if(e.Column.FieldName == "Pickup_Count")
            {
                if (!string.IsNullOrWhiteSpace(gvList.GetRowCellDisplayText(e.RowHandle, "Pickup_Count_Target")))
                {
                    if (!string.IsNullOrWhiteSpace(gvList.GetRowCellDisplayText(e.RowHandle, "Pickup_Count")))
                    {
                        string pickup_count = gvList.GetRowCellDisplayText(e.RowHandle, "Pickup_Count");
                        string pickup_count_target = gvList.GetRowCellDisplayText(e.RowHandle, "Pickup_Count_Target");
                        pickup_count = pickup_count.Replace(",", "");
                        pickup_count_target = pickup_count_target.Replace(",", "");
                        int pc = Int32.Parse(pickup_count);
                        int pct = Int32.Parse(pickup_count_target);
                        double z = pc * 1.0 / pct;
                        if (z >= 0.9 && z < 1.0)
                        {
                            e.Appearance.BackColor = Color.Yellow;
                        }
                        if (z >= 1.0)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 102, 102);
                        }
                    }
                }
            }

            //if (e.Column.FieldName == "Trend")
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        if(Convert.ToDouble(cellValue) > 0)
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
            //            e.Appearance.ForeColor = Color.Red;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 0x9F, 0xFB, 0x69);
            //            e.Appearance.ForeColor = Color.BlueViolet;
            //        }
            //    }
            //}
        }

    }

    //class PointF
    //{
    //    public float X;
    //    public float Y;

    //    public PointF(float x, float y)
    //    {
    //        X = x;
    //        Y = y;
    //    }
    //}
}
