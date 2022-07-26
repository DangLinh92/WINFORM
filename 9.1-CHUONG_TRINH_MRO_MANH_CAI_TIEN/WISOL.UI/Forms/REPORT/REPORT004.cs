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
    public partial class REPORT004 : PageType
    {
        DataTable dt = new DataTable();
        DataTable dtChart = new DataTable();
        public REPORT004()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
            this.layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            dtpFromMonth.Properties.ShowMonthHeaders = true;
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
                else if (ho.Text.Length < 2)
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
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    //base.m_BindData.BindGridView(gcList,
                    //    base.m_ResultDB.ReturnDataSet.Tables[0]
                    //    );
                    dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            ComboBoxItemCollection coll = cbModel.Properties.Items;
            coll.BeginUpdate();
            try
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    coll.Add(dt.Rows[i][0].ToString());
                }
            }
            finally
            {
                coll.EndUpdate();
            }

            coll = cbYear.Properties.Items;
            coll.BeginUpdate();
            try
            {
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
            cbYear.SelectedText = DateTime.Now.ToString("yyyy");
        }

        public override void SearchPage()
        {
            if (txtFromMonth.EditValue is null || txtToMonth.EditValue is null)
            {
                MsgBox.Show("Hãy chọn khoảng tháng.", MsgType.Warning);
                return;
            }
            string monthFrom = txtFromMonth.EditValue.ToString();
            string monthTo = txtToMonth.EditValue.ToString();
            if (string.Compare(monthFrom, monthTo) > 0)
            {
                MsgBox.Show("Khoảng tháng không hợp lệ.", MsgType.Warning);
                return;
            }

            int intYear = Convert.ToInt32(cbYear.Text);
            int intMonthFrom = Convert.ToInt32(monthFrom);
            int intMonthTo = Convert.ToInt32(monthTo);
            int count = intMonthTo - intMonthFrom;
            if (count > 5)
            {
                MsgBox.Show("Khoảng tháng phải nhỏ hơn 7.", MsgType.Warning);
                return;
            }
            if (cbModel.EditValue is null)
            {
                MsgBox.Show("Hãy chọn Model.", MsgType.Warning);
                return;
            }
            base.SearchPage();
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();

            DateTime firstDayOfMonth = new DateTime(intYear, intMonthFrom, 1);
            DateTime lastDayOfQuery = new DateTime(intYear, intMonthTo + 1, 1);

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT004.GET_LIST"
                    , new string[] { "A_FROM_DATE", "A_TO_DATE", "A_MODEL"
                    }
                    , new string[] { firstDayOfMonth.ToString("yyyy-MM-dd"),
                                     lastDayOfQuery.ToString("yyyy-MM-dd"), 
                                     cbModel.EditValue.ToString()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    dtChart = base.m_ResultDB.ReturnDataSet.Tables[1];
                    gvList.OptionsView.ShowFooter = false;
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            if(dtChart.Rows.Count < 1)
            {
                MsgBox.Show("Không có dữ liệu.", MsgType.Warning);
                return;
            }

            gvList.OptionsView.ShowFooter = false;
            gvList.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[1].DisplayFormat.FormatString = "n0";
            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[2].DisplayFormat.FormatString = "n0";
            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "n0";
            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[4].DisplayFormat.FormatString = "n0";
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "n0";
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "n0";
            gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[7].DisplayFormat.FormatString = "n0";
            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "n0";
            gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[9].DisplayFormat.FormatString = "n0";
            gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[10].DisplayFormat.FormatString = "n0";
            gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[11].DisplayFormat.FormatString = "n0";
            gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[12].DisplayFormat.FormatString = "n0";
            gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[13].DisplayFormat.FormatString = "n0";
            gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[14].DisplayFormat.FormatString = "n0";
            gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[15].DisplayFormat.FormatString = "n0";
            gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[16].DisplayFormat.FormatString = "n0";
            gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[17].DisplayFormat.FormatString = "n0";
            gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[18].DisplayFormat.FormatString = "n0";

            //Series series1 = new Series("BONG".Translation(), ViewType.Bar);
            //Series series2 = new Series("SAW_SOLDER_OPEN".Translation(), ViewType.Bar);
            //Series series3 = new Series("SW_CRACK".Translation(), ViewType.Bar);
            //Series series4 = new Series("SW_OPEN".Translation(), ViewType.Bar);
            //Series series5 = new Series("NGHI_NGO_SW".Translation(), ViewType.Bar);
            //Series series6 = new Series("LNA_OPEN".Translation(), ViewType.Bar);
            //Series series7 = new Series("NHGI_NGO_LNA".Translation(), ViewType.Bar);
            //Series series8 = new Series("NGHI_NGO_SW_LNA".Translation(), ViewType.Bar);
            //Series series9 = new Series("SAW_CRACK".Translation(), ViewType.Bar);
            //Series series10 = new Series("BALL_OPEN".Translation(), ViewType.Bar);
            //Series series11 = new Series("NHIEM_DIEN".Translation(), ViewType.Bar);
            //Series series12 = new Series("LOANG_BAN".Translation(), ViewType.Bar);
            //Series series13 = new Series("PATTERN".Translation(), ViewType.Bar);
            //Series series14 = new Series("XUOC".Translation(), ViewType.Bar);
            //Series series15 = new Series("SAW_KHONG_LOI".Translation(), ViewType.Bar);
            //Series series16 = new Series("LKG".Translation(), ViewType.Bar);
            //Series series17 = new Series("LOI_GIA".Translation(), ViewType.Bar);

            //for (int i = 0; i < dtChart.Rows.Count; i++)
            //{
            //    series1.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][2].ToString()));
            //    series2.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][3].ToString()));
            //    series3.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][4].ToString()));
            //    series4.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][5].ToString()));
            //    series5.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][6].ToString()));
            //    series6.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][7].ToString()));
            //    series7.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][8].ToString()));
            //    series8.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][9].ToString()));
            //    series9.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][10].ToString()));
            //    series10.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][11].ToString()));
            //    series11.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][12].ToString()));
            //    series12.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][13].ToString()));
            //    series13.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][14].ToString()));
            //    series14.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][15].ToString()));
            //    series15.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][16].ToString()));
            //    series16.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][17].ToString()));
            //    series17.Points.Add(new SeriesPoint(dtChart.Rows[i][0].ToString(), dtChart.Rows[i][18].ToString()));
            //    //series16.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][19].ToString()));
            //}
            //chartControl1.Series.AddRange(new Series[] { series1, series2, series3, series4, series5, series6, series7, series8, series9,
            //                                             series10, series11, series12, series13, series14, series15, series16, series17});

            chartControl1.DataSource = dtChart;

            chartControl1.SeriesDataMember = "MONTH";
            chartControl1.SeriesTemplate.ArgumentDataMember = "ERROR";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "VAL" });

            chartControl1.SeriesTemplate.View = new SideBySideBarSeriesView();

            for (int i = 0; i < chartControl1.Series.Count; i++)
            {
                if (i == 0)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 1)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 2)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(165, 165, 165);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 3)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(255, 192, 0);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 4)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(112, 173, 71);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 5)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(37, 94, 145);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 6)
                {
                    chartControl1.Series[i].View.Color = Color.FromArgb(122, 122, 82);
                    //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
            }

            chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartControl1.Legend.Direction = LegendDirection.LeftToRight;

            //chartControl1.Series.Add(series);
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Alignment = StringAlignment.Center;
            diagram.AxisY.Title.Text = "PPM";
            //diagram.AxisY.Title.TextColor = Color.Blue;
            //diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Font = new Font("Tahoma", 11, FontStyle.Regular);


            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            //chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Model " + cbModel.EditValue.ToString() + " Tháng " + txtFromMonth.Text.Trim() + "-" + txtToMonth.Text.Trim();
            chartControl1.Titles.Add(chartTitle1);

            //series17.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //series17.Label.TextPattern = "{V:#,#}";
            //series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //series1.Label.TextPattern = "{V:#,#}";
            chartControl1.Dock = DockStyle.Fill;
        }

        
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            
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
    }
}
