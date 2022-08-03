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
    public partial class REPORT003 : PageType
    {
        DataTable dt = new DataTable();
        DataTable dtChart = new DataTable();
        public REPORT003()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
            this.layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            dtpMonth.Properties.ShowMonthHeaders = true;
            dtpMonth.Properties.Popup += Month_Popup;
        }

        private void Month_Popup(object sender, EventArgs e)
        {
            IPopupControl edit = sender as IPopupControl;
            PopupDateEditForm form = edit.PopupWindow as PopupDateEditForm;
            form.Calendar.MouseDown -= Month_MouseDown;
            form.Calendar.MouseDown += Month_MouseDown;
        }
        void Month_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
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
                    txtMonth.EditValue = (monthInt < 10 ? "0" + monthInt : monthInt.ToString());
                }
                else if (ho.Text.Length < 2)
                {
                    txtMonth.EditValue = "0" + ho.Text;
                }
                else
                {
                    txtMonth.EditValue = ho.Text;
                }
                dtpMonth.ClosePopup();
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
            if (txtMonth.EditValue is null)
            {
                MsgBox.Show("Hãy chọn tháng.", MsgType.Warning);
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

            int intYear = Convert.ToInt32(cbYear.Text);
            string stringMonth = txtMonth.Text.ToString();
            int intMonth = Convert.ToInt32(stringMonth.Substring(1));
            DateTime firstDayOfMonth = new DateTime(intYear, intMonth, 1);

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT003.GET_LIST"
                    , new string[] { "A_FROM_DATE", "A_TO_DATE", "A_MODEL"
                    }
                    , new string[] { firstDayOfMonth.ToString("yyyy-MM-dd"),
                                     firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd"), 
                                     cbModel.EditValue.ToString()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    dtChart = base.m_ResultDB.ReturnDataSet.Tables[0];
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
            ////SideBySideBarSeriesView view17 = series17.View as SideBySideBarSeriesView;
            ////view17.BarWidth = 10;
            //((SideBySideBarSeriesView)series17.View).BarWidth = 10;
            ////((SideBySideBarSeriesLabel)series17.Label).Position = BarSeriesLabelPosition.Center;
            //series17.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //(series17.Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //((SideBySideBarSeriesView)series16.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series15.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series14.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series13.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series12.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series11.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series10.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series9.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series8.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series7.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series6.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series5.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series4.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series3.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series2.View).BarWidth = 10;
            //((SideBySideBarSeriesView)series1.View).BarWidth = 10;

            //series1.Points.Add(new SeriesPoint("BONG", dtChart.Rows[0]["Bong"].ToString()));
            //series2.Points.Add(new SeriesPoint("SAW_SOLDER_OPEN", dtChart.Rows[0]["SAW_SOLDER_OPEN"].ToString()));
            //series3.Points.Add(new SeriesPoint("SW_CRACK", dtChart.Rows[0]["SW_CRACK"].ToString()));
            //series4.Points.Add(new SeriesPoint("SW_OPEN", dtChart.Rows[0]["SW_OPEN"].ToString()));
            //series5.Points.Add(new SeriesPoint("NGHI_NGO_SW", dtChart.Rows[0]["NGHI_NGO_SW"].ToString()));
            //series6.Points.Add(new SeriesPoint("LNA_OPEN", dtChart.Rows[0]["LNA_OPEN"].ToString()));
            //series7.Points.Add(new SeriesPoint("NHGI_NGO_LNA", dtChart.Rows[0]["NGHI_NGO_LNA"].ToString()));
            //series8.Points.Add(new SeriesPoint("NGHI_NGO_SW_LNA", dtChart.Rows[0]["NGHI_NGO_SW_LNA"].ToString()));
            //series9.Points.Add(new SeriesPoint("SAW_CRACK", dtChart.Rows[0]["SAW_CRACK"].ToString()));
            //series10.Points.Add(new SeriesPoint("BALL_OPEN", dtChart.Rows[0]["BALL_OPEN"].ToString()));
            //series11.Points.Add(new SeriesPoint("NHIEM_DIEN", dtChart.Rows[0]["NHIEM_DIEN"].ToString()));
            //series12.Points.Add(new SeriesPoint("LOANG_BAN", dtChart.Rows[0]["LOANG_BAN"].ToString()));
            //series13.Points.Add(new SeriesPoint("PATTERN", dtChart.Rows[0]["PATTERN"].ToString()));
            //series14.Points.Add(new SeriesPoint("XUOC", dtChart.Rows[0]["XUOC"].ToString()));
            //series15.Points.Add(new SeriesPoint("SAW_KHONG_LOI", dtChart.Rows[0]["SAW_KHONG_LOI"].ToString()));
            //series16.Points.Add(new SeriesPoint("LKG", dtChart.Rows[0]["LKG"].ToString()));
            //series17.Points.Add(new SeriesPoint("LOI_GIA", dtChart.Rows[0]["LOI_GIA"].ToString()));

            //chartControl1.Series.AddRange(new Series[] {series1, series2, series3, series4, series5, series6, series7, series8, series9,
            //                                            series10, series11, series12, series13, series14,
            //                                            series15, series16, series17 });

            Series series = new Series("Chart", ViewType.Bar);
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            (series.Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            series.Points.AddPoint("BONG", Convert.ToDouble(dtChart.Rows[0]["Bong"].ToString()));
            series.Points.AddPoint("SAW_SOLDER_OPEN", Convert.ToDouble(dtChart.Rows[0]["SAW_SOLDER_OPEN"].ToString()));
            series.Points.AddPoint("SW_CRACK", Convert.ToDouble(dtChart.Rows[0]["SW_CRACK"].ToString()));
            series.Points.AddPoint("SW_OPEN", Convert.ToDouble(dtChart.Rows[0]["SW_OPEN"].ToString()));
            series.Points.AddPoint("NGHI_NGO_SW", Convert.ToDouble(dtChart.Rows[0]["NGHI_NGO_SW"].ToString()));
            series.Points.AddPoint("LNA_OPEN", Convert.ToDouble(dtChart.Rows[0]["LNA_OPEN"].ToString()));
            series.Points.AddPoint("NHGI_NGO_LNA", Convert.ToDouble(dtChart.Rows[0]["NGHI_NGO_LNA"].ToString()));
            series.Points.AddPoint("NGHI_NGO_SW_LNA", Convert.ToDouble(dtChart.Rows[0]["NGHI_NGO_SW_LNA"].ToString()));
            series.Points.AddPoint("SAW_CRACK", Convert.ToDouble(dtChart.Rows[0]["SAW_CRACK"].ToString()));
            series.Points.AddPoint("BALL_OPEN", Convert.ToDouble(dtChart.Rows[0]["BALL_OPEN"].ToString()));
            series.Points.AddPoint("NHIEM_DIEN", Convert.ToDouble(dtChart.Rows[0]["NHIEM_DIEN"].ToString()));
            series.Points.AddPoint("LOANG_BAN", Convert.ToDouble(dtChart.Rows[0]["LOANG_BAN"].ToString()));
            series.Points.AddPoint("PATTERN", Convert.ToDouble(dtChart.Rows[0]["PATTERN"].ToString()));
            series.Points.AddPoint("XUOC", Convert.ToDouble(dtChart.Rows[0]["XUOC"].ToString()));
            series.Points.AddPoint("SAW_KHONG_LOI", Convert.ToDouble(dtChart.Rows[0]["SAW_KHONG_LOI"].ToString()));
            series.Points.AddPoint("LKG", Convert.ToDouble(dtChart.Rows[0]["LKG"].ToString()));
            series.Points.AddPoint("LOI_GIA", Convert.ToDouble(dtChart.Rows[0]["LOI_GIA"].ToString()));

            chartControl1.Series.Add(series);
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Alignment = StringAlignment.Center;
            diagram.AxisY.Title.Text = "PPM";
            //diagram.AxisY.Title.TextColor = Color.Blue;
            //diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Font = new Font("Tahoma", 11, FontStyle.Regular);


            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "Model " + cbModel.EditValue.ToString() + " Tháng " + txtMonth.Text.ToString().Trim();
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
