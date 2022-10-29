//using DevExpress.Charts.Model;
//using DevExpress.Charts.ChartData;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.REPORT;
using Wisol.MES.Forms.SETTING;
using Wisol.MES.Inherit;
//using System.Linq;
//using DevExpress.Spreadsheet.Charts;
//using System.Net.Sockets;
//using System.Runtime.Serialization.Formatters.Binary;
using DevExpress.XtraCharts;
//using System.Xml.Linq;
//using Wisol.DataAcess;
//using Extensions = Wisol.DataAcess.Extensions;

namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1105 : PageType
    {
        public WLP1105()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            
        }
        string code;


        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;
            btView.Enabled = false;
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {

                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    base.m_BindData.BindGridView(gcList2,
                        base.m_ResultDB.ReturnDataSet.Tables[1]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            base.InitializePage();

        }

        public override void SearchPage()
        {
            gcList2.DataSource = null;
            base.SearchPage();
            DataTable tempDT = new DataTable();
            //DataTable vdt2 = new DataTable();
            
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.GET_LIST"
                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT"},
                  new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    tempDT = base.m_ResultDB.ReturnDataSet.Tables[0];

                    // Revise table ton_kho, them cột kiểu số nguyên chứa số tuần có thể dung. Mục đích để có thể Filter được. **********************
                    if (!tempDT.Columns.Contains("CAN_USE"))
                    {
                        tempDT.Columns.Add("CAN_USE", typeof(double));
                    }
                    for (int i = 0; i < tempDT.Rows.Count; i++)
                    {
                        if (tempDT.Rows[i]["CAN_USE_FOR"].NullString() != "")
                        {
                            tempDT.Rows[i]["CAN_USE"] = Convert.ToDouble(tempDT.Rows[i]["CAN_USE_FOR"].NullString().Replace("W","").Replace("~",""));
                        }
                    }

                    // ******************************************************************************************************************************
                    base.m_BindData.BindGridView(gcList, tempDT);
                    
                    // Cập nhật tất cả các CODE vào bảng LICH_SU_TON_KHO_ALL
                    //base.m_DBaccess.ExecuteNoneQuery("Delete from LICH_SU_TON_KHO_ALL Where DEPARTMENT ='" + Consts.DEPARTMENT + "'");
                    //for (int i = 0; i < gvList.RowCount; i++)
                    //{
                    //    string vCODE = gvList.GetRowCellValue(i, "CODE").ToString();
                    //    //string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM,sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKIN_NEW T2 where DEPARTMENT= '" + Consts.DEPARTMENT + "' AND CODE='" + vCODE + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) UNION  SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG, YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM, sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKOUT_NEW T1 where DEPARTMENT='" + Consts.DEPARTMENT + "'  AND CODE='" + vCODE + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME))order by THANG";
                    //    string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM,sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKIN_NEW T2 where DEPARTMENT= '" + Consts.DEPARTMENT + "' AND CODE='" + vCODE + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) UNION  SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG, YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM, sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKOUT_NEW T1 where DEPARTMENT='" + Consts.DEPARTMENT + "'  AND CODE='" + vCODE + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME))order by NAM,THANG";
                    //    DataTable dt_test = base.m_DBaccess.ExecuteQuery(sql);
                    //    DataTable mydt_ton_kho_test = new DataTable();
                    //    base.m_DBaccess.Update_LICH_SU_TON_KHO_ALL(vCODE, Consts.DEPARTMENT, dt_test, ref mydt_ton_kho_test);

                    //}
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }

                gvList.Columns["LOAI_VAN_CHUYEN"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gvList.Columns["LOAI_VAN_CHUYEN"].Width = 100;

                gvList.Columns["UNIT"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gvList.Columns["UNIT"].Width = 50;

                gvList.Columns["QUANTITY"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gvList.Columns["QUANTITY"].Width = 100;
                gvList.Columns["QUANTITY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns["QUANTITY"].DisplayFormat.FormatString = "n3";
                gvList.Columns["TOTAL_MONEY_USD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns["TOTAL_MONEY_USD"].DisplayFormat.FormatString = "n0";
                gvList.Columns["TOTAL_MONEY_USD"].Width = 50;

                gvList.Columns["CAN_USE_FOR"].Visible = false;

                if (Consts.DEPARTMENT == "WLP1") // Ẩn những cột không cần thiết trong Gridview.
                {
                    
                    for (int i = 7; i < gvList.Columns.Count-1; i++)
                    {
                        gvList.Columns[i].Visible = false;
                    }
                    gvList.Columns[6].Visible = false; // Ẩn thêm cột mục đích sử dụng của WLP1
                    //gvList.Columns[gvList.Columns.Count-1].Visible = true;
                }
                // 
                //btView.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

        }


        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
        }

        private void gvList_RowClick(object sender, RowClickEventArgs e)
        {
            // *********** TẠM THỜI BỎ ĐOẠN CODE NÀY ĐỂ SHOW BIỂU ĐỒ TÒN KHO THEO THÁNG *******************

            if (e.RowHandle >= 0)
            {
                //string code = gvList.GetRowCellDisplayText(e.RowHandle, "CODE");
                code = gvList.GetRowCellDisplayText(e.RowHandle, "CODE");
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.GET_LIST_DETAIL"
                    , new string[] { "A_CODE", "A_DEPARTMENT" },
                      new string[] { code, Consts.DEPARTMENT }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        base.m_BindData.BindGridView(gcList2,
                          base.m_ResultDB.ReturnDataSet.Tables[0]
                          );
                        gvList2.Columns["QUANTITY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList2.Columns["QUANTITY"].DisplayFormat.FormatString = "n3";
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

            //***********************************************************************************************

        }

        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            btView.Enabled = true;
            ChartTitle ct = new ChartTitle();
            try
            {
                chartControl1.Series.Clear();
                chartControl2.Series.Clear();
                chartControl3.Series.Clear();
                chartControl2.Titles.Clear();

                DataTable dt = new DataTable();
                DataTable mydt_out = new DataTable();
                DataTable mydt_in = new DataTable();
                string vCurrentdate = System.DateTime.Today.ToString("yyyyMMdd");
                string vHistorymonth = System.DateTime.Today.AddMonths(-6).ToString("yyyyMM01");

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT005.IN_OUT"
                    , new string[] { "A_CODE", "A_PLANT", "A_DEPARTMENT"}
                    , new string[] { code, Consts.PLANT, Consts.DEPARTMENT});
                dt.Columns.Add("CODE", typeof(string));
                dt.Columns.Add("DEPARTMENT", typeof(string));
                dt.Columns.Add("SO_LUONG_NHAP", typeof(int));
                dt.Columns.Add("SO_LUONG_XUAT", typeof(int));
                dt.Columns.Add("TON_KHO", typeof(int));
                dt.Columns.Add("THANG", typeof(string)); 
                dt.Columns.Add("NAM", typeof(string));

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    mydt_out = base.m_ResultDB.ReturnDataSet.Tables[0];
                    mydt_in = base.m_ResultDB.ReturnDataSet.Tables[1];

                    Series series1 = new Series("In", ViewType.Bar);
                    Series series2 = new Series("Out", ViewType.Bar);
                    Series series3 = new Series("Ton_kho", ViewType.Bar);


                    for (int i = 0; i < mydt_in.Rows.Count; i++)
                    {
                        //dt.Rows.Add(new object[] { mydt.Rows[i]["THANG"].ToInt(), mydt.Rows[i]["SO_LUONG_OUT"].ToInt() });
                        series1.Points.Add(new SeriesPoint(mydt_in.Rows[i]["THANG"].ToInt(), mydt_in.Rows[i]["SO_LUONG_IN"].ToInt()));
                    }
                    for (int i = 0; i < mydt_out.Rows.Count; i++)
                    {
                        series2.Points.Add(new SeriesPoint(mydt_out.Rows[i]["THANG"].ToInt(), mydt_out.Rows[i]["SO_LUONG_OUT"].ToInt()));
                    }

                    // Lấy lịch sử tồn kho của CODE tương ứng.****
                    
                    //string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM,sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKIN_NEW T2 where DEPARTMENT= '" + Consts.DEPARTMENT + "' AND CODE='C0003' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) UNION SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG, YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM, sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKOUT_NEW T1 where DEPARTMENT='" + Consts.DEPARTMENT + "'  AND CODE='C0003' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) order by THANG";
                    string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM,sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKIN_NEW T2 where DEPARTMENT= '" + Consts.DEPARTMENT + "' AND CODE='" + code + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) UNION  SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG, YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM, sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKOUT_NEW T1 where DEPARTMENT='" + Consts.DEPARTMENT + "'  AND CODE='" + code + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME))order by NAM,THANG";
                    dt = base.m_DBaccess.ExecuteQuery(sql);
                    DataTable mydt_ton_kho = new DataTable();
                    base.m_DBaccess.Update_LICH_SU_TON_KHO(code, Consts.DEPARTMENT, dt, ref mydt_ton_kho);

                    for (int i = 0; i < mydt_ton_kho.Rows.Count; i++)
                    {
                        series3.Points.Add(new SeriesPoint(mydt_ton_kho.Rows[i]["THANG"].ToInt(), mydt_ton_kho.Rows[i]["SO_LUONG_IN_OUT"].ToInt()));
                    }
                    //*********************************************

                    // ***HIEN THI BIEU DO THEO SO LUONG *****************************************************
                    dt = base.m_DBaccess.ExecuteQuery("SELECT (THANG + '-' + NAM) as 'TN',SO_LUONG_NHAP,SO_LUONG_XUAT,SO_LUONG_TON FROM LICH_SU_TON_KHO_NEW");
                    Series series_A = new Series("Input", ViewType.Bar);
                    chartControl1.Series.Add(series_A);
                    series_A.DataSource = dt;
                    series_A.ArgumentScaleType = ScaleType.Auto;
                    //series_A.ArgumentDataMember = "THANG";
                    series_A.ArgumentDataMember = "TN";
                    series_A.ValueScaleType = ScaleType.Numerical;
                    series_A.ValueDataMembers.AddRange(new string[] { "SO_LUONG_NHAP" });

                    Series series_B = new Series("Output", ViewType.Bar);
                    chartControl1.Series.Add(series_B);
                    series_B.DataSource = dt;
                    series_B.ArgumentScaleType = ScaleType.Auto;
                    //series_B.ArgumentDataMember = "THANG";
                    series_B.ArgumentDataMember = "TN";
                    series_B.ValueScaleType = ScaleType.Numerical;
                    series_B.ValueDataMembers.AddRange(new string[] { "SO_LUONG_XUAT" });

                    Series series_C = new Series("Remain stock", ViewType.Bar);
                    chartControl1.Series.Add(series_C);
                    series_C.DataSource = dt;
                    series_C.ArgumentScaleType = ScaleType.Auto;
                    //series_C.ArgumentDataMember = "THANG";
                    series_C.ArgumentDataMember = "TN";
                    series_C.ValueScaleType = ScaleType.Numerical;
                    series_C.ValueDataMembers.AddRange(new string[] { "SO_LUONG_TON" });

                    chartControl1.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl1.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl1.Series[2].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                    ((XYDiagram)chartControl1.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisY.Title.Text = "Q'ty";
                    diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    diagram.AxisY.Title.Visible = true;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    // *****************************************************************************************

                    // ***HIEN THI BIEU DO GIA TRI TIEN CUA CODE TUONG UNG **************************************
                    //dt = base.m_DBaccess.ExecuteQuery("select T1.CODE,T1.DEPARTMENT,cast(T1.THANG as int) THANG,T1.NAM, cast(T1.SO_LUONG_NHAP*T2.PRICE as int) 'GIA_TRI_NHAP', cast(T1.SO_LUONG_XUAT*T2.PRICE as int) 'GIA_TRI_XUAT', cast(T1.SO_LUONG_TON*T2.PRICE as int) 'GIA_TRI_TON' from [LICH_SU_TON_KHO_NEW] T1, EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT");
                    dt = base.m_DBaccess.ExecuteQuery("select T1.CODE,T1.DEPARTMENT,(T1.THANG + '-' + T1.NAM) as 'TN', cast(T1.SO_LUONG_NHAP*T2.PRICE as int) 'GIA_TRI_NHAP', cast(T1.SO_LUONG_XUAT*T2.PRICE as int) 'GIA_TRI_XUAT', cast(T1.SO_LUONG_TON*T2.PRICE as int) 'GIA_TRI_TON' from [LICH_SU_TON_KHO_NEW] T1, EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT");
                    Series series_AA = new Series("Input_price", ViewType.Bar);
                    chartControl2.Series.Add(series_AA);
                    series_AA.DataSource = dt;
                    series_AA.ArgumentScaleType = ScaleType.Auto;
                    //series_AA.ArgumentDataMember = "THANG";
                    series_AA.ArgumentDataMember = "TN";
                    series_AA.ValueScaleType = ScaleType.Numerical;
                    series_AA.ValueDataMembers.AddRange(new string[] { "GIA_TRI_NHAP" });

                    Series series_BB = new Series("Output_price", ViewType.Bar);
                    chartControl2.Series.Add(series_BB);
                    series_BB.DataSource = dt;
                    series_BB.ArgumentScaleType = ScaleType.Auto;
                    //series_BB.ArgumentDataMember = "THANG";
                    series_BB.ArgumentDataMember = "TN";
                    series_BB.ValueScaleType = ScaleType.Numerical;
                    series_BB.ValueDataMembers.AddRange(new string[] { "GIA_TRI_XUAT" });

                    Series series_CC = new Series("Remain stock_price", ViewType.Bar);
                    chartControl2.Series.Add(series_CC);
                    series_CC.DataSource = dt;
                    series_CC.ArgumentScaleType = ScaleType.Auto;
                    //series_CC.ArgumentDataMember = "THANG";
                    series_CC.ArgumentDataMember = "TN";
                    series_CC.ValueScaleType = ScaleType.Numerical;
                    series_CC.ValueDataMembers.AddRange(new string[] { "GIA_TRI_TON" });

                    //((SideBySideBarSeriesView)series_A.View).ColorEach = true;
                    chartControl2.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl2.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl2.Series[2].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((XYDiagram)chartControl2.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    XYDiagram diagram1 = (XYDiagram)chartControl2.Diagram;
                    //diagram1.AxisX.Title.Text = "Month";
                    diagram1.AxisY.Title.Text = "$";
                    diagram1.AxisY.Title.Font= new Font("Tahoma", 14, FontStyle.Bold);
                    diagram1.AxisY.Title.Visible = true;
                    ct.Font=new Font("Tahoma", 14, FontStyle.Regular);
                    ct.Text = "CODE: "+dt.Rows[0]["CODE"].ToString();
                    chartControl2.Titles.Add(ct);
                    chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    // *****************************************************************************************

                    // *** BIEU DO HIEN THI TONG GIA TRI TIEN NHAP/XUAT VA TON ***********************************

                    //dt = base.m_DBaccess.ExecuteQuery("SELECT right(('0'+T1.THANG + '-' + T1.NAM),7) as 'TN', SUM(CEILING(t1.[SO_LUONG_TON]*t2.PRICE)) as 'TON' ,sum(CEILING(t1.[SO_LUONG_NHAP]*t2.PRICE)) as 'NHAP',sum(CEILING(t1.[SO_LUONG_XUAT]*t2.PRICE)) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT AND T1.DEPARTMENT ='" + Consts.DEPARTMENT + "' group by right(('0'+T1.THANG + '-' + T1.NAM),7) order by TN");
                    dt = base.m_DBaccess.ExecuteQuery("SELECT (T1.NAM + right(('0' + T1.THANG),2)) as TN, SUM(CEILING(t1.[SO_LUONG_TON]*t2.PRICE)) as 'TON' ,sum(CEILING(t1.[SO_LUONG_NHAP]*t2.PRICE)) as 'NHAP',sum(CEILING(t1.[SO_LUONG_XUAT]*t2.PRICE)) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT AND T1.DEPARTMENT ='" + Consts.DEPARTMENT + "' group by T1.NAM + right(('0' + T1.THANG),2)");

                    Series series_AAA = new Series("Input_price", ViewType.Bar);
                    chartControl3.Series.Add(series_AAA);
                    series_AAA.DataSource = dt;
                    series_AAA.ArgumentScaleType = ScaleType.Auto;
                    //series_AA.ArgumentDataMember = "THANG";
                    series_AAA.ArgumentDataMember = "TN";
                    series_AAA.ValueScaleType = ScaleType.Numerical;
                    series_AAA.ValueDataMembers.AddRange(new string[] { "NHAP" });

                    Series series_BBB = new Series("Output_price", ViewType.Bar);
                    chartControl3.Series.Add(series_BBB);
                    series_BBB.DataSource = dt;
                    series_BBB.ArgumentScaleType = ScaleType.Auto;
                    //series_BB.ArgumentDataMember = "THANG";
                    series_BBB.ArgumentDataMember = "TN";
                    series_BBB.ValueScaleType = ScaleType.Numerical;
                    series_BBB.ValueDataMembers.AddRange(new string[] { "XUAT" });

                    Series series_CCC = new Series("Remain stock_price", ViewType.Bar);
                    chartControl3.Series.Add(series_CCC);
                    series_CCC.DataSource = dt;
                    series_CCC.ArgumentScaleType = ScaleType.Auto;
                    //series_CC.ArgumentDataMember = "THANG";
                    series_CCC.ArgumentDataMember = "TN";
                    series_CCC.ValueScaleType = ScaleType.Numerical;
                    series_CCC.ValueDataMembers.AddRange(new string[] { "TON" });

                    //((SideBySideBarSeriesView)series_A.View).ColorEach = true;

                    chartControl3.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl3.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl3.Series[2].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    ((XYDiagram)chartControl3.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    XYDiagram diagram2 = (XYDiagram)chartControl3.Diagram;
                    //diagram1.AxisX.Title.Text = "Month";
                    diagram2.AxisY.Title.Text = "$";
                    diagram2.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    //diagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Near;
                    //diagram1.AxisX.Title.Visible = true;
                    diagram2.AxisY.Title.Visible = true;
                    chartControl3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    //******************************************************************************************

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

        private void gcList_Click(object sender, EventArgs e)
        {

        }

        private void btView_Click(object sender, EventArgs e)
        {
            // Ve lai bieu do, lay du lieu theo thang nguoi dung chon
            string vFrom, vTo;
            DataTable dt = new DataTable();
            ChartTitle ct = new ChartTitle();
            chartControl1.Series.Clear();
            chartControl2.Series.Clear();
            chartControl3.Series.Clear();
            chartControl2.Titles.Clear();
            if ((dateEditFrom.Text == "") || (dateEditTo.Text == "")) 
            {
                MessageBox.Show("Please select Date.");
                return;
            }
            //vFrom = string.Format("{0:yyyyMMdd}", dateEditFrom.DateTime);
            //vTo = string.Format("{0:yyyyMMdd}", dateEditTo.DateTime);
            vFrom = string.Format("{0:MM-yyyy}", dateEditFrom.DateTime);
            vTo = string.Format("{0:MM-yyyy}", dateEditTo.DateTime);
            //MessageBox.Show("From: " + vFrom + " To " + vTo);

            // Lấy lịch sử tồn kho của CODE tương ứng.****
            string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM,sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKIN_NEW T2 where DEPARTMENT= '" + Consts.DEPARTMENT + "' AND CODE='" + code + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) UNION  SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG, YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM, sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKOUT_NEW T1 where DEPARTMENT='" + Consts.DEPARTMENT + "'  AND CODE='" + code + "' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME))order by THANG";
            //string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM,sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKIN_NEW T2 where DEPARTMENT= '" + Consts.DEPARTMENT + "' AND CODE='C0003' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) UNION SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG, YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) as NAM, sum(QUANTITY) as 'SO_LUONG_IN_OUT' FROM EWIPSTOCKOUT_NEW T1 where DEPARTMENT='" + Consts.DEPARTMENT + "'  AND CODE='C0003' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)),YEAR(DBO.CHAR_TO_DATE(CREATE_TIME)) order by THANG";
            dt = base.m_DBaccess.ExecuteQuery(sql);
            DataTable mydt_ton_kho = new DataTable();
            base.m_DBaccess.Update_LICH_SU_TON_KHO(code, Consts.DEPARTMENT, dt, ref mydt_ton_kho);

            //*********************************************

            // ***HIEN THI BIEU DO GIA TRI TIEN THEO TUNG CODE ****************************************************
            //dt = base.m_DBaccess.ExecuteQuery("select T1.CODE,T1.DEPARTMENT,right(('0' + T1.THANG + '-' + T1.NAM),7) as 'TN', cast(T1.SO_LUONG_NHAP*T2.PRICE as int) 'GIA_TRI_NHAP', cast(T1.SO_LUONG_XUAT*T2.PRICE as int) 'GIA_TRI_XUAT', cast(T1.SO_LUONG_TON*T2.PRICE as int) 'GIA_TRI_TON' from [LICH_SU_TON_KHO_NEW] T1, EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT");
            dt = base.m_DBaccess.ExecuteQuery("select T1.CODE,T1.DEPARTMENT, right(('0' + T1.THANG + '-' + T1.NAM),7) as 'TN', cast(T1.SO_LUONG_NHAP*T2.PRICE as int) 'GIA_TRI_NHAP', cast(T1.SO_LUONG_XUAT*T2.PRICE as int) 'GIA_TRI_XUAT', cast(T1.SO_LUONG_TON*T2.PRICE as int) 'GIA_TRI_TON' from [LICH_SU_TON_KHO_NEW] T1, EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" +vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <='" + vTo + "' order by TN");
            Series series_AA = new Series("Input_price", ViewType.Bar);
            chartControl2.Series.Add(series_AA);
            series_AA.DataSource = dt;
            series_AA.ArgumentScaleType = ScaleType.Auto;
            //series_AA.ArgumentDataMember = "THANG";
            series_AA.ArgumentDataMember = "TN";
            series_AA.ValueScaleType = ScaleType.Numerical;
            series_AA.ValueDataMembers.AddRange(new string[] { "GIA_TRI_NHAP" });

            Series series_BB = new Series("Output_price", ViewType.Bar);
            chartControl2.Series.Add(series_BB);
            series_BB.DataSource = dt;
            series_BB.ArgumentScaleType = ScaleType.Auto;
            //series_BB.ArgumentDataMember = "THANG";
            series_BB.ArgumentDataMember = "TN";
            series_BB.ValueScaleType = ScaleType.Numerical;
            series_BB.ValueDataMembers.AddRange(new string[] { "GIA_TRI_XUAT" });

            Series series_CC = new Series("Remain stock_price", ViewType.Bar);
            chartControl2.Series.Add(series_CC);
            series_CC.DataSource = dt;
            series_CC.ArgumentScaleType = ScaleType.Auto;
            //series_CC.ArgumentDataMember = "THANG";
            series_CC.ArgumentDataMember = "TN";
            series_CC.ValueScaleType = ScaleType.Numerical;
            series_CC.ValueDataMembers.AddRange(new string[] { "GIA_TRI_TON" });

            //((SideBySideBarSeriesView)series_A.View).ColorEach = true;
            chartControl2.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl2.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl2.Series[2].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)chartControl2.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;

            XYDiagram diagram1 = (XYDiagram)chartControl2.Diagram;
            //diagram1.AxisX.Title.Text = "Month";
            diagram1.AxisY.Title.Text = "$";
            diagram1.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            diagram1.AxisY.Title.Visible = true;
            ct.Font = new Font("Tahoma", 14, FontStyle.Regular);
            ct.Text = "CODE: " + dt.Rows[0]["CODE"].ToString();
            chartControl2.Titles.Add(ct);
            chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // *****************************************************************************************

            // *** BIEU DO HIEN THI TONG GIA TRI TIEN NHAP/XUAT VA TON **************************************************
            //dt = base.m_DBaccess.ExecuteQuery("SELECT right(('0'+T1.THANG + '-' + T1.NAM),7) as 'TN',SUM(t1.[SO_LUONG_TON]*t2.PRICE) as 'TON' ,sum(t1.[SO_LUONG_NHAP]*t2.PRICE) as 'NHAP',sum(t1.[SO_LUONG_XUAT]*t2.PRICE) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT group by THANG,NAM order by TN");
            dt = base.m_DBaccess.ExecuteQuery("SELECT right(('0' +T1.THANG + '-' + T1.NAM),7) as 'TN',SUM(t1.[SO_LUONG_TON]*t2.PRICE) as 'TON' , sum(t1.[SO_LUONG_NHAP]*t2.PRICE) as 'NHAP',sum(t1.[SO_LUONG_XUAT]*t2.PRICE) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT='" + Consts.DEPARTMENT + "' and T1.CODE=t2.CODE and t1.DEPARTMENT=t2.DEPARTMENT and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' group by THANG,NAM order by TN");
            Series series_AAA = new Series("Input_price", ViewType.Bar);
            chartControl3.Series.Add(series_AAA);
            series_AAA.DataSource = dt;
            series_AAA.ArgumentScaleType = ScaleType.Auto;
            //series_AA.ArgumentDataMember = "THANG";
            series_AAA.ArgumentDataMember = "TN";
            series_AAA.ValueScaleType = ScaleType.Numerical;
            series_AAA.ValueDataMembers.AddRange(new string[] { "NHAP" });

            Series series_BBB = new Series("Output_price", ViewType.Bar);
            chartControl3.Series.Add(series_BBB);
            series_BBB.DataSource = dt;
            series_BBB.ArgumentScaleType = ScaleType.Auto;
            //series_BB.ArgumentDataMember = "THANG";
            series_BBB.ArgumentDataMember = "TN";
            series_BBB.ValueScaleType = ScaleType.Numerical;
            series_BBB.ValueDataMembers.AddRange(new string[] { "XUAT" });

            Series series_CCC = new Series("Remain stock_price", ViewType.Bar);
            chartControl3.Series.Add(series_CCC);
            series_CCC.DataSource = dt;
            series_CCC.ArgumentScaleType = ScaleType.Auto;
            //series_CC.ArgumentDataMember = "THANG";
            series_CCC.ArgumentDataMember = "TN";
            series_CCC.ValueScaleType = ScaleType.Numerical;
            series_CCC.ValueDataMembers.AddRange(new string[] { "TON" });

            //((SideBySideBarSeriesView)series_A.View).ColorEach = true;

            chartControl3.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl3.Series[1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl3.Series[2].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)chartControl3.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;

            XYDiagram diagram2 = (XYDiagram)chartControl3.Diagram;
            //diagram1.AxisX.Title.Text = "Month";
            diagram2.AxisY.Title.Text = "$";
            diagram2.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            //diagram1.AxisX.Title.Alignment = System.Drawing.StringAlignment.Near;
            //diagram1.AxisX.Title.Visible = true;
            diagram2.AxisY.Title.Visible = true;
            chartControl3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            //******************************************************************************************


        }

        private void gvList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            // Warning here.
            GridView currentView = sender as GridView;
            if ((e.RowHandle >= 0))
            {
                if ((e.Column.FieldName == "CAN_USE"))
                {
                    if ((e.Column.FieldName == "CAN_USE"))
                    {
                        string temp = currentView.GetRowCellValue(e.RowHandle, "CAN_USE").ToString();
                        if ((temp != "") && (temp != null) && (temp != " "))
                        {
                            //temp = temp.Substring(1, temp.Length - 1);
                            //string[] vArray = temp.Split(' ');

                            if (Convert.ToDouble(temp) < 4.0)
                            {
                                e.Appearance.BackColor = Color.OrangeRed;
                            }
                            if ((Convert.ToDouble(temp) >= 4.0) && (Convert.ToDouble(temp) <= 6.0))
                            {
                                e.Appearance.BackColor = Color.Green;
                            }
                            if ((Convert.ToDouble(temp) > 6.0))
                            {
                                e.Appearance.BackColor = Color.Yellow;
                            }
                        }
                    }

                }

            }
        }
    }
}

