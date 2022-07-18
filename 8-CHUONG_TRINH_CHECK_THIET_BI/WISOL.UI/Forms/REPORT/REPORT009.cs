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
using System.Globalization;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT009 : PageType
    {
        public REPORT009()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();

            dtpFromWeek.Properties.ShowWeekNumbers = true;
            dtpToWeek.Properties.ShowWeekNumbers = true;

            dtpFromWeek.Properties.Popup += From_Popup;
            dtpToWeek.Properties.Popup += To_Popup;
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
            if (hitInfo.HitTest == CalendarHitInfoType.WeekNumber)
            {
                CalendarCellViewInfo ho = hitInfo.HitObject as CalendarCellViewInfo;
                //int weekNumber = Convert.ToInt32(ho.Text);
                txtFromWeek.EditValue = "W" + (ho.Text.Length < 2 ? "0" + ho.Text : ho.Text);
                dtpFromWeek.ClosePopup();
            }
        }
        void To_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CalendarControl calendar = sender as CalendarControl;
            CalendarHitInfo hitInfo = calendar.GetHitInfo(e.Location);
            if (hitInfo.HitTest == CalendarHitInfoType.WeekNumber)
            {
                CalendarCellViewInfo ho = hitInfo.HitObject as CalendarCellViewInfo;
                //int weekNumber = Convert.ToInt32(ho.Text);
                txtToWeek.EditValue = "W" + (ho.Text.Length < 2 ? "0" + ho.Text : ho.Text);
                dtpToWeek.ClosePopup();
            }
        }

        public override void InitializePage()
        {
            txtTarget.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTarget.Properties.Mask.EditMask = "d";
            txtY1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtY1.Properties.Mask.EditMask = "d";
            txtY2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtY2.Properties.Mask.EditMask = "d";

            ComboBoxItemCollection coll = cbYear.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("2019");
                coll.Add("2020");
                coll.Add("2021");
                coll.Add("2022");
                coll.Add("2023");
                coll.Add("2024");
                coll.Add("2025");
                coll.Add("2026");
                coll.Add("2027");
                coll.Add("2028");
                coll.Add("2029");
            }
            finally
            {
                coll.EndUpdate();
            }

            string YearNow = DateTime.Now.Year.ToString();

            //cbYear.SelectedIndex = 1;
            cbYear.SelectedText = YearNow;
        }

        public override void SearchPage()
        {
            base.SearchPage();
            C1();
        }


        private void C1()
        {
            if (txtFromWeek.EditValue is null || txtToWeek.EditValue is null)
            {
                MsgBox.Show("Please select week range.", MsgType.Warning);
                return;
            }
            string weekFrom = txtFromWeek.EditValue.ToString();
            string weekTo = txtToWeek.EditValue.ToString();
            if (string.Compare(weekFrom, weekTo) > 0)
            {
                MsgBox.Show("Week range is not valid.", MsgType.Warning);
                return;
            }

            int intYear = Convert.ToInt32(cbYear.Text);
            int intWeekFrom = Convert.ToInt32(weekFrom.Substring(1));
            int intWeekTo = Convert.ToInt32(weekTo.Substring(1));
            int count = intWeekTo - intWeekFrom;
            if (count > 5)
            {
                MsgBox.Show("Week range must less than 7.", MsgType.Warning);
                return;
            }

            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            gcList.DataSource = null;

            DataTable results = CreateChartData(intYear, intWeekFrom, intWeekTo);
            if (results.Rows.Count < 1)
            {
                return;
            }
            base.m_BindData.BindGridView(gcList, results);
            gvList.Columns[0].Width = 80;
            gvList.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvList.Columns[1].Width = 60;
            gvList.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[1].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[2].Width = 80;
            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[2].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[3].Width = 50;
            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[4].Width = 120;
            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[4].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[5].Width = 50;
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[6].Width = 120;
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[7].Width = 50;
            gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[7].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[8].Width = 80;
            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[9].Width = 90;
            gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[9].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[10].Width = 80;
            gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[10].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[11].Width = 110;
            gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[11].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[12].Width = 80;
            gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[12].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[13].Width = 80;
            gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[13].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[14].Width = 110;
            gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[14].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[15].Width = 130;
            gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[15].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[16].Width = 80;
            gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[16].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[16].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[17].Width = 80;
            gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[17].DisplayFormat.FormatString = "n0";
            gvList.Columns[17].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //gvList.Columns[18].Width = 80;
            //gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[18].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.OptionsView.ShowFooter = false;
     
            
            Series series1 = new Series("CHUA_DAN".Translation(), ViewType.StackedBar);
            series1.View.Color = Color.FromArgb(255, 255, 204);
            Series series2 = new Series("SHORT".Translation(), ViewType.StackedBar);
            series2.View.Color = Color.FromArgb(204, 255, 255);
            Series series3 = new Series("THUA_THIEU_THIEC".Translation(), ViewType.StackedBar);
            series3.View.Color = Color.FromArgb(187, 153, 255);
            Series series4 = new Series("BONG".Translation(), ViewType.StackedBar);
            series4.View.Color = Color.FromArgb(255, 128, 128);
            Series series5 = new Series("SAW_SOLDER_OPEN".Translation(), ViewType.StackedBar);
            series5.View.Color = Color.FromArgb(0, 204, 102);
            Series series6 = new Series("LECH".Translation(), ViewType.StackedBar);
            series6.View.Color = Color.FromArgb(0, 102, 204);
            Series series7 = new Series("DI_HINH".Translation(), ViewType.StackedBar);
            series7.View.Color = Color.FromArgb(204, 204, 255);
            Series series8 = new Series("SW_CRACK".Translation(), ViewType.StackedBar);
            series8.View.Color = Color.FromArgb(51, 153, 255);
            Series series9 = new Series("SW_OPEN".Translation(), ViewType.StackedBar);
            series9.View.Color = Color.FromArgb(255, 102, 204);
            Series series10 = new Series("NGHI_NGO_SW".Translation(), ViewType.StackedBar);
            series10.View.Color = Color.FromArgb(230, 230, 0);
            Series series11 = new Series("LNA_CRACK".Translation(), ViewType.StackedBar);
            series11.View.Color = Color.FromArgb(0, 255, 255);
            Series series12 = new Series("LNA_OPEN".Translation(), ViewType.StackedBar);
            series12.View.Color = Color.FromArgb(153, 0, 153);
            Series series13 = new Series("NGHI_NGO_LNA".Translation(), ViewType.StackedBar);
            series13.View.Color = Color.FromArgb(102, 153, 255);
            Series series14 = new Series("NGHI_NGO_SW_LNA".Translation(), ViewType.StackedBar);
            series14.View.Color = Color.FromArgb(0, 128, 128);
            //Series series15 = new Series("SAW_CRACK".Translation(), ViewType.StackedBar);
            //series15.View.Color = Color.FromArgb(204, 153, 0);
            Series line = new Series("INPUT", ViewType.Line);

            for (int i = 0; i < results.Rows.Count; i++)
            {
                series1.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][2].ToString()));
                series2.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][3].ToString()));
                series3.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][4].ToString()));
                series4.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][5].ToString()));
                series5.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][6].ToString()));
                series6.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][7].ToString()));
                series7.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][8].ToString()));
                series8.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][9].ToString()));
                series9.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][10].ToString()));
                series10.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][11].ToString()));
                series11.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][12].ToString()));
                series12.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][13].ToString()));
                series13.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][14].ToString()));
                series14.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][15].ToString()));
                //series15.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][16].ToString()));
                line.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][17].ToString()));
            }

            //series1.Points.Add(new SeriesPoint("C", 10));
            //series1.Points.Add(new SeriesPoint("D", 12));
            //series1.Points.Add(new SeriesPoint("E", 14));
            //series1.Points.Add(new SeriesPoint("F", 17));
            //series1.Points.Add(new SeriesPoint("G", 12));
            //series1.Points.Add(new SeriesPoint("H", 14));
            //series1.Points.Add(new SeriesPoint("I", 17));

            //series2.Points.Add(new SeriesPoint("C", 25));
            //series2.Points.Add(new SeriesPoint("D", 33));
            //series1.Points.Add(new SeriesPoint("E", 14));
            //series1.Points.Add(new SeriesPoint("F", 40));
            //series1.Points.Add(new SeriesPoint("G", 12));
            //series1.Points.Add(new SeriesPoint("H", 20));
            //series1.Points.Add(new SeriesPoint("I", 17));

            chartControl1.Series.AddRange(new Series[] { series1, series2, series3, series4, series5, series6, series7, series8, series9,
                                                         series10, series11, series12, series13, series14,  line});


            //chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            //chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            //chartControl1.Legend.Direction = LegendDirection.LeftToRight;

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Alignment = StringAlignment.Center;
            diagram.AxisY.Title.Text = "PPM";
            //diagram.AxisY.Title.TextColor = Color.Blue;
            //diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Font = new Font("Tahoma", 11, FontStyle.Regular);


            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(txtY1.EditValue.ToString()));
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            if (results.Rows.Count == 1)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 2.5;
            }
            if (results.Rows.Count == 2)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 2.0;
            }
            if (results.Rows.Count == 3)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 1.5;
            }
            if (results.Rows.Count == 4)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 0.9;
            }
            if (results.Rows.Count == 5)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 0.7;
            }
            if (results.Rows.Count == 6)
            {
                diagram.AxisX.WholeRange.SideMarginsValue = 0.5;
            }

            diagram.AxisY.ConstantLines.Clear();
            ConstantLine constantLine1 = new ConstantLine("");
            diagram.AxisY.ConstantLines.Add(constantLine1);

            constantLine1.AxisValue = txtTarget.EditValue;
            constantLine1.LineStyle.DashStyle = DashStyle.Dash;
            constantLine1.LineStyle.Thickness = 2;
            constantLine1.Color = Color.Red;
            constantLine1.ShowInLegend = true;
            constantLine1.LegendText = "TARGET PPM(" + txtTarget.EditValue + ")";
            constantLine1.ShowBehind = true;

            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
            SecondaryAxisY myAxisY = new SecondaryAxisY("my Y-Axis");
            myAxisY.WholeRange.Auto = false;
            myAxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(txtY2.EditValue.ToString()));
            myAxisY.Label.TextPattern = "{V:#,#0}";
            myAxisY.WholeRange.AlwaysShowZeroLevel = true;
            myAxisY.Title.Text = "INPUT";
            myAxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            myAxisY.Title.Alignment = StringAlignment.Center;
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(myAxisY);


            line.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //line.Label.LineColor = Color.Blue;
            //line.Label.BackColor = Color.Blue;
            //line.View.Color = Color.FromArgb(77, 121, 255);
            line.Label.TextPattern = "{V:#,#}";
            //((LineSeriesView)series2.View).AxisX = myAxisX;
            ((LineSeriesView)line.View).AxisY = myAxisY;
            //((StackedBarSeriesView)series1.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series2.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series3.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series4.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series5.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series6.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series7.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series8.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series9.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series10.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series11.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series12.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series13.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series14.View).BarWidth = 0.8;
            //((StackedBarSeriesView)series15.View).BarWidth = 0.8;
            //chartControl1.DataSource = results;
            //chartControl1.SeriesDataMember = "LINE";
            //chartControl1.SeriesTemplate.ArgumentDataMember = "LINE";
            //chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "BONG", "SHORT" });


            //chartControl1.SeriesTemplate.View = new StackedBarSeriesView();

            //for (int i = 0; i < chartControl1.Series.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
            //    }
            //    if (i == 1)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
            //    }
            //    if (i == 2)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(165, 165, 165);
            //    }
            //    if (i == 3)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(255, 192, 0);
            //    }
            //    if (i == 4)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(112, 173, 71);
            //    }
            //    if (i == 5)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(37, 94, 145);
            //    }
            //    if (i == 6)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(122, 122, 82);
            //    }
            //}

            //chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            //chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            //chartControl1.Legend.Direction = LegendDirection.LeftToRight;
            //((XYDiagram)chartControl1.Diagram).Rotated = false;


            //((XYDiagram)chartControl1.Diagram).AxisX.Tickmarks.MinorVisible = false;
            //((XYDiagram)chartControl1.Diagram).AxisX.Reverse = false;


            //XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            //diagram.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();

            //diagram.AxisY.ConstantLines.Clear();

            //diagram.AxisY.WholeRange.Auto = false;
            //diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(txtY1.EditValue.ToString()));
            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            //ConstantLine constantLine1 = new ConstantLine("");
            //diagram.AxisY.ConstantLines.Add(constantLine1);

            //constantLine1.AxisValue = txtTarget.EditValue;
            //constantLine1.LineStyle.DashStyle = DashStyle.Dash;
            //constantLine1.LineStyle.Thickness = 2;
            //constantLine1.Color = Color.Red;
            //constantLine1.ShowInLegend = true;
            //constantLine1.LegendText = "Target(" + txtTarget.EditValue + "%)";
            //constantLine1.ShowBehind = true;

            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "LOSS BY PICK UP RATE(%)";
            //chartControl1.Titles.Add(chartTitle1);

            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "FA";
            //chartControl1.Titles.Add(chartTitle1);
            chartControl1.Dock = DockStyle.Fill;

        }


        private DataTable CreateChartData(int intYear, int intWeekFrom, int intWeekTo)
        {
            DataTable table = new DataTable("Table1");

            try
            {
                DateTime firstDayOfWeek = new DateTime();
                for (int c = intWeekFrom; c <= intWeekTo; c++)
                {
                    int count = 0;
                    string week = string.Empty;
                    if(c < 10)
                    {
                        week = "W0" + c;
                    }
                    else
                    {
                        week = "W" + c;
                    }
                    if (c == 1)
                    {
                        firstDayOfWeek = new DateTime(intYear, 1, 1);
                        count = 7 - (int)firstDayOfWeek.DayOfWeek;
                    }
                    else if (c == 53)
                    {
                        count = (int)(new DateTime(intYear, 12, 31)).DayOfWeek;
                        firstDayOfWeek = (new DateTime(intYear, 12, 31)).AddDays(-count);
                    }
                    else
                    {
                        firstDayOfWeek = FirstDateOfWeekISO8601(intYear, c);
                        count = 7;
                    }

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT009.GET_LIST"
                    , new string[] { "A_WEEK", "A_FROM_DATE", "A_TO_DATE","A_TARGET_PPM"
                    }
                    , new string[] { week,
                                     firstDayOfWeek.ToString("yyyy-MM-dd"), 
                                     firstDayOfWeek.AddDays(count).ToString("yyyy-MM-dd"),
                                     txtTarget.EditValue.ToString()
                    }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        table.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            for(int i = 0; i < table.Rows.Count; i++)
            {
                for(int j = 0; j < table.Columns.Count; j++)
                {
                    if (string.IsNullOrWhiteSpace(table.Rows[i][j].ToString()))
                    {
                        table.Rows[i][j] = 0;
                    }
                }
            }

            return table;
        }

        private DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-4);
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
            gvList.OptionsPrint.PrintFooter = false;
            using(var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files(*.xlsx)|*.xlsx";
                saveDialog.FileName = "FA_By One Day_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var pringtingSystem = new PrintingSystemBase();
                    var compositeLink = new CompositeLinkBase();
                    compositeLink.PrintingSystemBase = pringtingSystem;

                    var link1 = new PrintableComponentLinkBase();
                    link1.Component = (IPrintable)chartControl1;
                    var link2 = new PrintableComponentLinkBase();
                    link2.Component = (IPrintable)gcList;
                    
                    compositeLink.Links.Add(link1);
                    compositeLink.Links.Add(link2);

                    var options = new XlsxExportOptions();
                    options.ExportMode = XlsxExportMode.SingleFile;
                    compositeLink.ExportToXlsx(saveDialog.FileName, options);
                }
            }
        }
    }
}
