using System;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING002 : PageType
    {
        public SETTING002()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING002.INT_LIST"
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING002.GET_LIST"
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
                txtMakerName.EditValue = string.Empty;
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
                //if (string.IsNullOrEmpty(txtCodeGroup.EditValue.NullString()) == true)
                //{
                //    MsgBox.Show("MSG_ERR_022".Translation(), MsgType.Warning);
                //    return;
                //}

                //if (string.IsNullOrEmpty(txtMakerName.EditValue.NullString()) == true)
                //{
                //    MsgBox.Show("MSG_ERR_023".Translation(), MsgType.Warning);
                //    return;
                //}
                if (string.IsNullOrEmpty(txtCodeGroup.EditValue.NullString()) || string.IsNullOrEmpty(txtMakerName.EditValue.NullString())
                   )
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING002.PUT_ITEM"
                    , new string[] { "A_CODE",
                        "A_MAKER_NAME",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] {
                        txtCodeGroup.EditValue.NullString().ToUpper(),
                        txtMakerName.EditValue.NullString(),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
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
                    txtCodeGroup.EditValue = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                    txtMakerName.EditValue = gvList.GetDataRow(e.RowHandle)["MAKER_NAME"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
