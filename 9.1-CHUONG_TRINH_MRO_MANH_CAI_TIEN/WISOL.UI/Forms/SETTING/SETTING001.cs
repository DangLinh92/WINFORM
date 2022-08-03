using System;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING001 : PageType
    {
        public SETTING001()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.INT_LIST"
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

                    Init_Control(true);
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.GET_LIST"
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
                txtNameOfCode.EditValue = string.Empty;
                txtNameOfCodeVN.EditValue = string.Empty;
                txtNameOfCodeKR.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtCodeGroup.EditValue.NullString()) || string.IsNullOrEmpty(txtNameOfCode.EditValue.NullString())
                    || string.IsNullOrEmpty(txtNameOfCodeVN.EditValue.NullString()) || string.IsNullOrEmpty(txtNameOfCodeKR.EditValue.NullString())
                    )
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }



                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.PUT_ITEM"
                    , new string[] { "A_CODE_GROUP",
                        "A_CODE_NAME",
                        "A_CODE_NAME_VN",
                        "A_CODE_NAME_KR",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { txtCodeGroup.Text.Trim(),
                        txtNameOfCode.Text.Trim(),
                        txtNameOfCodeVN.Text.Trim(),
                        txtNameOfCodeKR.Text.Trim(),
                        Consts.USER_INFO.Id
                    }
                    );

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
                    txtCodeGroup.EditValue = gvList.GetDataRow(e.RowHandle)["CODE_GROUP"].NullString();
                    txtNameOfCode.EditValue = gvList.GetDataRow(e.RowHandle)["NAME_OF_CODE"].NullString();
                    txtNameOfCodeVN.EditValue = gvList.GetDataRow(e.RowHandle)["NAME_OF_CODE_VN"].NullString();
                    txtNameOfCodeKR.EditValue = gvList.GetDataRow(e.RowHandle)["NAME_OF_CODE_KR"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
