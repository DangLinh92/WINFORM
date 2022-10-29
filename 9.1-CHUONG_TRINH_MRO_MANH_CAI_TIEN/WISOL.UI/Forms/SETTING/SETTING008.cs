using System;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraEditors.Repository;
//using System.Linq;
//using DevExpress.Utils;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING008 :PageType
    {
        bool option_plan;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public SETTING008()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();
            //btMonthly.Enabled = false;
            this.InitializePage();

        }



        public override void InitializePage()
        {
            DataTable dt = new DataTable();
            DataTable dt_WLP1 = new DataTable();
            try
            {
                if (Consts.DEPARTMENT == "WLP1") // Phòng WLP1 không tính theo sản lượng, không dùng các MODEL sản xuất.
                {
                    layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.GET_LIST", new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT" }, new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT });
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                        dt_WLP1 = base.m_DBaccess.THEO_DOI_NGUYEN_LIEU_WLP1(dt);

                        //  Copy data from dt_WLP1 vao bang EWIPDINH_MUC_NGUYEN_LIEU_WLP1 de phuc vu show estimate thoi gian dung duoc bao lau.*************
                        m_DBaccess.CopyDataTableToTable(dt_WLP1);

                        // *********************************************************************************************************************************
                        
                        base.m_BindData.BindGridView(gcList, dt_WLP1);
                        //****** Hiển thị theo định dạng...........................
                        //gvList.Columns["SO_LUONG_HIEN_TAI"].Width = 110;
                        gvList.Columns["SO_LUONG_HIEN_TAI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns["SO_LUONG_HIEN_TAI"].DisplayFormat.FormatString = "n3";
                        gvList.Columns["SO_TIEN_HIEN_TAI_usd"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns["SO_TIEN_HIEN_TAI_usd"].DisplayFormat.FormatString = "n3";

                        gvList.Columns["SO_LUONG_DAT_HANG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns["SO_LUONG_DAT_HANG"].DisplayFormat.FormatString = "n3";
                        gvList.Columns["SO_TIEN_DAT_HANG_usd"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns["SO_TIEN_DAT_HANG_usd"].DisplayFormat.FormatString = "n0";
                        // Freeze column **
                        for (int i = 0; i <= 4; i++)
                        {
                            gvList.VisibleColumns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        }
                        //******** ..................................................
                    }
                }
                else // Phòng CSP, WLP2,LFEM tính lượng cần dùng dựa vào kế hoạch sản xuất của các MODEL.
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.INT_LIST" , new string[] { "A_PLANT","A_LANG","A_DEPARTMENT" } , new string[] { Consts.PLANT, Consts.USER_INFO.Language,Consts.DEPARTMENT});

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        foreach (DataRow row in base.m_ResultDB.ReturnDataSet.Tables[0].Rows)
                        {
                            cbModel.Properties.Items.Add(row[0]);
                        }
                        cbModel.SelectedIndex = 0;
                    }
                }
                Init_Control();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            base.InitializePage();
            
        }

        public override void SearchPage()
        {
            if (string.IsNullOrWhiteSpace(cbModel.Text))
            {
                return;
            }
            base.SearchPage();
            try
            {
                if (Consts.DEPARTMENT == "CSP")
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST"
                        , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_MODEL"
                        }
                        , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, cbModel.Text
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                        dt.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                        dt.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                        dt.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                        dt.AcceptChanges();

                        if (cbModel.Text == "ALL")
                        {

                            // Manh sua doan code nay de chuyen doi san luong theo thang ra tuan. ***************************************************
                            string vsql = "";
                            int total_Lead_time = 0; // Total Lead time = Lead time + Offset Lead Time
                            //int total_by_lead_time = 0;
                            double total_by_lead_time = 0;
                            float he_so_an_toan = 1.0F;
                            double dinh_muc_can_thiet = 0;
                            int luong_can_mua = 0;
                            double total_price = 0;
                            //int ton_kho = 0;
                            double ton_kho;
                            int luong_chua_nhap = 0;
                            int luong_can_mua_before = 0;
                            double total_usd = 0;
                            int offset_lead_time = 0;
                            string can_use_for = "";
                            base.m_DBaccess.ExecuteNoneQuery("Delete from EWIPDINH_MUC_NGUYEN_LIEU");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt();
                                if ((dt.Rows[i][7].ToString() != ""))
                                {
                                    //ton_kho = (uint)(Convert.ToDouble(dt.Rows[i][7].ToString()));
                                    ton_kho = (Convert.ToDouble(dt.Rows[i][7].IfNullZero()));
                                }
                                else { ton_kho = 0; }
                                if ((dt.Rows[i][8].ToString() != ""))
                                {
                                    luong_chua_nhap = (int)(Convert.ToDouble(dt.Rows[i][8].IfNullZero()));
                                }
                                else { luong_chua_nhap = 0; }
                                if ((dt.Rows[i][9].ToString() != ""))
                                {
                                    luong_can_mua_before = (int)(Convert.ToDouble(dt.Rows[i][9].IfNullZero()));
                                }
                                else { luong_can_mua_before = 0; }
                                if ((dt.Rows[i][11].ToString() != ""))
                                {
                                    total_usd = Convert.ToDouble(dt.Rows[i][11].IfNullZero());
                                }
                                else { total_usd = 0; }
                                //vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + (int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + "," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].IfNullZero()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].IfNullZero()) / 4)) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].IfNullZero())) + "," + (int)(Convert.ToDouble(dt.Rows[i][6].IfNullZero())) + "," + ton_kho + ",0," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].IfNullZero()) + "," + total_usd + ")";
                                vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][2].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][3].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][4].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][4].IfNullZero()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4,2)) + "," + (Math.Round(Convert.ToDouble(dt.Rows[i][4].IfNullZero()) / 4,2)) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].IfNullZero())) + "," + (Convert.ToDouble(dt.Rows[i][6].IfNullZero())) + "," + ton_kho + ",0," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].IfNullZero()) + "," + total_usd + ")";
                                base.m_DBaccess.ExecuteNoneQuery(vsql);
                            }

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );
                            //DataTable dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                            dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];

                            // Update TOTAL_BY_LEAD_TIME, DINH_MUC_CAN_THIET vao bang EWIPDINH_MUC_NGUYEN_LIEU ********
                            DataTable mydt = new DataTable();
                            double temp = 0;
                            double temp1 = 0;
                            double a = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                mydt = base.m_DBaccess.ExecuteQuery("Select DU_PHONG,OFFSET_LEAD_TIME from EWIPMRO where DEPARTMENT='" + Consts.DEPARTMENT + "' and CODE='" + dt.Rows[i][0].ToString() + "'");
                                if (mydt.Rows.Count != 0)
                                {
                                    if (mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString() == "")
                                    {
                                        total_Lead_time = dt.Rows[i]["LEAD_TIME"].ToString().ToInt();
                                        offset_lead_time = 0;
                                    }
                                    else
                                    {
                                        total_Lead_time = dt.Rows[i]["LEAD_TIME"].ToString().ToInt() + mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString().ToInt();// Tinh tong Lead Time
                                        offset_lead_time = mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString().ToInt();
                                        //if (total_Lead_time > 12) { total_Lead_time = 12; }
                                        if (total_Lead_time > 12) { total_Lead_time = 13; }
                                    }

                                    he_so_an_toan = (float)Convert.ToDouble(mydt.Rows[0][0].IfNullZero());// Lay he so an toan cua tung CODE

                                }
                                for (int j = 5; j < total_Lead_time + 5; j++) // Chạy từ 5 là cột sản lượng tuần đầu tiên......................
                                {
                                    //total_by_lead_time = total_by_lead_time + dt1.Rows[i][j].ToString().ToInt(); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][j].IfNullZero()); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                }

                                // Check total_lead_time, nếu bằng 13 tức là Lead_time=9 và Offset_lead_time = 4. ************************* ************
                                if (total_Lead_time == 13)
                                {
                                    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][16].IfNullZero());// Cộng thêm 1 tuần cuối nữa.
                                }
                                //********************************************************************************************************* *************

                                dinh_muc_can_thiet = Math.Round(total_by_lead_time * he_so_an_toan,2); // Dinh muc can thiet 
                                luong_can_mua = (int)dinh_muc_can_thiet - dt1.Rows[i]["TON_KHO"].ToString().ToInt() - dt1.Rows[i]["LUONG_CHUA_NHAP"].ToString().ToInt();// So lung can mua
                                if (luong_can_mua < 0) { luong_can_mua = 0; }// Neu con trong kho thi khong phai mua.

                                total_price = luong_can_mua * Convert.ToDouble(dt1.Rows[i]["UNIT_PRICE"].IfNullZero());

                                // Tính lượng trung bình 3 tháng của các phụ liệu.

                                for (int j = 5; j <= 16; j++) {
                                    //temp=temp+ dt1.Rows[i][j].ToString().ToInt();
                                    temp = temp + Convert.ToDouble(dt1.Rows[i][j].IfNullZero());
                                }
                                if (temp != 0)
                                {
                                    //temp1 = Math.Round(temp / (double)12.0, 2);
                                    temp1 = temp / (double)12.0;
                                    // So sanh voi ton kho hien tai...

                                    a = Convert.ToDouble(dt.Rows[i]["TON_KHO"].IfNullZero());
                                    //if (temp != 0) { a = Math.Floor((double)a / temp1); }
                                    if (temp != 0) { a = Math.Round((double)a / temp1,1); }
                                    else { a = 0; }
                                    can_use_for = "~" + a.ToString() + " W";
                                }
                                else {
                                    can_use_for = " ";
                                }

                                base.m_DBaccess.ExecuteNoneQuery("Update EWIPDINH_MUC_NGUYEN_LIEU set CAN_USE_FOR ='" + can_use_for + "', OFFSET_LEAD_TIME =" + offset_lead_time + ", TOTAL_BY_LEAD_TIME =" + total_by_lead_time + ", DINH_MUC_CAN_THIET =" + dinh_muc_can_thiet + ", LUONG_CAN_MUA =" + luong_can_mua + ", TOTAL_PRICE =" + total_price + " where CODE='" + dt.Rows[i][0].ToString() + "'");

                                //Reset lai bien
                                total_by_lead_time = 0;
                                dinh_muc_can_thiet = 0;
                                total_price = 0;
                                can_use_for = " ";
                                temp = 0;
                                temp1 = 0;
                            }

                            // *****************************************************************

                            // Hien thi lai du lieu trong bang EWIPDINH_MUC_NGUYEN_LIEU vao Grid

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );    
                            dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                            //*******************************************************************

                            // Combine tuan thanh thang de rut gon Grid view *********************

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP_Monthly"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );
                            dt2 = base.m_ResultDB.ReturnDataSet.Tables[0];
                            //*******************************************************************

                            dt1.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_4"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W4";
                            dt1.Columns["NEXT_5"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_6"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_7"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_8"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W4";
                            dt1.Columns["NEXT_9"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_10"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_11"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_12"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W4";

                            dt2.Columns["Column1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                            dt2.Columns["Column2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                            dt2.Columns["Column3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();

                            // Do du lieu vao grid control ********************

                            base.m_BindData.BindGridView(gcList,dt1);
                            Display_Gridview_Dinh_muc_nguyen_lieu();

                        }
                        else
                        {
                            base.m_BindData.BindGridView(gcList,dt);
                            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[2].DisplayFormat.FormatString = "{0:n2}";
                            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[3].DisplayFormat.FormatString = "{0:n2}";
                            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[4].DisplayFormat.FormatString = "{0:n2}";
                            //****************************************
                        }
                    }
                } // Kết thúc phòng CSP.**********************************************************************************************
                // ********************************************************************************************************************

                if (Consts.DEPARTMENT == "WLP2") // bắt đầu cho  WLP2 ....
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_WLP2_WAFER_CHIP", new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_MODEL" }
                            , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, cbModel.Text
                            }
                            );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                        dt.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                        dt.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                        dt.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                        dt.AcceptChanges();
                        if (cbModel.Text == "ALL")
                        {
                            string vsql = "";
                            int total_Lead_time = 0; // Total Lead time = Lead time + Offset Lead Time
                            //int total_by_lead_time = 0;
                            double total_by_lead_time = 0;
                            float he_so_an_toan = 1.0F;
                            //float dinh_muc_can_thiet = 0;
                            double dinh_muc_can_thiet = 0;
                            //int luong_can_mua = 0;
                            double luong_can_mua = 0;
                            double total_price = 0;
                            //int ton_kho = 0;
                            double ton_kho;
                            int luong_chua_nhap = 0;
                            int luong_can_mua_before = 0;
                            double total_usd = 0;
                            int offset_lead_time = 0;
                            string can_use_for = "";
                            base.m_DBaccess.ExecuteNoneQuery("Delete from EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt();
                                if ((dt.Rows[i][7].ToString() != ""))
                                {
                                    //ton_kho = (uint)(Convert.ToDouble(dt.Rows[i][7].ToString()));
                                    ton_kho = (Convert.ToDouble(dt.Rows[i][7].IfNullZero()));
                                }
                                else { ton_kho = 0; }
                                if ((dt.Rows[i][8].ToString() != ""))
                                {
                                    luong_chua_nhap = (int)(Convert.ToDouble(dt.Rows[i][8].IfNullZero()));
                                }
                                else { luong_chua_nhap = 0; }
                                if ((dt.Rows[i][9].ToString() != ""))
                                {
                                    luong_can_mua_before = (int)(Convert.ToDouble(dt.Rows[i][9].IfNullZero()));
                                }
                                else { luong_can_mua_before = 0; }
                                if ((dt.Rows[i][11].ToString() != ""))
                                {
                                    total_usd = Convert.ToDouble(dt.Rows[i][11].IfNullZero());
                                }
                                else { total_usd = 0; }
                                //vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + 				(int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + "," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                //vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + (int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + ",0," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4,2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4,2) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + (int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + ",0," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                base.m_DBaccess.ExecuteNoneQuery(vsql);
                            }
                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                                );
                            dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                            // Update TOTAL_BY_LEAD_TIME, DINH_MUC_CAN_THIET vao bang EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER ********
                            DataTable mydt = new DataTable();
                            //int temp = 0;
                            double temp = 0;
                            double temp1 = 0;
                            double a = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                mydt = base.m_DBaccess.ExecuteQuery("Select DU_PHONG,OFFSET_LEAD_TIME from EWIPMRO where DEPARTMENT='" + Consts.DEPARTMENT + "' and CODE='" + dt.Rows[i][0].ToString() + "'");
                                if (mydt.Rows.Count != 0)
                                {
                                    if (mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString() == "")
                                    {
                                        total_Lead_time = dt.Rows[i]["LEAD_TIME"].ToString().ToInt();
                                        offset_lead_time = 0;
                                    }
                                    else
                                    {
                                        total_Lead_time = dt.Rows[i]["LEAD_TIME"].ToString().ToInt() + mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString().ToInt();// Tinh tong Lead Time
                                        offset_lead_time = mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString().ToInt();
                                        if (total_Lead_time > 12) { total_Lead_time = 13; }
                                    }

                                    he_so_an_toan = (float)Convert.ToDouble(mydt.Rows[0][0].IfNullZero());// Lay he so an toan cua tung CODE

                                }

                                //for (int j = 5; j < total_Lead_time + 4; j++)
                                //{
                                //    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][j].ToString()); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                //}

                                for (int j = 5; j < total_Lead_time + 5; j++) // Chạy từ 5 là cột sản lượng tuần đầu tiên......................
                                {
                                    //total_by_lead_time = total_by_lead_time + dt1.Rows[i][j].ToString().ToInt(); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][j].IfNullZero()); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                }

                                // Check total_lead_time, nếu bằng 13 tức là Lead_time=9 và Offset_lead_time = 4. ************************* ************
                                if (total_Lead_time == 13)
                                {
                                    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][16].IfNullZero());// Cộng thêm 1 tuần cuối nữa.
                                }
                                //********************************************************************************************************* *************

                                //dinh_muc_can_thiet = total_by_lead_time * he_so_an_toan; // Dinh muc can thiet 
                                dinh_muc_can_thiet = Math.Round(total_by_lead_time * he_so_an_toan,2); // Dinh muc can thiet 
                                //luong_can_mua = (int)dinh_muc_can_thiet - dt1.Rows[i]["TON_KHO"].ToString().ToInt() - dt1.Rows[i]["LUONG_CHUA_NHAP"].ToString().ToInt();// So lung can mua
                                luong_can_mua = dinh_muc_can_thiet - Convert.ToDouble(dt1.Rows[i]["TON_KHO"].IfNullZero()) - Convert.ToDouble(dt1.Rows[i]["LUONG_CHUA_NHAP"].IfNullZero());// So lung can mua
                                if (luong_can_mua < 0) { luong_can_mua = 0; }// Neu con trong kho thi khong phai mua.

                                total_price = Math.Round(luong_can_mua * Convert.ToDouble(dt1.Rows[i]["UNIT_PRICE"].IfNullZero()),2);

                                // Tính lượng trung bình 3 tháng của các phụ liệu.

                                for (int j = 5; j <= 16; j++)
                                {
                                    temp = temp + Convert.ToDouble(dt1.Rows[i][j].IfNullZero());
                                }
                                if (temp != 0)
                                {
                                    //temp1 = Math.Round(temp / (double)12.0,2);
                                    temp1 = temp / (double)12.0;
                                    // So sanh voi ton kho hien tai...

                                    if ((dt.Rows[i]["TON_KHO"].ToString() == null) || (dt.Rows[i]["TON_KHO"].ToString() == "")) {
                                        a = 0;
                                    }
                                    else
                                    {
                                        a = Convert.ToDouble(dt.Rows[i]["TON_KHO"].IfNullZero());
                                    }
                                    //if (temp != 0) { a = Math.Floor((double)a / temp1); }
                                    if (temp != 0) 
                                    {
                                        a = Math.Round(a / temp1, 1);
                                    }
                                    else { a = 0; }

                                    can_use_for = "~" + a.ToString() + " W";
                                }
                                else
                                {
                                    can_use_for = " ";
                                }

                                base.m_DBaccess.ExecuteNoneQuery("Update EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER set CAN_USE_FOR ='" + can_use_for + "', OFFSET_LEAD_TIME =" + offset_lead_time + ", TOTAL_BY_LEAD_TIME =" + total_by_lead_time + ", DINH_MUC_CAN_THIET =" + dinh_muc_can_thiet + ", LUONG_CAN_MUA =" + luong_can_mua + ", TOTAL_PRICE =" + total_price + " where CODE='" + dt.Rows[i][0].ToString() + "'");

                                //Reset lai bien
                                total_by_lead_time = 0;
                                dinh_muc_can_thiet = 0;
                                total_price = 0;
                                can_use_for = " ";
                                temp = 0;
                                temp1 = 0;
                            }
                            // ***********************************************************************************************************************************

                            // Hien thi lai du lieu trong bang EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER vao Grid

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );

                            dt1 = base.m_ResultDB.ReturnDataSet.Tables[0]; // Để hiển thị theo tuần
                            //*******************************************************************

                            // Combine tuan thanh thang de rut gon Grid view *********************

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP_Monthly"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );
                            dt2 = base.m_ResultDB.ReturnDataSet.Tables[0]; // Để hiển tị theo tháng
                                                                           //*******************************************************************
                            dt1.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_4"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W4";
                            dt1.Columns["NEXT_5"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_6"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_7"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_8"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W4";
                            dt1.Columns["NEXT_9"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_10"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_11"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_12"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W4";

                            dt2.Columns["Column1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                            dt2.Columns["Column2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                            dt2.Columns["Column3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();

                            // Do du lieu vao grid control ********************

                            base.m_BindData.BindGridView(gcList,dt1);
                            Display_Gridview_Dinh_muc_nguyen_lieu();

                        }
                        else {
                            base.m_BindData.BindGridView(gcList, dt);
                            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[2].DisplayFormat.FormatString = "{0:n2}";
                            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[3].DisplayFormat.FormatString = "{0:n2}";
                            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[4].DisplayFormat.FormatString = "{0:n2}";
                            //****************************************
                        }
                    }
                }
                // *******************************************************************************************************************************

                // Ket thuc phong WLP2 **********************************************************************************************************
                if (Consts.DEPARTMENT == "LFEM")  // bắt đầu cho  phong LFEM
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_LFEM", new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_MODEL" }
                           , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, cbModel.Text
                           }
                           );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                        dt.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                        dt.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                        dt.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                        dt.AcceptChanges();
                        if (cbModel.Text == "ALL")
                        {
                            string vsql = "";
                            int total_Lead_time = 0; // Total Lead time = Lead time + Offset Lead Time
                            //int total_by_lead_time = 0;
                            double total_by_lead_time = 0;
                            float he_so_an_toan = 1.0F;
                            //float dinh_muc_can_thiet = 0;
                            double dinh_muc_can_thiet = 0;
                            //int luong_can_mua = 0;
                            double luong_can_mua = 0;
                            double total_price = 0;
                            //int ton_kho = 0;
                            double ton_kho;
                            int luong_chua_nhap = 0;
                            int luong_can_mua_before = 0;
                            double total_usd = 0;
                            int offset_lead_time = 0;
                            string can_use_for = "";
                            base.m_DBaccess.ExecuteNoneQuery("Delete from EWIPDINH_MUC_NGUYEN_LIEU_LFEM");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt();
                                if ((dt.Rows[i][7].ToString() != ""))
                                {
                                    //ton_kho = (uint)(Convert.ToDouble(dt.Rows[i][7].ToString()));
                                    ton_kho = (Convert.ToDouble(dt.Rows[i][7].IfNullZero()));
                                }
                                else { ton_kho = 0; }
                                if ((dt.Rows[i][8].ToString() != ""))
                                {
                                    luong_chua_nhap = (int)(Convert.ToDouble(dt.Rows[i][8].IfNullZero()));
                                }
                                else { luong_chua_nhap = 0; }
                                if ((dt.Rows[i][9].ToString() != ""))
                                {
                                    luong_can_mua_before = (int)(Convert.ToDouble(dt.Rows[i][9].IfNullZero()));
                                }
                                else { luong_can_mua_before = 0; }
                                if ((dt.Rows[i][11].ToString() != ""))
                                {
                                    total_usd = Convert.ToDouble(dt.Rows[i][11].IfNullZero());
                                }
                                else { total_usd = 0; }
                                //vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + 				(int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + "," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                //vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4)) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + (int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + ",0," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                vsql = "insert into EWIPDINH_MUC_NGUYEN_LIEU_LFEM values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][dt.Columns.Count - 1].ToString().ToInt() + ",0," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][2].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4, 2) + "," + Math.Round(Convert.ToDouble(dt.Rows[i][4].ToString()) / 4, 2) + "," + (int)(Convert.ToDouble(dt.Rows[i][5].ToString())) + "," + (int)(Convert.ToDouble(dt.Rows[i][6].ToString())) + "," + ton_kho + ",0," + luong_chua_nhap + "," + luong_can_mua_before + "," + Convert.ToDouble(dt.Rows[i][10].ToString()) + "," + total_usd + ")";
                                base.m_DBaccess.ExecuteNoneQuery(vsql);
                            }
                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP"
                               , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                               }
                               , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                               }
                               );
                            dt1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                            // Update TOTAL_BY_LEAD_TIME, DINH_MUC_CAN_THIET vao bang EWIPDINH_MUC_NGUYEN_LIEU_LFEM ********
                            DataTable mydt = new DataTable();
                            //int temp = 0;
                            double temp = 0;
                            double temp1 = 0;
                            double a = 0;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                mydt = base.m_DBaccess.ExecuteQuery("Select DU_PHONG,OFFSET_LEAD_TIME from EWIPMRO where DEPARTMENT='" + Consts.DEPARTMENT + "' and CODE='" + dt.Rows[i][0].ToString() + "'");
                                if (mydt.Rows.Count != 0)
                                {
                                    if (mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString() == "")
                                    {
                                        total_Lead_time = dt.Rows[i]["LEAD_TIME"].ToString().ToInt();
                                        offset_lead_time = 0;
                                    }
                                    else
                                    {
                                        total_Lead_time = dt.Rows[i]["LEAD_TIME"].ToString().ToInt() + mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString().ToInt();// Tinh tong Lead Time
                                        offset_lead_time = mydt.Rows[0]["OFFSET_LEAD_TIME"].ToString().ToInt();
                                        if (total_Lead_time > 12) { total_Lead_time = 13; }
                                    }

                                    he_so_an_toan = (float)Convert.ToDouble(mydt.Rows[0][0].IfNullZero());// Lay he so an toan cua tung CODE

                                }
                                //for (int j = 5; j < total_Lead_time + 4; j++)
                                //{
                                //    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][j].ToString()); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                //}

                                for (int j = 5; j < total_Lead_time + 5; j++) // Chạy từ 5 là cột sản lượng tuần đầu tiên......................
                                {
                                    //total_by_lead_time = total_by_lead_time + dt1.Rows[i][j].ToString().ToInt(); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][j].IfNullZero()); // Tong san luong theo tong Lead time ( khong tinh san luong theo thang).
                                }

                                // Check total_lead_time, nếu bằng 13 tức là Lead_time=9 và Offset_lead_time = 4. ************************* ************
                                if (total_Lead_time == 13)
                                {
                                    total_by_lead_time = total_by_lead_time + Convert.ToDouble(dt1.Rows[i][16].IfNullZero());// Cộng thêm 1 tuần cuối nữa.
                                }
                                //********************************************************************************************************* *************

                                //dinh_muc_can_thiet = total_by_lead_time * he_so_an_toan; // Dinh muc can thiet 
                                dinh_muc_can_thiet = Math.Round(total_by_lead_time * he_so_an_toan, 2); // Dinh muc can thiet 

                                if(dt.Rows[i][0].ToString() == "C0041")
                                {
                                    dinh_muc_can_thiet = 300;
                                }
                                else if (dt.Rows[i][0].ToString() == "C0042")
                                {
                                    dinh_muc_can_thiet = 400;
                                }

                                //luong_can_mua = (int)dinh_muc_can_thiet - dt1.Rows[i]["TON_KHO"].ToString().ToInt() - dt1.Rows[i]["LUONG_CHUA_NHAP"].ToString().ToInt();// So lung can mua
                                luong_can_mua = dinh_muc_can_thiet - Convert.ToDouble(dt1.Rows[i]["TON_KHO"].IfNullZero()) - Convert.ToDouble(dt1.Rows[i]["LUONG_CHUA_NHAP"].IfNullZero());// So lung can mua
                                if (luong_can_mua < 0) { luong_can_mua = 0; }// Neu con trong kho thi khong phai mua.

                                total_price = Math.Round(luong_can_mua * Convert.ToDouble(dt1.Rows[i]["UNIT_PRICE"].IfNullZero()), 2);

                                // Tính lượng trung bình 3 tháng của các phụ liệu.

                                for (int j = 5; j <= 16; j++)
                                {
                                    temp = temp + Convert.ToDouble(dt1.Rows[i][j].IfNullZero());
                                }
                                if (temp != 0)
                                {
                                    //temp1 = Math.Round(temp / (double)12.0,2);
                                    temp1 = temp / (double)12.0;
                                    // So sanh voi ton kho hien tai...

                                    if ((dt.Rows[i]["TON_KHO"].ToString() == null) || (dt.Rows[i]["TON_KHO"].ToString() == ""))
                                    {
                                        a = 0;
                                    }
                                    else
                                    {
                                        a = Convert.ToDouble(dt.Rows[i]["TON_KHO"].IfNullZero());
                                    }
                                    //if (temp != 0) { a = Math.Floor((double)a / temp1); }
                                    if (temp != 0)
                                    {
                                        a = Math.Round(a / temp1, 1);
                                    }
                                    else { a = 0; }

                                    can_use_for = "~" + a.ToString() + " W";
                                }
                                else
                                {
                                    can_use_for = " ";
                                }

                                base.m_DBaccess.ExecuteNoneQuery("Update EWIPDINH_MUC_NGUYEN_LIEU_LFEM set CAN_USE_FOR ='" + can_use_for + "', OFFSET_LEAD_TIME =" + offset_lead_time + ", TOTAL_BY_LEAD_TIME =" + total_by_lead_time + ", DINH_MUC_CAN_THIET =" + dinh_muc_can_thiet + ", LUONG_CAN_MUA =" + luong_can_mua + ", TOTAL_PRICE =" + total_price + " where CODE='" + dt.Rows[i][0].ToString() + "'");

                                //Reset lai bien
                                total_by_lead_time = 0;
                                dinh_muc_can_thiet = 0;
                                total_price = 0;
                                can_use_for = " ";
                                temp = 0;
                                temp1 = 0;
                            }
                            // ******************************************************************************************************************

                            // Hien thi lai du lieu trong bang EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER vao Grid

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );

                            dt1 = base.m_ResultDB.ReturnDataSet.Tables[0]; // Để hiển thị theo tuần
                            //*******************************************************************

                            // Combine tuan thanh thang de rut gon Grid view *********************

                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST_TEMP_Monthly"
                                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                                }
                                , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                                }
                            );
                            dt2 = base.m_ResultDB.ReturnDataSet.Tables[0]; // Để hiển tị theo tháng
                                                                           //*******************************************************************
                            dt1.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_4"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W4";
                            dt1.Columns["NEXT_5"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_6"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_7"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_8"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W4";
                            dt1.Columns["NEXT_9"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W1";
                            dt1.Columns["NEXT_10"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W2";
                            dt1.Columns["NEXT_11"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W3";
                            dt1.Columns["NEXT_12"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W4";

                            dt2.Columns["Column1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                            dt2.Columns["Column2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                            dt2.Columns["Column3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();


                            // Do du lieu vao grid control ********************
                            base.m_BindData.BindGridView(gcList, dt1);
                            Display_Gridview_Dinh_muc_nguyen_lieu();

                        }
                        else
                        {
                            base.m_BindData.BindGridView(gcList, dt);
                            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[2].DisplayFormat.FormatString = "{0:n2}";
                            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[3].DisplayFormat.FormatString = "{0:n2}";
                            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvList.Columns[4].DisplayFormat.FormatString = "{0:n2}";
                            //****************************************
                        }
                    }

                }
                // *******************************************************************************************************************************
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        public void Display_Gridview_Dinh_muc_nguyen_lieu() {
            gvList.Columns["LEAD_TIME"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["LEAD_TIME"].DisplayFormat.FormatString = "{0:n0}";
            gvList.Columns["OFFSET_LEAD_TIME"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["OFFSET_LEAD_TIME"].DisplayFormat.FormatString = "{0:n0}";
            gvList.Columns["DU_PHONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            //gvList.Columns["DU_PHONG"].DisplayFormat.FormatString = "{0:n2}";

            for (int j = 5; j < 17; j++)
            {
                gvList.Columns[j].Width = 50;
                gvList.Columns[j].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[j].DisplayFormat.FormatString = "{0:n2}";
            }
            for (int j = 16; j < 21; j++)
            {
                gvList.Columns[j].DisplayFormat.FormatString = "{0:n2}";
            }

            gvList.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[8].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[9].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[10].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[11].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[12].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[13].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[14].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[15].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[16].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[17].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[18].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[19].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[20].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[21].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[22].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            // Freeze column **
            for (int i = 0; i < 4; i++)
            {
                gvList.VisibleColumns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
        }
        private void Init_Control()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(cbModel.Text))
            {
                return;
            }
            else
            {
                this.SearchPage();
            }
            if (cbModel.Text != "ALL")
            {
                btWeekly.Enabled = false;
                btMonthly.Enabled = false;
            }
            else {
                btWeekly.Enabled = true;
                btMonthly.Enabled = true;
            }
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if ((e.RowHandle >= 0))
            {
                if ((e.Column.FieldName == "CAN_USE_FOR") || (e.Column.FieldName == "TON_KHO"))
                {
                    if ((e.Column.FieldName == "CAN_USE_FOR")) {
                        string temp = currentView.GetRowCellValue(e.RowHandle, "CAN_USE_FOR").ToString();
                        if ((temp != "") && (temp != null) && (temp != " "))
                        {
                            temp = temp.Substring(1, temp.Length - 1);
                            string[] vArray = temp.Split(' ');
                            //if (Convert.ToInt32(vArray[0]) < 5)
                            //{
                            //    e.Appearance.BackColor = Color.OrangeRed;
                            //}
                            //if ((Convert.ToInt32(vArray[0]) >= 5) && (Convert.ToInt32(vArray[0]) < 8))
                            //{
                            //    e.Appearance.BackColor = Color.Yellow;
                            //}

                            if (Convert.ToDouble(vArray[0]) < 4.0)
                            {
                                e.Appearance.BackColor = Color.OrangeRed;
                            }
                            if ((Convert.ToDouble(vArray[0]) >= 4.0) && (Convert.ToDouble(vArray[0]) <= 6.0))
                            {
                                e.Appearance.BackColor = Color.Green;
                            }
                            if ((Convert.ToDouble(vArray[0]) > 6.0))
                            {
                                e.Appearance.BackColor = Color.Yellow;
                            }
                        }
                    }

                }
            }
        }

        private void btWeekly_Click(object sender, EventArgs e)
        {
            btMonthly.Enabled = true;
            btWeekly.Enabled = false;
            option_plan = true;
            base.m_BindData.BindGridView(gcList,
                dt1
            );

            gvList.Columns["LEAD_TIME"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["LEAD_TIME"].DisplayFormat.FormatString = "{0:n0}";
            gvList.Columns["OFFSET_LEAD_TIME"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["OFFSET_LEAD_TIME"].DisplayFormat.FormatString = "{0:n0}";
            gvList.Columns["DU_PHONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            //gvList.Columns["DU_PHONG"].DisplayFormat.FormatString = "{0:n2}";

            for (int j = 5; j < 17; j++)
            {
                gvList.Columns[j].Width = 50;
                gvList.Columns[j].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[j].DisplayFormat.FormatString = "{0:n2}";
            }
            for (int j = 17; j < 21; j++)
            {
                gvList.Columns[j].DisplayFormat.FormatString = "{0:n2}";
            }

            gvList.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[8].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[9].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[10].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[11].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[12].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[13].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[14].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[15].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[16].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[17].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[18].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[19].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[20].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[21].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gvList.Columns[22].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            // Freeze column **
            for (int i = 0; i < 4; i++)
            {
                gvList.VisibleColumns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            // ****************
        }

        private void btMonthly_Click(object sender, EventArgs e)
        {
            btMonthly.Enabled = false;
            btWeekly.Enabled = true;
            option_plan = false;
            base.m_BindData.BindGridView(gcList,
                dt2
            );

            for (int j = 5; j < 8; j++)
            {
                gvList.Columns[j].Width = 60;
                gvList.Columns[j].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[j].DisplayFormat.FormatString = "{0:n2}";
            }
            // Freeze column **
            for (int i = 0; i < 4; i++)
            {
                gvList.VisibleColumns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            // *****************
        }

        private void gcList_Click(object sender, EventArgs e)
        {

        }
    }
}
