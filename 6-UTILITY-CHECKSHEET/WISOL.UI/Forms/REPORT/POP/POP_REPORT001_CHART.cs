using DevExpress.XtraCharts;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT001_CHART : FormType
    {
        string id = string.Empty;
        string code = string.Empty;
        string name = string.Empty;
        string item_name = string.Empty;
        string previousDate = string.Empty;
        string currentDate = string.Empty;
        public POP_REPORT001_CHART(string dv_id, string c_code, string c_name, string c_item_name, string previous_date, string current_date)
        {
            InitializeComponent();

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();

            rgDay.EditValue = "30";

            id = dv_id;
            code = c_code;
            name = c_name;
            item_name = c_item_name;
            previousDate = previous_date;
            currentDate = current_date;



            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT001.GET_CHART"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ITEM_CHECK_ID",
                        "A_PREVIOUS_DATE",
                        "A_CURRENT_DATE",
                        "A_DAYS"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        dv_id,
                        previous_date,
                        current_date,
                        "30"
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    string tu_dien = string.Empty;
                    if (!string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString()))
                    {
                        tu_dien = base.mResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString();
                        //if (tu_dien.ToUpper() == "N")
                        //{
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
                                if (dt.Rows[i][2].ToString().ToUpper() == "DAY")
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
                            //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                            //}
                            //else
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                            //}
                            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;


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

                            //if (!string.IsNullOrWhiteSpace(bottom))
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(bottom_out, top_out);
                            //}


                            //line_day.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                            //line_night.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                            line_day.View.Color = Color.Orange;
                            ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                            ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                            line_night.View.Color = Color.FromArgb(84, 154, 214);
                            ((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                            ((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                        //}

                        //if (tu_dien.ToUpper() == "Y")
                        //{
                        //    DataTable dt = base.mResultDB.ReturnDataSet.Tables[0];
                        //    DataTable dt_d = new DataTable();
                        //    DataTable dt_n = new DataTable();

                        //    dt_d = dt.Select("SHIFT_WORK = 'DAY' ").CopyToDataTable();
                        //    //dt_n = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();

                        //    Series line_day = new Series("DAY", ViewType.Line);
                        //    //Series line_night = new Series("NIGHT", ViewType.Line);

                        //    for (int i = 0; i < dt.Rows.Count; i++)
                        //    {
                        //        if (dt.Rows[i][2].ToString().ToUpper() == "DAY")
                        //        {
                        //            line_day.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        //        }
                        //        //if (dt.Rows[i][2].ToString().ToUpper() == "NIGHT")
                        //        //{
                        //        //    line_night.Points.Add(new SeriesPoint(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                        //        //}
                        //    }

                        //    chartControl1.Series.AddRange(new Series[] { line_day });
                        //    //chartControl1.Series.AddRange(new Series[] { line_day, line_night });
                        //    //chartControl1.DataSource = base.mResultDB.ReturnDataSet.Tables[0];
                        //    //chartControl1.SeriesDataMember = "SHIFT_WORK";
                        //    //chartControl1.SeriesTemplate.ArgumentDataMember = "DATECHECK";
                        //    //chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "value" });

                        //    //chartControl1.SeriesTemplate.View = new SideBySideBarSeriesView();


                        //    //for (int i = 0; i < chartControl1.Series.Count; i++)
                        //    //{
                        //    //    if (i == 0)
                        //    //    {
                        //    //        chartControl1.Series[i].View.Color = Color.FromArgb(91, 155, 213);
                        //    //    }
                        //    //    if (i == 1)
                        //    //    {
                        //    //        chartControl1.Series[i].View.Color = Color.FromArgb(112, 173, 71);
                        //    //    }
                        //    //    chartControl1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                        //    //    BarSeriesLabel seriesLabel = chartControl1.Series[i].Label as BarSeriesLabel;
                        //    //    seriesLabel.BackColor = Color.White;
                        //    //    seriesLabel.Border.Color = Color.DarkSlateGray;
                        //    //    seriesLabel.Font = new Font("Tahoma", 10, FontStyle.Regular);
                        //    //    seriesLabel.Position = BarSeriesLabelPosition.Center;
                        //    //}

                        //    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                        //    diagram.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();
                        //    diagram.AxisX.Label.TextPattern = "{S:MM-dd}";

                        //    ConstantLine constantLine1 = new ConstantLine("MIN");
                        //    diagram.AxisY.ConstantLines.Add(constantLine1);
                        //    ConstantLine constantLine2 = new ConstantLine("MAX");
                        //    diagram.AxisY.ConstantLines.Add(constantLine2);


                        //    constantLine1.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                        //    constantLine1.LineStyle.DashStyle = DashStyle.Dash;
                        //    constantLine1.LineStyle.Thickness = 2;
                        //    constantLine1.Color = Color.Red;
                        //    constantLine1.LegendText = "MIN: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();

                        //    constantLine2.AxisValue = Convert.ToDouble(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString());
                        //    constantLine2.LineStyle.DashStyle = DashStyle.Dash;
                        //    constantLine2.LineStyle.Thickness = 2;
                        //    constantLine2.Color = Color.Red;
                        //    constantLine2.LegendText = "MAX: " + base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString();



                        //    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                        //    chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                        //    chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                        //    chartControl1.Legend.Direction = LegendDirection.LeftToRight;

                        //    ChartTitle chartTitle1 = new ChartTitle();
                        //    //chartTitle1.Text = c_code + " - " + c_name + " - " + c_item_name;
                        //    chartTitle1.Text = c_name + " - " + c_item_name;
                        //    chartControl1.Titles.Add(chartTitle1);

                        //    diagram.AxisY.WholeRange.Auto = false;
                        //    //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                        //    //{
                        //    //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                        //    //}
                        //    //else
                        //    //{
                        //    //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                        //    //}
                        //    //diagram.AxisY.WholeRange.SetMinMaxValues(-60, 60);
                        //    //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                        //    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) < 0)
                        //    {
                        //        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 10, 0);
                        //    }
                        //    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) > 0)
                        //    {
                        //        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 10, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) * 3);
                        //    }


                        //    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) < 0)
                        //    {
                        //        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) * 3, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 10);
                        //    }
                        //    if (string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) && Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                        //    {
                        //        diagram.AxisY.WholeRange.SetMinMaxValues(0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 10);
                        //    }

                        //    //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0 && !string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()))
                        //    //{
                        //    //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 5, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 5);
                        //    //}
                        //    //else
                        //    //{
                        //    //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 5, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 5);
                        //    //}
                        //    if (!string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) && !string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()))
                        //    {
                        //        float min_value = (float)Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                        //        float max_value = (float)Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString());
                        //        float max_min = max_value - min_value;

                        //        diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - Convert.ToDecimal(max_min), Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + Convert.ToDecimal(max_min));
                        //    }

                        //    //if (!string.IsNullOrWhiteSpace(bottom))
                        //    //{
                        //    //    diagram.AxisY.WholeRange.SetMinMaxValues(bottom_out, top_out);
                        //    //}

                        //    //line_day.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //    //line_night.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                        //    line_day.View.Color = Color.Orange;
                        //    ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //    ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                        //    //line_night.View.Color = Color.FromArgb(84, 154, 214);
                        //    //((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                        //    //((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                        //}
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

        private void rgDay_EditValueChanged(object sender, EventArgs e)
        {
            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            try
            {
                if (rgDay.EditValue.NullString() == null || id.IsNullOrEmpty() || previousDate.IsNullOrEmpty() || currentDate.IsNullOrEmpty())
                {
                    return;
                }

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT001.GET_CHART"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ITEM_CHECK_ID",
                        "A_PREVIOUS_DATE",
                        "A_CURRENT_DATE",
                        "A_DAYS"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        id,
                        previousDate,
                        currentDate,
                        rgDay.EditValue.NullString()
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    string tu_dien = string.Empty;
                    if (!string.IsNullOrWhiteSpace(base.mResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString()))
                    {
                        tu_dien = base.mResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString();
                        if (tu_dien.ToUpper() == "N")
                        {
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
                                if (dt.Rows[i][2].ToString().ToUpper() == "DAY")
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
                            chartTitle1.Text = code + " - " + name + " - " + item_name;
                            //chartControl1.Titles.Add(chartTitle1);

                            diagram.AxisY.WholeRange.Auto = false;
                            //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                            //}
                            //else
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                            //}
                            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                            diagram.AxisY.WholeRange.Auto = false;
                            //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                            //}
                            //else
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                            //}
                            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;


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

                            //if (!string.IsNullOrWhiteSpace(bottom))
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(bottom_out, top_out);
                            //}

                            //line_day.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                            //line_night.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                            line_day.View.Color = Color.Orange;
                            ((LineSeriesView)line_day.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                            ((LineSeriesView)line_day.View).LineMarkerOptions.Kind = MarkerKind.Circle;

                            line_night.View.Color = Color.FromArgb(84, 154, 214);
                            ((LineSeriesView)line_night.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                            ((LineSeriesView)line_night.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                        }

                        if (tu_dien.ToUpper() == "Y")
                        {
                            DataTable dt = base.mResultDB.ReturnDataSet.Tables[0];
                            DataTable dt_d = new DataTable();
                            DataTable dt_n = new DataTable();

                            dt_d = dt.Select("SHIFT_WORK = 'DAY' ").CopyToDataTable();
                            //dt_n = dt.Select("SHIFT_WORK = 'NIGHT' ").CopyToDataTable();

                            Series line_day = new Series("DAY", ViewType.Line);
                            //Series line_night = new Series("NIGHT", ViewType.Line);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][2].ToString().ToUpper() == "DAY")
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
                            //chartTitle1.Text = c_code + " - " + c_name + " - " + c_item_name;
                            chartTitle1.Text = name + " - " + item_name;
                            //chartControl1.Titles.Add(chartTitle1);

                            diagram.AxisY.WholeRange.Auto = false;
                            //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                            //}
                            //else
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                            //}
                            //diagram.AxisY.WholeRange.SetMinMaxValues(-60, 60);
                            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                            diagram.AxisY.WholeRange.Auto = false;
                            //if (Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) > 0)
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(0.0, Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString()) + 1);
                            //}
                            //else
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(Convert.ToDecimal(base.mResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString()) - 1, 0.0);
                            //}
                            //diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;


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

                            //if (!string.IsNullOrWhiteSpace(bottom))
                            //{
                            //    diagram.AxisY.WholeRange.SetMinMaxValues(bottom_out, top_out);
                            //}

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
