using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING005 : PageType
    {
        private string b64 = string.Empty;
        public SETTING005()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //this.layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            txtCodeGroup.ReadOnly = true;

            // Manh them vao ****************************
            txtOffetLeadTime.Properties.MaxValue = 3;
            txtLeadTime.Properties.MaxValue = 9;
            //*******************************************

            base.InitializePage();
            if ((Consts.DEPARTMENT == "CSP") || ((Consts.DEPARTMENT == "WLP2"))|| ((Consts.DEPARTMENT == "LFEM")))
            {
                txtOffsetLeadTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; // Show Offset Lead Time 

            }
            else
            {
                txtOffsetLeadTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; // Hide Offset Lead Time 
                this.layoutControlItem24.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem25.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem26.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem27.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem29.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem30.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem31.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            }
        }



        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.INT_LIST"
                    , new string[] { "A_PLANT", "A_LANGUAGE", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    base.m_BindData.BindGridLookEdit(gleUnit, base.m_ResultDB.ReturnDataSet.Tables[2], "CODE_GROUP", "NAME_OF_CODE");
                    base.m_BindData.BindGridLookEdit(gleUnitStockIn, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE_GROUP", "NAME_OF_CODE");
                    //base.m_BindData.BindGridLookEdit(gleMaker, base.m_ResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    //base.m_BindData.BindGridLookEdit(gleStageUse, base.m_ResultDB.ReturnDataSet.Tables[3], "CODE", "NAME_OF_CODE");
                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            //byte[] imagebytes = Convert.FromBase64String("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");
            //using (var ms = new MemoryStream(imagebytes, 0, imagebytes.Length))
            //{
            //    pictureEdit1.Image = Image.FromStream(ms, true);
            //}

            ////pictureEdit1.Image
            //pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //pictureEdit1.Size = pictureEdit1.Image.Size;
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.GET_LIST_TEMP"
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
                    gvList.OptionsView.ShowFooter = false;
                    gvList.Columns["PRICE_USD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PRICE_USD"].DisplayFormat.FormatString = "{0:n1}";
                    // MANH TAM THOI BO DI - DE DUNG CHUNG CHO CAC PHONG BAN.....
                    //gvList.Columns["QUOTA_WAFER"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //gvList.Columns["QUOTA_WAFER"].DisplayFormat.FormatString = "n0";
                    //gvList.Columns["QUOTA_CHIP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //gvList.Columns["QUOTA_CHIP"].DisplayFormat.FormatString = "n0";
                    //**********************************************************
                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtCodeGroup.EditValue = string.Empty;
                txtName.EditValue = string.Empty;
                txtQuantitative.EditValue = string.Empty;
                gleUnitStockIn.EditValue = string.Empty;
                gleUnit.EditValue = string.Empty;
                gleMaker.EditValue = string.Empty;
                pictureEdit1.Image = null;
                txtExpiryHour.EditValue = string.Empty;
                gleStageUse.EditValue = string.Empty;
                txtMinStock.EditValue = string.Empty;
                txtLeadTime.EditValue = string.Empty;
                txtConsume.EditValue = string.Empty;
                txtReplacementPeriod.EditValue = string.Empty;
                txtPriceUSD.EditValue = string.Empty;
                txtCodeGroup.ReadOnly = true;
                txtQuotaWafer.EditValue = string.Empty;
                txtQuotaChip.EditValue = string.Empty;
                txtDuPhong.EditValue = string.Empty;

                txtLeadTime.Text = "";
                txtOffetLeadTime.Text = "";
                txtMucdichsudung.Text = "";
                txtNhaCC.Text = "";
                txtMOQ.Text = "";
                txtTilesudung.Text = "";
                txtSoluongthietbi.Text = "";
                txtSongaylamviec.Text = "";
                txtSonhanluc.Text = "";
                txtSolangiaoca.Text = "";

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodeGroup.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_114".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtName.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_045".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(gleUnit.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_046".Translation(), MsgType.Warning);
                    return;
                }
                //if (string.IsNullOrEmpty(gleMaker.EditValue.NullString()))
                //{
                //    MsgBox.Show("MSG_ERR_047".Translation(), MsgType.Warning);
                //    return;
                //}
                //if (string.IsNullOrEmpty(gleStageUse.EditValue.NullString()))
                //{
                //    MsgBox.Show("MSG_ERR_053".Translation(), MsgType.Warning);
                //    return;
                //}

                if (Consts.DEPARTMENT == "WLP2")
                {
                    if (txtQuotaWafer.EditValue.NullString() == "0" && txtQuotaChip.EditValue.NullString() == "0")
                    {
                        MsgBox.Show("Hãy set định mức".Translation(), MsgType.Warning);
                        return;
                    }

                    if (txtQuotaWafer.EditValue.NullString() != "0" && txtQuotaChip.EditValue.NullString() != "0")
                    {
                        MsgBox.Show("Định mức chỉ của WAFER hoặc của CHIP".Translation(), MsgType.Warning);
                        return;
                    }
                }

                if(Consts.DEPARTMENT == "CSP" || Consts.DEPARTMENT == "LFEM" || Consts.DEPARTMENT == "WLP1" || Consts.DEPARTMENT == "WLP2")
                {
                    //if (string.IsNullOrEmpty(rdgPhanLoai.EditValue.NullString()))
                    //{
                    //    MsgBox.Show("Hãy lựa chọn Phân Loại".Translation(), MsgType.Warning);
                    //    return;
                    //}

                    if (string.IsNullOrEmpty(txtDuPhong.EditValue.NullString()))
                    {
                        MsgBox.Show("Hãy set tỷ lệ dự phòng".Translation(), MsgType.Warning);
                        return;
                    }
                }

                if (Consts.DEPARTMENT == "WLP1")// 
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.PUT_ITEM_NEW"
                        , new string[] { "A_CODE",
                        "A_NAME",
                        "A_UNIT",
                        "A_UNIT_STOCK_IN",

                        "A_PRICE_USD",
                        "A_PHANLOAI",
                        "A_TRAN_USER_ID",
                        "A_DINH_MUC_WAFER",
                        "A_DINH_MUC_CHIP",
                        "A_DEPARTMENT",
                        "A_LEAD_TIME",

                        "A_DU_PHONG",
                        "A_MUC_DICH_SU_DUNG",
                        "A_NHA_CUNG_CAP",
                        "A_MOQ",
                        "A_TI_LE_SU_DUNG",
                        "A_SO_LUONG_THIET_BI",
                        "A_SO_NGAY_LAM_VIEC",
                        "A_SO_NHAN_LUC",
                        "A_SO_LAN_GIAO_CA",

                        }
                        , new string[] {
                        txtCodeGroup.EditValue.NullString().ToUpper(),
                        txtName.EditValue.NullString().Trim(),
                        gleUnit.EditValue.NullString(),
                        gleUnitStockIn.EditValue.NullString(),
                        txtPriceUSD.EditValue.NullString(),
                        "MRO",
                        Consts.USER_INFO.Id,
                        txtQuotaWafer.EditValue.NullString(),
                        txtQuotaChip.EditValue.NullString(),
                        Consts.DEPARTMENT,
                        txtLeadTime.EditValue.NullString(),
                        txtDuPhong.EditValue.NullString(),
                        txtMucdichsudung.EditValue.NullString(),
                        txtNhaCC.EditValue.NullString(),
                        txtMOQ.EditValue.NullString(),
                        txtTilesudung.EditValue.NullString(),
                        txtSoluongthietbi.EditValue.NullString(),
                        txtSongaylamviec.EditValue.NullString(),
                        txtSonhanluc.EditValue.NullString(),
                        txtSolangiaoca.EditValue.NullString()
                        }
                        ); 
                }
                else if (Consts.DEPARTMENT == "CSP"|| Consts.DEPARTMENT == "WLP2")
                {

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.PUT_ITEM_TEMP"
                        , new string[] { "A_CODE",
                        "A_NAME",
                        "A_UNIT",
                        "A_UNIT_STOCK_IN",
                        "A_PRICE_USD",
                        "A_TRAN_USER_ID",
                        "A_DINH_MUC_WAFER",
                        "A_DINH_MUC_CHIP",
                        "A_DEPARTMENT",
                        "A_DU_PHONG",
                        "A_PHAN_LOAI",
                        "A_LEAD_TIME",
                        "A_OFFSET_LEAD_TIME"
                        }
                        , new string[] {
                        txtCodeGroup.EditValue.NullString().ToUpper(),
                        txtName.EditValue.NullString().Trim(),
                        gleUnit.EditValue.NullString(),
                        gleUnitStockIn.EditValue.NullString(),
                        txtPriceUSD.EditValue.NullString(),
                        Consts.USER_INFO.Id,
                        txtQuotaWafer.EditValue.NullString(),
                        txtQuotaChip.EditValue.NullString(),
                        Consts.DEPARTMENT,
                        txtDuPhong.Text.NullString(),
                        "MRO",
                        txtLeadTime.EditValue.NullString(),
                         txtOffetLeadTime.EditValue.NullString()
                        }
                        );
                }
                else {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.PUT_ITEM_PL"
                    , new string[] { "A_CODE",
                        "A_NAME",
                        "A_UNIT",
                        "A_UNIT_STOCK_IN",
                        "A_PRICE_USD",
                        "A_TRAN_USER_ID",
                        "A_DINH_MUC_WAFER",
                        "A_DINH_MUC_CHIP",
                        "A_DEPARTMENT",
                        "A_DU_PHONG",
                        "A_PHAN_LOAI",
                        "A_LEAD_TIME",
                        "A_OFFSET_LEAD_TIME"
                    }
                    , new string[] {
                        txtCodeGroup.EditValue.NullString().ToUpper(),
                        txtName.EditValue.NullString().Trim(),
                        gleUnit.EditValue.NullString(),
                        gleUnitStockIn.EditValue.NullString(),
                        txtPriceUSD.EditValue.NullString(),
                        Consts.USER_INFO.Id,
                        txtQuotaWafer.EditValue.NullString(),
                        txtQuotaChip.EditValue.NullString(),
                        Consts.DEPARTMENT,
                        txtDuPhong.Text.NullString(),
                        "MRO",
                        txtLeadTime.EditValue.NullString(),
                        txtOffetLeadTime.EditValue.NullString()
                        }
                        );

                }

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    SearchPage();
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

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    try
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.GET_ITEM_DETAIL"
                            , new string[] { "A_CODE", "A_DEPARTMENT"
                            }
                            , new string[] { gvList.GetDataRow(e.RowHandle)["CODE"].NullString(), Consts.DEPARTMENT
                            }
                            );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            this.Init_Control(true);
                            DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                            txtCodeGroup.EditValue = dt.Rows[0]["CODE"].NullString();
                            txtName.EditValue = dt.Rows[0]["NAME"].NullString();
                            gleUnitStockIn.EditValue = dt.Rows[0]["UNIT_STOCK_IN"].NullString();
                            gleUnit.EditValue = dt.Rows[0]["UNIT"].NullString();
                            //txtQuantitative.EditValue = dt.Rows[0]["QUANTITATIVE"].NullString();
                            //gleMaker.EditValue = dt.Rows[0]["MAKER"].NullString();
                            ////txtExpiryHour.EditValue = dt.Rows[0]["EXPIRY_HOUR"].NullString();
                            //gleStageUse.EditValue = dt.Rows[0]["STAGE_USE"].NullString();
                            //txtMinStock.EditValue = dt.Rows[0]["MIN_STOCK"].NullString();
                            //txtLeadTime.EditValue = dt.Rows[0]["LEAD_TIME_DAY"].NullString();
                            //txtConsume.EditValue = dt.Rows[0]["CONSUME"].NullString();
                            //txtReplacementPeriod.EditValue = dt.Rows[0]["REPLACEMENT_PERIOD"].NullString();
                            txtPriceUSD.EditValue = dt.Rows[0]["PRICE_USD"].NullString();
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["QUOTA_WAFER"].NullString()))
                            {
                                txtQuotaWafer.EditValue = dt.Rows[0]["QUOTA_WAFER"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["QUOTA_CHIP"].NullString()))
                            {
                                txtQuotaChip.EditValue = dt.Rows[0]["QUOTA_CHIP"].NullString();
                            }
                            txtDuPhong.Text = dt.Rows[0]["DU_PHONG"].NullString();
                            //pictureEdit1.Image = null;
                            //if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PICTURE"].NullString()))
                            //{
                            //    byte[] imagebytes = Convert.FromBase64String(dt.Rows[0]["PICTURE"].NullString());
                            //    using (var ms = new MemoryStream(imagebytes, 0, imagebytes.Length))
                            //    {
                            //        pictureEdit1.Image = Image.FromStream(ms, true);
                            //    }
                            //    pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                            //    pictureEdit1.Size = pictureEdit1.Image.Size;
                            //}
                            if (Consts.DEPARTMENT == "CSP" || Consts.DEPARTMENT == "LFEM")
                            {
                                rdgPhanLoai.EditValue = dt.Rows[0]["PHAN_LOAI"].NullString();
                            }

                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["LEAD_TIME"].NullString()))
                            {
                                txtLeadTime.EditValue = dt.Rows[0]["LEAD_TIME"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["OFFSET_LEAD_TIME"].NullString()))
                            {
                                txtOffetLeadTime.EditValue = dt.Rows[0]["OFFSET_LEAD_TIME"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["MUC_DICH_SU_DUNG"].NullString()))
                            {
                                txtMucdichsudung.EditValue = dt.Rows[0]["MUC_DICH_SU_DUNG"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["NHA_CUNG_CAP"].NullString()))
                            {
                                txtNhaCC.EditValue = dt.Rows[0]["NHA_CUNG_CAP"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["MOQ"].NullString()))
                            {
                                txtMOQ.EditValue = dt.Rows[0]["MOQ"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["TI_LE_SU_DUNG"].NullString()))
                            {
                                txtTilesudung.EditValue = dt.Rows[0]["TI_LE_SU_DUNG"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SO_LUONG_THIET_BI"].NullString()))
                            {
                                txtSoluongthietbi.EditValue = dt.Rows[0]["SO_LUONG_THIET_BI"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SO_NGAY_LAM_VIEC"].NullString()))
                            {
                                txtSongaylamviec.EditValue = dt.Rows[0]["SO_NGAY_LAM_VIEC"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SO_NHAN_LUC"].NullString()))
                            {
                                txtSonhanluc.EditValue = dt.Rows[0]["SO_NHAN_LUC"].NullString();
                            }
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SO_LAN_GIAO_CA"].NullString()))
                            {
                                txtSolangiaoca.EditValue = dt.Rows[0]["SO_LAN_GIAO_CA"].NullString();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                b64 = Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName));
                pictureEdit1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtCodeGroup.Text.Trim() == string.Empty)
            {
                return;
            }

            string code = txtCodeGroup.Text.Trim();
            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.DELETE_ITEM"
                        , new string[] { "A_CODE", "A_TRAN_USER_ID", "A_DEPARTMENT"
                        }
                        , new string[] { code, Consts.USER_INFO.Id, Consts.DEPARTMENT
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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
                SearchPage();
            }
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.Init_Control(true);
            txtCodeGroup.ReadOnly = false;
            txtCodeGroup.Focus();
        }

        private void txtOffetLeadTime_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

                if (Convert.ToDecimal(e.NewValue) > txtOffetLeadTime.Properties.MaxValue)
                {
                    
                    txtOffetLeadTime.EditValue = 0;
                    MessageBox.Show("Nhập lại! Giá trị trong khoảng từ 0 -> 3");
                    e.Cancel = true;                    
                }
   
        }

        private void txtLeadTime_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Convert.ToDecimal(e.NewValue) > txtLeadTime.Properties.MaxValue)
            {
                txtLeadTime.EditValue = 0;
                MessageBox.Show("Nhập lại! Giá trị trong khoảng từ 0 -> 9");
                e.Cancel = true;
            }
        }

        private void gcList_Click(object sender, EventArgs e)
        {

        }
    }
}
