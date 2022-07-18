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

namespace Wisol.MES.Dialog
{
    public partial class DialogueNoticeMinChemical : FormType
    {
        DataTable dtChart = new DataTable();

        public DialogueNoticeMinChemical()
        {
            InitializeComponent();
            Init_Control();
        }

        private void Init_Control()
        {
            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT_NOTICE.GET_LIST"
                    , new string[] { "A_PLANT", "A_LANG"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language
                    }
                    );
                if (mResultDB.ReturnInt == 0)
                {
                    dtChart = base.mResultDB.ReturnDataSet.Tables[0];
                    //dt2 = base.mResultDB.ReturnDataSet.Tables[1];
                    //dt1 = SetColumnsOrder(dt1, dt2);
                    base.mBindData.BindGridView(gcList,
                        dtChart
                        , true
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

            for (int i = 0; i < dtChart.Rows.Count; i++)
            {
                dt.Rows.Add(new object[] { "MIN_STOCK".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), dtChart.Rows[i]["MIN_STOCK"].ToString() });
                dt.Rows.Add(new object[] { "CURRENT_QUANTITY".Translation(), dtChart.Rows[i]["CHEMICAL_NAME"].ToString(), dtChart.Rows[i]["CURRENT_QUANTITY"].ToString() });
            }

            chartControl1.DataSource = dt;

            chartControl1.SeriesDataMember = "VALUE_NAME";
            chartControl1.SeriesTemplate.ArgumentDataMember = "CHEMICAL_NAME";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "VALUE" });

            chartControl1.SeriesTemplate.View = new SideBySideBarSeriesView();

            for (int i = 0; i < chartControl1.Series.Count; i++)
            {
                if (i == 0)
                {
                    //chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                    chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    (chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 1)
                {
                    //chartControl1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                    chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    (chartControl1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
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
            //diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            //diagram.AxisY.Title.Alignment = StringAlignment.Center;
            //diagram.AxisY.Title.Text = "PPM";
            //diagram.AxisY.Title.TextColor = Color.Blue;
            //diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            //diagram.AxisY.Title.Font = new Font("Tahoma", 11, FontStyle.Regular);


            diagram.AxisY.WholeRange.Auto = true;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

            //chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "Model " + cbModel.EditValue.ToString() + " Tháng " + txtFromMonth.Text.Trim() + "-" + txtToMonth.Text.Trim();
            //chartControl1.Titles.Add(chartTitle1);

            //series17.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //series17.Label.TextPattern = "{V:#,#}";
            //series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //series1.Label.TextPattern = "{V:#,#}";
            chartControl1.Dock = DockStyle.Fill;
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
