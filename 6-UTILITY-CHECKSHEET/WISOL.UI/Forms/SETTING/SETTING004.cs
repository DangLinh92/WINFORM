using DevExpress.XtraEditors.Controls;
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
    public partial class SETTING004 : PageType
    {
        private string b64 = string.Empty;
        public SETTING004()
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
            //try
            //{
            //    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.INT_LIST"
            //        , new string[] { "A_PLANT", "A_LANGUAGE"
            //        }
            //        , new string[] { Consts.PLANT, Consts.USER_INFO.Language
            //        }
            //        );
            //    if (base.m_ResultDB.ReturnInt == 0)
            //    {
            //        base.m_BindData.BindGridView(gcList,
            //            base.m_ResultDB.ReturnDataSet.Tables[0]
            //            );
            //        base.m_BindData.BindGridLookEdit(gleUnit, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE_GROUP", "NAME_OF_CODE");
            //        base.m_BindData.BindGridLookEdit(gleMaker, base.m_ResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
            //        base.m_BindData.BindGridLookEdit(gleStageUse, base.m_ResultDB.ReturnDataSet.Tables[3], "CODE", "NAME_OF_CODE");
            //        Init_Control(true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
            //this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.SearchPage();
            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    gvList.OptionsView.ShowFooter = false;
                    gvList.Columns["PRICE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PRICE"].DisplayFormat.FormatString = "n0";
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
                txtCode.EditValue = string.Empty;
                txtTenVatTu.EditValue = string.Empty;
                txtPrice.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtCode.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ER_114".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtTenVatTu.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_045".Translation(), MsgType.Warning);
                    return;
                }

                if(gvList.DataRowCount > 0)
                {
                    if (txtCode.ReadOnly == false)
                    {
                        for (int i = 0; i < gvList.DataRowCount; i++)
                        {
                            if (gvList.GetRowCellValue(i, "CODE").ToString().ToUpper() == txtCode.EditValue.ToString().ToUpper())
                            {
                                MsgBox.Show("MSG_ERR_056".Translation(), MsgType.Warning);
                                return;
                            }
                        }
                    }
                }

                string afterLoc = LocDau(txtTenVatTu.Text.Trim());

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_CODE",
                        "A_TEN_VAT_TU",
                        "A_PRICE"
                    }
                    , new string[] {
                        Consts.PLANT,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        txtCode.Text.Trim(),
                        afterLoc,
                        txtPrice.EditValue.NullString()
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    SearchPage();
                    this.txtCode.ReadOnly = true;
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

            if (e.RowHandle < 0)
                return;
            else
            {
                string code = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                string ten_vat_tu = gvList.GetDataRow(e.RowHandle)["TEN_VAT_TU"].NullString();
                string price = gvList.GetDataRow(e.RowHandle)["PRICE"].NullString();
                txtCode.Text = code.Trim();
                txtTenVatTu.Text = ten_vat_tu.Trim();
                txtPrice.Text =  price;
                txtCode.ReadOnly = true;
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtCode.Text.Trim() == string.Empty)
            {
                return;
            }

            string code = txtCode.Text.Trim();
            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.DELETE_ITEM"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                        }
                        , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, code
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
            this.txtCode.ReadOnly = false;
            this.ActiveControl = txtCode;
        }

        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
    }
}
