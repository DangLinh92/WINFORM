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
    public partial class REPORT015 : PageType
    {
        public REPORT015()
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
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            int hour = DateTime.Now.Hour;
            if (hour >= 8)
            {
                dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            ComboBoxItemCollection coll = cbShiftWork.Properties.Items;
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

            txtMinutes.Properties.MinValue = 5;
            txtMinutes.Properties.MaxValue = 99;
            txtMinutes.Properties.Mask.EditMask = "\\d+";
            txtMinutes.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtMinutes.EditValue = 5;

            this.timerReport015.Enabled = false;
            this.timerReport015.Interval = 5000;
            this.timerReport015.Tick += new System.EventHandler(this.timerReport015_Tick);
        }

        public override void SearchPage()
        {
            base.SearchPage();
            C1();
        }


        private void C1()
        {
            //if (txtFromMonth.EditValue is null || txtToMonth.EditValue is null)
            //{
            //    MsgBox.Show("Please select month range.", MsgType.Warning);
            //    return;
            //}

            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            gcList.DataSource = null;

            DataTable results = CreateChartData(dtpDate.DateTime.ToString("yyyy-MM-dd"), cbShiftWork.EditValue.ToString());

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


        private DataTable CreateChartData(string date, string shift)
        {
            DataTable table = new DataTable("Table1");

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT015.GET_LIST"
                , new string[] { "A_DATE", "A_SHIFT_WORK"
                }
                , new string[] {date, shift
                }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    table.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]);
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

        private void timerReport015_Tick(object sender, EventArgs e)
        {
            int hour = DateTime.Now.Hour;
            if (hour >= 8)
            {
                dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            if (hour >= 8 && hour < 20)
            {
                cbShiftWork.SelectedIndex = 1;
            }
            if (hour >= 20 && hour < 24)
            {
                cbShiftWork.SelectedIndex = 2;
            }
            if (hour < 8)
            {
                cbShiftWork.SelectedIndex = 2;
            }
            this.SearchPage();
        }

        private void chkRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                int hour = DateTime.Now.Hour;
                if (hour >= 8)
                {
                    dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if(hour >= 8 && hour < 20)
                {
                    cbShiftWork.SelectedIndex = 1;
                }
                if(hour >= 20 && hour < 24)
                {
                    cbShiftWork.SelectedIndex = 2;
                }
                if(hour < 8)
                {
                    cbShiftWork.SelectedIndex = 2;
                }
                int minute = Int32.Parse(txtMinutes.EditValue.ToString());
                timerReport015.Interval = minute * 60 * 1000;
                timerReport015.Enabled = true;
                this.SearchPage();
            }
            else
            {
                int hour = DateTime.Now.Hour;
                if (hour >= 8)
                {
                    dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (hour >= 8 && hour < 20)
                {
                    cbShiftWork.SelectedIndex = 1;
                }
                if (hour >= 20 && hour < 24)
                {
                    cbShiftWork.SelectedIndex = 2;
                }
                if (hour < 8)
                {
                    cbShiftWork.SelectedIndex = 2;
                }
                timerReport015.Enabled = false;
                this.SearchPage();
            }
        }

        private void txtMinutes_EditValueChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                timerReport015.Enabled = false;
                int minute = Int32.Parse(txtMinutes.EditValue.ToString());
                timerReport015.Interval = minute * 60 * 1000;
                timerReport015.Enabled = true;
            }
        }
    }
}
