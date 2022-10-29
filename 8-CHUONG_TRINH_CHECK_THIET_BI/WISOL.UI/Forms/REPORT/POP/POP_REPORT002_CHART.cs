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
    public partial class POP_REPORT002_CHART : FormType
    {
        string dv_id;
        string p_date;
        string c_date;
        public POP_REPORT002_CHART(string _dv_id, string c_code, string c_name, string c_item_name, string previous_date, string current_date)
        {
            InitializeComponent();

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            dv_id = _dv_id;
            p_date = previous_date;
            c_date = current_date;

            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT002.GET_CHART"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ITEM_CHECK_ID",
                        "A_PREVIOUS_DATE",
                        "A_CURRENT_DATE"
                    }
                    , new string[] { Consts.PLANT ,
                        Consts.DEPARTMENT,
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        dv_id,
                        previous_date,
                        current_date
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    if (Consts.DEPARTMENT == "ESD")
                    {
                        DataTable dt = base.mResultDB.ReturnDataSet.Tables[0];
                        DataTable dt_d = new DataTable();
                        DataTable dt_n = new DataTable();

                        //dt_d = dt.Select("SHIFT_WORK = 'DAY' ").CopyToDataTable();
                        //dt_n = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();

                        Series line_day = new Series("DAY", ViewType.Line);
                        //Series line_night = new Series("NIGHT", ViewType.Line);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            //if (dt.Rows[i][2].ToString().ToUpper() == "DAY")
                            //{
                            //    line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            //}
                            //if (dt.Rows[i][2].ToString().ToUpper() == "NIGHT")
                            //{
                            //    line_night.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            //}
                        }

                        chartControl1.Series.AddRange(new Series[] { line_day });
                        //chartControl1.Series.AddRange(new Series[] { line_day, line_night });
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

                        ConstantLine constantLine3 = new ConstantLine("DOWN 2");
                        diagram.AxisY.ConstantLines.Add(constantLine3);
                        ConstantLine constantLine4 = new ConstantLine("UP 2");
                        diagram.AxisY.ConstantLines.Add(constantLine4);

                        ConstantLine constantLine5 = new ConstantLine("DOWN 1");
                        diagram.AxisY.ConstantLines.Add(constantLine5);
                        ConstantLine constantLine6 = new ConstantLine("UP 1");
                        diagram.AxisY.ConstantLines.Add(constantLine6);

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

                        constantLine3.AxisValue = -20;
                        constantLine3.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine3.LineStyle.Thickness = 2;
                        constantLine3.Color = Color.Brown;
                        constantLine3.LegendText = "DOWN 2: -20";

                        constantLine4.AxisValue = 20;
                        constantLine4.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine4.LineStyle.Thickness = 2;
                        constantLine4.Color = Color.Brown;
                        constantLine4.LegendText = "UP 2: 20";

                        constantLine5.AxisValue = -10;
                        constantLine5.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine5.LineStyle.Thickness = 2;
                        constantLine5.Color = Color.Blue;
                        constantLine5.LegendText = "DOWN 1: -10";

                        constantLine6.AxisValue = 10;
                        constantLine6.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine6.LineStyle.Thickness = 2;
                        constantLine6.Color = Color.Blue;
                        constantLine6.LegendText = "UP 1: 10";

                        chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                        chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                        chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                        chartControl1.Legend.Direction = LegendDirection.LeftToRight;

                        ChartTitle chartTitle1 = new ChartTitle();
                        //chartTitle1.Text = c_code + " - " + c_name + " - " + c_item_name;
                        chartTitle1.Text = c_name + " - " + c_item_name;
                        chartControl1.Titles.Add(chartTitle1);

                        diagram.AxisY.WholeRange.Auto = false;
                        //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                        //{
                        //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                        //}
                        //else
                        //{
                        //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                        //}
                        diagram.AxisY.WholeRange.SetMinMaxValues(-60, 60);
                        diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                        //line_day.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //line_night.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                        line_day.View.Color = Color.Orange;
                        ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                        //line_night.View.Color = Color.FromArgb(84, 154, 214);
                        //((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                    }

                    if (Consts.DEPARTMENT == "EOS")
                    {
                        DataTable dt = base.mResultDB.ReturnDataSet.Tables[0];
                        DataTable dt_d = new DataTable();
                        DataTable dt_n = new DataTable();

                        dt_d = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();
                        //dt_n = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();

                        Series line_day = new Series("NIGHT", ViewType.Line);
                        //Series line_night = new Series("NIGHT", ViewType.Line);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][2].ToString().ToUpper() == "NIGHT")
                            {
                                line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            }
                            //if (dt.Rows[i][2].ToString().ToUpper() == "NIGHT")
                            //{
                            //    line_night.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                            //}
                        }

                        chartControl1.Series.AddRange(new Series[] { line_day });
                        //chartControl1.Series.AddRange(new Series[] { line_day, line_night });
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

                        //ConstantLine constantLine3 = new ConstantLine("DOWN 2");
                        //diagram.AxisY.ConstantLines.Add(constantLine3);
                        //ConstantLine constantLine4 = new ConstantLine("UP 2");
                        //diagram.AxisY.ConstantLines.Add(constantLine4);

                        //ConstantLine constantLine5 = new ConstantLine("DOWN 1");
                        //diagram.AxisY.ConstantLines.Add(constantLine5);
                        //ConstantLine constantLine6 = new ConstantLine("UP 1");
                        //diagram.AxisY.ConstantLines.Add(constantLine6);

                        constantLine1.AxisValue = 0;// Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                        constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine1.LineStyle.Thickness = 2;
                        constantLine1.Color = Color.Red;
                        constantLine1.LegendText = "MIN: 0";// + base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();

                        constantLine2.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                        constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                        constantLine2.LineStyle.Thickness = 2;
                        constantLine2.Color = Color.Red;
                        constantLine2.LegendText = "MAX: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();

                        //constantLine3.AxisValue = -20;
                        //constantLine3.LineStyle.DashStyle = DashStyle.Dash;
                        //constantLine3.LineStyle.Thickness = 2;
                        //constantLine3.Color = Color.Brown;
                        //constantLine3.LegendText = "DOWN 2: -20";

                        //constantLine4.AxisValue = 20;
                        //constantLine4.LineStyle.DashStyle = DashStyle.Dash;
                        //constantLine4.LineStyle.Thickness = 2;
                        //constantLine4.Color = Color.Brown;
                        //constantLine4.LegendText = "UP 2: 20";

                        //constantLine5.AxisValue = -10;
                        //constantLine5.LineStyle.DashStyle = DashStyle.Dash;
                        //constantLine5.LineStyle.Thickness = 2;
                        //constantLine5.Color = Color.Blue;
                        //constantLine5.LegendText = "DOWN 1: -10";

                        //constantLine6.AxisValue = 10;
                        //constantLine6.LineStyle.DashStyle = DashStyle.Dash;
                        //constantLine6.LineStyle.Thickness = 2;
                        //constantLine6.Color = Color.Blue;
                        //constantLine6.LegendText = "UP 1: 10";

                        chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                        chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                        chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                        chartControl1.Legend.Direction = LegendDirection.LeftToRight;

                        ChartTitle chartTitle1 = new ChartTitle();
                        //chartTitle1.Text = c_code + " - " + c_name + " - " + c_item_name;
                        chartTitle1.Text = c_name + " - " + c_item_name;
                        chartControl1.Titles.Add(chartTitle1);

                        diagram.AxisY.WholeRange.Auto = false;
                        //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                        //{
                        //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                        //}
                        //else
                        //{
                        //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                        //}
                        diagram.AxisY.WholeRange.SetMinMaxValues(0, 2);
                        diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                        //line_day.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //line_night.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                        line_day.View.Color = Color.Orange;
                        ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                        //line_night.View.Color = Color.FromArgb(84, 154, 214);
                        //((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                    }
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            string From = dateFrom.DateTime.ToString("yyyy-MM-dd");
            string To = dateTo.DateTime.ToString("yyyy-MM-dd");
            if (From != "0001-01-01" && To == "0001-01-01")
            {
                MsgBox.Show("Hãy nhập khoảng thời gian đầy đủ.\r\n\r\nPlease enter the full time period.", MsgType.Warning);
                return;
            }
            if (From == "0001-01-01" && To != "0001-01-01")
            {
                MsgBox.Show("Hãy nhập khoảng thời gian đầy đủ.\r\n\r\nPlease enter the full time period.", MsgType.Warning);
                return;
            }
            if ((From != "0001-01-01" && To != "0001-01-01") && DateTime.Compare(dateFrom.DateTime, dateTo.DateTime) > 0)
            {
                MsgBox.Show("Khoảng thời gian không hợp lệ.\r\n\r\nTime period is invalid.", MsgType.Warning);
                return;
            }

            string bottom = txtBottom.Text.Trim();
            string top = txtTop.Text.Trim();
            if (!string.IsNullOrWhiteSpace(bottom) && string.IsNullOrWhiteSpace(top))
            {
                MsgBox.Show("Hãy nhập giá trị Bottom và Top đầy đủ.\r\n\r\nPlease enter full Bottom and Top values.", MsgType.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(bottom) && !string.IsNullOrWhiteSpace(top))
            {
                MsgBox.Show("Hãy nhập giá trị Bottom và Top đầy đủ.\r\n\r\nPlease enter full Bottom và Top values.", MsgType.Warning);
                return;
            }

            float bottom_out = 0f;
            float top_out = 0f;
            if (!string.IsNullOrWhiteSpace(bottom) && bottom.Contains(","))
            {
                MsgBox.Show("Giá trị Bottom không hợp lệ.\r\n\r\nBottom value is invalid.", MsgType.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(bottom) && !float.TryParse(bottom, out bottom_out))
            {
                MsgBox.Show("Bottom phải là một số.\r\n\r\nBottom value must be digit.", MsgType.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(top) && top.Contains(","))
            {
                MsgBox.Show("Giá trị Top không hợp lệ.\r\n\r\nTop value is invalid.", MsgType.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(top) && !float.TryParse(top, out top_out))
            {
                MsgBox.Show("Top phải là một số.\r\n\r\nTop value must be digit.", MsgType.Warning);
                return;
            }

            if ((!string.IsNullOrWhiteSpace(bottom) && !string.IsNullOrWhiteSpace(top)) && bottom_out > top_out)
            {
                MsgBox.Show("Bottom không được lớn hơn Top.\r\n\r\nBottom cannot be bigger than Top.", MsgType.Warning);
                return;
            }


            try
            {
                if (From == "0001-01-01")
                {
                    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT002.GET_CHART"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ITEM_CHECK_ID",
                        "A_PREVIOUS_DATE",
                        "A_CURRENT_DATE"
                    }
                    , new string[] { Consts.PLANT ,
                        Consts.DEPARTMENT,
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        dv_id,
                        p_date,
                        c_date
                    }
                    ); ;
                }
                else
                {
                    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT002.GET_CHART_MORE"
                        , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ITEM_CHECK_ID",
                        "A_FROM_DATE",
                        "A_TO_DATE"
                        }
                        , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        dv_id,
                        From,
                        To
                        }
                        ); ;
                }
                if (base.mResultDB.ReturnInt == 0)
                {
                    chartControl1.DataSource = null;
                    chartControl1.Series.Clear();
                    chartControl1.Titles.Clear();
                    DataTable dt = base.mResultDB.ReturnDataSet.Tables[0];
                    DataTable dt_d = new DataTable();
                    DataTable dt_n = new DataTable();

                    string shift_work = "DAY";
                    bool contain1 = dt.AsEnumerable().Any(row => shift_work == row.Field<String>("SHIFT_WORK"));
                    if (contain1)
                    {
                        dt_d = dt.Select("SHIFT_WORK = 'DAY' ").CopyToDataTable();
                    }

                    shift_work = "NIGHT";
                    bool contain2 = dt.AsEnumerable().Any(row => shift_work == row.Field<String>("SHIFT_WORK"));
                    if (contain2)
                    {
                        dt_n = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();
                    }

                    Series line_day = new Series("DAY", ViewType.Line);
                    Series line_night = new Series("NIGHT", ViewType.Line);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        //if (dt.Rows[i][2].ToString().ToUpper() == "DAY")
                        //{
                        //    line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        //}
                        //if (dt.Rows[i][2].ToString().ToUpper() == "NIGHT")
                        //{
                        //    line_night.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        //}
                    }

                    chartControl1.Series.AddRange(new Series[] { line_day });


                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();
                    diagram.AxisX.Label.TextPattern = "{S:MM-dd}";


                    //if (!string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["minvalue"].ToString()))
                    //{
                    //    ConstantLine constantLine1 = new ConstantLine("MIN");
                    //    diagram.AxisY.ConstantLines.Add(constantLine1);
                    //    constantLine1.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["minvalue"].ToString());
                    //    constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                    //    constantLine1.LineStyle.Thickness = 2;
                    //    constantLine1.Color = Color.Red;
                    //    constantLine1.LegendText = "MIN: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["minvalue"].ToString();
                    //}

                    //if (!string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["maxvalue"].ToString()))
                    //{
                    //    ConstantLine constantLine2 = new ConstantLine("MAX");
                    //    diagram.AxisY.ConstantLines.Add(constantLine2);
                    //    constantLine2.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["maxvalue"].ToString());
                    //    constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                    //    constantLine2.LineStyle.Thickness = 2;
                    //    constantLine2.Color = Color.Red;
                    //    constantLine2.LegendText = "MAX: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["maxvalue"].ToString();
                    //}

                    //chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    //chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                    //chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                    //chartControl1.Legend.Direction = LegendDirection.LeftToRight;


                    diagram.AxisY.WholeRange.Auto = false;
                    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) < 0)
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 10, 0);
                    }
                    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) > 0)
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 10, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) * 3);
                    }


                    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) < 0)
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) * 3, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 10);
                    }
                    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 10);
                    }

                    //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0 && !string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()))
                    //{
                    //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 5, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 5);
                    //}
                    //else
                    //{
                    //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 5, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 5);
                    //}
                    if (!string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) && !string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()))
                    {
                        float min_value = (float)Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                        float max_value = (float)Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString());
                        float max_min = max_value - min_value;

                        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - Convert.ToDecimal(max_min), Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + Convert.ToDecimal(max_min));
                    }

                    if (!string.IsNullOrWhiteSpace(bottom))
                    {
                        diagram.AxisY.WholeRange.SetMinMaxValues(bottom_out, top_out);
                    }

                    line_day.View.Color = Color.Orange;
                    ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                    //line_night.View.Color = Color.FromArgb(84, 154, 214);
                    //((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                //MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
