using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM007 : PageType
    {
        public SYSTEM007()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            Classes.Common.SetFormIdToButton(this, "SYSTEM007");
        }

        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM007.INT_LIST"
                    , new string[] { "A_PLANT",
                        "A_USER_ID"
                    }
                    , new string[] { Consts.PLANT,
                        Consts.USER_INFO.Id
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0],
                        false,
                        "REMARKS"
                        );

                    base.m_BindData.BindGridLookEdit(gleGroup,
                        base.m_ResultDB.ReturnDataSet.Tables[1],
                        "COMMGRP",
                        "COMMGRPNAME"
                        );
                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            this.layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SYSTEM007.GET_LIST",
                    new string[]{"A_PLANT",
                        "A_COMMGRP"
                    },
                    new string[]{Consts.PLANT,
                        gleGroup.EditValue.NullString()
                    },
                    false,
                    "REMARKS"
                    );
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
                if (condFlag)
                {
                    gleGroup.EditValue = string.Empty;
                }
                txtCommCode.EditValue = string.Empty;
                txtCommName.EditValue = string.Empty;
                txtCommNameKR.EditValue = string.Empty;
                txtCommNameVT.EditValue = string.Empty;
                txtValue1.EditValue = string.Empty;
                txtValue2.EditValue = string.Empty;
                txtValue3.EditValue = string.Empty;
                txtValue4.EditValue = string.Empty;
                txtValue5.EditValue = string.Empty;
                txtValue6.EditValue = string.Empty;
                txtValue7.EditValue = string.Empty;
                txtValue8.EditValue = string.Empty;
                txtValue9.EditValue = string.Empty;
                txtValue10.EditValue = string.Empty;
                txtValue11.EditValue = string.Empty;
                rdgUseFlag.EditValue = "Y";
                txtRemarks.EditValue = string.Empty;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void btnGroup_Click(object sender, EventArgs e)
        {
            try
            {
                POP.POP_SYSTEM007 popup = new POP.POP_SYSTEM007();
                popup.ShowDialog();
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
                if (string.IsNullOrEmpty(txtCommCode.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_110".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(gleGroup.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_111".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM007.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_COMMGRP",
                        "A_COMMCODE",
                        "A_COMMNAME",
                        "A_COMMNAME_KR",
                        "A_COMMNAME_VT",
                        "A_VALUE1",
                        "A_VALUE2",
                        "A_VALUE3",
                        "A_VALUE4",
                        "A_VALUE5",
                        "A_VALUE6",
                        "A_VALUE7",
                        "A_VALUE8",
                        "A_VALUE9",
                        "A_VALUE10",
                        "A_VALUE11",
                        "A_USEFLAG",
                        "A_REMARKS",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { Consts.PLANT,
                        gleGroup.EditValue.NullString().ToUpper(),
                        txtCommCode.EditValue.NullString().ToUpper(),
                        txtCommName.EditValue.NullString().ToUpper(),
                        txtCommNameKR.EditValue.NullString().ToUpper(),
                        txtCommNameVT.EditValue.NullString().ToUpper(),
                        txtValue1.EditValue.NullString().ToUpper(),
                        txtValue2.EditValue.NullString().ToUpper(),
                        txtValue3.EditValue.NullString().ToUpper(),
                        txtValue4.EditValue.NullString().ToUpper(),
                        txtValue5.EditValue.NullString().ToUpper(),
                        txtValue6.EditValue.NullString().ToUpper(),
                        txtValue7.EditValue.NullString().ToUpper(),
                        txtValue8.EditValue.NullString().ToUpper(),
                        txtValue9.EditValue.NullString().ToUpper(),
                        txtValue10.EditValue.NullString().ToUpper(),
                        txtValue11.EditValue.NullString().ToUpper(),
                        rdgUseFlag.EditValue.NullString(),
                        txtRemarks.EditValue.NullString(),
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

        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                Init_Control(false);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(gleGroup.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_141".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtCommCode.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_142".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM007.DEL_ITEM",
                    new string[]{"A_PLANT",
                        "A_COMMGRP",
                        "A_COMMCODE"
                    },
                    new string[]{Consts.PLANT,
                        gleGroup.EditValue.NullString(),
                        txtCommCode.EditValue.NullString()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    this.SearchPage();
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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
                    txtCommCode.EditValue = gvList.GetDataRow(e.RowHandle)["COMMCODE"].NullString();
                    txtCommName.EditValue = gvList.GetDataRow(e.RowHandle)["COMMNAME"].NullString();
                    txtCommNameKR.EditValue = gvList.GetDataRow(e.RowHandle)["COMMNAME_KR"].NullString();
                    txtCommNameVT.EditValue = gvList.GetDataRow(e.RowHandle)["COMMNAME_VT"].NullString();
                    txtValue1.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE1"].NullString();
                    txtValue2.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE2"].NullString();
                    txtValue3.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE3"].NullString();
                    txtValue4.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE4"].NullString();
                    txtValue5.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE5"].NullString();
                    txtValue6.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE6"].NullString();
                    txtValue7.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE7"].NullString();
                    txtValue8.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE8"].NullString();
                    txtValue9.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE9"].NullString();
                    txtValue10.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE10"].NullString();
                    txtValue11.EditValue = gvList.GetDataRow(e.RowHandle)["VALUE11"].NullString();
                    txtRemarks.EditValue = gvList.GetDataRow(e.RowHandle)["REMARKS"].NullString();
                    rdgUseFlag.EditValue = gvList.GetDataRow(e.RowHandle)["USEFLAG"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }






    }
}
