using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1107 : PageType
    {
        public WLP1107()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            //this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //chkPrintLabel.Checked = true;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                cmbTYPE.SelectedIndex = 0;
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1101.INT_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                       base.m_ResultDB.ReturnDataSet.Tables[0]
                       );

                    //base.m_BindData.BindGridLookEdit(gleCategory, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE", "NAME");

                    //base.m_BindData.BindGridLookEdit(gleMaker, base.m_ResultDB.ReturnDataSet.Tables[0], "MAKER", "MAKER_NAME");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            dtpFromDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();
        }


        public override void SearchPage()
        {
            base.SearchPage();
            string sql_SPP = "";
            try
            {
                if (cmbTYPE.SelectedIndex == 0)
                {
                    // **** Lay chi tiet thong tin nhap kho MRO do ******************* ***********
                    base.m_BindData.BindGridView(gcList,
                        "PKG_WLP1107.GET_LIST",
                        new string[] { "A_LANG", "A_FROM_DATE", "A_TO_DATE", "A_DEPARTMENT" },
                        new string[] { Consts.USER_INFO.Language, dtpFromDate.DateTime.ToString("yyyyMMdd"), dtpToDate.DateTime.ToString("yyyyMMdd"), Consts.DEPARTMENT }
                        ); 

                    // ******************************************************************************
                    // Thong tin nhap kho Spare part ************************************************
                    sql_SPP = sql_SPP + " Select T1.DEPT_CODE as DEPARTMENT,T1.SPARE_PART_CODE AS CODE, T3.NAME_VI as NAME, T1.QUANTITY, T3.UNIT_ID AS UNIT,T1.DATE_IN AS STOCK_IN_TIME, T1.QUANTITY* T2.PRICE_US as AMOUNT ";
                    sql_SPP = sql_SPP + " From EWIP_SP_STOCKIN T1, EWIP_SPAREPART_PRICE T2, EWIP_SPARE_PART t3 ";
                    sql_SPP = sql_SPP + " Where T1.DEPT_CODE=t2.DEPT_CODE  ";
                    sql_SPP = sql_SPP + "  And T1.SPARE_PART_CODE =T2.SPARE_PART_CODE  ";
                    sql_SPP = sql_SPP + "  And T1.DEPT_CODE=T3.SP_DEPT_CODE  ";
                    sql_SPP = sql_SPP + "   And T1.SPARE_PART_CODE = T3.CODE  ";
                    sql_SPP = sql_SPP + "    And T1.DATE_IN >='" + dtpFromDate.EditValue + "' And T1. DATE_IN <='" + dtpToDate.EditValue + "'";
                    sql_SPP = sql_SPP + "    And T1.DEPT_CODE='SMT'  ";

                    // Câu lệnh Summuzise tổng tiền của spare part/
                    string vsql_SPP_Tong = " Select T1.DEPT_CODE as DEPARTMENT, round(sum(T1.QUANTITY* T2.PRICE_US),2) as AMOUNT_$ ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " From EWIP_SP_STOCKIN T1, EWIP_SPAREPART_PRICE T2 ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " Where T1.DEPT_CODE=t2.DEPT_CODE ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " And T1.SPARE_PART_CODE =T2.SPARE_PART_CODE ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " And T1.DATE_IN >='" + dtpFromDate.EditValue + "' And T1. DATE_IN <='" + dtpToDate.EditValue + "'";
                    vsql_SPP_Tong = vsql_SPP_Tong + " And T1.DEPT_CODE='SMT' ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " group by T1.DEPT_CODE ";
                    // *******************************************
                    // Ket noi database SPARE_PARE 
                    DataTable vdt_SPP = new DataTable();
                    DataTable vdt_SPP_Tong = new DataTable();
                    string connString_spp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp = new SqlConnection(connString_spp);
                    conn_spp.Open();
                    SqlDataAdapter dap1 = new SqlDataAdapter(sql_SPP, conn_spp);
                    SqlDataAdapter dap2 = new SqlDataAdapter(vsql_SPP_Tong, conn_spp);
                    // Gộp 2 datatable ******************************************************
                    dap1.Fill(vdt_SPP);
                    dap2.Fill(vdt_SPP_Tong);

                    DataTable dt_all_01 = base.m_BindData._Result.ReturnDataSet.Tables[0].Copy();
                    dt_all_01.Merge(vdt_SPP);
                    // Hien thi chi tiet MRO va SPARE PART vao gcList .......................
                    gcList.DataSource = dt_all_01;
                    // ***********************************************************************
                    conn_spp.Close();

                   
                    string vsql_MRO_Tong = "SELECT T1.DEPARTMENT, ROUND(sum(T1.QUANTITY*T2.PRICE),2) AMOUNT_$ FROM EWIPSTOCKIN_NEW T1,  EWIPMRO T2 where T1.CREATE_TIME >= '" + dtpFromDate.DateTime.ToString("yyyyMMdd") + "000000' and T1.CREATE_TIME <= '" + dtpToDate.DateTime.ToString("yyyyMMdd") + "235959'  and T1.CANCEL_STOCK_IN = 'N' AND T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT group by T1.DEPARTMENT";
                    DataTable vdt_MRO_Tong = base.m_DBaccess.ExecuteQuery(vsql_MRO_Tong);
                    DataTable dt_all_02 = vdt_MRO_Tong.Copy();
                    dt_all_02.Merge(vdt_SPP_Tong);
                    // Hiển thị tổng số tiền  cua MRO vaf SPARE PART vao bảng gvList1.
                    gcList1.DataSource = dt_all_02; 
                    // *****************************************************************************
                }
                else
                {
                    // ********** Lay chi tiet thong tin xuat kho do vao Grid control ***********
                    base.m_BindData.BindGridView(gcList,
                        "PKG_WLP1108.GET_LIST",
                        new string[] { "A_LANG", "A_FROM_DATE", "A_TO_DATE", "A_DEPARTMENT" },
                        new string[] { Consts.USER_INFO.Language, dtpFromDate.DateTime.ToString("yyyyMMdd"), dtpToDate.DateTime.ToString("yyyyMMdd"), Consts.DEPARTMENT }
                        );
                    //string vsql = "SELECT T1.DEPARTMENT, ROUND(sum(T1.QUANTITY*T2.PRICE),2) AMOUNT_$ FROM EWIPSTOCKOUT_NEW T1,  EWIPMRO T2 where T1.CREATE_TIME >= '" + dtpFromDate.DateTime.ToString("yyyyMMdd") + "000000' and T1.CREATE_TIME <= '" + dtpToDate.DateTime.ToString("yyyyMMdd") + "235959' AND T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT group by T1.DEPARTMENT";
                    //DataTable vdt = base.m_DBaccess.ExecuteQuery(vsql);

                    //// ******************************************************************************

                    //// Thong tin xuat kho Spare Part *********************************************
                    //sql_SPP = sql_SPP + " Select T1.DEPT_CODE as DEPARTMENT,T1.SPARE_PART_CODE AS CODE, T3.NAME_VI as NAME, T1.QUANTITY, T3.UNIT_ID AS UNIT,T1.DATE AS STOCK_OUT_TIME, T1.QUANTITY* T2.PRICE_US as AMOUNT ";
                    //sql_SPP = sql_SPP + " From EWIP_STOCK_OUT T1, EWIP_SPAREPART_PRICE T2, EWIP_SPARE_PART t3 ";
                    //sql_SPP = sql_SPP + " Where T1.DEPT_CODE=t2.DEPT_CODE  ";
                    //sql_SPP = sql_SPP + "  And T1.SPARE_PART_CODE =T2.SPARE_PART_CODE  ";
                    //sql_SPP = sql_SPP + "  And T1.DEPT_CODE=T3.SP_DEPT_CODE  ";
                    //sql_SPP = sql_SPP + "   And T1.SPARE_PART_CODE = T3.CODE  ";
                    //sql_SPP = sql_SPP + "    And T1.DATE >='" + dtpFromDate.EditValue + "' And T1. DATE <='" + dtpToDate.EditValue + "'";
                    //sql_SPP = sql_SPP + "    And T1.DEPT_CODE='SMT'  ";

                    //// Ket noi database SPARE_PARE 
                    //DataTable vdt_SPP = new DataTable();
                    //string connString_spp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    //SqlConnection conn_spp = new SqlConnection(connString_spp);
                    //conn_spp.Open();
                    //SqlDataAdapter dap = new SqlDataAdapter(sql_SPP, conn_spp);
                    //// Gộp 2 datatable ******************************************************
                    //dap.Fill(vdt_SPP);
                    //DataTable dt_all = base.m_BindData._Result.ReturnDataSet.Tables[0].Copy();
                    //dt_all.Merge(vdt_SPP);
                    //// ***********************************************************************
                    //conn_spp.Close();

                    //gcList.DataSource = dt_all;// Hiển thị chi tiết ra bảng gvList
                    //gcList1.DataSource = vdt; // Hiển thị tổng số và tiền ra bảng gvList1.
                    //// *****************************************************************************
                    //// ***************************************************************************



                    // ******************** MANH **********************************************************************
                    // ******************************************************************************
                    // Thong tin nhap kho Spare part ************************************************
                    sql_SPP = sql_SPP + " Select T1.DEPT_CODE as DEPARTMENT,T1.SPARE_PART_CODE AS CODE, T3.NAME_VI as NAME, T1.QUANTITY, T3.UNIT_ID AS UNIT,T1.DATE AS STOCK_OUT_TIME, T1.QUANTITY* T2.PRICE_US as AMOUNT ";
                    sql_SPP = sql_SPP + " From EWIP_STOCK_OUT T1, EWIP_SPAREPART_PRICE T2, EWIP_SPARE_PART t3 ";
                    sql_SPP = sql_SPP + " Where T1.DEPT_CODE=t2.DEPT_CODE  ";
                    sql_SPP = sql_SPP + "  And T1.SPARE_PART_CODE =T2.SPARE_PART_CODE  ";
                    sql_SPP = sql_SPP + "  And T1.DEPT_CODE=T3.SP_DEPT_CODE  ";
                    sql_SPP = sql_SPP + "   And T1.SPARE_PART_CODE = T3.CODE  ";
                    sql_SPP = sql_SPP + "    And T1.DATE >='" + dtpFromDate.EditValue + "' And T1. DATE <='" + dtpToDate.EditValue + "'";
                    sql_SPP = sql_SPP + "    And T1.DEPT_CODE='SMT'  ";

                    // Câu lệnh Summuzise tổng tiền của spare part/
                    string vsql_SPP_Tong = " Select T1.DEPT_CODE as DEPARTMENT, round(sum(T1.QUANTITY* T2.PRICE_US),2) as AMOUNT_$ ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " From EWIP_STOCK_OUT T1, EWIP_SPAREPART_PRICE T2 ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " Where T1.DEPT_CODE=t2.DEPT_CODE ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " And T1.SPARE_PART_CODE =T2.SPARE_PART_CODE ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " And T1.DATE >='" + dtpFromDate.EditValue + "' And T1. DATE <='" + dtpToDate.EditValue + "'";
                    vsql_SPP_Tong = vsql_SPP_Tong + " And T1.DEPT_CODE='SMT' ";
                    vsql_SPP_Tong = vsql_SPP_Tong + " group by T1.DEPT_CODE ";
                    // *******************************************
                    // Ket noi database SPARE_PARE 
                    DataTable vdt_SPP = new DataTable();
                    DataTable vdt_SPP_Tong = new DataTable();
                    string connString_spp = "Data Source = 10.70.10.97;Initial Catalog = SPARE_PART_UPDATE;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                    SqlConnection conn_spp = new SqlConnection(connString_spp);
                    conn_spp.Open();
                    SqlDataAdapter dap1 = new SqlDataAdapter(sql_SPP, conn_spp);
                    SqlDataAdapter dap2 = new SqlDataAdapter(vsql_SPP_Tong, conn_spp);
                    // Gộp 2 datatable ******************************************************
                    dap1.Fill(vdt_SPP);
                    dap2.Fill(vdt_SPP_Tong);

                    DataTable dt_all_01 = base.m_BindData._Result.ReturnDataSet.Tables[0].Copy();
                    dt_all_01.Merge(vdt_SPP);
                    // Hien thi chi tiet MRO va SPARE PART vao gcList ........................
                    gcList.DataSource = dt_all_01;
                    // ***********************************************************************
                    conn_spp.Close();


                    string vsql_MRO_Tong = "SELECT T1.DEPARTMENT, ROUND(sum(T1.QUANTITY*T2.PRICE),2) AMOUNT_$ FROM EWIPSTOCKOUT_NEW T1,  EWIPMRO T2 where T1.CREATE_TIME >= '" + dtpFromDate.DateTime.ToString("yyyyMMdd") + "000000' and T1.CREATE_TIME <= '" + dtpToDate.DateTime.ToString("yyyyMMdd") + "235959' AND T1.CODE = T2.CODE AND T1.DEPARTMENT = T2.DEPARTMENT group by T1.DEPARTMENT";
                    DataTable vdt_MRO_Tong = base.m_DBaccess.ExecuteQuery(vsql_MRO_Tong);
                    DataTable dt_all_02 = vdt_MRO_Tong.Copy();
                    // Hiển thị tổng số và tiền  cuar MRO vaf SPARE PART vao bảng gvList1.

                    dt_all_02.Merge(vdt_SPP_Tong);
                    gcList1.DataSource = dt_all_02;
                    // *****************************************************************************
                    //*************************************************************************************************

                }



                // Tuy chinh hien thi ********************************************
                gvList1.Columns[1].DisplayFormat.FormatType = FormatType.Numeric;
                gvList1.Columns[1].DisplayFormat.FormatString = "n2";
                gvList1.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gvList1.Columns[1].SummaryItem.DisplayFormat = "SUM={0:n0}";
                

                gvList.Columns["AMOUNT"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["AMOUNT"].DisplayFormat.FormatString = "n2";
                gvList.Columns["AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gvList.Columns["AMOUNT"].SummaryItem.DisplayFormat = "SUM={0:n0}";

                gvList.Columns[3].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns[3].DisplayFormat.FormatString = "n2";
                gvList.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gvList.Columns[3].SummaryItem.DisplayFormat = "SUM={0:n0}";

                gvList.OptionsView.ShowFooter = true;
                //gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[3].DisplayFormat.FormatString = "n3";

                // Mạnh ẩn 2 cột này đi, nếu là phòng ban WLP1
                if (Consts.DEPARTMENT == "WLP1") 
                {
                    //gvList.Columns[5].Visible = false;
                    //gvList.Columns[6].Visible = false;
                }
                //****************************************************************

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            //gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[3].DisplayFormat.FormatString = "n0";
            //for (int i = 9; i < gvList.Columns.Count - 2; i++)
            //{
            //    gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    gvList.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //}
            //gvList.OptionsView.ShowFooter = false;
        }

        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            //int indexRow = gvList.FocusedRowHandle;
            //if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle && gvList.RowCount > 0)
            //{
            //    string code = gvList.GetDataRow(indexRow)["CODE"].ToString().Trim();
            //    string lot = gvList.GetDataRow(indexRow)["LOT_NO"].ToString().Trim();
            //    string quantity = gvList.GetDataRow(indexRow)["SO_LUONG"].ToString().Trim();
            //    string make_date = gvList.GetDataRow(indexRow)["NGAY_SAN_XUAT"].ToString().Trim();
            //    string exp_date = gvList.GetDataRow(indexRow)["NGAY_HET_HAN"].ToString().Trim();
            //    gleCategory.EditValue = code;
            //    txtLot.Text = lot;
            //    txtQuanity.EditValue = quantity;
            //    txtMakeDate.EditValue = make_date;
            //    txtExpDate.EditValue = exp_date;
            //}
        }


        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName == "NGAY_HET_HAN" )
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        DateTime exp_date = DateTime.Parse(cellValue);
            //        DateTime current_date = DateTime.Now.Date;
            //        double count = (exp_date - current_date).TotalDays;
            //        if(count <= 3 && count > 0)
            //        {
            //            e.Appearance.BackColor = Color.Yellow;
            //        }
            //        if(count <= 0)
            //        {
            //            e.Appearance.BackColor = Color.Red;
            //        }
            //    }
            //}
        }
    }
}
