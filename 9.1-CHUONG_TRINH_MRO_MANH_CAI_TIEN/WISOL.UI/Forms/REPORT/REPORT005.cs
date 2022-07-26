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
using System.Globalization;
using System.Drawing.Imaging;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT005 : PageType
    {
        public REPORT005()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();

            dtpFromMonth.Properties.Popup += From_Popup;
            dtpToMonth.Properties.Popup += To_Popup;
        }

        private void From_Popup(object sender, EventArgs e)
        {
            IPopupControl edit = sender as IPopupControl;
            PopupDateEditForm form = edit.PopupWindow as PopupDateEditForm;
            form.Calendar.MouseDown -= From_MouseDown;
            form.Calendar.MouseDown += From_MouseDown;
        }
        private void To_Popup(object sender, EventArgs e)
        {
            IPopupControl edit = sender as IPopupControl;
            PopupDateEditForm form = edit.PopupWindow as PopupDateEditForm;
            form.Calendar.MouseDown -= To_MouseDown;
            form.Calendar.MouseDown += To_MouseDown;
        }
        void From_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CalendarControl calendar = sender as CalendarControl;
            CalendarHitInfo hitInfo = calendar.GetHitInfo(e.Location);
            if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
            {
                CalendarCellViewInfo ho = hitInfo.HitObject as CalendarCellViewInfo;
                //int weekNumber = Convert.ToInt32(ho.Text);
                if (ho.Text.Length > 2)
                {
                    int monthInt = DateTime.ParseExact(ho.Text, "MMM", CultureInfo.CurrentCulture).Month;
                    txtFromMonth.EditValue = (monthInt < 10 ? "0" + monthInt : monthInt.ToString());
                }
                else if(ho.Text.Length < 2)
                {
                    txtFromMonth.EditValue = "0" + ho.Text;
                }
                else
                {
                    txtFromMonth.EditValue = ho.Text;
                }
                dtpFromMonth.ClosePopup();
            }
        }
        void To_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CalendarControl calendar = sender as CalendarControl;
            CalendarHitInfo hitInfo = calendar.GetHitInfo(e.Location);
            if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
            {
                CalendarCellViewInfo ho = hitInfo.HitObject as CalendarCellViewInfo;
                //int weekNumber = Convert.ToInt32(ho.Text);
                if (ho.Text.Length > 2)
                {
                    int monthInt = DateTime.ParseExact(ho.Text, "MMM", CultureInfo.CurrentCulture).Month;
                    txtToMonth.EditValue = (monthInt < 10 ? "0" + monthInt : monthInt.ToString());
                }
                else if (ho.Text.Length < 2)
                {
                    txtToMonth.EditValue = "0" + ho.Text;
                }
                else
                {
                    txtToMonth.EditValue = ho.Text;
                }
                dtpToMonth.ClosePopup();
            }
        }
        public override void InitializePage()
        {
            txtTarget.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTarget.Properties.Mask.EditMask = "n2";
            txtY1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtY1.Properties.Mask.EditMask = "n2";
            txtY2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtY2.Properties.Mask.EditMask = "d";
            ComboBoxItemCollection coll = cbYear.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("2020");
            }
            finally
            {
                coll.EndUpdate();
            }

            cbYear.SelectedIndex = 0;

            IWorkbook workbook;
            Worksheet worksheet;


            //spreadsheetControl1.WorksheetDisplayArea.SetSize(0, 100, 100);
            spreadsheetControl1.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;

            workbook = spreadsheetControl1.Document;
            worksheet = workbook.Worksheets[0];
            worksheet.ActiveView.ShowGridlines = false;
            worksheet.MergeCells(worksheet.Range["A1:A2"]);
            worksheet.MergeCells(worksheet.Range["C1:H1"]);
            worksheet.MergeCells(worksheet.Range["I1:N1"]);
            worksheet.MergeCells(worksheet.Range["O1:T1"]);
            worksheet.MergeCells(worksheet.Range["U1:Z1"]);
            worksheet.MergeCells(worksheet.Range["AA1:AF1"]);
            worksheet.MergeCells(worksheet.Range["AG1:AL1"]);
            //worksheet.MergeCells(worksheet.Range["AM1:AR1"]);
            worksheet.MergeCells(worksheet.Range["B1:B2"]);

            worksheet.Cells["A1"].Value = "Line";
            worksheet.Cells["A3"].Value = "Total";
            worksheet.Cells["A4"].Value = "C";
            worksheet.Cells["A5"].Value = "D";
            worksheet.Cells["A6"].Value = "E";
            worksheet.Cells["A7"].Value = "F";
            worksheet.Cells["A8"].Value = "G";
            worksheet.Cells["A9"].Value = "H";
            worksheet.Cells["A10"].Value = "I";
            worksheet.Cells["B1"].Value = "Target(%)";
            //worksheet.Range["B3:B10"].Value = txtTarget.EditValue.ToString();

            CellRange range = worksheet.Range["A1:AL2"];
            Formatting rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.Font.Color = Color.Black;
            rangeFormatting.Font.Bold = true;
            rangeFormatting.Fill.BackgroundColor = Color.FromArgb(255, 255, 128);
            rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range.EndUpdateFormatting(rangeFormatting);

            range = worksheet.Range["C3:AL10"];
            rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.Font.Color = Color.Blue;
            rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
            range.EndUpdateFormatting(rangeFormatting);

            //range = worksheet.Range["C1:AR2"];
            //rangeFormatting = range.BeginUpdateFormatting();
            //rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //range.EndUpdateFormatting(rangeFormatting);

            worksheet.Columns["A"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["A"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["B"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["B"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            
            worksheet.Cells["C2"].Value = "Input";
            worksheet.Cells["D2"].Value = "Loss";
            worksheet.Cells["E2"].Value = "Loss (%)";
            worksheet.Cells["F2"].Value = "Total Loss (VND)";
            worksheet.Cells["G2"].Value = "Finished product(ea)";
            worksheet.Cells["H2"].Value = "Loss per Unit";
            worksheet.Cells["H2"].RowHeight = 120;

            worksheet.Columns["C"].Width = 250;
            worksheet.Columns["F"].Width = 300;
            worksheet.Cells["F2"].Alignment.WrapText = true;
            worksheet.Columns["G"].Width = 320;
            worksheet.Cells["G2"].Alignment.WrapText = true;
            worksheet.Columns["H"].Width = 300;

            worksheet.Columns["I"].Width = 250;
            worksheet.Columns["L"].Width = 300;
            worksheet.Cells["L2"].Alignment.WrapText = true;
            worksheet.Columns["M"].Width = 320;
            worksheet.Cells["M2"].Alignment.WrapText = true;
            worksheet.Columns["N"].Width = 300;

            worksheet.Columns["O"].Width = 250;
            worksheet.Columns["R"].Width = 300;
            worksheet.Cells["R2"].Alignment.WrapText = true;
            worksheet.Columns["S"].Width = 320;
            worksheet.Cells["S2"].Alignment.WrapText = true;
            worksheet.Columns["T"].Width = 300;

            worksheet.Columns["U"].Width = 250;
            worksheet.Columns["X"].Width = 300;
            worksheet.Cells["X2"].Alignment.WrapText = true;
            worksheet.Columns["Y"].Width = 320;
            worksheet.Cells["Y2"].Alignment.WrapText = true;
            worksheet.Columns["Z"].Width = 300;

            worksheet.Columns["AA"].Width = 250;
            worksheet.Columns["AD"].Width = 300;
            worksheet.Cells["AD2"].Alignment.WrapText = true;
            worksheet.Columns["AE"].Width = 320;
            worksheet.Cells["AE2"].Alignment.WrapText = true;
            worksheet.Columns["AF"].Width = 300;

            worksheet.Columns["AG"].Width = 250;
            worksheet.Columns["AJ"].Width = 300;
            worksheet.Cells["AJ2"].Alignment.WrapText = true;
            worksheet.Columns["AK"].Width = 320;
            worksheet.Cells["AK2"].Alignment.WrapText = true;
            worksheet.Columns["AL"].Width = 300;

            worksheet.MergeCells(worksheet.Range["A11:AL11"]);
            worksheet.Cells["A11"].Value = "Loss per Unit = Total Loss(VND)/Finished product(ea)";
            worksheet.Cells["A11"].Font.Size = 9;
            worksheet.Cells["A11"].Font.Italic = true;
            worksheet.Cells["A11"].Font.Color = Color.Blue;
            worksheet.Cells["A11"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            worksheet.Cells["I2"].CopyFrom(worksheet.Cells["C2"]);
            worksheet.Cells["J2"].CopyFrom(worksheet.Cells["D2"]);
            worksheet.Cells["K2"].CopyFrom(worksheet.Cells["E2"]);
            worksheet.Cells["L2"].CopyFrom(worksheet.Cells["F2"]);
            worksheet.Cells["M2"].CopyFrom(worksheet.Cells["G2"]);
            worksheet.Cells["N2"].CopyFrom(worksheet.Cells["H2"]);
            worksheet.Cells["O2"].CopyFrom(worksheet.Cells["C2"]);
            worksheet.Cells["P2"].CopyFrom(worksheet.Cells["D2"]);
            worksheet.Cells["Q2"].CopyFrom(worksheet.Cells["E2"]);
            worksheet.Cells["R2"].CopyFrom(worksheet.Cells["F2"]);
            worksheet.Cells["S2"].CopyFrom(worksheet.Cells["G2"]);
            worksheet.Cells["T2"].CopyFrom(worksheet.Cells["H2"]);
            worksheet.Cells["U2"].CopyFrom(worksheet.Cells["C2"]);
            worksheet.Cells["V2"].CopyFrom(worksheet.Cells["D2"]);
            worksheet.Cells["W2"].CopyFrom(worksheet.Cells["E2"]);
            worksheet.Cells["X2"].CopyFrom(worksheet.Cells["F2"]);
            worksheet.Cells["Y2"].CopyFrom(worksheet.Cells["G2"]);
            worksheet.Cells["Z2"].CopyFrom(worksheet.Cells["H2"]);
            worksheet.Cells["AA2"].CopyFrom(worksheet.Cells["C2"]);
            worksheet.Cells["AB2"].CopyFrom(worksheet.Cells["D2"]);
            worksheet.Cells["AC2"].CopyFrom(worksheet.Cells["E2"]);
            worksheet.Cells["AD2"].CopyFrom(worksheet.Cells["F2"]);
            worksheet.Cells["AE2"].CopyFrom(worksheet.Cells["G2"]);
            worksheet.Cells["AF2"].CopyFrom(worksheet.Cells["H2"]);
            worksheet.Cells["AG2"].CopyFrom(worksheet.Cells["C2"]);
            worksheet.Cells["AH2"].CopyFrom(worksheet.Cells["D2"]);
            worksheet.Cells["AI2"].CopyFrom(worksheet.Cells["E2"]);
            worksheet.Cells["AJ2"].CopyFrom(worksheet.Cells["F2"]);
            worksheet.Cells["AK2"].CopyFrom(worksheet.Cells["G2"]);
            worksheet.Cells["AL2"].CopyFrom(worksheet.Cells["H2"]);
            //worksheet.Cells["AM2"].CopyFrom(worksheet.Cells["C2"]);
            //worksheet.Cells["AN2"].CopyFrom(worksheet.Cells["D2"]);
            //worksheet.Cells["AO2"].CopyFrom(worksheet.Cells["E2"]);
            //worksheet.Cells["AP2"].CopyFrom(worksheet.Cells["F2"]);
            //worksheet.Cells["AQ2"].CopyFrom(worksheet.Cells["G2"]);
            //worksheet.Cells["AR2"].CopyFrom(worksheet.Cells["H2"]);


            spreadsheetControl1.Options.Behavior.Group.Group = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled;
            range = worksheet["C2:D2"];
            range.GroupColumns(true);
            range = worksheet["F2:G2"];
            range.GroupColumns(true);
            range = worksheet["I2:J2"];
            range.GroupColumns(true);
            range = worksheet["L2:M2"];
            range.GroupColumns(true);
            range = worksheet["O2:P2"];
            range.GroupColumns(true);
            range = worksheet["R2:S2"];
            range.GroupColumns(true);
            range = worksheet["U2:V2"];
            range.GroupColumns(true);
            range = worksheet["X2:Y2"];
            range.GroupColumns(true);
            range = worksheet["AA2:AB2"];
            range.GroupColumns(true);
            range = worksheet["AD2:AE2"];
            range.GroupColumns(true);
            range = worksheet["AG2:AH2"];
            range.GroupColumns(true);
            range = worksheet["AJ2:AK2"];
            range.GroupColumns(true);
            //range = worksheet["AM2:AN2"];
            //range.GroupColumns(true);
            //range = worksheet["AP2:AQ2"];
            //range.GroupColumns(true);

            range = worksheet.Range["A1:AL10"];
            range.SetInsideBorders(Color.Black, BorderLineStyle.Thin);
            range.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
        }

        public override void SearchPage()
        {
            base.SearchPage();
            C1();
        }


        private void C1()
        {
            if (txtFromMonth.EditValue is null || txtToMonth.EditValue is null)
            {
                MsgBox.Show("Please select month range.", MsgType.Warning);
                return;
            }
            string monthFrom = txtFromMonth.EditValue.ToString();
            string monthTo = txtToMonth.EditValue.ToString();
            if(string.Compare(monthFrom, monthTo) > 0)
            {
                MsgBox.Show("Month range is not valid.", MsgType.Warning);
                return;
            }

            int intYear = Convert.ToInt32(cbYear.Text);
            int intMonthFrom = Convert.ToInt32(monthFrom);
            int intMonthTo = Convert.ToInt32(monthTo);
            int count = intMonthTo - intMonthFrom;
            if (count > 5)
            {
                MsgBox.Show("Month range must less than 7.", MsgType.Warning);
                return;
            }
            //DateTime dtStartDate = DateTime.Parse(txtFromDate.EditValue.ToString());
            //DateTime dtEndDate = DateTime.Parse(txtToDate.EditValue.ToString());
            //double count = (dtEndDate.AddDays(1) - dtStartDate).TotalDays;
            //if (count > 7)
            //{
            //    MsgBox.Show("MSG_ERR_021".Translation(), MsgType.Warning);
            //    return;
            //}

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();

            chartControl2.DataSource = null;
            chartControl2.Series.Clear();
            chartControl2.Titles.Clear();

            DataTable results = CreateChartData(intYear, intMonthFrom, intMonthTo);
            chartControl1.DataSource = results;
            chartControl2.DataSource = results;

             
            chartControl1.SeriesDataMember = "Day";
            chartControl1.SeriesTemplate.ArgumentDataMember = "Line";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Loss_Rate" });
            chartControl2.SeriesDataMember = "Day";
            chartControl2.SeriesTemplate.ArgumentDataMember = "Line";
            chartControl2.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Loss amount(VND)/Finished product(ea)" });


            chartControl1.SeriesTemplate.View = new SideBySideBarSeriesView();
            chartControl2.SeriesTemplate.View = new SideBySideBarSeriesView();


            for (int i = 0; i < chartControl1.Series.Count; i++)
            {
                if (i == 0)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    chartControl2.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                }
                if (i == 1)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                    chartControl2.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                }
                if (i == 2)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(165, 165, 165);
                    chartControl2.Series[i].View.Color = Color.FromArgb(165, 165, 165);
                }
                if (i == 3)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(255, 192, 0);
                    chartControl2.Series[i].View.Color = Color.FromArgb(255, 192, 0);
                }
                if (i == 4)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(112, 173, 71);
                    chartControl2.Series[i].View.Color = Color.FromArgb(112, 173, 71);
                }
                if (i == 5)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(37, 94, 145);
                    chartControl2.Series[i].View.Color = Color.FromArgb(37, 94, 145);
                }
                if (i == 6)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(122, 122, 82);
                    chartControl2.Series[i].View.Color = Color.FromArgb(122, 122, 82);
                }
            }

            chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartControl1.Legend.Direction = LegendDirection.LeftToRight;
            ((XYDiagram)chartControl1.Diagram).Rotated = false;


            ((XYDiagram)chartControl1.Diagram).AxisX.Tickmarks.MinorVisible = false;
            ((XYDiagram)chartControl1.Diagram).AxisX.Reverse = false;


            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            diagram.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();

            diagram.AxisY.ConstantLines.Clear();

            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(txtY1.EditValue.ToString()));
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            ConstantLine constantLine1 = new ConstantLine("");
            diagram.AxisY.ConstantLines.Add(constantLine1);

            constantLine1.AxisValue = txtTarget.EditValue;
            constantLine1.LineStyle.DashStyle = DashStyle.Dash;
            constantLine1.LineStyle.Thickness = 2;
            constantLine1.Color = Color.Red;
            constantLine1.ShowInLegend = true;
            constantLine1.LegendText = "Target(" + txtTarget.EditValue + "%)";
            constantLine1.ShowBehind = true;

            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "LOSS BY PICK UP RATE(%)";
            chartControl1.Titles.Add(chartTitle1);

            chartControl1.Dock = DockStyle.Fill;



            chartControl2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControl2.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartControl2.Legend.Direction = LegendDirection.LeftToRight;
            ((XYDiagram)chartControl2.Diagram).Rotated = false;


            ((XYDiagram)chartControl2.Diagram).AxisX.Tickmarks.MinorVisible = false;
            ((XYDiagram)chartControl2.Diagram).AxisX.Reverse = false;


            XYDiagram diagram2 = (XYDiagram)chartControl2.Diagram;

            diagram2.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();

            diagram2.AxisY.ConstantLines.Clear();

            diagram2.AxisY.WholeRange.Auto = false;
            diagram2.AxisY.WholeRange.SetMinMaxValues(0, Convert.ToDecimal(txtY2.EditValue.ToString()));
            diagram2.AxisY.WholeRange.AlwaysShowZeroLevel = true;


            ChartTitle chartTitle2 = new ChartTitle();
            chartTitle2.Text = "LOSS BY AMOUNT/FINISHED PRODUCT";
            chartControl2.Titles.Add(chartTitle2);

            chartControl2.Dock = DockStyle.Fill;


            IWorkbook workbook;
            Worksheet worksheet;

            workbook = spreadsheetControl1.Document;
            worksheet = workbook.Worksheets[0];
            RowCollection rows = worksheet.Rows;
            Row firstRow = rows[2];
            Row secondRow = rows[3];
            Row thirdRow = rows[4];
            Row fourthRow = rows[5];
            Row fifthRow = rows[6];
            Row sixthRow = rows[7];
            Row seventhRow = rows[8];
            Row eighthRow = rows[9];

            worksheet.ClearContents(worksheet["C1:AG1"]);
            worksheet.ClearContents(worksheet["B3:AL10"]);
            for (int i = 0; i < 6; i++)
            {
                firstRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                firstRow[i * 6 + 4].Font.Color = Color.Blue;
                secondRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                secondRow[i * 6 + 4].Font.Color = Color.Blue;
                thirdRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                thirdRow[i * 6 + 4].Font.Color = Color.Blue;
                fourthRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                fourthRow[i * 6 + 4].Font.Color = Color.Blue;
                fifthRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                fifthRow[i * 6 + 4].Font.Color = Color.Blue;
                sixthRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                sixthRow[i * 6 + 4].Font.Color = Color.Blue;
                seventhRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                seventhRow[i * 6 + 4].Font.Color = Color.Blue;
                eighthRow[i * 6 + 4].Fill.BackgroundColor = Color.White;
                eighthRow[i * 6 + 4].Font.Color = Color.Blue;
            }

            worksheet.Range["B3:B10"].Value = txtTarget.EditValue.ToString();
            if (results.Rows.Count > 0)
            {
                for (int i = 0; i <= count; i++)
                {
                    string month = string.Empty;
                    if (intMonthFrom + i < 10)
                    {
                        month = "0" + (intMonthFrom + i);
                    }
                    else
                    {
                        month = (intMonthFrom + i).ToString();
                    }
                    if (i == 0)
                        worksheet.Cells["C1"].Value = intYear + "." + month;
                    if (i == 1)
                        worksheet.Cells["I1"].Value = intYear + "." + month;
                    if (i == 2)
                        worksheet.Cells["O1"].Value = intYear + "." + month;
                    if (i == 3)
                        worksheet.Cells["U1"].Value = intYear + "." + month;
                    if (i == 4)
                        worksheet.Cells["AA1"].Value = intYear + "." + month;
                    if (i == 5)
                        worksheet.Cells["AG1"].Value = intYear + "." + month;
                    //if (i == 6)
                    //    worksheet.Cells["AM1"].Value = "W" + week;
                    for (int j = 0; j < results.Rows.Count; j++)
                    {
                        if (results.Rows[j]["Day"].ToString() == intYear + "." + month)
                        {
                            if (results.Rows[j]["Line"].ToString().Trim() == "Total")
                            {
                                firstRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                firstRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                firstRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    firstRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    firstRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                firstRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                firstRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                firstRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineC")
                            {
                                secondRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                secondRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                secondRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    secondRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    secondRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                secondRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                secondRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                secondRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineD")
                            {
                                thirdRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                thirdRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                thirdRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    thirdRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    thirdRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                thirdRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                thirdRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                thirdRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineE")
                            {
                                fourthRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                fourthRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                fourthRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    fourthRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    fourthRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                fourthRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                fourthRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                fourthRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineF")
                            {
                                fifthRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                fifthRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                fifthRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    fifthRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    fifthRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                fifthRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                fifthRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                fifthRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineG")
                            {
                                sixthRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                sixthRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                sixthRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    sixthRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    sixthRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                sixthRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                sixthRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                sixthRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineH")
                            {
                                seventhRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                seventhRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                seventhRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    seventhRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    seventhRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                seventhRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                seventhRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                seventhRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                            if (results.Rows[j]["Line"].ToString().Trim() == "LineI")
                            {
                                eighthRow[i * 6 + 2].Value = String.Format("{0:#,##0}", results.Rows[j][2].ToInt());
                                eighthRow[i * 6 + 3].Value = String.Format("{0:#,##0}", results.Rows[j][3].ToInt());
                                eighthRow[i * 6 + 4].Value = results.Rows[j][4].ToString();
                                if (string.Compare(results.Rows[j][4].ToString(), txtTarget.EditValue.ToString()) > 0)
                                {
                                    eighthRow[i * 6 + 4].Fill.BackgroundColor = Color.FromArgb(255, 199, 206);
                                    eighthRow[i * 6 + 4].Font.Color = Color.FromArgb(156, 0, 6);
                                }
                                eighthRow[i * 6 + 5].Value = String.Format("{0:#,##0}", results.Rows[j][5].ToInt());
                                eighthRow[i * 6 + 6].Value = String.Format("{0:#,##0}", results.Rows[j][6].ToInt());
                                eighthRow[i * 6 + 7].Value = results.Rows[j][7].ToString();
                            }
                        }
                    }
                }
            }

        }

        
        private DataTable CreateChartData(int intYear, int intMonthFrom, int intMonthTo)
        {
            // Create an empty table. 
            DataTable table = new DataTable("Table1");
            DataTable tb = new DataTable();

            // Add three columns to the table. 
            table.Columns.Add("Day", typeof(String));
            table.Columns.Add("Line", typeof(String));
            table.Columns.Add("Total_Pickup", typeof(double));
            table.Columns.Add("Total_Loss", typeof(double));
            table.Columns.Add("Loss_Rate", typeof(double));
            table.Columns.Add("Loss amount(VND)", typeof(double));
            table.Columns.Add("Finished product (ea)", typeof(double));
            table.Columns.Add("Loss amount(VND)/Finished product(ea)", typeof(double));


            DateTime firstDayOfMonth = new DateTime();
            // Add data rows to the table. 
            for (int c = intMonthFrom; c <= intMonthTo; c++)
            {
                firstDayOfMonth = new DateTime(intYear, c, 1);
                //if(c == 1)
                //{
                //    firstDayOfWeek = new DateTime(intYear, 1, 2);
                //    count = 7 - (int)firstDayOfWeek.DayOfWeek;
                //}
                //else if(c == 53)
                //{
                //    count = (int)(new DateTime(intYear, 12, 31)).DayOfWeek;
                //    firstDayOfWeek = (new DateTime(intYear, 12, 31)).AddDays(-count);
                //}
                //else
                //{
                //    firstDayOfWeek = new Week(intYear, c).FirstDayOfWeek;
                //    count = 7;
                //}

                tb.Clear();
                //for (double i = 0; i <= count; i++)
                //{
                    string month = firstDayOfMonth.Month.ToString();
                    if (month.Length == 1)
                    {
                        month = "0" + month;
                    }

                //using (SqlConnection conn = new SqlConnection(Program.connectionString))
                //{
                //    for (int j = 0; j < 7; j++)
                //    {
                //        StringBuilder strSqlString = new StringBuilder();

                //        strSqlString.AppendFormat("SELECT t4.Line, \n");
                //        strSqlString.AppendFormat("       t4.Total_Pickup, \n");
                //        strSqlString.AppendFormat("       t4.Total_Loss, \n");
                //        strSqlString.AppendFormat("       t4.Loss_Rate, \n");
                //        strSqlString.AppendFormat("       cast(round(t3.Tong, 0) AS int) AS Tong, \n");
                //        strSqlString.AppendFormat("       t3.Finish, \n");
                //        strSqlString.AppendFormat("       round(t3.Tong*1.0/t3.Finish, 1) AS kq \n");
                //        strSqlString.AppendFormat("FROM ( \n");
                //        strSqlString.AppendFormat("SELECT t2.*, \n");
                //        strSqlString.AppendFormat("       t1.Tong \n");
                //        strSqlString.AppendFormat("FROM ( \n");
                //        strSqlString.AppendFormat("SELECT LINE, \n");
                //        strSqlString.AppendFormat("       sum(Total_VND) AS Tong \n");
                //        strSqlString.AppendFormat("FROM ( \n");
                //        strSqlString.AppendFormat(" SELECT xx.Line, xx.Total_Loss*pri.PRICE as Total_VND FROM \n");
                //        strSqlString.AppendFormat(" ( \n");
                //        strSqlString.AppendFormat("SELECT right(Station.strline, \n");
                //        strSqlString.AppendFormat("             charindex('\\', reverse(Station.strline)) - 1) as Line, \n");
                //        strSqlString.AppendFormat(" right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) as PartNumber, \n ");
                //        strSqlString.AppendFormat("(sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty)) as Total_Loss \n");
                //        //strSqlString.AppendFormat(", pri.PRICE*(sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty)) as Total_VND \n");
                //        strSqlString.AppendFormat(" FROM \n");
                //        strSqlString.AppendFormat("[PCCLIENT" + j + "].SiplaceOIS.dbo.compBlock  inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.compdetail on compBlock.lIdBlock = compDetail.lIdBlock \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.compPosition on compDetail.lIdPosition = compPosition.lIdPosition \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.Station on compblock.lId = Station.lId \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.PARTNUMBER on PARTNUMBER.lPartNumber = Compposition.lPartNumber \n");
                //        //strSqlString.AppendFormat("inner join ewipsmtpri pri on right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) = pri.COMMCODE collate SQL_Latin1_General_CP1_CI_AS \n");
                //        strSqlString.AppendFormat(" WHERE \n");
                //        strSqlString.AppendFormat("dttime >= '{0}' + ' 08:00:00.000' and dttime <= '{1}' + ' 08:00:00.000' \n", firstDayOfMonth.ToString("yyyy-MM-dd"), firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd")); // and pri.YEAR = '{2}' and pri.MONTH = '{3}' \n", firstDayOfMonth.ToString("yyyy-MM-dd"), firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd"), intYear.ToString(), month);
                //        strSqlString.AppendFormat("group by strline, PARTNUMBER.strPartNumber \n");// , pri.PRICE \n");
                //        strSqlString.AppendFormat(" )xx \n");
                //        strSqlString.AppendFormat(" inner join EWIPSMTPRI pri on xx.PartNumber = pri.COMMCODE collate SQL_Latin1_General_CP1_CI_AS \n");
                //        strSqlString.AppendFormat(" where pri.Year = '{0}' and pri.Month = '{1}' \n", intYear.ToString(), month);
                //        strSqlString.AppendFormat(") src \n");
                //        strSqlString.AppendFormat("group by src.Line \n");
                //        strSqlString.AppendFormat(") t1 \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat("inner join \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat("( \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat("select t5.Line, sum(t5.Finish) as Finish from \n");
                //        strSqlString.AppendFormat("( select yy.Line, yy.Finish/ cri.criteria as Finish \n");
                //        strSqlString.AppendFormat(" from( \n");
                //        strSqlString.AppendFormat("SELECT \n");
                //        strSqlString.AppendFormat("right(Station.strline, charindex('\\', reverse(Station.strline)) - 1) as Line, \n");
                //        strSqlString.AppendFormat("right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) as PartNumber, \n");
                //        //strSqlString.AppendFormat("cri.CRITERIA, \n");
                //        strSqlString.AppendFormat("(sum(compDetail.sAccessTotal) - (sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty))) as Finish \n");// /cri.CRITERIA as Finish \n");
                //        strSqlString.AppendFormat(" FROM \n");
                //        strSqlString.AppendFormat("[PCCLIENT" + j + "].SiplaceOIS.dbo.compBlock  inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.compdetail on compBlock.lIdBlock = compDetail.lIdBlock \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.compPosition on compDetail.lIdPosition = compPosition.lIdPosition \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.Station on compblock.lId = Station.lId \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.PARTNUMBER on PARTNUMBER.lPartNumber = Compposition.lPartNumber \n");
                //        //strSqlString.AppendFormat("inner join EWIPSMTCRI cri on right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) = cri.COMMCODE collate SQL_Latin1_General_CP1_CI_AS \n");
                //        //strSqlString.AppendFormat("inner join ewipsmtpri pri on cri.COMMCODE = pri.COMMCODE \n");
                //        strSqlString.AppendFormat(" WHERE \n");
                //        strSqlString.AppendFormat("dttime >= '{0}' + ' 08:00:00.000' and dttime <= '{1}' + ' 08:00:00.000' \n", firstDayOfMonth.ToString("yyyy-MM-dd"), firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd"));// and pri.YEAR = '{2}' and pri.MONTH = '{3}' \n", firstDayOfMonth.ToString("yyyy-MM-dd"), firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd"), intYear.ToString(), month);
                //        strSqlString.AppendFormat("group by strline, PARTNUMBER.strPartNumber \n");// cri.CRITERIA, Compposition.lPartNumber \n");
                //        strSqlString.AppendFormat(" ) yy inner join EWIPSMTCRI cri on yy.PartNumber = cri.COMMCODE collate SQL_Latin1_General_CP1_CI_AS  \n");
                //        strSqlString.AppendFormat(" inner join ewipsmtpri pri on cri.COMMCODE = pri.COMMCODE \n");
                //        strSqlString.AppendFormat(" where pri.Year = '{0}' and pri.Month = '{1}' \n", intYear.ToString(), month);
                //        strSqlString.AppendFormat(") t5 \n");
                //        strSqlString.AppendFormat("group by t5.Line \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat(") t2 \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat("on t1.Line = t2.Line \n");
                //        strSqlString.AppendFormat(") t3 \n");
                //        strSqlString.AppendFormat(" \n");
                //        strSqlString.AppendFormat("inner join \n");
                //        strSqlString.AppendFormat("( \n");
                //        strSqlString.AppendFormat("SELECT \n");
                //        strSqlString.AppendFormat("right(Station.strline, charindex('\\', reverse(Station.strline)) - 1) as Line, \n");
                //        strSqlString.AppendFormat("sum(compDetail.sAccessTotal) as Total_Pickup, \n");
                //        strSqlString.AppendFormat("(sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty)) as Total_Loss, \n");
                //        strSqlString.AppendFormat("Cast(Round(CAST((sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty)) as float)/CAST( sum(compDetail.sAccessTotal) as float),4)*100 as numeric(5,2)) as Loss_Rate \n");
                //        strSqlString.AppendFormat(" FROM \n");
                //        strSqlString.AppendFormat("[PCCLIENT" + j + "].SiplaceOIS.dbo.compBlock  inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.compdetail on compBlock.lIdBlock = compDetail.lIdBlock \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.Station on compblock.lId = Station.lId \n");
                //        strSqlString.AppendFormat(" WHERE \n");
                //        strSqlString.AppendFormat("dttime >= '{0}' + ' 08:00:00.000' and dttime <= '{1}' + ' 08:00:00.000'\n", firstDayOfMonth.ToString("yyyy-MM-dd"), firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd"));
                //        strSqlString.AppendFormat("GROUP BY strline) t4 ON t3.Line = t4.Line");

                //        new SqlDataAdapter(strSqlString.ToString(), conn).Fill(tb);
                //    }
                //}
                //}
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005.GET_LIST"
                        , new string[] { "A_FROM_DATE",
                                         "A_TO_DATE",
                                         "A_YEAR",
                                         "A_MONTH"
                        }
                        , new string[] { firstDayOfMonth.ToString("yyyy-MM-dd") + " 08:00:00.000",
                                         firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd") + " 08:00:00.000",
                                         intYear.ToString(), month
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {

                        tb = base.m_ResultDB.ReturnDataSet.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }


                double total_Pickup = 0;
                double total_Loss = 0;
                double tong = 0;
                double finish = 0;
                //string week = (c < 10 ? "0" + c : c.ToString());
                if (tb.Rows.Count > 0)
                {
                    for (int z = 0; z < tb.Rows.Count; z++)
                    {
                        total_Pickup += Convert.ToDouble(tb.Rows[z]["Total_Pickup"].ToString());
                        total_Loss += Convert.ToDouble(tb.Rows[z]["Total_Loss"].ToString());
                        tong += Convert.ToDouble(tb.Rows[z]["Tong"].ToString());
                        finish += Convert.ToDouble(tb.Rows[z]["Finish"].ToString());
                        table.Rows.Add(new object[] { intYear + "." + month, tb.Rows[z]["Line"].ToString(), Convert.ToDouble(tb.Rows[z]["Total_Pickup"].ToString()), Convert.ToDouble(tb.Rows[z]["Total_Loss"].ToString()), Convert.ToDouble(tb.Rows[z]["Loss_Rate"].ToString()), Convert.ToDouble(tb.Rows[z]["Tong"].ToString()), Convert.ToDouble(tb.Rows[z]["Finish"].ToString()), Convert.ToDouble(tb.Rows[z]["kq"].ToString()) });
                    }

                    table.Rows.Add(new object[] { intYear + "." + month, " Total", total_Pickup, total_Loss, Math.Round(total_Loss * 100 / total_Pickup, 2), tong, finish, Math.Round(tong / finish, 1).ToString("N1") });
                }
            }
            return table;
        }

        

        private void dtpDateTime_EditValueChanged(object sender, EventArgs e)
        {
            //dtpDateTime.EditValue = "";
        }

        private void gvList_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            //ColumnView view = sender as ColumnView;
            //string total = view.GetListSourceRowCellValue(e.ListSourceRow, "Loss_Rate").ToString();
            //if (total.Trim() == "NaN")
            //{
            //    gvList.SetRowCellValue(e.ListSourceRow, "Loss_Rate", 0);
            //}
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files(*.xlsx)|*.xlsx";
                saveDialog.FileName = "Pick Up Rate_By Month and Line_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    chartControl1.ExportToImage(Application.StartupPath + @"\images1.png", ImageFormat.Png);
                    chartControl2.ExportToImage(Application.StartupPath + @"\images2.png", ImageFormat.Png);

                    IWorkbook workbook;
                    Worksheet worksheet;

                    workbook = spreadsheetControl1.Document;
                    worksheet = workbook.Worksheets[0];

                    workbook.BeginUpdate();
                    try
                    {
                        worksheet.Pictures.AddPicture(Application.StartupPath + @"\images1.png", worksheet.Cells["A13"]);
                        worksheet.Pictures.AddPicture(Application.StartupPath + @"\images2.png", worksheet.Cells["A30"]);
                    }
                    finally
                    {
                        workbook.EndUpdate();
                    }

                    spreadsheetControl1.SaveDocument(saveDialog.FileName);
                    worksheet.Pictures.Clear();
                }
            }
        }

    }
}
