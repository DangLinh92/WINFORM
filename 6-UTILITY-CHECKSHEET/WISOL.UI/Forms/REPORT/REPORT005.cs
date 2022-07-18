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
            this.layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            dtpFromWeek.Properties.ShowWeekNumbers = true;
            dtpToWeek.Properties.ShowWeekNumbers = true;

            dtpFromWeek.Properties.Popup += From_Popup;
            dtpToWeek.Properties.Popup += To_Popup;

            dtpFrom2.EditValue = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dtpTo2.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
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
            ComboBoxItemCollection coll = cbYear.Properties.Items;
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

            string YearNow = DateTime.Now.Year.ToString();

            cbYear.SelectedText = YearNow;
        }

        public override void SearchPage()
        {
            base.SearchPage();

            if(tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005_1.GET_LIST"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE"
                        }
                        , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     DateTime.Now.AddMonths(-4).ToString("yyyy-MM") + "-01 08:00:00.000",
                                     DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01 08:00:00.000"
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        chartControl1.Series.Clear();
                        chartControl1.Titles.Clear();
                        chartControl2.Series.Clear();
                        chartControl2.Titles.Clear();
                        dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                        dt2 = base.m_ResultDB.ReturnDataSet.Tables[1];


                        Series line1 = new Series("NG ITEM RATE(%)", ViewType.Line);
                        for(int i = 0; i < dt1.Rows.Count; i++)
                        {
                            line1.Points.Add(new SeriesPoint(dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString()));
                        }

                        chartControl1.Series.AddRange(new Series[] { line1 });


                        XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                        //diagram.AxisX.Label.TextPattern = "{yyyy-MM}";
                        line1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //line1.Label.TextPattern = "{V:#,#0}";
                        line1.Label.TextPattern = "{V:n2}";
                        chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                        chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                        chartControl1.Legend.Direction = LegendDirection.LeftToRight;
                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = "NG ITEM RATE(%) BY MONTH";
                        chartControl1.Titles.Add(chartTitle1);
                        line1.View.Color = Color.Orange;

                        Series line2 = new Series("NG (TOP 10 ITEM) RATE(%)", ViewType.Line);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            line2.Points.Add(new SeriesPoint(dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString()));
                        }

                        chartControl2.Series.AddRange(new Series[] { line2 });


                        diagram = (XYDiagram)chartControl2.Diagram;
                        //diagram.AxisX.Label.TextPattern = "{yyyy-MM}";
                        line2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //line1.Label.TextPattern = "{V:#,#0}";
                        line2.Label.TextPattern = "{V:n2}";
                        chartControl2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                        chartControl2.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                        chartControl2.Legend.Direction = LegendDirection.LeftToRight;
                        ChartTitle chartTitle2 = new ChartTitle();
                        chartTitle2.Text = "NG (TOP 10 ITEM) RATE(%) BY MONTH";
                        chartControl2.Titles.Add(chartTitle2);
                        line2.View.Color = Color.Orange;
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
            }
            else if(tabbedControlGroup1.SelectedTabPageIndex == 1)
            {
                string year = cbYear.Text;
                if (string.IsNullOrWhiteSpace(year))
                {
                    MsgBox.Show("Please select year.", MsgType.Warning);
                    return;
                }

                if (year != "2020" && year != "2021" && year != "2022"
                   && year != "2023" && year != "2024" && year != "2025"
                   && year != "2026" && year != "2027" && year != "2028" && year != "2029")
                {
                    MsgBox.Show("Year is not valid!", MsgType.Warning);
                    return;
                }

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
                if (count > 7)
                {
                    MsgBox.Show("Week range max is 8.", MsgType.Warning);
                    return;
                }



                DataTable dt1 = new DataTable("Table1");
                DataTable dt2 = new DataTable("Table2");

                try
                {
                    DateTime firstDayOfWeek = new DateTime();
                    for (int c = intWeekFrom; c <= intWeekTo; c++)
                    {
                        int dem = 0;
                        string week = string.Empty;
                        if (c < 10)
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
                            dem = 7 - (int)firstDayOfWeek.DayOfWeek;
                        }
                        else if (c == 53)
                        {
                            dem = (int)(new DateTime(intYear, 12, 31)).DayOfWeek;
                            firstDayOfWeek = (new DateTime(intYear, 12, 31)).AddDays(-dem);
                        }
                        else
                        {
                            firstDayOfWeek = FirstDateOfWeekISO8601(intYear, c);
                            dem = 7;
                        }
                        string firstday = firstDayOfWeek.ToString("yyyy-MM-dd") + " 08:00:00.000";
                        string lastday = firstDayOfWeek.AddDays(dem).ToString("yyyy-MM-dd") + " 08:00:00.000";

                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005_2.GET_LIST"
                       , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE", "A_WEEK"
                       }
                       , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     firstday,
                                     lastday,
                                     week
                       }
                       );

                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            dt1.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]);
                            dt2.Merge(base.m_ResultDB.ReturnDataSet.Tables[1]);
                        }
                        else
                        {
                            MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                            return;
                        }
                    }

                    chartControl3.Series.Clear();
                    chartControl3.Titles.Clear();
                    chartControl4.Series.Clear();
                    chartControl4.Titles.Clear();


                    Series line1 = new Series("NG ITEM RATE(%)", ViewType.Line);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        line1.Points.Add(new SeriesPoint(dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString()));
                    }

                    chartControl3.Series.AddRange(new Series[] { line1 });


                    XYDiagram diagram = (XYDiagram)chartControl3.Diagram;
                    //diagram.AxisX.Label.TextPattern = "{yyyy-MM}";
                    line1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //line1.Label.TextPattern = "{V:#,#0}";
                    line1.Label.TextPattern = "{V:n2}";
                    chartControl3.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                    chartControl3.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                    chartControl3.Legend.Direction = LegendDirection.LeftToRight;
                    ChartTitle chartTitle1 = new ChartTitle();
                    chartTitle1.Text = "NG ITEM RATE(%) BY WEEK";
                    chartControl3.Titles.Add(chartTitle1);
                    line1.View.Color = Color.Orange;
                    //this.chartControl3.CacheToMemory = true;

                    Series line2 = new Series("NG (TOP 10 ITEM) RATE(%)", ViewType.Line);
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        line2.Points.Add(new SeriesPoint(dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString()));
                    }

                    chartControl4.Series.AddRange(new Series[] { line2 });


                    diagram = (XYDiagram)chartControl4.Diagram;
                    //diagram.AxisX.Label.TextPattern = "{yyyy-MM}";
                    line2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //line1.Label.TextPattern = "{V:#,#0}";
                    line2.Label.TextPattern = "{V:n2}";
                    chartControl4.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                    chartControl4.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                    chartControl4.Legend.Direction = LegendDirection.LeftToRight;
                    ChartTitle chartTitle2 = new ChartTitle();
                    chartTitle2.Text = "NG (TOP 10 ITEM) RATE(%) BY WEEK";
                    chartControl4.Titles.Add(chartTitle2);
                    line2.View.Color = Color.Orange;
                    
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
            }
            else if (tabbedControlGroup1.SelectedTabPageIndex == 2)
            {
                if((dtpTo2.DateTime - dtpFrom2.DateTime).TotalDays > 30)
                {
                    MsgBox.Show("Khoảng thời gian tối đa 30 ngày\r\nMax dates range is 30 days", MsgType.Warning);
                }

                DataTable dt1 = new DataTable("Table1");

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005_3.GET_LIST"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE"
                        }
                        , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     dtpFrom2.DateTime.ToString("yyyy-MM-dd") + " 08:00:00.000",
                                     dtpTo2.DateTime.ToString("yyyy-MM-dd") + " 08:00:00.000"
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        chartControl5.Series.Clear();
                        chartControl5.Titles.Clear();
                        dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];


                        Series line1 = new Series("NG ITEM RATE(%)", ViewType.Line);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            line1.Points.Add(new SeriesPoint(dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString()));
                        }

                        chartControl5.Series.AddRange(new Series[] { line1 });


                        XYDiagram diagram = (XYDiagram)chartControl5.Diagram;
                        //diagram.AxisX.Label.TextPattern = "{MM-dd}";
                        diagram.AxisY.WholeRange.SetMinMaxValues(0.0, 5.5);
                        //diagram.AxisX.Label.TextPattern = "{yyyy-MM}";
                        line1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //line1.Label.TextPattern = "{V:#,#0}";
                        line1.Label.TextPattern = "{V:n2}";
                        chartControl5.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                        chartControl5.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                        chartControl5.Legend.Direction = LegendDirection.LeftToRight;
                        ChartTitle chartTitle1 = new ChartTitle();
                        chartTitle1.Text = "NG ITEM RATE(%) BY DAY";
                        chartControl5.Titles.Add(chartTitle1);
                        line1.View.Color = Color.Orange;
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
            }
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

        private void chartControl3_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo hitInfo = chartControl3.CalcHitInfo(e.X, e.Y);
            var coords = ((XYDiagram2D)this.chartControl3.Diagram).PointToDiagram(new Point(e.X, e.Y)); // assuming that the diagram is shown below axis labels  
            string  XValue = coords.QualitativeArgument;

            int weekInt = Convert.ToInt32(XValue.Substring(1));
            int yearInt = Convert.ToInt32(cbYear.Text);

            DateTime firstDayOfWeek = new DateTime();
            int dem = 0;

            if (weekInt == 1)
            {
                firstDayOfWeek = new DateTime(yearInt, 1, 1);
                dem = 7 - (int)firstDayOfWeek.DayOfWeek;
            }
            else if (weekInt == 53)
            {
                dem = (int)(new DateTime(yearInt, 12, 31)).DayOfWeek;
                firstDayOfWeek = (new DateTime(yearInt, 12, 31)).AddDays(-dem);
            }
            else
            {
                firstDayOfWeek = FirstDateOfWeekISO8601(yearInt, weekInt);
                dem = 7;
            }
            string firstday = firstDayOfWeek.ToString("yyyy-MM-dd") + " 08:00:00.000";
            string lastday = firstDayOfWeek.AddDays(dem).ToString("yyyy-MM-dd") + " 08:00:00.000";

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005_2.POP_GET_LIST_1"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE", "A_WEEK"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     firstday,
                                     lastday,
                                     XValue
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    POP.POP_REPORT005_21 popup = new POP.POP_REPORT005_21(dt);
                    popup.ShowDialog();
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
        }

        private void chartControl1_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo hitInfo = chartControl1.CalcHitInfo(e.X, e.Y);
            var coords = ((XYDiagram2D)this.chartControl1.Diagram).PointToDiagram(new Point(e.X, e.Y)); // assuming that the diagram is shown below axis labels  
            string XValue = coords.QualitativeArgument;

            string month = XValue.Substring(1, 2);
            string year = XValue.Substring(4, 4);

            string firstday =  year + "-" + month + "-01 08:00:00.000";
            string lastday =  (new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1)).AddMonths(1).ToString("yyyy-MM-dd") + " 08:00:00.000";

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005_1.POP_GET_LIST_1"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE", "A_MONTH"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     firstday,
                                     lastday,
                                     XValue
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    POP.POP_REPORT005_21 popup = new POP.POP_REPORT005_21(dt);
                    popup.ShowDialog();
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
        }

        private void chartControl5_MouseDown(object sender, MouseEventArgs e)
        {
            ChartHitInfo hitInfo = chartControl5.CalcHitInfo(e.X, e.Y);
            var coords = ((XYDiagram2D)this.chartControl5.Diagram).PointToDiagram(new Point(e.X, e.Y)); // assuming that the diagram is shown below axis labels  
            string XValue = coords.DateTimeArgument.ToString("yyyy-MM-dd");


            string firstday = XValue + " 08:00:00.000";
            string lastday = Convert.ToDateTime(firstday).AddDays(1).ToString("yyyy-MM-dd") + " 08:00:00.000";

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005_3.POP_GET_LIST_1"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     firstday,
                                     lastday
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    POP.POP_REPORT005_21 popup = new POP.POP_REPORT005_21(dt);
                    popup.ShowDialog();
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
        }
    }
}
