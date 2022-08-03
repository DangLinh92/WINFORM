using System;
using System.Data;
using System.Windows.Forms;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM008 : PageType
    {

        public SYSTEM008()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM008.INT_LIST"
                    , new string[] { "A_PLANT", "A_USER_ID", "A_DEPARTMENT" }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Id, Consts.DEPARTMENT }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcUserList
                        , base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    base.m_BindData.BindGridView(gcRoleList
                        , base.m_ResultDB.ReturnDataSet.Tables[1]
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
                if (gvUserList.FocusedRowHandle < 0)
                {
                    MsgBox.Show("MSG_ERR_2130".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM008.GET_LIST",
                    new string[]{"A_PLANT",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    },
                    new string[]{Consts.PLANT,
                        gvUserList.GetDataRow(gvUserList.FocusedRowHandle)["USER_ID"].NullString(),
                        Consts.DEPARTMENT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcRoleList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                    return;
                }
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

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = null;
            try
            {
                string strXml = string.Empty;

                dtTemp = (gcRoleList.DataSource as DataTable).Clone();
                for (int i = 0; i < (gcRoleList.DataSource as DataTable).Rows.Count; i++)
                {
                    if ((gcRoleList.DataSource as DataTable).Rows[i]["SEL"].NullString() == "Y")
                    {
                        dtTemp.Rows.Add((gcRoleList.DataSource as DataTable).Rows[i].ItemArray);
                    }
                }

                strXml = Converter.GetDataTableToXml(dtTemp);

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM008.PUT_ITEM",
                    new string[]{"A_PLANT",
                        "A_XML",
                        "A_USER_ID",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    },
                    new string[]{Consts.PLANT,
                        strXml,
                        gvUserList.GetDataRow(gvUserList.FocusedRowHandle)["USER_ID"].NullString(),
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

        private void gvRoleList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM008.GET_LIST",
                        new string[]{"A_PLANT",
                            "A_USER_ID",
                            "A_DEPARTMENT"
                        },
                        new string[]{Consts.PLANT,
                            gvUserList.GetDataRow(gvUserList.FocusedRowHandle)["USER_ID"].NullString(),
                            Consts.DEPARTMENT
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        base.m_BindData.BindGridView(gcRoleList,
                            base.m_ResultDB.ReturnDataSet.Tables[0]
                            );
                    }
                    else
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

    }
}
