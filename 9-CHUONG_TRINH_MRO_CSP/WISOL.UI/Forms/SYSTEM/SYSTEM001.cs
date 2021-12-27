using System;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM001 : PageType
    {
        public SYSTEM001()
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
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM001.INT_LIST"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    Init_Control();
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
            base.SearchPage();
            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SYSTEM001.GET_LIST",
                    new string[] { "A_PLANT" },
                    new string[] { Consts.PLANT }
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void Init_Control()
        {
            try
            {
                txtGlsr.EditValue = string.Empty;
                txtKor.EditValue = string.Empty;
                txtVtn.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtGlsr.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_101".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM001.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_GLSR",
                        "A_KOR",
                        "A_VTN",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { Consts.PLANT,
                        txtGlsr.EditValue.NullString(),
                        txtKor.EditValue.NullString(),
                        txtVtn.EditValue.NullString(),
                        Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    SearchPage();
                    if (chkInitData.Checked)
                    {
                        this.Init_Control();
                    }
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
                    txtGlsr.EditValue = gvList.GetDataRow(e.RowHandle)["GLSR"].NullString();
                    txtKor.EditValue = gvList.GetDataRow(e.RowHandle)["KOR"].NullString();
                    txtVtn.EditValue = gvList.GetDataRow(e.RowHandle)["VTN"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

    }
}
