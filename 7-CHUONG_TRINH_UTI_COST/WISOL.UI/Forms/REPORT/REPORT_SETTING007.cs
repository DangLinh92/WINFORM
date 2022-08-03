using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT_SETTING007 : PageType
    {
        DataTable dtChart = new DataTable();
        DataTable dtChartTotal = new DataTable();

        public REPORT_SETTING007()
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
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            dtOpenDate.EditValue = firstDayOfMonth.ToString("yyyy-MM-dd");
            dtCloseDate.EditValue = lastDayOfMonth.ToString("yyyy-MM-dd");
            radioStock.EditValue = "STOCK";

            base.InitializePage();
        }
        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_RP_SETTING007.GET", new string[]
                {
                    "A_OPEN_DATE", "A_CLOSE_DATE", "A_STOCK"
                }, new string[] { dtOpenDate.DateTime.ToString("yyyyMMdd"), dtCloseDate.DateTime.ToString("yyyyMMdd"), radioStock.EditValue.NullString() });
                if(base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    dtChart = base.m_ResultDB.ReturnDataSet.Tables[0];
                    dtChartTotal = base.m_ResultDB.ReturnDataSet.Tables[1];

                    gvList.Columns["NAME_MATERIAL"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                    gvList.Columns["OS_AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["OS_AMOUNT"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["SI_AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["SI_AMOUNT"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["SO_AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["SO_AMOUNT"].DisplayFormat.FormatString = "n0";
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            // start chart 1
            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE_NAME", typeof(string));
            dt.Columns.Add("NAME_MATERIAL", typeof(string));
            dt.Columns.Add("VALUE", typeof(double));

            for(int i = 0; i < dtChart.Rows.Count; i++)
            {
                dt.Rows.Add(new object[] { "OS_AMOUNT".Translation(), dtChart.Rows[i]["NAME_MATERIAL"].ToString(), dtChart.Rows[i]["OS_AMOUNT"].ToString() });
                dt.Rows.Add(new object[] { "SI_AMOUNT".Translation(), dtChart.Rows[i]["NAME_MATERIAL"].ToString(), dtChart.Rows[i]["SI_AMOUNT"].ToString() });
                dt.Rows.Add(new object[] { "SO_AMOUNT".Translation(), dtChart.Rows[i]["NAME_MATERIAL"].ToString(), dtChart.Rows[i]["SO_AMOUNT"].ToString() });
            }

            chartReport1.DataSource = dt;

            chartReport1.SeriesDataMember = "VALUE_NAME";
            chartReport1.SeriesTemplate.ArgumentDataMember = "NAME_MATERIAL";
            chartReport1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "VALUE" });
            chartReport1.SeriesTemplate.View = new SideBySideBarSeriesView();

            for (int i = 0; i < chartReport1.Series.Count; i++)
            {
                if (i == 0)
                {
                    //chartReport1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    chartReport1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                    chartReport1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    (chartReport1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 1)
                {
                    //chartReport1.Series[i].View.Color = Color.FromArgb(237, 125, 49);
                    chartReport1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    //chartReport1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartReport1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
                if (i == 2)
                {
                    chartReport1.Series[i].View.Color = Color.FromArgb(165, 165, 165);
                    //chartReport1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //(chartReport1.Series[i].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
                }
            }
            chartReport1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartReport1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartReport1.Legend.Direction = LegendDirection.LeftToRight;

            XYDiagram diagram = (XYDiagram)chartReport1.Diagram;
            diagram.AxisY.WholeRange.Auto = true;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
            diagram.EnableAxisXZooming = true;
            diagram.EnableAxisYZooming = true;
            chartReport1.Dock = DockStyle.Fill;

            //start chart 2
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("VALUE_NAME", typeof(string));
            dt2.Columns.Add("NAME_MATERIAL", typeof(string));
            dt2.Columns.Add("VALUE", typeof(double));

            dt2.Rows.Add(new object[] { "TOTAL_OS_AMOUNT".Translation(), "TOTAL_OS_AMOUNT".Translation(), dtChartTotal.Rows[0]["OS_AMOUNT"].ToString() });
            dt2.Rows.Add(new object[] { "TOTAL_SI_AMOUNT".Translation(), "TOTAL_SI_AMOUNT".Translation(), dtChartTotal.Rows[0]["SI_AMOUNT"].ToString() });
            dt2.Rows.Add(new object[] { "TOTAL_SO_AMOUNT".Translation(), "TOTAL_SO_AMOUNT".Translation(), dtChartTotal.Rows[0]["SO_AMOUNT"].ToString() });

            chartTotal.DataSource = dt2;

            chartTotal.SeriesDataMember = "VALUE_NAME";
            chartTotal.SeriesTemplate.ArgumentDataMember = "NAME_MATERIAL";
            chartTotal.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "VALUE" });
            chartTotal.SeriesTemplate.View = new SideBySideBarSeriesView();

            chartTotal.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartTotal.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartTotal.Legend.Direction = LegendDirection.LeftToRight;
            XYDiagram diagram1 = (XYDiagram)chartTotal.Diagram;
            diagram1.AxisY.WholeRange.Auto = true;
            diagram1.AxisY.WholeRange.AlwaysShowZeroLevel = true;
            chartTotal.Dock = DockStyle.Fill;

            chartTotal.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            (chartTotal.Series[0].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            chartTotal.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            (chartTotal.Series[1].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
            chartTotal.Series[2].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            (chartTotal.Series[2].Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;
        }
    }
}
