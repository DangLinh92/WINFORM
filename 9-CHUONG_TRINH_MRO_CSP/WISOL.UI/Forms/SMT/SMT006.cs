using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT006 : PageType
    {
        DataTable table = new DataTable("SMT_ERROR");
        public SMT006()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            gvSMT006.OptionsBehavior.Editable = true;
        }



        public override void InitializePage()
        {
            table.Columns.Add("Line", typeof(String));
            table.Columns.Add("Lot", typeof(String));
            table.Columns.Add("Model", typeof(String));
            table.Columns.Add("Input", typeof(String));
            table.Columns.Add("Position", typeof(String));
            table.Columns.Add("Thua_thiec", typeof(String));
            table.Columns.Add("Thieu_thiec", typeof(String));
            table.Columns.Add("Di_vat", typeof(String));
            table.Columns.Add("Vai", typeof(String));
            table.Columns.Add("Short", typeof(String));
            table.Columns.Add("Mat", typeof(String));
            table.Columns.Add("Kenh", typeof(String));
            table.Columns.Add("Lech", typeof(String));
            table.Columns.Add("Nguoc", typeof(String));
            table.Columns.Add("Dung", typeof(String));
            table.Columns.Add("Lat", typeof(String));
            table.Columns.Add("PCB_Loss", typeof(String));
            table.Columns.Add("Vo", typeof(String));
            table.Columns.Add("DST", typeof(String));
            table.Columns.Add("Loi_khac", typeof(String));
            table.Columns.Add("Sample", typeof(String));
            table.Columns.Add("Comment", typeof(String));
            //base.m_BindData.BindGridView(gcSMT006, table);

            //gvSMT006.Columns["Lot"].Width = 200;

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT006.INT_LIST"
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

            int hour = DateTime.Now.Hour;
            if (hour >= 8)
            {
                dtpFromDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                dtpFromDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dtpToDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            ComboBoxItemCollection coll = cbLine.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("C");
                coll.Add("D");
                coll.Add("E");
                coll.Add("F");
                coll.Add("G");
                coll.Add("H");
                coll.Add("I");
            }
            finally
            {
                coll.EndUpdate();
            }

            cbLine.SelectedIndex = 10;
            cbLine.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //cbLine.ReadOnly = true;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            //if(dtpFromDate.DateTime.ToString("yyyyMMdd") == dtpToDate.DateTime.ToString("yyyyMMdd"))
            //{
            //    if(dtpFromDate.DateTime.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            //    {
            //        MsgBox.Show("From Date và To Date không được trùng với ngày hiện tại", MsgType.Warning);
            //        return;
            //    }
            //}

            base.SearchPage();

            DataTable dt = new DataTable();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SMT006.GET_LIST",
                    new string[] { "A_PLANT", "A_FROM", "A_TO" },
                    new string[] { Consts.PLANT, dtpFromDate.DateTime.ToString("yyyyMMdd"), dtpToDate.DateTime.ToString("yyyyMMdd") }
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "n0";
            for (int i = 9; i < gvList.Columns.Count - 2; i++)
            {
                gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            }
            gvList.OptionsView.ShowFooter = false;
            gvList.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[4].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[5].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[6].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[7].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[8].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[9].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNguoiThaoTac.Text.Trim()))
            {
                MsgBox.Show("Người thao tác không được để trống.", MsgType.Warning);
                return;
            }

            string xml = Converter.GetDataTableToXml(gcSMT006.DataSource as DataTable);

            try
            {
                string strXml = string.Empty;

                strXml = Converter.GetDataTableToXml((gcSMT006.DataSource as DataTable));

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT006.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_XML",
                        "A_TRAN_USER_ID",
                        "A_COMMENT"
                    }
                    , new string[] { Consts.PLANT,
                        strXml,
                        txtNguoiThaoTac.Text.Trim(),
                        txtComment.Text.Trim()
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtNguoiThaoTac.Text = string.Empty;
                    SearchPage();
                    txtLot.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtInput.Text = string.Empty;
                    gcSMT006.DataSource = null;
                    gvSMT006.Columns.Clear();
                    btnAddNew.Enabled = true;
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


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cbLine.Text))
            //{
            //    MsgBox.Show("Line không được để trống.", MsgType.Warning);
            //    return;
            //}
            if (string.IsNullOrWhiteSpace(txtLot.Text))
            {
                MsgBox.Show("Lot không được để trống.", MsgType.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MsgBox.Show("Model không được để trống.", MsgType.Warning);
                return;
            }
            if (txtModel.Text.Substring(0, 3).ToUpper () == "HNM")
            {
                MsgBox.Show("Model không đúng.", MsgType.Warning);
                return;
            }
            if(txtModel.Text.Length > 11)
            {
                MsgBox.Show("Model tối đa 11 ký tự.", MsgType.Warning);
                return;
            }
            int result;
            if(int.TryParse(txtInput.Text.Trim(), out result))
            {
                if(result <= 0)
                {
                    MsgBox.Show("Input đang để trống hoặc không hợp lệ.", MsgType.Warning);
                    return;
                }
            }
            else
            {
                MsgBox.Show("Input đang để trống hoặc không hợp lệ.", MsgType.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNguoiThaoTac.Text))
            {
                MsgBox.Show("Người thao tác không được để trống.", MsgType.Warning);
                return;
            }
            base.m_BindData.BindGridView(gcSMT006, table);
            gvSMT006.Columns["Lot"].Width = 190;
            gvSMT006.Columns["Comment"].Width = 220;
            for (int i = 0; i < 7; i++)
            {
                gvSMT006.AddNewRow();
                gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Line"], " ");
                gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Lot"], txtLot.EditValue.ToString());
                gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Model"], txtModel.EditValue.ToString());
                gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Input"], txtInput.Text.Trim());
                if(i == 0)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "Chip");
                }
                if (i == 1)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "DST");
                }
                if (i == 2)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "LNA");
                }
                if (i == 3)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "Sample");
                }
                if (i == 4)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "SAW");
                }
                if (i == 5)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "SPI");
                }
                if (i == 6)
                {
                    gvSMT006.SetRowCellValue(GridControl.NewItemRowHandle, gvSMT006.Columns["Position"], "SW");
                }
            }
            gvSMT006.FocusedRowHandle = 0;
            gvSMT006.FocusedColumn = gvSMT006.VisibleColumns[0];
            gvSMT006.ShowEditor();
            btnAddNew.Enabled = false;
            gvSMT006.OptionsView.ShowFooter = false;
            gvSMT006.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvSMT006.Columns[3].DisplayFormat.FormatString = "n0";
            for (int i = 5; i < gvSMT006.Columns.Count - 1; i++)
            {
                gvSMT006.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvSMT006.Columns[i].DisplayFormat.FormatString = "n0";
            }
        }

        private void gcSMT006_DoubleClick(object sender, EventArgs e)
        {
            int indexRow = gvSMT006.FocusedRowHandle;
            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string Line = gvSMT006.GetDataRow(indexRow)["Line"].ToString();
                string Lot = gvSMT006.GetDataRow(indexRow)["Lot"].ToString();
                string Position = gvSMT006.GetDataRow(indexRow)["Position"].ToString();
                string thuathiec = gvSMT006.GetDataRow(indexRow)[5].ToString();
                string thieuthiec = gvSMT006.GetDataRow(indexRow)[6].ToString();
                string divat = gvSMT006.GetDataRow(indexRow)[7].ToString();
                string vai = gvSMT006.GetDataRow(indexRow)[8].ToString();
                string _short = gvSMT006.GetDataRow(indexRow)[9].ToString();
                string mat = gvSMT006.GetDataRow(indexRow)[10].ToString();
                string kenh = gvSMT006.GetDataRow(indexRow)[11].ToString();
                string lech = gvSMT006.GetDataRow(indexRow)[12].ToString();
                string nguoc = gvSMT006.GetDataRow(indexRow)[13].ToString();
                string dung = gvSMT006.GetDataRow(indexRow)[14].ToString();
                string lat = gvSMT006.GetDataRow(indexRow)[15].ToString();
                string pcb_loss = gvSMT006.GetDataRow(indexRow)[16].ToString();
                string vo = gvSMT006.GetDataRow(indexRow)[17].ToString();
                string dst = gvSMT006.GetDataRow(indexRow)[18].ToString();
                string loi_khac = gvSMT006.GetDataRow(indexRow)[19].ToString();
                string sample = gvSMT006.GetDataRow(indexRow)[20].ToString();
                string comment = gvSMT006.GetDataRow(indexRow)[21].ToString();
                POP.POP_SMT006 popup = new POP.POP_SMT006(Line, Lot, Position, thuathiec, thieuthiec, divat, vai,
                                                          _short, mat, kenh, lech, nguoc, dung, lat,
                                                          pcb_loss, vo, dst, loi_khac, sample, comment);
                popup.ShowDialog();
                //if (popup.ShowDialog() == DialogResult.OK)
                //{
                    if (Convert.ToInt32(popup.thuathiec) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[5], popup.thuathiec);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[5], "");
                    }
                    if (Convert.ToInt32(popup.thieuthiec) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[6], popup.thieuthiec);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[6], "");
                    }
                    if (Convert.ToInt32(popup.divat) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[7], popup.divat);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[7], "");
                    }
                    if (Convert.ToInt32(popup.vai) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[8], popup.vai);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[8], "");
                    }
                    if (Convert.ToInt32(popup._short) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[9], popup._short);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[9], "");
                    }
                    if (Convert.ToInt32(popup.mat) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[10], popup.mat);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[10], "");
                    }
                    if (Convert.ToInt32(popup.kenh) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[11], popup.kenh);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[11], "");
                    }
                    if (Convert.ToInt32(popup.lech) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[12], popup.lech);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[12], "");
                    }
                    if (Convert.ToInt32(popup.nguoc) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[13], popup.nguoc);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[13], "");
                    }
                    if (Convert.ToInt32(popup.dung) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[14], popup.dung);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[14], "");
                    }
                    if (Convert.ToInt32(popup.lat) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[15], popup.lat);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[15], "");
                    }
                    if (Convert.ToInt32(popup.pcb_loss) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[16], popup.pcb_loss);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[16], "");
                    }
                    if (Convert.ToInt32(popup.vo) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[17], popup.vo);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[17], "");
                    }
                    if (Convert.ToInt32(popup.dst) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[18], popup.dst);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[18], "");
                    }
                    if (Convert.ToInt32(popup.loi_khac) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[19], popup.loi_khac);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[19], "");
                    }
                    if (Convert.ToInt32(popup.sample) > 0)
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[20], popup.sample);
                    }
                    else
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[20], "");
                    }
                    if (!string.IsNullOrWhiteSpace(popup.comment))
                    {
                        gvSMT006.SetRowCellValue(indexRow, gvSMT006.Columns[21], popup.comment);
                    }
                //}
                //gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[5].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[6].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[8].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[9].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[9].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[10].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[11].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[12].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[13].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[14].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[15].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[16].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[17].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[18].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                //gvList.Columns[19].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //gvList.Columns[19].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                for (int i = 5; i < gvSMT006.Columns.Count - 1; i++)
                {
                    gvSMT006.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvSMT006.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                }
                
            }
        }

        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;
            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string Lot = gvList.GetDataRow(indexRow)["LOT"].ToString().Trim();
                gcSMT006.DataSource = null;
                gvSMT006.Columns.Clear();
                btnAddNew.Enabled = false;
                try
                {
                    base.m_BindData.BindGridView(gcSMT006,
                        "PKG_SMT006.GET_DETAIL",
                        new string[] { "A_PLANT", "A_LOT" },
                        new string[] { Consts.PLANT, Lot }
                        );
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
                btnDelete.Enabled = true;
                gvSMT006.Columns["Lot"].Width = 190;
                gvSMT006.Columns["Comment"].Width = 220;
                gvSMT006.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvSMT006.Columns[3].DisplayFormat.FormatString = "n0";
                gvSMT006.OptionsView.ShowFooter = false;
                for (int i = 5; i < gvSMT006.Columns.Count - 1; i++)
                {
                    gvSMT006.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvSMT006.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
                }
            }
        }

        //private void gvList_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.Column.AbsoluteIndex > 6)
        //    {
        //        if (e.Value != DBNull.Value)
        //        {
        //            if (Convert.ToDouble(e.Value.ToString()) == 0D)
        //            {
        //                e.DisplayText = string.Empty;
        //            }
        //        }
        //    }
        //}

        //private void gvSMT006_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.Column.AbsoluteIndex > 4)
        //    {
        //        if (e.Value != DBNull.Value)
        //        {
        //            if (Convert.ToDouble(e.Value) == 0D)
        //            {
        //                e.DisplayText = string.Empty;
        //            }
        //        }
        //    }
        //}

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string x = string.Empty;
            POP.POP_SMT006_2 popup = new POP.POP_SMT006_2(x);
            popup.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(gvSMT006.DataSource is null)
            {
                
            }
            else
            {
                string Lot = gvSMT006.GetDataRow(0)["LOT"].ToString().Trim();
                DialogResult dialogResult = MsgBox.Show("Bạn chắc chắn muốn xóa?", MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    try
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT006.DELETE_ITEM"
                            , new string[] { "A_PLANT", "A_LOT"
                            }
                            , new string[] { Consts.PLANT, Lot
                            }
                            );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            MsgBox.Show("Xóa thành công.", MsgType.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, MsgType.Error);
                    }
                    SearchPage();
                    gcSMT006.DataSource = null;
                    gvSMT006.Columns.Clear();
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            gcSMT006.DataSource = null;
            gvSMT006.Columns.Clear();
            btnAddNew.Enabled = true;
            btnDelete.Enabled = false;
        }

        private void txtLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtLot.Text.Trim() == string.Empty)
                {
                    return;
                }
                else
                {
                    txtModel.Focus();
                }
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.AbsoluteIndex > 8 && e.Column.FieldName != "COMMENT" && e.Column.FieldName != "CREATE_USER")
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (Convert.ToDouble(cellValue) > 0)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                }
            }
        }
    }
}
