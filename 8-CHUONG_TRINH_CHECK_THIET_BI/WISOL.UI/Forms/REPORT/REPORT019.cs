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
    public partial class REPORT019 : PageType
    {
        public REPORT019()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            dtpFromMonth.Properties.ShowWeekNumbers = true;
            dtpFromMonth.Properties.Popup += From_Popup;
        }
        private void From_Popup(object sender, EventArgs e)
        {
            IPopupControl edit = sender as IPopupControl;
            PopupDateEditForm form = edit.PopupWindow as PopupDateEditForm;
            form.Calendar.MouseDown -= From_MouseDown;
            form.Calendar.MouseDown += From_MouseDown;
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

            //cbYear.SelectedIndex = 1;
            cbYear.SelectedText = YearNow;

            coll = cbShiftWork.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("All");
                coll.Add("Day");
                coll.Add("Night");
            }
            finally
            {
                coll.EndUpdate();
            }

            cbShiftWork.SelectedIndex = 0;
            cbShiftWork.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

        }

        public override void SearchPage()
        {
            base.SearchPage();
            C1();
        }


        private void C1()
        {
            if (txtFromMonth.EditValue is null)
            {
                MsgBox.Show("Hãy chọn tháng", MsgType.Warning);
                return;
            }
            string monthFrom = txtFromMonth.EditValue.ToString();
            int intYear = Convert.ToInt32(cbYear.Text);
            int intMonthFrom = Convert.ToInt32(monthFrom);

            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            gcList.DataSource = null;

            DataTable results = CreateChartData(intYear, intMonthFrom, cbShiftWork.EditValue.ToString());

            if (results.Rows.Count < 1)
            {
                return;
            }
            base.m_BindData.BindGridView(gcList, results);
            gvList.Columns[0].Width = 80;
            gvList.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvList.Columns[3].Width = 100;
            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "n0";
            gvList.Columns[4].Width = 100;
            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[4].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[5].Width = 100;
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[6].Width = 80;
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[7].Width = 80;
            gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[7].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[8].Width = 80;
            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[9].Width = 80;
            gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[9].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[10].Width = 80;
            gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[10].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[11].Width = 80;
            gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[11].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[12].Width = 80;
            gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[12].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[13].Width = 80;
            gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[13].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[14].Width = 80;
            gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[14].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[15].Width = 80;
            gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[15].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[16].Width = 80;
            gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[16].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[17].Width = 80;
            gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[17].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[18].Width = 80;
            gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[18].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[19].Width = 80;
            gvList.Columns[19].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[19].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[20].Width = 80;
            gvList.Columns[20].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[20].DisplayFormat.FormatString = "{0:##.#;;\"\"}";

            //gvList.Columns[18].Width = 80;
            //gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[18].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.OptionsView.ShowFooter = false;


            Series series1 = new Series("Thừa_Thiếc".Translation(), ViewType.StackedBar);
            series1.View.Color = Color.FromArgb(0, 128, 0);
            Series series2 = new Series("Thiếu_Thiếc".Translation(), ViewType.StackedBar);
            series2.View.Color = Color.FromArgb(204, 255, 255);
            Series series3 = new Series("Dị_Vật".Translation(), ViewType.StackedBar);
            series3.View.Color = Color.FromArgb(187, 153, 255);
            Series series4 = new Series("Vãi".Translation(), ViewType.StackedBar);
            series4.View.Color = Color.FromArgb(255, 128, 128);
            Series series5 = new Series("Short".Translation(), ViewType.StackedBar);
            series5.View.Color = Color.FromArgb(0, 204, 102);
            Series series6 = new Series("Mất".Translation(), ViewType.StackedBar);
            series6.View.Color = Color.FromArgb(0, 102, 204);
            Series series7 = new Series("Kênh".Translation(), ViewType.StackedBar);
            series7.View.Color = Color.FromArgb(204, 204, 255);
            Series series8 = new Series("Lệch".Translation(), ViewType.StackedBar);
            series8.View.Color = Color.FromArgb(51, 153, 255);
            Series series9 = new Series("Ngược".Translation(), ViewType.StackedBar);
            series9.View.Color = Color.FromArgb(255, 102, 204);
            Series series10 = new Series("Dựng".Translation(), ViewType.StackedBar);
            series10.View.Color = Color.FromArgb(230, 230, 0);
            Series series11 = new Series("Lật".Translation(), ViewType.StackedBar);
            series11.View.Color = Color.FromArgb(0, 255, 255);
            Series series12 = new Series("PCB_Loss".Translation(), ViewType.StackedBar);
            series12.View.Color = Color.FromArgb(153, 0, 153);
            Series series13 = new Series("Vỡ".Translation(), ViewType.StackedBar);
            series13.View.Color = Color.FromArgb(102, 153, 255);
            Series series14 = new Series("DST".Translation(), ViewType.StackedBar);
            series14.View.Color = Color.FromArgb(0, 128, 128);
            Series series15 = new Series("Lỗi_Khác".Translation(), ViewType.StackedBar);
            series15.View.Color = Color.FromArgb(204, 153, 0);
            //Series series16 = new Series("Sample".Translation(), ViewType.StackedBar);
            //series16.View.Color = Color.FromArgb(172, 230, 0);
            Series line = new Series("Input", ViewType.Line);

            for (int i = 0; i < results.Rows.Count - 1; i++)
            {
                series1.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][4].ToString()));
                series2.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][5].ToString()));
                series3.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][6].ToString()));
                series4.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][7].ToString()));
                series5.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][8].ToString()));
                series6.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][9].ToString()));
                series7.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][10].ToString()));
                series8.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][11].ToString()));
                series9.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][12].ToString()));
                series10.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][13].ToString()));
                series11.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][14].ToString()));
                series12.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][15].ToString()));
                series13.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][16].ToString()));
                series14.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][17].ToString()));
                series15.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][18].ToString()));
                //series16.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][19].ToString()));
                line.Points.Add(new SeriesPoint(results.Rows[i][0].ToString(), results.Rows[i][3].ToString()));
            }


            chartControl1.Series.AddRange(new Series[] { series1, series2, series3, series4, series5, series6, series7, series8, series9,
                                                         series10, series11, series12, series13, series14, series15, line});


            //XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            //diagram.AxisX.Label.TextPattern = "{yyyy-MM}";

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

            //diagram.AxisY.ConstantLines.Clear();
            //ConstantLine constantLine1 = new ConstantLine("");
            //diagram.AxisY.ConstantLines.Add(constantLine1);

            //constantLine1.AxisValue = txtTarget.EditValue;
            //constantLine1.LineStyle.DashStyle = DashStyle.Dash;
            //constantLine1.LineStyle.Thickness = 2;
            //constantLine1.Color = Color.Red;
            //constantLine1.ShowInLegend = true;
            //constantLine1.LegendText = "TARGET PPM(" + txtTarget.EditValue + ")";
            //constantLine1.ShowBehind = true;

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

            chartControl1.Dock = DockStyle.Fill;
        }


        private DataTable CreateChartData(int intYear, int intMonth, string shift)
        {
            DataTable table = new DataTable("Table1");

            DateTime firstDayOfMonth = new DateTime(intYear, intMonth, 1);
            string month = firstDayOfMonth.Month.ToString();
            if (month.Length == 1)
            {
                month = "M" + "0" + month;
                //month = "0" + month;
            }
            else
            {
                month = "M" + month;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT019.GET_LIST"
                , new string[] { "A_MONTH", "A_FIRST_DAY", "A_END_DAY", "A_SHIFT_WORK"
                }
                , new string[] {month, 
                                firstDayOfMonth.ToString("yyyy-MM-dd"),
                                firstDayOfMonth.AddMonths(1).ToString("yyyy-MM-dd"),
                                shift
                }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    table.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]);
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

            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    for (int j = 0; j < table.Columns.Count; j++)
            //    {
            //        if (string.IsNullOrWhiteSpace(table.Rows[i][j].ToString()))
            //        {
            //            table.Rows[i][j] = 0;
            //        }
            //    }
            //}

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

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            gvList.OptionsPrint.PrintFooter = false;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files(*.xlsx)|*.xlsx";
                saveDialog.FileName = "REPORT SMT ERROR DATA_" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
