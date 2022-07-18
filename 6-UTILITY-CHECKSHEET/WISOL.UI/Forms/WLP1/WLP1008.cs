using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.MES.Forms.WLP1.POP;
using Wisol.Common;
using Wisol.Components;
//using Wisol.MES.Forms.WLP1.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.WLP1008
{
    public partial class WLP1008 : PageType
    {
        DataTable table = new DataTable("WLP1008_ERROR");
        private int[] x = null;
        public WLP1008()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            //this.emptySpaceItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //this.layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.InitializePage();
            this.layoutControlGroup4.Width = 400;
        }



        public override void InitializePage()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Check", typeof(string));
            dt.Columns.Add("MONTH", typeof(string));
            //dt.Columns.Add("LUONG_CHUA_NHAP", typeof(string));
            dt.Columns.Add("SO_LUONG_WAFER", typeof(int));

            //dt.Rows.Add("N", "2020-06", "", "");
            //dt.Rows.Add("N", "2020-07", "", "");
            //dt.Rows.Add("N", "2020-08", "", "");
            //dt.Rows.Add("N", "2020-09", "", "");
            //dt.Rows.Add("N", "2020-10", "", "");
            //dt.Rows.Add("N", "2020-11", "", "");
            for (int i = 0; i < 6; i++)
            {
                dt.Rows.Add("N", DateTime.Now.AddMonths(i + 1).ToString("yyyy-MM"),  null);
            }


            base.m_BindData.BindGridView(gcList1, dt);

            var checkEdit = new RepositoryItemCheckEdit
            {
                Tag = gvList1,
                ValueChecked = "Y",
                ValueUnchecked = "N",
                ValueGrayed = "N"
            };
            gcList1.RepositoryItems.Add(checkEdit);
            checkEdit.CheckedChanged += CheckEdit_CheckedChanged;


            GridColumn gridColumn = gvList1.Columns["Check"];
            gridColumn.ColumnEdit = checkEdit;
            gridColumn.OptionsColumn.AllowEdit = true;
            gridColumn.OptionsColumn.AllowSort = DefaultBoolean.False;
            gridColumn.Width = 70;
            gvList1.OptionsBehavior.Editable = true;
            gvList1.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1008.INT_LIST_M"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList2,
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

        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            x = gvList1.GetSelectedRows();
            if (x.Length < 1) return;
            string month = gvList1.GetDataRow(x[0])["MONTH"].ToString();
            //string luongchuanhap = gvList1.GetDataRow(x[0])["LUONG_CHUA_NHAP"].ToString();
            string soluongwafer = gvList1.GetDataRow(x[0])["SO_LUONG_WAFER"].ToString();
            if (edit.Checked)
            {
                WLP1.POP.POP_WLP1008 popup = new WLP1.POP.POP_WLP1008(month, soluongwafer);

                popup.ShowDialog();

                gvList1.SetRowCellValue(x[0], gvList1.Columns[0], "Y");
               // gvList1.SetRowCellValue(x[0], gvList1.Columns[2], popup.luongchuanhap);
                gvList1.SetRowCellValue(x[0], gvList1.Columns[2], popup.soluongwafer);
            }

            gvList1.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            //gvList1.Columns[3].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gvList1.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList1.Columns[2].DisplayFormat.FormatString = "n0";
            //gvList1.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList1.Columns[3].DisplayFormat.FormatString = "n0";
        }

        public override void SearchPage()
        {
            int x = 0;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1008.GET_LIST_2",
                    new string[] { "A_PLANT", "A_LANG" },
                    new string[] { Consts.PLANT, Consts.USER_INFO.Language }
                 );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    x = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString());
                    dt1 = base.m_ResultDB.ReturnDataSet.Tables[1];
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[3]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            for(int i = 0; i < dt1.Rows.Count; i++)
            {
                for(int j = 0; j < gvList1.RowCount; j++)
                {
                    if(dt1.Rows[i]["CODE"].ToString() == gvList1.GetRowCellValue(j, "MONTH").ToString())
                    {
                        gvList1.SetRowCellValue(j, "Check", "Y");
                        gvList1.SetRowCellValue(j, "SO_LUONG_WAFER", dt1.Rows[i]["VAL"].ToString());
                    }
                }
            }


            gvList1.Columns["SO_LUONG_WAFER"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList1.Columns["SO_LUONG_WAFER"].DisplayFormat.FormatString = "n0";

            gvList2.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList2.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatString = "n0";

            gvList.Columns["MIN_STOCK"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["MIN_STOCK"].DisplayFormat.FormatString = "n0";
            gvList.Columns["QUANTITY"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["QUANTITY"].DisplayFormat.FormatString = "n0";
            //gvList.Columns["LEAD_TIME_DAY"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvList.Columns["LEAD_TIME_DAY"].DisplayFormat.FormatString = "n0";
            gvList.Columns["CONSUME"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["CONSUME"].DisplayFormat.FormatString = "n0";
            gvList.Columns["TONG_SO_WAFER"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["TONG_SO_WAFER"].DisplayFormat.FormatString = "n0";
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatString = "n0";
            gvList.Columns["LUONG_CHUA_NHAP"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

            gvList.Columns["SO_HOA_CHAT_DUA_TREN_WAFER"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gvList.Columns["QUANTITY_REQUIRED"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gvList.OptionsView.ShowFooter = false;

            for (int i = 0; i < gvList.RowCount; i++)
            {
                int so_luong_wafer = 0;
                double luong_tieu_hao = 0;
                double luong_chua_nhap = 0;
                double so_luong_hien_tai = 0;
                double so_luong_toi_thieu = 0;
                double hoa_chat_wafer = 0;
                if (!string.IsNullOrWhiteSpace(gvList.GetRowCellValue(i, "CONSUME").ToString()))
                {
                    so_luong_wafer = Convert.ToInt32(gvList.GetRowCellValue(i, "TONG_SO_WAFER").ToString());
                    luong_tieu_hao = Convert.ToDouble(gvList.GetRowCellValue(i, "CONSUME").ToString());
                    luong_chua_nhap = Convert.ToDouble(string.IsNullOrWhiteSpace(gvList.GetRowCellValue(i, "LUONG_CHUA_NHAP").ToString())? "0" : gvList.GetRowCellValue(i, "LUONG_CHUA_NHAP").ToString());
                    so_luong_hien_tai = Convert.ToDouble(gvList.GetRowCellValue(i, "QUANTITY").ToString());
                    so_luong_toi_thieu = Convert.ToDouble(gvList.GetRowCellValue(i, "MIN_STOCK").ToString());
                    hoa_chat_wafer = Math.Ceiling(so_luong_wafer * 1.0 / luong_tieu_hao);
                    gvList.SetRowCellValue(i, "SO_HOA_CHAT_DUA_TREN_WAFER", hoa_chat_wafer);
                    gvList.SetRowCellValue(i, "QUANTITY_REQUIRED", hoa_chat_wafer - so_luong_hien_tai - luong_chua_nhap + so_luong_toi_thieu);
                }
                else
                {
                    so_luong_wafer = Convert.ToInt32(gvList.GetRowCellValue(i, "TONG_SO_WAFER").ToString());
                    luong_tieu_hao = Convert.ToDouble(gvList.GetRowCellValue(i, "REPLACEMENT_PERIOD").ToString());
                    luong_chua_nhap = Convert.ToDouble(string.IsNullOrWhiteSpace(gvList.GetRowCellValue(i, "LUONG_CHUA_NHAP").ToString()) ? "0" : gvList.GetRowCellValue(i, "LUONG_CHUA_NHAP").ToString());
                    so_luong_hien_tai = Convert.ToDouble(gvList.GetRowCellValue(i, "QUANTITY").ToString());
                    so_luong_toi_thieu = Convert.ToDouble(gvList.GetRowCellValue(i, "MIN_STOCK").ToString());
                    hoa_chat_wafer = Math.Ceiling(x * luong_tieu_hao);
                    gvList.SetRowCellValue(i, "SO_HOA_CHAT_DUA_TREN_WAFER", hoa_chat_wafer);
                    gvList.SetRowCellValue(i, "QUANTITY_REQUIRED", hoa_chat_wafer - so_luong_hien_tai - luong_chua_nhap + so_luong_toi_thieu);
                }
            }
            gvList.Columns["QUANTITY_REQUIRED"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
        }


        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.AbsoluteIndex > 8 && e.Column.FieldName != "COMMENT" && e.Column.FieldName != "CREATE_USER")
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        if (Convert.ToDouble(cellValue) > 0)
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
            //        }
            //    }
            //}
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            int count = 0;
            int numberOfMonth = 0;
            string XML = Converter.GetDataTableToXml(gcList2.DataSource as DataTable);

            for (int i = 0; i < gvList1.RowCount; i++)
            {
                if(gvList1.GetRowCellValue(i, "Check").ToString() == "Y")
                {
                    count += Convert.ToInt32(gvList1.GetRowCellValue(i, "SO_LUONG_WAFER").ToString());
                    numberOfMonth += 1;
                }
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1008.GET_LIST_XacNhan",
                    new string[] { "A_PLANT", "A_LANG", "A_SO_LUONG_WAFER", "A_XML" },
                    new string[] { Consts.PLANT, Consts.USER_INFO.Language, count.ToString(), XML }
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

            gvList.Columns["MIN_STOCK"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["MIN_STOCK"].DisplayFormat.FormatString = "n0";
            gvList.Columns["QUANTITY"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["QUANTITY"].DisplayFormat.FormatString = "n0";
            //gvList.Columns["LEAD_TIME_DAY"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvList.Columns["LEAD_TIME_DAY"].DisplayFormat.FormatString = "n0";
            gvList.Columns["CONSUME"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["CONSUME"].DisplayFormat.FormatString = "n0";
            gvList.Columns["TONG_SO_WAFER"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["TONG_SO_WAFER"].DisplayFormat.FormatString = "n0";
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatString = "n0";
            gvList.Columns["LUONG_CHUA_NHAP"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

            gvList.Columns["SO_HOA_CHAT_DUA_TREN_WAFER"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
            gvList.OptionsView.ShowFooter = false;

            for(int i = 0; i < gvList.RowCount; i++)
            {
                int so_luong_wafer = 0;
                double luong_tieu_hao = 0;
                double luong_chua_nhap = 0;
                double so_luong_hien_tai = 0;
                double so_luong_toi_thieu = 0;
                double hoa_chat_wafer = 0;
                if (!string.IsNullOrWhiteSpace(gvList.GetRowCellValue(i, "CONSUME").ToString()))
                {
                    so_luong_wafer = Convert.ToInt32(gvList.GetRowCellValue(i, "TONG_SO_WAFER").ToString());
                    luong_tieu_hao = Convert.ToDouble(gvList.GetRowCellValue(i, "CONSUME").ToString());
                    luong_chua_nhap = Convert.ToDouble(gvList.GetRowCellValue(i, "LUONG_CHUA_NHAP").ToString());
                    so_luong_hien_tai = Convert.ToDouble(gvList.GetRowCellValue(i, "QUANTITY").ToString());
                    so_luong_toi_thieu = Convert.ToDouble(gvList.GetRowCellValue(i, "MIN_STOCK").ToString());
                    hoa_chat_wafer = Math.Ceiling(so_luong_wafer * 1.0 / luong_tieu_hao);
                    gvList.SetRowCellValue(i, "SO_HOA_CHAT_DUA_TREN_WAFER", hoa_chat_wafer);
                    gvList.SetRowCellValue(i, "QUANTITY_REQUIRED", hoa_chat_wafer - so_luong_hien_tai - luong_chua_nhap + so_luong_toi_thieu);
                }
                else
                {
                    so_luong_wafer = Convert.ToInt32(gvList.GetRowCellValue(i, "TONG_SO_WAFER").ToString());
                    luong_tieu_hao = Convert.ToDouble(gvList.GetRowCellValue(i, "REPLACEMENT_PERIOD").ToString());
                    luong_chua_nhap = Convert.ToDouble(gvList.GetRowCellValue(i, "LUONG_CHUA_NHAP").ToString());
                    so_luong_hien_tai = Convert.ToDouble(gvList.GetRowCellValue(i, "QUANTITY").ToString());
                    so_luong_toi_thieu = Convert.ToDouble(gvList.GetRowCellValue(i, "MIN_STOCK").ToString());
                    hoa_chat_wafer = Math.Ceiling(numberOfMonth*luong_tieu_hao);
                    gvList.SetRowCellValue(i, "SO_HOA_CHAT_DUA_TREN_WAFER", hoa_chat_wafer);
                    gvList.SetRowCellValue(i, "QUANTITY_REQUIRED", hoa_chat_wafer - so_luong_hien_tai - luong_chua_nhap + so_luong_toi_thieu);
                }
            }
            gvList.Columns["QUANTITY_REQUIRED"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string XML1 = Converter.GetDataTableToXml(gcList1.DataSource as DataTable);
            string XML2 = Converter.GetDataTableToXml(gcList2.DataSource as DataTable);

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1008.PUT_ITEM"
                    , new string[] { "A_XML1", "A_XML2", "A_TRAN_USER"
                    }
                    , new string[] { XML1, XML2, Consts.USER_INFO.Id
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);

                    this.SearchPage();
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
    }
}
