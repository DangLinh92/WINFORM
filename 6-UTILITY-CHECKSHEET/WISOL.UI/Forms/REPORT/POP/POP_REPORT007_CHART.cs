using DevExpress.XtraCharts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT007_CHART : FormType
    {
        public POP_REPORT007_CHART(string dv_id, string c_code, string c_name, string c_item_name, string previous_date, string current_date)
        {
            InitializeComponent();

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();

            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT001.GET_CHART"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ITEM_CHECK_ID",
                        "A_PREVIOUS_DATE",
                        "A_CURRENT_DATE"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        dv_id,
                        previous_date,
                        current_date
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.mResultDB.ReturnDataSet.Tables[0];
                    DataTable dt_d = new DataTable();
                    DataTable dt_n = new DataTable();

                    dt_d = dt.Select("SHIFT_WORK = 'DAY' ").CopyToDataTable();
                    dt_n = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();

                    Series line_day = new Series("DAY", ViewType.Line);
                    Series line_night = new Series("NIGHT", ViewType.Line);

                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(dt.Rows[i][2].ToString().ToUpper() == "DAY")
                        {
                            line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        }
                        if (dt.Rows[i][2].ToString().ToUpper() == "NIGHT")
                        {
                            line_night.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        }
                    }

                    chartControl1.Series.AddRange(new Series[] { line_day, line_night });
                    //chartControl1.DataSource = base.mResultDB.ReturnDataSet.Tables[0];
                    //chartControl1.SeriesDataMember = "SHIFT_WORK";
                    //chartControl1.SeriesTemplate.ArgumentDataMember = "DATECHECK";
                    //chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "value" });

                    //chartControl1.SeriesTemplate.View = new SideBySideBarSeriesView();


                    //for (int i = 0; i < chartControl1.Series.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                    //    }
                    //    if (i == 1)
                    //    {
                    //        chartControl1.Series[i].View.Color = Color.FromArgb(112, 173, 71);
                    //    }
                    //    chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                    //    BarSeriesLabel seriesLabel = chartControl1.Series[i].Label as BarSeriesLabel;
                    //    seriesLabel.BackColor = Color.White;
                    //    seriesLabel.Border.Color = Color.DarkSlateGray;
                    //    seriesLabel.Font = new Font("Tahoma", 10, FontStyle.Regular);
                    //    seriesLabel.Position = BarSeriesLabelPosition.Center;
                    //}

                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();
                    diagram.AxisX.Label.TextPattern = "{S:MM-dd}";

                    ConstantLine constantLine1 = new ConstantLine("MIN");
                    diagram.AxisY.ConstantLines.Add(constantLine1);
                    ConstantLine constantLine2 = new ConstantLine("MAX");
                    diagram.AxisY.ConstantLines.Add(constantLine2);

                    constantLine1.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                    constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                    constantLine1.LineStyle.Thickness = 2;
                    constantLine1.Color = Color.Red;
                    constantLine1.LegendText = "MIN: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();

                    constantLine2.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString());
                    constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                    constantLine2.LineStyle.Thickness = 2;
                    constantLine2.Color = Color.Red;
                    constantLine2.LegendText = "MAX: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString();

                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                    chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                    chartControl1.Legend.Direction = LegendDirection.LeftToRight;

                    ChartTitle chartTitle1 = new ChartTitle();
                    chartTitle1.Text = c_code + " - " + c_name + " - " + c_item_name;
                    chartControl1.Titles.Add(chartTitle1);

                    diagram.AxisY.WholeRange.Auto = false;
                    if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                    }
                    else
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                    }
                    diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                    //line_day.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //line_night.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    
                    line_day.View.Color = Color.Orange;
                    ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                    line_night.View.Color = Color.FromArgb(84, 154, 214);
                    ((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
