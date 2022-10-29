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
using System.Collections.Generic;
using System.Linq;
using Wisol.Common;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT016 : PageType
    {
        DataTable dt_hoa_chat = new DataTable();
        DataTable dt_spp = new DataTable();
        public REPORT016()
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
            FromDate_t.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            FromDate.Enabled = false;
            ToDate.Enabled = false;
            cmbView.Enabled = false;
            ToDate.Properties.Mask.EditMask = "Y";
            ToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            ToDate.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT016.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            base.InitializePage();
        }

        public double GET_DINH_LUONG_HOA_CHAT(string vCODE)
        {
            double temp = 0;
            string sql = "select LUONG_DUNG_MOT_THANG from DINH_LUONG_HOA_CHAT_WLP1 where CODE ='" + vCODE + "'";
            try
            {
                // ********************
                string strcnn = "Server=10.70.10.97;Database=WLP1;User Id=sa;Password=Wisol@123";
                DataTable dt = new DataTable();
                SqlConnection cnn = new SqlConnection(strcnn);
                if (cnn.State == ConnectionState.Open) { cnn.Close(); }
                cnn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, cnn);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    temp = Convert.ToDouble(dt.Rows[0][0].ToString());
                }
                else
                {
                    temp = 0;
                }
                cnn.Close();
                cnn.Dispose();

                return temp;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return 0;
            }
        }

        public override void SearchPage()
        {

            base.SearchPage();
            //FromDate.Enabled = true;
            ToDate.Enabled = true;
            ToDate.EditValue = !isSearch ? "" : ToDate.EditValue;

            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();

            cmbView.Enabled = true;
            gvList.FocusedRowHandle = 0;
            try
            {
                string vFrom, vTo;
                string tinh_trang_hien_tai;
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT016.GET_LIST"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }

                vFrom = string.Format("{0:MM-yyyy}", FromDate.DateTime);
                vTo = string.Format("{0:MM-yyyy}", ToDate.DateTime);
                //MessageBox.Show(FromDate.Text);



                //******************** Lấy tồn kho hien tai cua hóa chất WLP1 *********************************************
                string connString = "Data Source = 10.70.10.97;Initial Catalog = WLP1;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("PKG_WLP1005@GET_LIST_NEW_TEMP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@A_PLANT", SqlDbType.VarChar).Value = Consts.PLANT;
                cmd.Parameters.Add("@A_LANG", SqlDbType.VarChar).Value = Consts.USER_INFO.Language;
                cmd.Parameters.Add("@A_LOCATION", SqlDbType.VarChar).Value = "ALL";
                cmd.Parameters.Add("@N_RETURN", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@V_RETURN", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                conn.Open();
                var reader = cmd.ExecuteReader();
                //DataTable dt_hoa_chat = new DataTable();
                dt_hoa_chat.Clear();
                dt_hoa_chat.Load(reader);

                cmd.Dispose();
                conn.Close();
                conn.Dispose();

                foreach (System.Data.DataColumn col in dt_hoa_chat.Columns) col.ReadOnly = false;

                dt_hoa_chat.Columns["STOCK"].MaxLength = 30;
                dt_hoa_chat.Columns["USE_PER_M"].MaxLength = 30;
                dt_hoa_chat.Columns["CAN_USE_FOR"].MaxLength = 30;


                for (int i = 0; i < dt_hoa_chat.Rows.Count; i++)
                {
                    int so_luong;
                    if ((dt_hoa_chat.Rows[i]["QUANTITY"].ToString() != null) || (dt_hoa_chat.Rows[i]["QUANTITY"].ToString() != ""))
                    {
                        so_luong = Convert.ToInt32(dt_hoa_chat.Rows[i]["QUANTITY"].ToString());
                    }
                    else { so_luong = 0; }
                    if (dt_hoa_chat.Rows[i]["QUANTITATIVE"].ToString() == "EA")
                    {
                        dt_hoa_chat.Rows[i]["STOCK"] = 0;
                        dt_hoa_chat.Rows[i]["TOTAL_MONEY_USD"] = 0;
                    }
                    else
                    {
                        string dinh_luong = dt_hoa_chat.Rows[i]["QUANTITATIVE"].ToString();

                        if (string.IsNullOrEmpty(dinh_luong))
                        {
                            dinh_luong = "0 ";
                        }

                        double price_usd = Convert.ToDouble(dt_hoa_chat.Rows[i]["PRICE_USD"].ToString());
                        string v1 = dinh_luong.Substring(0, dinh_luong.IndexOf(' '));
                        string v2 = dinh_luong.Substring(dinh_luong.IndexOf(' ') + 1);
                        double value = Convert.ToDouble(v1);
                        dt_hoa_chat.Rows[i]["STOCK"] = (so_luong * value);
                        dt_hoa_chat.Rows[i]["TOTAL_MONEY_USD"] = so_luong * value * price_usd;

                        // Update vao cot CAN_USE_FOR (dua vao bang DINH_LUONG_HOA_CHAT ***********************************
                        double vDinh_luong_1_thang = GET_DINH_LUONG_HOA_CHAT(dt_hoa_chat.Rows[i]["CODE"].ToString());
                        double can_use = Math.Round(so_luong / vDinh_luong_1_thang * 4.0, 1);

                        //gvList1.SetRowCellValue(i, "USE_PER_M", vDinh_luong_1_thang.ToString());
                        //gvList1.SetRowCellValue(i, "CAN_USE_FOR", "~" + can_use.ToString() + " W");
                        dt_hoa_chat.Rows[i]["USE_PER_M"] = vDinh_luong_1_thang.ToString();
                        dt_hoa_chat.Rows[i]["CAN_USE_FOR"] = "~" + can_use.ToString() + " W";

                        // ************************************************************************************************

                    }
                }

                // Tính số lượng và tiền tồn kho hóa chất hiện tại.*********************************************************
                double so_luong_hoa_chat = 0;// so luong hoa chat hien tai
                double so_tien_hoa_chat = 0;// so tien hoa chat hien tai
                for (int i = 0; i < dt_hoa_chat.Rows.Count; i++)
                {
                    so_luong_hoa_chat = so_luong_hoa_chat + Convert.ToDouble(dt_hoa_chat.Rows[i]["STOCK"].ToString());
                    so_tien_hoa_chat = so_tien_hoa_chat + Convert.ToDouble(dt_hoa_chat.Rows[i]["TOTAL_MONEY_USD"].ToString());
                }

                // **********************************************************************************************************

                // Lây số lượng và giá trị tồn hiện tại spare part của SMT  *********** **************************************
                double so_luong_ton_hien_tai_spp = 0; // dung cho SMT
                double so_tien_ton_hien_tai_spp = 0; // dung cho SMT
                double so_luong_ton_hien_tai_spp_csp = 0; // dung cho CSP
                double so_tien_ton_hien_tai_spp_csp = 0; // dung cho CSP
                double so_luong_ton_hien_tai_spp_wlp1 = 0; // dung cho WLP1
                double so_tien_ton_hien_tai_spp_wlp1 = 0; // dung cho WLP1
                double so_luong_ton_hien_tai_spp_wlp2 = 0; // dung cho WLP2
                double so_tien_ton_hien_tai_spp_wlp2 = 0; // dung cho WLP2
                double so_luong_ton_hien_tai_spp_lfem = 0; // dung cho LFEM
                double so_tien_ton_hien_tai_spp_lfem = 0; // dung cho LFEM

                string connString_spp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                SqlConnection conn_spp = new SqlConnection(connString_spp);
                conn_spp.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select sum(QUANTITY) as 'SO_LUONG_GIA_TRI' from EWIP_SPAREPART_INVENTORY where DEPT_CODE='SMT' union select sum(INVENTORY_VALUES_US) as 'SO_LUONG_GIA_TRI' from EWIP_INVENTORY_VALUES_BY_TIME where DEPT_CODE='SMT' and EWIP_INVENTORY_VALUES_BY_TIME.DATE like  '%' + FORMAT(GETDATE(),'yyyy-MM') + '%' group by DEPT_CODE", conn_spp);
                //DataTable dt_spp = new DataTable();
                sda.Fill(dt_spp);
                if (dt_spp.Rows.Count > 0)
                {
                    so_luong_ton_hien_tai_spp = Convert.ToDouble(dt_spp.Rows[0][0].ToString());
                    so_tien_ton_hien_tai_spp = Convert.ToDouble(dt_spp.Rows[1][0].ToString());
                }
                else
                {
                    so_luong_ton_hien_tai_spp = 0;
                    so_tien_ton_hien_tai_spp = 0;
                }
                conn_spp.Close();
                conn_spp.Dispose();

                // *************************************************************************************************************

                // Lấy tồn kho MRO của các bộ phận ****************************************************************************
                DataTable dt = new DataTable();
                dt = base.m_DBaccess.ExecuteQuery("select * from TON_KHO_CAC_BO_PHAN");
                base.m_BindData.BindGridView(gcList, dt);
                //**************************************************************************************************************

                int vMonth = ToDate.DateTime.Month;
                int vYear = ToDate.DateTime.Year;
                for (int i = 0; i < gvList.RowCount; i++)
                {
                    switch (gvList.GetRowCellValue(i, "DEPARTMENT").ToString())
                    {
                        case "CSP":
                            //if((FromDate.Text!="") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {
                                tinh_trang_hien_tai = "SELECT right(('0' + T1.THANG + '-' + T1.NAM),7) AS TN, T1.DEPARTMENT, sum(T1.SO_LUONG_TON) AS 'LUONG_TON_HIEN_TAI' , sum(T1.SO_LUONG_TON * T2.PRICE) as GIA_TRI_TON_HIEN_TAI FROM[MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT = 'CSP' AND T1.DEPARTMENT = T2.DEPARTMENT AND T1.CODE = T2.CODE  AND CAST(T1.THANG AS int) <=" + vMonth + " AND CAST(T1.NAM AS int) <=" + vYear + " group by right(('0' + T1.THANG + '-' + T1.NAM), 7),T1.DEPARTMENT ORDER BY TN DESC";
                            }
                            else     // Không chọn thời gian, mặc định là tồn kho hiện tại.
                            {
                                tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='CSP' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";
                            }
                            dt = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);

                            if (dt.Rows.Count > 0)
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", dt.Rows[0]["LUONG_TON_HIEN_TAI"]);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", dt.Rows[0]["GIA_TRI_TON_HIEN_TAI"]);
                            }
                            else
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", 0);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", 0);
                            }

                            break;
                        case "WLP2":
                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {
                                tinh_trang_hien_tai = "SELECT right(('0' + T1.THANG + '-' + T1.NAM),7) AS TN, T1.DEPARTMENT, sum(T1.SO_LUONG_TON) AS 'LUONG_TON_HIEN_TAI' , sum(T1.SO_LUONG_TON * T2.PRICE) as GIA_TRI_TON_HIEN_TAI FROM[MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT = 'WLP2' AND T1.DEPARTMENT = T2.DEPARTMENT AND T1.CODE = T2.CODE  AND CAST(T1.THANG AS int) <=" + vMonth + " AND CAST(T1.NAM AS int) <=" + vYear + " group by right(('0' + T1.THANG + '-' + T1.NAM), 7),T1.DEPARTMENT ORDER BY TN DESC";
                            }
                            else    // Không chọn thời gian, mặc định là tồn kho hiện tại.
                            {
                                tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='WLP2' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";
                            }
                            //tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='WLP2' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";
                            dt = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                            if (dt.Rows.Count > 0)
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", dt.Rows[0]["LUONG_TON_HIEN_TAI"]);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", dt.Rows[0]["GIA_TRI_TON_HIEN_TAI"]);
                            }
                            else
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", 0);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", 0);
                            }

                            break;
                        case "LFEM":
                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {
                                tinh_trang_hien_tai = "SELECT right(('0' + T1.THANG + '-' + T1.NAM),7) AS TN, T1.DEPARTMENT, sum(T1.SO_LUONG_TON) AS 'LUONG_TON_HIEN_TAI' , sum(T1.SO_LUONG_TON * T2.PRICE) as GIA_TRI_TON_HIEN_TAI FROM[MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT = 'LFEM' AND T1.DEPARTMENT = T2.DEPARTMENT AND T1.CODE = T2.CODE  AND CAST(T1.THANG AS int) <=" + vMonth + " AND CAST(T1.NAM AS int) <=" + vYear + " group by right(('0' + T1.THANG + '-' + T1.NAM), 7),T1.DEPARTMENT ORDER BY TN DESC";
                            }
                            else    // Không chọn thời gian, mặc định là tồn kho hiện tại.
                            {
                                tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='LFEM' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";
                            }
                            //tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='LFEM' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";
                            dt = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                            if (dt.Rows.Count > 0)
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", dt.Rows[0]["LUONG_TON_HIEN_TAI"]);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", dt.Rows[0]["GIA_TRI_TON_HIEN_TAI"]);
                            }
                            else
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", 0);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", 0);
                            }

                            break;

                        case "WLP1-MRO":
                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {
                                tinh_trang_hien_tai = "SELECT right(('0' + T1.THANG + '-' + T1.NAM),7) AS TN, T1.DEPARTMENT, sum(T1.SO_LUONG_TON) AS 'LUONG_TON_HIEN_TAI' , sum(T1.SO_LUONG_TON * T2.PRICE) as GIA_TRI_TON_HIEN_TAI FROM[MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT = 'WLP1' AND T1.DEPARTMENT = T2.DEPARTMENT AND T1.CODE = T2.CODE  AND CAST(T1.THANG AS int) <=" + vMonth + " AND CAST(T1.NAM AS int) <=" + vYear + " group by right(('0' + T1.THANG + '-' + T1.NAM), 7),T1.DEPARTMENT ORDER BY TN DESC";
                            }
                            else   // Không chọn thời gian, mặc định là tồn kho hiện tại.
                            {
                                tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='WLP1' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";
                            }
                            //tinh_trang_hien_tai = "select x.DEPARTMENT, sum(X.QUANTITY) as 'LUONG_TON_HIEN_TAI',sum(x.TOTAL_MONEY_USD) as 'GIA_TRI_TON_HIEN_TAI' from(SELECT T1.DEPARTMENT, T1.CODE, T2.NAME, SUM(T1.QUANTITY) AS 'QUANTITY', SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT  INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP where T1.DEPARTMENT ='WLP1' GROUP BY T1.DEPARTMENT, T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE) X group by x.DEPARTMENT";

                            dt = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                            if (dt.Rows.Count > 0)
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", dt.Rows[0]["LUONG_TON_HIEN_TAI"]);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", dt.Rows[0]["GIA_TRI_TON_HIEN_TAI"]);
                            }
                            else
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", 0);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", 0);
                            }

                            break;

                        case "WLP1-CHEM":

                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {
                                //string vFrom1= string.Format("{0:yyyyMMdd}", FromDate.DateTime);
                                string vFrom1 = "20200101"; // Tam thoi Fix thoi diem bawt dau, de khong phai sua thu tuc trong SQL.
                                string vTo1 = string.Format("{0:yyyyMMdd}", ToDate.DateTime.AddMonths(1).AddDays(-1));
                                this.Get_ton_kho_HOA_CHAT(ref so_luong_hoa_chat, ref so_tien_hoa_chat, vFrom1, vTo1);
                                gvList.SetRowCellValue(i, "QUANTITY", so_luong_hoa_chat);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_hoa_chat);
                            }
                            else // Ton kho hoa chat hien tai.....
                            {
                                gvList.SetRowCellValue(i, "QUANTITY", so_luong_hoa_chat);
                                gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_hoa_chat);
                            }

                            so_luong_hoa_chat = 0;
                            so_tien_hoa_chat = 0;

                            break;
                        case "SMT-SPARE-PART":

                            so_luong_ton_hien_tai_spp = 0;
                            so_tien_ton_hien_tai_spp = 0;

                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {

                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp, ref so_tien_ton_hien_tai_spp, false, vTo, "SMT");
                            }
                            else  // Không chọn thời gian, mặc định là tồn kho hiện tại.                                        
                            {
                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp, ref so_tien_ton_hien_tai_spp, true, "", "SMT");
                            }

                            gvList.SetRowCellValue(i, "QUANTITY", so_luong_ton_hien_tai_spp);
                            gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_ton_hien_tai_spp);

                            break;

                        case "CSP-SPARE-PART":
                            so_luong_ton_hien_tai_spp_csp = 0;
                            so_tien_ton_hien_tai_spp_csp = 0;

                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {

                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_csp, ref so_tien_ton_hien_tai_spp_csp, false, vTo, "CSP");
                            }
                            else  // Không chọn thời gian, mặc định là tồn kho hiện tại.                                        
                            {
                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_csp, ref so_tien_ton_hien_tai_spp_csp, true, "", "CSP");
                            }

                            gvList.SetRowCellValue(i, "QUANTITY", so_luong_ton_hien_tai_spp_csp);
                            gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_ton_hien_tai_spp_csp);
                            break;

                        case "WLP1-SPARE-PART":
                            so_luong_ton_hien_tai_spp_wlp1 = 0;
                            so_tien_ton_hien_tai_spp_wlp1 = 0;

                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {

                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_wlp1, ref so_tien_ton_hien_tai_spp_wlp1, false, vTo, "WLP1");
                            }
                            else  // Không chọn thời gian, mặc định là tồn kho hiện tại.                                        
                            {
                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_wlp1, ref so_tien_ton_hien_tai_spp_wlp1, true, "", "WLP1");
                            }

                            gvList.SetRowCellValue(i, "QUANTITY", so_luong_ton_hien_tai_spp_wlp1);
                            gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_ton_hien_tai_spp_wlp1);
                            break;

                        case "WLP2-SPARE-PART":
                            so_luong_ton_hien_tai_spp_wlp2 = 0;
                            so_tien_ton_hien_tai_spp_wlp2 = 0;

                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {

                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_wlp2, ref so_tien_ton_hien_tai_spp_wlp2, false, vTo, "WLP2");
                            }
                            else  // Không chọn thời gian, mặc định là tồn kho hiện tại.                                        
                            {
                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_wlp2, ref so_tien_ton_hien_tai_spp_wlp2, true, "", "WLP2");
                            }

                            gvList.SetRowCellValue(i, "QUANTITY", so_luong_ton_hien_tai_spp_wlp2);
                            gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_ton_hien_tai_spp_wlp2);
                            break;

                        case "LFEM-SPARE-PART":
                            so_luong_ton_hien_tai_spp_lfem = 0;
                            so_tien_ton_hien_tai_spp_lfem = 0;

                            //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                            if ((ToDate.Text != ""))
                            {

                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_lfem, ref so_tien_ton_hien_tai_spp_lfem, false, vTo, "LFEM");
                            }
                            else  // Không chọn thời gian, mặc định là tồn kho hiện tại.                                        
                            {
                                Get_ton_kho_SPP(ref so_luong_ton_hien_tai_spp_lfem, ref so_tien_ton_hien_tai_spp_lfem, true, "", "LFEM");
                            }

                            gvList.SetRowCellValue(i, "QUANTITY", so_luong_ton_hien_tai_spp_lfem);
                            gvList.SetRowCellValue(i, "AMOUNT_USD", so_tien_ton_hien_tai_spp_lfem);
                            break;
                    }

                    gvList.RefreshRow(i);
                }
                // ************ Hien thi canh bao dung duoc bao lau dua tren so ton kho.****************
                if (ToDate.Text == "")
                {
                    //this.gvList_RowClick(sender,e);
                }
                //**************************************************************************************
                gvList.Columns[0].Width = 200;
                gvList.Columns[1].Width = 200;
                gvList.Columns[2].Width = 200;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {


        }

        private void gvList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ChartTitle ct = new ChartTitle();
            string query_bieu_do = "";
            DataTable bieu_do_ton_kho = new DataTable();
            string dept = gvList.GetRowCellValue(e.RowHandle, "DEPARTMENT").ToString();
            string vFrom, vTo;
            //vFrom = string.Format("{0:MM-yyyy}", FromDate.DateTime); Tam thoi bo thoi diem bat dau.
            vTo = string.Format("{0:MM-yyyy}", ToDate.DateTime);
            //string t= ToDate.DateTime.ToString("yyyyMM");
            DataTable ton_kho = new DataTable();
            //to_kho = base.m_DBaccess.ExecuteQuery("SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = '" + dept + "' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE");
            string tinh_trang_hien_tai = "";
            string vsql = "";
            switch (dept)
            {
                case "CSP":

                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                    if ((ToDate.Text != ""))
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, T1.SO_LUONG_TON,(T1.SO_LUONG_TON * T2.PRICE) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='CSP' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo +"'";
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='CSP' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='CSP' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='CSP' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME, T2.LOAI_VAN_CHUYEN";
                        //query_bieu_do = "SELECT right(('0' +T1.THANG + '-' + T1.NAM),7) as 'TN',SUM(t1.[SO_LUONG_TON]*t2.PRICE) as 'TON' , sum(t1.[SO_LUONG_NHAP]*t2.PRICE) as 'NHAP',sum(t1.[SO_LUONG_XUAT]*t2.PRICE) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT='CSP'  and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' group by THANG,NAM order by TN";
                    }
                    else
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'CSP' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1 LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU X ON T1.CODE=X.CODE INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'CSP' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE, X.CAN_USE_FOR ORDER BY T1.CODE ";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME, T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1 LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU X ON T1.CODE=X.CODE INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'CSP' GROUP BY T1.CODE, T2.NAME,T2.LOAI_VAN_CHUYEN, T3.NAME_OF_CODE_VN, T2.PRICE, X.CAN_USE_FOR ORDER BY T1.CODE ";
                    }

                    ton_kho = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                    break;
                case "WLP2":

                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                    if ((ToDate.Text != ""))
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, T1.SO_LUONG_TON,(T1.SO_LUONG_TON * T2.PRICE) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP2' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "'";
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP2' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" +vFrom +"' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" +vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP2' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME,T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP2' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME, T2.LOAI_VAN_CHUYEN";
                        //query_bieu_do = "SELECT right(('0' +T1.THANG + '-' + T1.NAM),7) as 'TN',SUM(t1.[SO_LUONG_TON]*t2.PRICE) as 'TON' , sum(t1.[SO_LUONG_NHAP]*t2.PRICE) as 'NHAP',sum(t1.[SO_LUONG_XUAT]*t2.PRICE) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT='WLP2'  and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' group by THANG,NAM order by TN";
                    }
                    else
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'WLP2' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1 LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER X ON T1.CODE=X.CODE INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'WLP2' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE, X.CAN_USE_FOR ORDER BY T1.CODE ";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME, T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1 LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU_WLP2_WAFER X ON T1.CODE=X.CODE INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'WLP2' GROUP BY T1.CODE, T2.NAME,T2.LOAI_VAN_CHUYEN, T3.NAME_OF_CODE_VN, T2.PRICE, X.CAN_USE_FOR ORDER BY T1.CODE ";
                    }

                    ton_kho = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                    break;
                case "LFEM":

                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                    if ((ToDate.Text != ""))
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, T1.SO_LUONG_TON,(T1.SO_LUONG_TON * T2.PRICE) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='LFEM' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "'";
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='LFEM' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='LFEM' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME,T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='LFEM' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME,T2.LOAI_VAN_CHUYEN";
                        //query_bieu_do = "SELECT right(('0' +T1.THANG + '-' + T1.NAM),7) as 'TN',SUM(t1.[SO_LUONG_TON]*t2.PRICE) as 'TON' , sum(t1.[SO_LUONG_NHAP]*t2.PRICE) as 'NHAP',sum(t1.[SO_LUONG_XUAT]*t2.PRICE) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT='LFEM'  and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' group by THANG,NAM order by TN";
                    }
                    else
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'LFEM' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1 LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU_LFEM X ON T1.CODE = X.CODE INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'LFEM' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE, X.CAN_USE_FOR ORDER BY T1.CODE ASC";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME, T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY) * T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1 LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU_LFEM X ON T1.CODE = X.CODE INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'LFEM' GROUP BY T1.CODE, T2.NAME, T2.LOAI_VAN_CHUYEN, T3.NAME_OF_CODE_VN, T2.PRICE, X.CAN_USE_FOR ORDER BY T1.CODE ASC";
                    }

                    ton_kho = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                    break;
                case "WLP1-MRO":

                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Người dùng chọn khoảng thời gian để kiểm tra tồn kho.
                    if ((ToDate.Text != ""))
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, T1.SO_LUONG_TON,(T1.SO_LUONG_TON * T2.PRICE) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP1' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "'";
                        //tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP1' and right(('0' + T1.THANG + '-' + T1.NAM),7) >='" + vFrom + "' and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP1' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME";
                        tinh_trang_hien_tai = "SELECT T1.CODE,T2.NAME, T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', SUM(T1.SO_LUONG_TON) AS 'SO_LUONG_TON',SUM((T1.SO_LUONG_TON * T2.PRICE)) as 'TOTAL_MONEY_USD'  FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT= T2.DEPARTMENT and T1.DEPARTMENT='WLP1' and right(('0' + T1.THANG + '-' + T1.NAM),7) = '" + vTo + "' GROUP BY T1.CODE,T2.NAME,T2.LOAI_VAN_CHUYEN";
                        //query_bieu_do = "SELECT right(('0' +T1.THANG + '-' + T1.NAM),7) as 'TN',SUM(t1.[SO_LUONG_TON]*t2.PRICE) as 'TON' , sum(t1.[SO_LUONG_NHAP]*t2.PRICE) as 'NHAP',sum(t1.[SO_LUONG_XUAT]*t2.PRICE) as 'XUAT' FROM [MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.CODE=t2.CODE and t1.DEPARTMENT='WLP1'  and right(('0' + T1.THANG + '-' + T1.NAM),7) <= '" + vTo + "' group by THANG,NAM order by TN";
                    }
                    else
                    {
                        //tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD FROM EWIPSTOCK T1 INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT	INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'WLP1' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME,CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1  LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU_WLP1 X ON T1.CODE= X.CODE   INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'WLP1' GROUP BY T1.CODE, T2.NAME, T3.NAME_OF_CODE_VN, T2.PRICE,X.CAN_USE_FOR ORDER BY T1.CODE";
                        tinh_trang_hien_tai = "SELECT T1.CODE, T2.NAME, T2.LOAI_VAN_CHUYEN AS 'TRANSPORTATION', CAST(SUM(T1.QUANTITY) AS VARCHAR(20)) AS 'QUANTITY_',SUM(T1.QUANTITY)*T2.PRICE AS TOTAL_MONEY_USD, X.CAN_USE_FOR FROM EWIPSTOCK T1  LEFT JOIN EWIPDINH_MUC_NGUYEN_LIEU_WLP1 X ON T1.CODE= X.CODE   INNER JOIN EWIPMRO T2 ON T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT INNER JOIN ESYSMSTUNIT T3 ON T2.UNIT = T3.CODE_GROUP WHERE T1.DEPARTMENT = 'WLP1' GROUP BY T1.CODE, T2.NAME, T2.LOAI_VAN_CHUYEN, T3.NAME_OF_CODE_VN, T2.PRICE,X.CAN_USE_FOR ORDER BY T1.CODE";
                    }

                    ton_kho = base.m_DBaccess.ExecuteQuery(tinh_trang_hien_tai);
                    break;
                case "WLP1-CHEM":
                    //ton_kho = dt_hoa_chat;
                    //string vFrom1 = string.Format("{0:yyyyMMdd}", FromDate.DateTime);
                    string vFrom1 = "20200101"; // tam thoi Fix thoi diem bat dau, de khong phai sua thu tuc trong SQL.
                    string vTo1 = string.Format("{0:yyyyMMdd}", ToDate.DateTime.AddMonths(1).AddDays(-1));
                    string connString_hoa_chat = "Data Source = 10.70.10.97;Initial Catalog = WLP1;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_hoa_chat = new SqlConnection(connString_hoa_chat);
                    //if ((FromDate.Text != "") && (ToDate.Text != ""))
                    if ((ToDate.Text != ""))
                    {
                        SqlCommand cmd = new SqlCommand("PKG_WLP1007@GET_LIST_TEMP", conn_hoa_chat);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@A_PLANT", SqlDbType.VarChar).Value = Consts.PLANT;
                        cmd.Parameters.Add("@A_LANG", SqlDbType.VarChar).Value = Consts.USER_INFO.Language;
                        cmd.Parameters.Add("@A_FROM", SqlDbType.VarChar).Value = vFrom1;
                        cmd.Parameters.Add("@A_TO", SqlDbType.VarChar).Value = vTo1;
                        cmd.Parameters.Add("@N_RETURN", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@V_RETURN", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                        conn_hoa_chat.Open();
                        var reader = cmd.ExecuteReader();
                        DataTable dt_hoa_chat_temp = new DataTable();
                        //dt_hoa_chat.Clear();

                        dt_hoa_chat_temp.Load(reader);
                        dt_hoa_chat_temp.Columns["TOTAL_MONEY_USD"].ReadOnly = false;
                        foreach (DataRow row in dt_hoa_chat_temp.Rows)
                        {
                            if(double.TryParse(row["TOTAL_MONEY_USD"].NullString(),out double usd))
                            {
                                if (row["QUANTITATIVE"].ToString() != "EA")
                                {
                                    string dinh_luong = row["QUANTITATIVE"].ToString();

                                    if (string.IsNullOrEmpty(dinh_luong))
                                    {
                                        dinh_luong = "0 ";
                                    }

                                    string v1 = dinh_luong.Substring(0, dinh_luong.IndexOf(' '));
                                    double value = Convert.ToDouble(v1);
                                    row["TOTAL_MONEY_USD"] = usd * value;
                                }
                            }
                        }

                        ton_kho = dt_hoa_chat_temp;

                        cmd.Dispose();
                        conn_hoa_chat.Close();
                    }
                    else
                    {
                        ton_kho = dt_hoa_chat;
                    }
                    conn_hoa_chat.Dispose();

                    break;
                case "SMT-SPARE-PART":


                    string connString_spp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp = new SqlConnection(connString_spp);
                    SqlDataAdapter sda;
                    SqlDataAdapter sda_query_bieu_do;
                    conn_spp.Open();
                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Lay theo khoang thoi gian nguoi dung chon.
                    if ((ToDate.Text != ""))
                    {
                        string fromDate = ToDate.DateTime.AddYears(-1).ToString("yyyyMM");

                        //sda = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,T4.QUANTITY, T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') >='" + vFrom + "' and FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') <='" + vTo + "' and T2.DEPT_CODE='SMT' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE --group by T2. DEPT_CODE", conn_spp);
                        sda = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,sum(T4.QUANTITY) as 'QUANTITY', sum(T2.INVENTORY_VALUES_US) as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') ='" + vTo + "' and T2.DEPT_CODE='SMT' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE group by t2.SPARE_PART_CODE,t3.NAME_VI", conn_spp);
                        //query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(CONVERT(Date,T1.DATE),'MM-yyyy') <='" + vTo + "' and T1.DEPT_CODE='SMT' group by T1.MONTH,T1.YEAR ORDER BY T1.MONTH,T1.YEAR";
                        query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(cast(t1.DATE as date),'yyyyMM') >= '" + fromDate + "' AND FORMAT(cast(t1.DATE as date),'yyyyMM') <='" + ToDate.DateTime.ToString("yyyyMM") + "' and T1.DEPT_CODE='SMT' group by T1.MONTH,T1.YEAR ORDER BY  T1.YEAR,T1.MONTH asc";
                        sda_query_bieu_do = new SqlDataAdapter(query_bieu_do, conn_spp);
                        sda_query_bieu_do.Fill(bieu_do_ton_kho);
                    }
                    else  // Mac dinh lay ton hien tai.
                    {
                        sda = new SqlDataAdapter(
                            @"select 
                                  T1.SPARE_PART_CODE, 
                                  T3.NAME_VI, 
                                  round(T1.QUANTITY,1) AS 'QUANTITY_', 
                                  T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD',
                                   CASE WHEN ISNULL(BM.WORKING_A_MONTH,BOM.WORKING_A_MONTH) IS NULL THEN NULL ELSE ROUND(([dbo].[CONVERT_UNIT](T1.UNIT_IN_CODE,'PACK',T1.SPARE_PART_CODE,'SMT' )* ISNULL(T1.[QUANTITY],0)/ (ISNULL(BM.WORKING_A_MONTH,BOM.WORKING_A_MONTH)/ISNULL(BM.WORKING_DAY_NUMBER,BOM.WORKING_DAY_NUMBER)))/7,1) END AS CAN_USE
                                   ,
                                   CASE WHEN [dbo].[CONVERT_UNIT](mins.UNIT_CODE,T1.UNIT_IN_CODE,T1.SPARE_PART_CODE,'SMT')* ISNULL(mins.MIN_STOCK,0) * 1.2 <= T1.QUANTITY AND [dbo].[CONVERT_UNIT](mins.UNIT_CODE,T1.UNIT_IN_CODE,T1.SPARE_PART_CODE,'SMT')* ISNULL(mins.MIN_STOCK,0) * 1.5 >= T1.QUANTITY THEN 'G' 
                                        WHEN [dbo].[CONVERT_UNIT](mins.UNIT_CODE,T1.UNIT_IN_CODE,T1.SPARE_PART_CODE,'SMT')* ISNULL(mins.MIN_STOCK,0) * 1.5 < T1.QUANTITY  THEN 'Y'
                                		ELSE 'R' END AS RYG
                                
                                from 
                                  EWIP_SPAREPART_INVENTORY T1 inner join 
                                  EWIP_INVENTORY_VALUES_BY_TIME T2 on T1.SPARE_PART_CODE = T2.SPARE_PART_CODE AND T1.DEPT_CODE = T2.DEPT_CODE  
                                  INNER JOIN  EWIP_SPARE_PART T3 ON T2.DEPT_CODE = t3.SP_DEPT_CODE AND T2.SPARE_PART_CODE = T3.CODE  
                                  LEFT JOIN [dbo].[EWIP_BOM_BY_MONTH] BM ON T1.SPARE_PART_CODE = BM.SPARE_PART_CODE AND T1.DEPT_CODE = BM.DEPT_CODE AND BM.MONTH = MONTH(GETDATE()) AND BM.YEART = YEAR(GETDATE())
                                			LEFT JOIN [dbo].[EWIP_BOM] BOM ON BOM.SPARE_PART_CODE = T1.SPARE_PART_CODE AND BOM.DEPT_CODE = T1.DEPT_CODE
                                  LEFT JOIN [dbo].[EWIP_SETUP_MINSTOCK] mins ON T1.SPARE_PART_CODE = mins.SPARE_PART_CODE AND T1.DEPT_CODE = mins.DEPT_CODE AND T1.STOCK_CODE =mins.STOCK_CODE
                                
                                where 
                                  T1.SPARE_PART_CODE = T2.SPARE_PART_CODE 
                                  and T2.SPARE_PART_CODE = T3.CODE 
                                  AND T1.DEPT_CODE = T2.DEPT_CODE 
                                  and T2.DEPT_CODE = t3.SP_DEPT_CODE 
                                  AND T1.DEPT_CODE = 'SMT' 
                                  AND T2.DATE like '%' + FORMAT(
                                    GETDATE(), 
                                    'yyyy-MM'
                                  ) + '%'", conn_spp);
                    }

                    //DataTable dt_spp = new DataTable();
                    sda.Fill(ton_kho);
                    conn_spp.Close();
                    conn_spp.Dispose();
                    break;

                case "CSP-SPARE-PART":

                    string connString_spp_csp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp_csp = new SqlConnection(connString_spp_csp);
                    SqlDataAdapter sda_csp;
                    SqlDataAdapter sda_query_bieu_do_csp;
                    conn_spp_csp.Open();
                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Lay theo khoang thoi gian nguoi dung chon.
                    if ((ToDate.Text != ""))
                    {
                        string fromDate = ToDate.DateTime.AddYears(-1).ToString("yyyyMM");

                        //sda = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,T4.QUANTITY, T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') >='" + vFrom + "' and FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') <='" + vTo + "' and T2.DEPT_CODE='SMT' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE --group by T2. DEPT_CODE", conn_spp);
                        sda_csp = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_KR,sum(T4.QUANTITY) as 'QUANTITY', sum(T2.INVENTORY_VALUES_US) as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') ='" + vTo + "' and T2.DEPT_CODE='CSP' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE AND T2.INVENTORY_VALUES_US >= 0 group by t2.SPARE_PART_CODE,t3.NAME_KR", conn_spp_csp);
                        //query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(CONVERT(Date,T1.DATE),'MM-yyyy') <='" + vTo + "' and T1.DEPT_CODE='SMT' group by T1.MONTH,T1.YEAR ORDER BY T1.MONTH,T1.YEAR";
                        query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(cast(t1.DATE as date),'yyyyMM') >= '" + fromDate + "' AND FORMAT(cast(t1.DATE as date),'yyyyMM') <='" + ToDate.DateTime.ToString("yyyyMM") + "' and T1.DEPT_CODE='CSP' AND T1.INVENTORY_VALUES_US > 0 group by T1.MONTH,T1.YEAR ORDER BY  T1.YEAR,T1.MONTH asc";
                        sda_query_bieu_do_csp = new SqlDataAdapter(query_bieu_do, conn_spp_csp);
                        sda_query_bieu_do_csp.Fill(bieu_do_ton_kho);
                    }
                    else  // Mac dinh lay ton hien tai.
                    {
                        vsql = vsql + " SELECT * \n";
                        vsql = vsql + " FROM( \n";
                        vsql = vsql + " SELECT ESL.SPARE_PART_CODE, sp.NAME_KR,ISNULL(ESL.QUANTITY,0) as QUANTITY_,	SUM(ISNULL(ESL.QUANTITY,0) * ([dbo].[CONVERT_UNIT](esl.UNIT_ID,'PACK',esl.SPARE_PART_CODE,'CSP')) * ISNULL(SUB2.PRICE_US,SUB.PRICE_US)) AS TOTAL_MONEY_USD \n";
                        vsql = vsql + " FROM EWIP_SPAREPART_LOCATION esl \n";
                        vsql = vsql + " INNER JOIN [dbo].[EWIP_SPARE_PART] sp ON esl.SPARE_PART_CODE = sp.CODE and ESL.DEPT_CODE = SP.SP_DEPT_CODE \n";
                        vsql = vsql + " LEFT JOIN ( \n";
                        vsql = vsql + " SELECT EO.PO_ID AS PO_NO,eprd.SPAREPART_CODE ,'PACK' as UNIT,eprd.DEPT_CODE,eprd.PRICE_VN/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'CSP') AS PRICE_VN,eprd.PRICE_US/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'CSP') AS PRICE_US \n";
                        vsql = vsql + " FROM EWIP_ORDER eo \n";
                        vsql = vsql + " INNER JOIN EWIP_ORDER_PR eop ON EO.PO_ID_TEMP = eop.PO_ID_TEMP \n";
                        vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST epr ON EOP.PR_CODE = EPR.PR_CODE AND EOP.DEPT_CODE = EPR.DEPT_CODE \n";
                        vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST_DETAIL eprd ON EPR.PR_CODE = eprd.PR_CODE AND EPR.DEPT_CODE = eprd.DEPT_CODE \n";
                        vsql = vsql + " ) SUB2 ON SUB2.SPAREPART_CODE = esl.SPARE_PART_CODE AND SUB2.DEPT_CODE = esl.DEPT_CODE AND SUB2.PO_NO = esl.PO_NO \n";
                        vsql = vsql + " LEFT JOIN (SELECT TOP 1 WITH TIES  \n";
                        vsql = vsql + " [SPARE_PART_CODE], \n";
                        vsql = vsql + " [PRICE_VN]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'CSP') AS [PRICE_VN], \n";
                        vsql = vsql + " [PRICE_US]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'CSP') AS [PRICE_US], \n";
                        vsql = vsql + " 'PACK' AS UNIT \n";
                        vsql = vsql + " FROM [dbo].[EWIP_SPAREPART_PRICE] \n";
                        vsql = vsql + " WHERE [DEPT_CODE] = 'CSP' \n";
                        vsql = vsql + " ORDER BY ROW_NUMBER() OVER(PARTITION BY SPARE_PART_CODE ORDER BY [DATE] DESC)) SUB ON SUB.SPARE_PART_CODE = ESL.SPARE_PART_CODE \n";
                        vsql = vsql + " LEFT JOIN EWIP_VENDER V ON V.VENDER_ID = SP.VENDER_ID \n";
                        vsql = vsql + " WHERE ESL.DEPT_CODE = 'CSP'  AND esl.CONDITION_CODE NOT IN('INTEGRATED','NG') \n";
                        vsql = vsql + " GROUP BY ESL.SPARE_PART_CODE,SP.SP_DEPT_CODE,sp.UNIT_ID, SP.NAME_KR, ESL.QUANTITY) SUB";


                        //sda_csp = new SqlDataAdapter("select T1.SPARE_PART_CODE,T3.NAME_VI, T1.QUANTITY AS 'QUANTITY_',T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_SPAREPART_INVENTORY T1 ,EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3 where T1.SPARE_PART_CODE= T2.SPARE_PART_CODE and T2.SPARE_PART_CODE =T3.CODE AND T1.DEPT_CODE= T2.DEPT_CODE and T2.DEPT_CODE=t3.SP_DEPT_CODE  AND T1.DEPT_CODE='CSP' AND T2.DATE like  '%' + FORMAT(GETDATE(),'yyyy-MM') + '%' ", conn_spp_csp);
                        sda_csp = new SqlDataAdapter(vsql, conn_spp_csp);
                    }

                    //DataTable dt_spp = new DataTable();
                    sda_csp.Fill(ton_kho);
                    conn_spp_csp.Close();
                    conn_spp_csp.Dispose();
                    break;

                case "WLP1-SPARE-PART":

                    string connString_spp_wlp1 = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp_wlp1 = new SqlConnection(connString_spp_wlp1);
                    SqlDataAdapter sda_wlp1;
                    SqlDataAdapter sda_query_bieu_do_wlp1;
                    conn_spp_wlp1.Open();
                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Lay theo khoang thoi gian nguoi dung chon.
                    if ((ToDate.Text != ""))
                    {
                        string fromDate = ToDate.DateTime.AddYears(-1).ToString("yyyyMM");

                        //sda = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,T4.QUANTITY, T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') >='" + vFrom + "' and FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') <='" + vTo + "' and T2.DEPT_CODE='SMT' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE --group by T2. DEPT_CODE", conn_spp);
                        sda_wlp1 = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,sum(T4.QUANTITY) as 'QUANTITY', sum(T2.INVENTORY_VALUES_US) as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') ='" + vTo + "' and T2.DEPT_CODE='WLP1' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE group by t2.SPARE_PART_CODE,t3.NAME_VI", conn_spp_wlp1);
                        //query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(CONVERT(Date,T1.DATE),'MM-yyyy') <='" + vTo + "' and T1.DEPT_CODE='SMT' group by T1.MONTH,T1.YEAR ORDER BY T1.MONTH,T1.YEAR";
                        query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(cast(t1.DATE as date),'yyyyMM') >='" + fromDate + "' AND FORMAT(cast(t1.DATE as date),'yyyyMM') <='" + ToDate.DateTime.ToString("yyyyMM") + "' and T1.DEPT_CODE='WLP1' group by T1.MONTH,T1.YEAR ORDER BY  T1.YEAR,T1.MONTH asc";
                        sda_query_bieu_do_wlp1 = new SqlDataAdapter(query_bieu_do, conn_spp_wlp1);
                        sda_query_bieu_do_wlp1.Fill(bieu_do_ton_kho);
                    }
                    else  // Mac dinh lay ton hien tai.
                    {
                        vsql = vsql + " SELECT * \n";
                        vsql = vsql + " FROM( \n";
                        vsql = vsql + " SELECT ESL.SPARE_PART_CODE, sp.NAME_VI,ISNULL(ESL.QUANTITY,0) as QUANTITY_,	SUM(ISNULL(ESL.QUANTITY,0) * ([dbo].[CONVERT_UNIT](esl.UNIT_ID,'PACK',esl.SPARE_PART_CODE,'WLP1')) * ISNULL(SUB2.PRICE_US,SUB.PRICE_US)) AS TOTAL_MONEY_USD \n";
                        vsql = vsql + " FROM EWIP_SPAREPART_LOCATION esl \n";
                        vsql = vsql + " INNER JOIN [dbo].[EWIP_SPARE_PART] sp ON esl.SPARE_PART_CODE = sp.CODE and ESL.DEPT_CODE = SP.SP_DEPT_CODE \n";
                        vsql = vsql + " LEFT JOIN ( \n";
                        vsql = vsql + " SELECT EO.PO_ID AS PO_NO,eprd.SPAREPART_CODE ,'PACK' as UNIT,eprd.DEPT_CODE,eprd.PRICE_VN/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'WLP1') AS PRICE_VN,eprd.PRICE_US/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'WLP1') AS PRICE_US \n";
                        vsql = vsql + " FROM EWIP_ORDER eo \n";
                        vsql = vsql + " INNER JOIN EWIP_ORDER_PR eop ON EO.PO_ID_TEMP = eop.PO_ID_TEMP \n";
                        vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST epr ON EOP.PR_CODE = EPR.PR_CODE AND EOP.DEPT_CODE = EPR.DEPT_CODE \n";
                        vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST_DETAIL eprd ON EPR.PR_CODE = eprd.PR_CODE AND EPR.DEPT_CODE = eprd.DEPT_CODE \n";
                        vsql = vsql + " ) SUB2 ON SUB2.SPAREPART_CODE = esl.SPARE_PART_CODE AND SUB2.DEPT_CODE = esl.DEPT_CODE AND SUB2.PO_NO = esl.PO_NO \n";
                        vsql = vsql + " LEFT JOIN (SELECT TOP 1 WITH TIES  \n";
                        vsql = vsql + " [SPARE_PART_CODE], \n";
                        vsql = vsql + " [PRICE_VN]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'WLP1') AS [PRICE_VN], \n";
                        vsql = vsql + " [PRICE_US]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'WLP1') AS [PRICE_US], \n";
                        vsql = vsql + " 'PACK' AS UNIT \n";
                        vsql = vsql + " FROM [dbo].[EWIP_SPAREPART_PRICE] \n";
                        vsql = vsql + " WHERE [DEPT_CODE] = 'WLP1' \n";
                        vsql = vsql + " ORDER BY ROW_NUMBER() OVER(PARTITION BY SPARE_PART_CODE ORDER BY [DATE] DESC)) SUB ON SUB.SPARE_PART_CODE = ESL.SPARE_PART_CODE \n";
                        vsql = vsql + " LEFT JOIN EWIP_VENDER V ON V.VENDER_ID = SP.VENDER_ID \n";
                        vsql = vsql + " WHERE ESL.DEPT_CODE = 'WLP1'  AND esl.CONDITION_CODE NOT IN('INTEGRATED','NG') \n";
                        vsql = vsql + " GROUP BY ESL.SPARE_PART_CODE,SP.SP_DEPT_CODE,sp.UNIT_ID, SP.NAME_VI, ESL.QUANTITY) SUB";

                        //sda_wlp1 = new SqlDataAdapter("select T1.SPARE_PART_CODE,T3.NAME_VI, T1.QUANTITY AS 'QUANTITY_',T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_SPAREPART_INVENTORY T1 ,EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3 where T1.SPARE_PART_CODE= T2.SPARE_PART_CODE and T2.SPARE_PART_CODE =T3.CODE AND T1.DEPT_CODE= T2.DEPT_CODE and T2.DEPT_CODE=t3.SP_DEPT_CODE  AND T1.DEPT_CODE='WLP1' AND T2.DATE like  '%' + FORMAT(GETDATE(),'yyyy-MM') + '%' ", conn_spp_wlp1);
                        sda_wlp1 = new SqlDataAdapter(vsql, conn_spp_wlp1);
                    }

                    //DataTable dt_spp = new DataTable();
                    sda_wlp1.Fill(ton_kho);
                    conn_spp_wlp1.Close();
                    conn_spp_wlp1.Dispose();
                    break;

                case "WLP2-SPARE-PART":

                    string connString_spp_wlp2 = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp_wlp2 = new SqlConnection(connString_spp_wlp2);
                    SqlDataAdapter sda_wlp2;
                    SqlDataAdapter sda_query_bieu_do_wlp2;
                    conn_spp_wlp2.Open();
                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Lay theo khoang thoi gian nguoi dung chon.
                    if ((ToDate.Text != ""))
                    {
                        string fromDate = ToDate.DateTime.AddYears(-1).ToString("yyyyMM");

                        //sda = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,T4.QUANTITY, T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') >='" + vFrom + "' and FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') <='" + vTo + "' and T2.DEPT_CODE='SMT' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE --group by T2. DEPT_CODE", conn_spp);
                        sda_wlp2 = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,sum(T4.QUANTITY) as 'QUANTITY', sum(T2.INVENTORY_VALUES_US) as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') ='" + vTo + "' and T2.DEPT_CODE='WLP2' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE group by t2.SPARE_PART_CODE,t3.NAME_VI", conn_spp_wlp2);
                        //query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(CONVERT(Date,T1.DATE),'MM-yyyy') <='" + vTo + "' and T1.DEPT_CODE='SMT' group by T1.MONTH,T1.YEAR ORDER BY T1.MONTH,T1.YEAR";
                        query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(cast(t1.DATE as date),'yyyyMM') >= '" + fromDate + "' AND FORMAT(cast(t1.DATE as date),'yyyyMM') <='" + ToDate.DateTime.ToString("yyyyMM") + "' and T1.DEPT_CODE='WLP2' group by T1.MONTH,T1.YEAR ORDER BY  T1.YEAR,T1.MONTH asc";
                        sda_query_bieu_do_wlp2 = new SqlDataAdapter(query_bieu_do, conn_spp_wlp2);
                        sda_query_bieu_do_wlp2.Fill(bieu_do_ton_kho);
                    }
                    else  // Mac dinh lay ton hien tai.
                    {
                        vsql = vsql + " SELECT * \n";
                        vsql = vsql + " FROM( \n";
                        vsql = vsql + " SELECT ESL.SPARE_PART_CODE, sp.NAME_VI,ISNULL(ESL.QUANTITY,0) as QUANTITY_,	SUM(ISNULL(ESL.QUANTITY,0) * ([dbo].[CONVERT_UNIT](esl.UNIT_ID,'PACK',esl.SPARE_PART_CODE,'WLP2')) * ISNULL(SUB2.PRICE_US,SUB.PRICE_US)) AS TOTAL_MONEY_USD \n";
                        vsql = vsql + " FROM EWIP_SPAREPART_LOCATION esl \n";
                        vsql = vsql + " INNER JOIN [dbo].[EWIP_SPARE_PART] sp ON esl.SPARE_PART_CODE = sp.CODE and ESL.DEPT_CODE = SP.SP_DEPT_CODE \n";
                        vsql = vsql + " LEFT JOIN ( \n";
                        vsql = vsql + " SELECT EO.PO_ID AS PO_NO,eprd.SPAREPART_CODE ,'PACK' as UNIT,eprd.DEPT_CODE,eprd.PRICE_VN/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'WLP2') AS PRICE_VN,eprd.PRICE_US/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'WLP2') AS PRICE_US \n";
                        vsql = vsql + " FROM EWIP_ORDER eo \n";
                        vsql = vsql + " INNER JOIN EWIP_ORDER_PR eop ON EO.PO_ID_TEMP = eop.PO_ID_TEMP \n";
                        vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST epr ON EOP.PR_CODE = EPR.PR_CODE AND EOP.DEPT_CODE = EPR.DEPT_CODE \n";
                        vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST_DETAIL eprd ON EPR.PR_CODE = eprd.PR_CODE AND EPR.DEPT_CODE = eprd.DEPT_CODE \n";
                        vsql = vsql + " ) SUB2 ON SUB2.SPAREPART_CODE = esl.SPARE_PART_CODE AND SUB2.DEPT_CODE = esl.DEPT_CODE AND SUB2.PO_NO = esl.PO_NO \n";
                        vsql = vsql + " LEFT JOIN (SELECT TOP 1 WITH TIES  \n";
                        vsql = vsql + " [SPARE_PART_CODE], \n";
                        vsql = vsql + " [PRICE_VN]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'WLP2') AS [PRICE_VN], \n";
                        vsql = vsql + " [PRICE_US]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'WLP2') AS [PRICE_US], \n";
                        vsql = vsql + " 'PACK' AS UNIT \n";
                        vsql = vsql + " FROM [dbo].[EWIP_SPAREPART_PRICE] \n";
                        vsql = vsql + " WHERE [DEPT_CODE] = 'WLP2' \n";
                        vsql = vsql + " ORDER BY ROW_NUMBER() OVER(PARTITION BY SPARE_PART_CODE ORDER BY [DATE] DESC)) SUB ON SUB.SPARE_PART_CODE = ESL.SPARE_PART_CODE \n";
                        vsql = vsql + " LEFT JOIN EWIP_VENDER V ON V.VENDER_ID = SP.VENDER_ID \n";
                        vsql = vsql + " WHERE ESL.DEPT_CODE = 'WLP2'  AND esl.CONDITION_CODE NOT IN('INTEGRATED','NG') \n";
                        vsql = vsql + " GROUP BY ESL.SPARE_PART_CODE,SP.SP_DEPT_CODE,sp.UNIT_ID, SP.NAME_VI, ESL.QUANTITY) SUB";


                        //sda_wlp2 = new SqlDataAdapter("select T1.SPARE_PART_CODE,T3.NAME_VI, T1.QUANTITY AS 'QUANTITY_',T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_SPAREPART_INVENTORY T1 ,EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3 where T1.SPARE_PART_CODE= T2.SPARE_PART_CODE and T2.SPARE_PART_CODE =T3.CODE AND T1.DEPT_CODE= T2.DEPT_CODE and T2.DEPT_CODE=t3.SP_DEPT_CODE  AND T1.DEPT_CODE='WLP2' AND T2.DATE like  '%' + FORMAT(GETDATE(),'yyyy-MM') + '%' ", conn_spp_wlp2);
                        sda_wlp2 = new SqlDataAdapter(vsql, conn_spp_wlp2);
                    }

                    //DataTable dt_spp = new DataTable();
                    sda_wlp2.Fill(ton_kho);
                    conn_spp_wlp2.Close();
                    conn_spp_wlp2.Dispose();
                    break;

                case "LFEM-SPARE-PART":

                    string connString_spp_lfem = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp_lfem = new SqlConnection(connString_spp_lfem);
                    SqlDataAdapter sda_lfem;
                    SqlDataAdapter sda_query_bieu_do_lfem;
                    conn_spp_lfem.Open();
                    //if ((FromDate.Text != "") && (ToDate.Text != "")) // Lay theo khoang thoi gian nguoi dung chon.
                    if ((ToDate.Text != ""))
                    {
                        string fromDate = ToDate.DateTime.AddYears(-1).ToString("yyyyMM");

                        //sda = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,T4.QUANTITY, T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') >='" + vFrom + "' and FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') <='" + vTo + "' and T2.DEPT_CODE='SMT' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE --group by T2. DEPT_CODE", conn_spp);
                        sda_lfem = new SqlDataAdapter("select t2.SPARE_PART_CODE, T3.NAME_VI,sum(T4.QUANTITY) as 'QUANTITY', sum(T2.INVENTORY_VALUES_US) as 'TOTAL_MONEY_USD' from EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3, EWIP_SPAREPART_INVENTORY T4 where FORMAT(CONVERT(Date,T2.DATE),'MM-yyyy') ='" + vTo + "' and T2.DEPT_CODE='LFEM' and t2.SPARE_PART_CODE=t3.CODE  and t2.DEPT_CODE=t3.SP_DEPT_CODE and T2.DEPT_CODE=T4.DEPT_CODE AND T2.SPARE_PART_CODE=T4.SPARE_PART_CODE group by t2.SPARE_PART_CODE,t3.NAME_VI", conn_spp_lfem);
                        //query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(CONVERT(Date,T1.DATE),'MM-yyyy') <='" + vTo + "' and T1.DEPT_CODE='SMT' group by T1.MONTH,T1.YEAR ORDER BY T1.MONTH,T1.YEAR";
                        query_bieu_do = "select right(('0' +  CAST(T1.MONTH AS VARCHAR(10)) + '-' + CAST(T1.YEAR AS VARCHAR(10))),7) as 'TN', ROUND(sum(T1.INVENTORY_VALUES_US),2) AS 'TON' from EWIP_INVENTORY_VALUES_BY_TIME T1  where FORMAT(cast(t1.DATE as date),'yyyyMM') >= '" + fromDate + "' AND FORMAT(cast(t1.DATE as date),'yyyyMM') <='" + ToDate.DateTime.ToString("yyyyMM") + "' and T1.DEPT_CODE='LFEM' group by T1.MONTH,T1.YEAR ORDER BY  T1.YEAR,T1.MONTH asc";
                        sda_query_bieu_do_lfem = new SqlDataAdapter(query_bieu_do, conn_spp_lfem);
                        sda_query_bieu_do_lfem.Fill(bieu_do_ton_kho);
                    }
                    else  // Mac dinh lay ton hien tai.
                    {
                        sda_lfem = new SqlDataAdapter("select T1.SPARE_PART_CODE,T3.NAME_VI, T1.QUANTITY AS 'QUANTITY_',T2.INVENTORY_VALUES_US as 'TOTAL_MONEY_USD' from EWIP_SPAREPART_INVENTORY T1 ,EWIP_INVENTORY_VALUES_BY_TIME T2,EWIP_SPARE_PART T3 where T1.SPARE_PART_CODE= T2.SPARE_PART_CODE and T2.SPARE_PART_CODE =T3.CODE AND T1.DEPT_CODE= T2.DEPT_CODE and T2.DEPT_CODE=t3.SP_DEPT_CODE  AND T1.DEPT_CODE='LFEM' AND T2.DATE like  '%' + FORMAT(GETDATE(),'yyyy-MM') + '%' ", conn_spp_lfem);
                    }

                    //DataTable dt_spp = new DataTable();
                    sda_lfem.Fill(ton_kho);
                    conn_spp_lfem.Close();
                    conn_spp_lfem.Dispose();
                    break;
            }

            // Revise table ton_kho, them cột kiểu số nguyên chứa số tuần có thể dung. Mục đích để có thể Filter được. **********************
            if (ToDate.Text == "")
            {
                if ((dept != "SMT-SPARE-PART") && (dept != "CSP-SPARE-PART") && (dept != "WLP1-SPARE-PART") && (dept != "WLP2-SPARE-PART") && (dept != "LFEM-SPARE-PART"))
                {
                    if (!ton_kho.Columns.Contains("CAN_USE"))
                    {
                        ton_kho.Columns.Add("CAN_USE", typeof(double));
                    }


                    for (int i = 0; i < ton_kho.Rows.Count; i++)
                    {
                        if ((ton_kho.Rows[i]["CAN_USE_FOR"].NullString() != ""))
                        {
                            ton_kho.Rows[i]["CAN_USE"] = Convert.ToDouble(ton_kho.Rows[i]["CAN_USE_FOR"].NullString().Replace("W", "").Replace("~", ""));
                        }
                    }
                }
            }

            // ******************************************************************************************************************************

            if (ton_kho.Columns.Count > 8)// Truong hop la ton kho hoa chat thi 
            {
                base.m_BindData.BindGridView(gcList1, ton_kho);


                //gvList1.Columns[2].Visible = false;
                gvList1.Columns[4].Visible = false;
                gvList1.Columns[5].Visible = false;
                gvList1.Columns[6].Visible = false;
                gvList1.Columns[7].Visible = false;
                //gvList1.Columns[9].Visible = false;
                //gvList1.Columns[11].Visible = false;
                if (ToDate.Text == "") { gvList1.Columns[10].Visible = false; }
                gvList1.RefreshData();

            }
            else
            {
                if (ton_kho.Columns.Count > 5)
                {
                    base.m_BindData.BindGridView(gcList1, ton_kho);
                    gvList1.Columns[5].Visible = false;
                }
                else
                {
                    base.m_BindData.BindGridView(gcList1, ton_kho); // Truong hop ton kho cua SPARE PART
                }
            }

            // Fix size column
            gvList1.Columns[1].Width = 400;
            gvList1.Columns[2].Width = 100;
            gvList1.Columns[3].Width = 100;

            // Ve bieu do ton kho khi nguoi dung chon phong ban tuong ung. *****************************

            if ((ToDate.Text != ""))
            {
                chartControl1.Series.Clear();
                chartControl1.Titles.Clear();
                string vdept = gvList.GetRowCellValue(e.RowHandle, "DEPARTMENT").ToString();
                if ((vdept != "SMT-SPARE-PART") && (vdept != "CSP-SPARE-PART") && (vdept != "WLP1-SPARE-PART") && (vdept != "WLP2-SPARE-PART") && (vdept != "LFEM-SPARE-PART") && (vdept != "WLP1-CHEM"))
                {
                    if (vdept == "WLP1-MRO") { vdept = "WLP1"; }
                    int vMonth = ToDate.DateTime.Month;
                    int vYear = ToDate.DateTime.Year;
                    string fromDate = ToDate.DateTime.AddYears(-1).ToString("yyyyMM");

                    //query_bieu_do = "SELECT right(('0' + T1.THANG + '-' + T1.NAM),7) AS TN, sum(T1.SO_LUONG_TON * T2.PRICE) as TON FROM[MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT = '" + vdept + "' AND T1.DEPARTMENT = T2.DEPARTMENT AND T1.CODE = T2.CODE  AND CAST(T1.THANG AS int) <=" + vMonth + " AND CAST(T1.NAM AS int) <= " + vYear + " group by right(('0' + T1.THANG + '-' + T1.NAM), 7) ORDER BY TN";
                    query_bieu_do = "SELECT right(('0' + T1.THANG + '-' + T1.NAM),7) AS TN, sum(T1.SO_LUONG_TON * T2.PRICE) as TON FROM[MRO].[dbo].[LICH_SU_TON_KHO_ALL] T1,EWIPMRO T2 where T1.DEPARTMENT = '" + vdept + "' AND T1.DEPARTMENT = T2.DEPARTMENT AND T1.CODE = T2.CODE  AND CAST(T1.NAM + right('0' + T1.THANG,2) as varchar) <='" + ToDate.DateTime.ToString("yyyyMM") + "' AND CAST(T1.NAM + right('0' + T1.THANG,2) as varchar) >='" + fromDate + "' group by T1.NAM,T1.THANG ORDER BY  cast( '01-' + T1.THANG +'-' + T1.NAM as datetime) ASC";
                    bieu_do_ton_kho = base.m_DBaccess.ExecuteQuery(query_bieu_do);
                    Series my_series_ton = new Series("inventory value(usd)", ViewType.Bar);
                    chartControl1.Series.Add(my_series_ton);
                    my_series_ton.DataSource = bieu_do_ton_kho;
                    my_series_ton.ArgumentScaleType = ScaleType.Auto;
                    my_series_ton.ArgumentDataMember = "TN";
                    my_series_ton.ValueScaleType = ScaleType.Numerical;
                    my_series_ton.ValueDataMembers.AddRange(new string[] { "TON" });
                }
                else
                if ((vdept == "SMT-SPARE-PART") || (vdept == "CSP-SPARE-PART") || (vdept == "WLP1-SPARE-PART") || (vdept == "WLP2-SPARE-PART") || (vdept == "LFEM-SPARE-PART"))
                {
                    Series my_series_ton = new Series("inventory value(usd)", ViewType.Bar);
                    chartControl1.Series.Add(my_series_ton);
                    my_series_ton.DataSource = bieu_do_ton_kho;
                    my_series_ton.ArgumentScaleType = ScaleType.Auto;
                    my_series_ton.ArgumentDataMember = "TN";
                    my_series_ton.ValueScaleType = ScaleType.Numerical;
                    my_series_ton.ValueDataMembers.AddRange(new string[] { "TON" });
                }
                else
                {
                    // Truong hop Hoa Chat
                    return;

                }

                chartControl1.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((XYDiagram)chartControl1.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                diagram.AxisY.Title.Text = "$";
                diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                diagram.AxisY.Title.Visible = true;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                ct.Font = new Font("Tahoma", 14, FontStyle.Regular);
                ct.Text = gvList.GetRowCellValue(e.RowHandle, "DEPARTMENT").ToString() + " STOCK SITUATION.";
                chartControl1.Titles.Add(ct);

            }
            // **************************************************************************************

            //**************************************************************************************

        }

        private bool isSearch = false;
        private void cmbView_Click(object sender, EventArgs e)
        {
            //if ((FromDate.Text != "") && (ToDate.Text != ""))
            if ((ToDate.Text != ""))
            {
                isSearch = true;
                this.SearchPage();
                isSearch = false;
            }
            else
            {
                MessageBox.Show("Please select date for searching.");
            }
        }

        public void Get_ton_kho_SPP(ref double so_luong_ton, ref double gia_tri_ton, bool vCheck, string toDate, string vDept)
        {
            //double so_luong_ton_hien_tai_spp;
            //double so_tien_ton_hien_tai_spp;
            string connString_spp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
            SqlConnection conn_spp = new SqlConnection(connString_spp);
            SqlDataAdapter sda;
            conn_spp.Open();
            string vsql = "";

            if (toDate == DateTime.Now.ToString("MM-yyyy"))
            {
                vCheck = true;
            }

            if (vCheck) // Lấy giá trị tồn kho hiện tại.....
            {
                vsql = vsql + " SELECT SUM(SUB.AMOUNT_US),SUM(SUB.QTY) \n";
                vsql = vsql + " FROM( \n";
                vsql = vsql + " SELECT SUM(ISNULL(ESL.QUANTITY,0) * ([dbo].[CONVERT_UNIT](esl.UNIT_ID,'PACK',esl.SPARE_PART_CODE,'" + vDept + "')) * ISNULL(SUB2.PRICE_US,SUB.PRICE_US)) AS AMOUNT_US, ISNULL(ESL.QUANTITY,0) as QTY \n";
                vsql = vsql + " FROM EWIP_SPAREPART_LOCATION esl \n";
                vsql = vsql + " INNER JOIN [dbo].[EWIP_SPARE_PART] sp ON esl.SPARE_PART_CODE = sp.CODE and ESL.DEPT_CODE = SP.SP_DEPT_CODE \n";
                vsql = vsql + " LEFT JOIN ( \n";
                vsql = vsql + " SELECT EO.PO_ID AS PO_NO,eprd.SPAREPART_CODE ,'PACK' as UNIT,eprd.DEPT_CODE,eprd.PRICE_VN/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'" + vDept + "') AS PRICE_VN,eprd.PRICE_US/[dbo].[CONVERT_UNIT](eprd.UNIT,'PACK',eprd.SPAREPART_CODE,'" + vDept + "') AS PRICE_US \n";
                vsql = vsql + " FROM EWIP_ORDER eo \n";
                vsql = vsql + " INNER JOIN EWIP_ORDER_PR eop ON EO.PO_ID_TEMP = eop.PO_ID_TEMP \n";
                vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST epr ON EOP.PR_CODE = EPR.PR_CODE AND EOP.DEPT_CODE = EPR.DEPT_CODE \n";
                vsql = vsql + " INNER JOIN EWIP_PURCHASE_REQUEST_DETAIL eprd ON EPR.PR_CODE = eprd.PR_CODE AND EPR.DEPT_CODE = eprd.DEPT_CODE \n";
                vsql = vsql + " ) SUB2 ON SUB2.SPAREPART_CODE = esl.SPARE_PART_CODE AND SUB2.DEPT_CODE = esl.DEPT_CODE AND SUB2.PO_NO = esl.PO_NO \n";
                vsql = vsql + " LEFT JOIN (SELECT TOP 1 WITH TIES  \n";
                vsql = vsql + " [SPARE_PART_CODE], \n";
                vsql = vsql + " [PRICE_VN]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'" + vDept + "') AS [PRICE_VN], \n";
                vsql = vsql + " [PRICE_US]/[dbo].[CONVERT_UNIT]([UNIT_CODE],'PACK',[SPARE_PART_CODE],'" + vDept + "') AS [PRICE_US], \n";
                vsql = vsql + " 'PACK' AS UNIT \n";
                vsql = vsql + " FROM [dbo].[EWIP_SPAREPART_PRICE] \n";
                vsql = vsql + " WHERE [DEPT_CODE] = '" + vDept + "' \n";
                vsql = vsql + " ORDER BY ROW_NUMBER() OVER(PARTITION BY SPARE_PART_CODE ORDER BY [DATE] DESC)) SUB ON SUB.SPARE_PART_CODE = ESL.SPARE_PART_CODE \n";
                vsql = vsql + " LEFT JOIN EWIP_VENDER V ON V.VENDER_ID = SP.VENDER_ID \n";
                vsql = vsql + " WHERE ESL.DEPT_CODE = '" + vDept + "'  AND esl.CONDITION_CODE NOT IN('INTEGRATED','NG') \n";
                vsql = vsql + " GROUP BY ESL.SPARE_PART_CODE,SP.SP_DEPT_CODE,sp.UNIT_ID,ESL.QUANTITY) SUB";

                //sda = new SqlDataAdapter("select sum(QUANTITY) as 'SO_LUONG_GIA_TRI' from EWIP_SPAREPART_INVENTORY where DEPT_CODE='" + vDept + "' union all select sum(INVENTORY_VALUES_US) as 'SO_LUONG_GIA_TRI' from EWIP_INVENTORY_VALUES_BY_TIME where DEPT_CODE='" + vDept + "' and EWIP_INVENTORY_VALUES_BY_TIME.DATE like  '%' + FORMAT(GETDATE(),'yyyy-MM') + '%' group by DEPT_CODE", conn_spp);
                sda = new SqlDataAdapter(vsql, conn_spp);
            }
            else // Lấy giá trị tồn kho theo người dùng chon khoảng thời gian....
            {
                sda = new SqlDataAdapter("select sum(QUANTITY) from EWIP_INVENTORY_BY_TIME where  FORMAT(CONVERT(Date,DATE),'MM-yyyy') ='" + toDate + "' and DEPT_CODE='" + vDept + "' group by DEPT_CODE union all select sum(INVENTORY_VALUES_US) from EWIP_INVENTORY_VALUES_BY_TIME where FORMAT(CONVERT(Date,DATE),'MM-yyyy') ='" + toDate + "' and DEPT_CODE='" + vDept + "'  and INVENTORY_VALUES_US >= 0 group by DEPT_CODE", conn_spp);
            }
            DataTable vDt_spp = new DataTable();
            sda.Fill(vDt_spp);
            if (vDt_spp.Rows.Count > 0)
            {
                if (vCheck)
                {
                    so_luong_ton = Math.Round(Convert.ToDouble(vDt_spp.Rows[0][1].ToString()), 2);
                    gia_tri_ton = Math.Round(Convert.ToDouble(vDt_spp.Rows[0][0].ToString()), 2);
                }
                else
                {
                    so_luong_ton = Math.Round(Convert.ToDouble(vDt_spp.Rows[0][0].ToString()), 2);
                    gia_tri_ton = Math.Round(Convert.ToDouble(vDt_spp.Rows[1][0].ToString()), 2);
                }
            }
            else
            {
                so_luong_ton = 0;
                gia_tri_ton = 0;
            }
            conn_spp.Close();
            conn_spp.Dispose();
        }

        public void Get_ton_kho_HOA_CHAT(ref double so_luong_ton, ref double gia_tri_ton, string fromDate, string toDate)
        {

            string connString_hoa_chat = "Data Source = 10.70.10.97;Initial Catalog = WLP1;User Id = sa;Password = Wisol@123;Connect Timeout=3";
            SqlConnection conn_hoa_chat = new SqlConnection(connString_hoa_chat);
            SqlCommand cmd = new SqlCommand("PKG_WLP1007@GET_LIST_TEMP", conn_hoa_chat);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@A_PLANT", SqlDbType.VarChar).Value = Consts.PLANT;
            cmd.Parameters.Add("@A_LANG", SqlDbType.VarChar).Value = Consts.USER_INFO.Language;
            cmd.Parameters.Add("@A_FROM", SqlDbType.VarChar).Value = fromDate;
            cmd.Parameters.Add("@A_TO", SqlDbType.VarChar).Value = toDate;
            cmd.Parameters.Add("@N_RETURN", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@V_RETURN", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
            conn_hoa_chat.Open();
            var reader = cmd.ExecuteReader();

            DataTable dt_hoa_chat = new DataTable();
            //dt_hoa_chat.Clear();
            dt_hoa_chat.Load(reader);
            double temp1 = 0;
            double temp2 = 0;
            for (int i = 0; i < dt_hoa_chat.Rows.Count; i++)
            {
                //if (dt_hoa_chat.Rows[i]["CLOSING_STOCK"].ToString() == "") { 
                //    temp1 = temp1 + 0;
                //    temp2 = temp2 + 0;
                //}
                //else 
                //{ 
                //    temp1 = temp1 + Convert.ToDouble(dt_hoa_chat.Rows[i]["CLOSING_STOCK"].ToString());
                //    if (dt_hoa_chat.Rows[i]["PRICE"].ToString() != "") { temp2 = temp2 + Convert.ToDouble(dt_hoa_chat.Rows[i]["CLOSING_STOCK"].ToString()) * Convert.ToDouble(dt_hoa_chat.Rows[i]["PRICE"].ToString()); }
                //}
                if ((dt_hoa_chat.Rows[i]["CLOSING_STOCK"].ToString() != "") && (dt_hoa_chat.Rows[i]["PRICE"].ToString() != ""))
                {
                    if (dt_hoa_chat.Rows[i]["QUANTITATIVE"].ToString() != "EA")
                    {
                        string dinh_luong = dt_hoa_chat.Rows[i]["QUANTITATIVE"].ToString();

                        if (string.IsNullOrEmpty(dinh_luong))
                        {
                            dinh_luong = "0 ";
                        }

                        string v1 = dinh_luong.Substring(0, dinh_luong.IndexOf(' '));
                        double value = Convert.ToDouble(v1);

                        temp1 = temp1 + Convert.ToDouble(dt_hoa_chat.Rows[i]["CLOSING_STOCK"].ToString()) * value;
                        temp2 = temp2 + Convert.ToDouble(dt_hoa_chat.Rows[i]["CLOSING_STOCK"].ToString()) * value * Convert.ToDouble(dt_hoa_chat.Rows[i]["PRICE"].ToString());
                    }
                }
            }
            so_luong_ton = temp1;
            gia_tri_ton = temp2;
            cmd.Dispose();
            conn_hoa_chat.Close();
            conn_hoa_chat.Dispose();
            //so_luong_ton = Math.Round(temp1,2);
            //gia_tri_ton = Math.Round(temp2,2);
        }

        private void gvList1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // Canh bao bang mau.....

            GridView currentView = sender as GridView;
            if ((e.RowHandle >= 0))
            {
                if ((e.Column.FieldName == "CAN_USE") || (e.Column.FieldName == "TON_KHO"))
                {
                    if (e.Column.FieldName == "CAN_USE" && currentView.Columns.FirstOrDefault(col => col.GetCaption() == "RYG") != null)
                    {
                        string temp = currentView.GetRowCellValue(e.RowHandle, "RYG").NullString();
                        string useCan = currentView.GetRowCellValue(e.RowHandle, "CAN_USE").NullString();
                        if ((temp != "") && useCan != "")
                        {
                            if (temp == "R")
                            {
                                e.Appearance.BackColor = Color.OrangeRed;
                            }
                            if (temp == "G")
                            {
                                e.Appearance.BackColor = Color.Green;
                            }
                            if (temp == "Y")
                            {
                                e.Appearance.BackColor = Color.Yellow;
                            }
                        }
                    }
                    else
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

        private void gcList_Click(object sender, EventArgs e)
        {

        }

        private void gvList1_ShowFilterPopupListBox(object sender, FilterPopupListBoxEventArgs e)
        {
            //if (e.Column.FieldName == "CAN_USE_FOR")
            //    e.ComboBox.DrawItem += ComboBox_DrawItem;

        }
        void ComboBox_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            //try
            //{
            //    int argb = Convert.ToInt32((e.Item  as FilterItem).Value);
            //    Color clr = Color.FromArgb(argb);
            //    e.Graphics.FillRectangle(new SolidBrush(clr), e.Bounds);
            //    e.Handled = true;
            //}
            //catch (Exception ee)
            //{
            //    //MessageBox.Show(ee.Message.ToString());
            //}

        }

        private void gvList1_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            //ColumnView view = sender as ColumnView;

            //string country = view.GetListSourceRowCellValue(e.ListSourceRow, "Country").ToString();
            //// Check whether the current row contains "USA" in the "Country" field.
            //if (country == "USA")
            //{
            //    // Make the current row visible.
            //    e.Visible = true;
            //    // Prevent default processing, so the row will be visible 
            //    // regardless of the view's filter.
            //    e.Handled = true;
            //}

        }
    }

}
