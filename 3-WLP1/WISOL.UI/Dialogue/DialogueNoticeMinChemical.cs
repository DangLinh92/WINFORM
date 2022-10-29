using System;
using System.Data;
using Wisol.Components;

using Wisol.MES.Inherit;
using System.Windows.Forms;
using PROJ_B_DLL.Objects;
using System.IO;
using Wisol.Common;
using System.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;

namespace Wisol.MES.Dialog
{
    public partial class DialogueNoticeMinChemical : FormType
    {
        DataTable dtChart = new DataTable();

        public DialogueNoticeMinChemical()
        {
            InitializeComponent();
            RadioGroupItem item1 = new RadioGroupItem();
            item1.Description = "One_Week".Translation();
            RadioGroupItem item2 = new RadioGroupItem();
            item2.Description = "Two_Week".Translation();
            RadioGroupItem item3 = new RadioGroupItem();
            item3.Description = "Three_Week".Translation();
            RadioGroupItem item4 = new RadioGroupItem();
            item4.Description = "Four_Week".Translation();
            RadioGroupItem item5 = new RadioGroupItem();
            item5.Description = "Six_Week".Translation();
            RadioGroupItem item6 = new RadioGroupItem();
            item6.Description = "Eight_Week".Translation();
            RadioGroupItem item7 = new RadioGroupItem();
            item7.Description = "Over_Eight".Translation();
            radioTime.Properties.Items.Add(item1);
            radioTime.Properties.Items.Add(item2);
            radioTime.Properties.Items.Add(item3);
            radioTime.Properties.Items.Add(item4);
            radioTime.Properties.Items.Add(item5);
            radioTime.Properties.Items.Add(item6);
            radioTime.Properties.Items.Add(item7);
            radioTime.SelectedIndex = 3;
            //Init_Control();
        }

        private void Init_Control()
        {
            this.chartControl1.Series.Clear();
            this.chartControl1.Titles.Clear();
            string date_expected = string.Empty;

            int index = radioTime.SelectedIndex;
            if (index == 0)
            {
                date_expected = DateTime.Now.AddDays(7).ToString("yyyyMMdd");
            }
            else if (index == 1)
            {
                date_expected = DateTime.Now.AddDays(14).ToString("yyyyMMdd");
            }
            else if (index == 2)
            {
                date_expected = DateTime.Now.AddDays(21).ToString("yyyyMMdd");
            }
            else if (index == 3)
            {
                date_expected = DateTime.Now.AddDays(28).ToString("yyyyMMdd");
            }
            else if (index == 4)
            {
                date_expected = DateTime.Now.AddDays(42).ToString("yyyyMMdd");
            }
            else if (index == 5)
            {
                date_expected = DateTime.Now.AddDays(56).ToString("yyyyMMdd");
            }
            else if (index == 6)
            {
                date_expected = DateTime.Now.AddDays(3650).ToString("yyyyMMdd");
            }

            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT_NOTICE.GET_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DATE_EXPECTED"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, date_expected
                    }
                    );
                if (mResultDB.ReturnInt == 0)
                {
                    dtChart = base.mResultDB.ReturnDataSet.Tables[0];
                    //dt2 = base.mResultDB.ReturnDataSet.Tables[1];
                    //dt1 = SetColumnsOrder(dt1, dt2);
                    base.mBindData.BindGridView(gcList,
                        dtChart
                        , false
                        );

                    gvList.OptionsView.ShowFooter = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            if (dtChart.Rows.Count < 1)
            {
                MsgBox.Show("Không có dữ liệu.", MsgType.Warning);
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE_NAME", typeof(string));
            dt.Columns.Add("CHEMICAL_NAME", typeof(string));
            dt.Columns.Add("VALUE", typeof(double));

            //for (int i = 0; i < dtChart.Rows.Count; i++)
            //{
            //    dt.Rows.Add(new object[] { "MIN_STOCK".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), dtChart.Rows[i]["MIN_STOCK"].ToString() });
            //    dt.Rows.Add(new object[] { "CURRENT_QUANTITY".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), dtChart.Rows[i]["CURRENT_QUANTITY"].ToString() });
            //}

            //chartControl1.DataSource = dt;

            //chartControl1.SeriesDataMember = "VALUE_NAME";
            //chartControl1.SeriesTemplate.ArgumentDataMember = "CHEMICAL_NAME";
            //chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "VALUE" });

            //chartControl1.SeriesTemplate.View = new SideBySideBarSeriesView();

            //for (int i = 0; i < chartControl1.Series.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        //chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
            //        chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
            //        chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        (chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //    if (i == 1)
            //    {
            //        //chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
            //        chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
            //        chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        (chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //    if (i == 2)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(165, 165, 165);
            //        //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //    if (i == 3)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(255, 192, 0);
            //        //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //    if (i == 4)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(112, 173, 71);
            //        //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //    if (i == 5)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(37, 94, 145);
            //        //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //    if (i == 6)
            //    {
            //        chartControl1.Series[i].View.Color = Color.FromArgb(122, 122, 82);
            //        //chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //        //(chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            //    }
            //}

            //chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            //chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            //chartControl1.Legend.Direction = LegendDirection.LeftToRight;

            ////chartControl1.Series.Add(series);
            //XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            ////diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ////diagram.AxisY.Title.Alignment = StringAlignment.Center;
            ////diagram.AxisY.Title.Text = "PPM";
            ////diagram.AxisY.Title.TextColor = Color.Blue;
            ////diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            ////diagram.AxisY.Title.Font = new Font("Tahoma", 11, FontStyle.Regular);


            //diagram.AxisY.WholeRange.Auto = true;
            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            ////chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            ////ChartTitle chartTitle1 = new ChartTitle();
            ////chartTitle1.Text = "Model " + cbModel.EditValue.ToString() + " Tháng " + txtFromMonth.Text.Trim() + "-" + txtToMonth.Text.Trim();
            ////chartControl1.Titles.Add(chartTitle1);

            ////series17.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ////series17.Label.TextPattern = "{V:#,#}";
            ////series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ////series1.Label.TextPattern = "{V:#,#}";
            //chartControl1.Dock = DockStyle.Fill;

            //=====================================================
            //=====================================================
            //===== 2020-09-05

            for (int i = 0; i < dtChart.Rows.Count; i++)
            {
                dt.Rows.Add(new object[] { "MIN_STOCK".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), dtChart.Rows[i]["MIN_STOCK"].ToString() });
                dt.Rows.Add(new object[] { "QUANTITY".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), dtChart.Rows[i]["QUANTITY"].ToString() });
                dt.Rows.Add(new object[] { "LUONG_CHUA_NHAP".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), string.IsNullOrWhiteSpace(dtChart.Rows[i]["LUONG_CHUA_NHAP"].ToString()) ? "0" : dtChart.Rows[i]["LUONG_CHUA_NHAP"].ToString() });
            }

            //ChartControl stackedBarChart = new ChartControl();

            Series series1 = new Series("MIN_STOCK".Translation(), ViewType.SideBySideStackedBar);
            Series series2 = new Series("QUANTITY".Translation(), ViewType.SideBySideStackedBar);
            Series series3 = new Series("LUONG_CHUA_NHAP".Translation(), ViewType.SideBySideStackedBar);

            series1.View.Color = Color.FromArgb(237, 125, 49);
            series2.View.Color = Color.FromArgb(91, 155, 213);
            series3.View.Color = Color.FromArgb(146, 208, 80);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 3 == 0)
                {
                    series1.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), Convert.ToInt32(dt.Rows[i][2].ToString())));
                }
                if (i % 3 == 1)
                {
                    series2.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), Convert.ToInt32(dt.Rows[i][2].ToString())));
                }
                if (i % 3 == 2)
                {
                    series3.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), Convert.ToInt32(dt.Rows[i][2].ToString())));
                }
            }

            chartControl1.Series.AddRange(new Series[] { series1, series2, series3 });
            StackedBarTotalLabel totalLabel = ((XYDiagram)chartControl1.Diagram).DefaultPane.StackedBarTotalLabel;
            totalLabel.Visible = true;
            totalLabel.ShowConnector = true;
            totalLabel.TextPattern = "{TV:F0}";

            //Group the first two series under the same stack.

            ((SideBySideStackedBarSeriesView)series2.View).StackedGroup = 0;
            ((SideBySideStackedBarSeriesView)series3.View).StackedGroup = 0;

            // Access the type-specific options of the diagram.
            ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary).
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartControl1.Legend.Direction = LegendDirection.LeftToRight;

            //// Add a title to the chart (if necessary).
            //chartControl1.Titles.Add(new ChartTitle());
            //chartControl1.Titles[0].Text = "A Side-By-Side Stacked Bar Chart";
            //chartControl1.Titles[0].WordWrap = true;

            // Add the chart to the form.
            chartControl1.Dock = DockStyle.Fill;
        }

        private void radioTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Init_Control();
        }

        //private static DataTable SetColumnsOrder(DataTable table, DataTable columnNames)
        //{
        //    int columnIndex = 4;

        //    for (int i = 0; i < columnNames.Rows.Count; i++)
        //    {
        //        table.Columns[columnNames.Rows[i][0].ToString()].SetOrdinal(columnIndex);
        //        columnIndex++;
        //    }
        //    return table;
        //}

    }
}
