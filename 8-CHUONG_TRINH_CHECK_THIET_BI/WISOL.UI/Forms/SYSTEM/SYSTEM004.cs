using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Data;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;


namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM004 : PageType
    {

        private RepositoryItemCheckEdit repChkSel = null;
        private RepositoryItemRadioGroup reprdgAuth = null;

        public SYSTEM004()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM004.INT_LIST"
                    , new string[] { "A_PLANT", "A_USER_ID", "A_DEPARTMENT" }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Id, Consts.DEPARTMENT }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcRoleList
                        , base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    repChkSel = new RepositoryItemCheckEdit();
                    repChkSel.ValueChecked = "Y";
                    repChkSel.ValueUnchecked = "N";
                    repChkSel.ValueGrayed = "Y";
                    repChkSel.CheckedChanged += new EventHandler(repChkSel_CheckedChanged);

                    reprdgAuth = new RepositoryItemRadioGroup();
                    reprdgAuth.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem("W", "WRITE_READ".Translation()));
                    reprdgAuth.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem("R", "READ".Translation()));

                    tlMenuList.KeyFieldName = "MENUSEQ";
                    tlMenuList.ParentFieldName = "UPRSEQ";
                    tlMenuList.OptionsView.ShowAutoFilterRow = false;
                    tlMenuList.EndSort();
                    tlMenuList.OptionsBehavior.Editable = true;
                    base.m_BindData.BindTreeList(tlMenuList,
                        base.m_ResultDB.ReturnDataSet.Tables[1]
                        , false
                        , "MENUSEQ, UPRSEQ"
                        );

                    tlMenuList.Columns["USEFLAG"].ColumnEdit = repChkSel;
                    tlMenuList.Columns["FORMROLE"].ColumnEdit = reprdgAuth;
                    tlMenuList.OptionsView.AutoWidth = false;
                    tlMenuList.RowHeight = 30;

                    tlMenuList.Columns["USEFLAG"].Width = 100;
                    tlMenuList.Columns["MENUNAME"].Width = 200;
                    tlMenuList.Columns["FORMROLE"].Width = 300;

                    tlMenuList.Columns["USEFLAG"].OptionsColumn.AllowEdit = true;
                    tlMenuList.Columns["FORMROLE"].OptionsColumn.AllowEdit = true;
                    tlMenuList.ExpandAll();

                    Init_Control();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            base.InitializePage();
        }

        void repChkSel_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckedParentNodes(tlMenuList.FocusedNode);
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_BindData.BindTreeList(tlMenuList,
                    "PKG_SYSTEM004.GET_LIST",
                    new string[] { "A_PLANT",
                        "A_USERROLE",
                        "A_LANG",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    },
                    new string[] { Consts.PLANT,
                        gvRoleList.GetDataRow(gvRoleList.FocusedRowHandle)["USERROLE"].NullString(),
                        Consts.USER_INFO.Language,
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    },
                    false,
                    "MENUSEQ, UPRSEQ"
                    );


                tlMenuList.Columns["USEFLAG"].Width = 100;
                tlMenuList.Columns["MENUNAME"].Width = 200;
                tlMenuList.Columns["FORMROLE"].Width = 300;

                tlMenuList.ExpandAll();
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

        private void SetCheckedParentNodes(TreeListNode parentnode)
        {
            if (parentnode.GetValue("USEFLAG").NullString() == "N")
            {
                parentnode.SetValue("USEFLAG", "Y");
                parentnode.SetValue("FORMROLE", "W");
            }
            else
            {
                parentnode.SetValue("USEFLAG", "N");
                parentnode.SetValue("FORMROLE", string.Empty);
            }
            SetCheckedsChildNodes(parentnode);
            SetParentCheckChange(parentnode);
            (tlMenuList.DataSource as DataTable).AcceptChanges();
        }
        private void SetCheckedsChildNodes(TreeListNode parentnode)
        {
            if (parentnode.HasChildren)
            {
                for (int i = 0; i < parentnode.Nodes.Count; i++)
                {
                    parentnode.Nodes[i].SetValue("USEFLAG", parentnode.GetValue("USEFLAG"));
                    parentnode.Nodes[i].SetValue("FORMROLE", parentnode.GetValue("FORMROLE"));
                    SetCheckedsChildNodes(parentnode.Nodes[i]);
                }
            }
        }
        private void SetParentCheckChange(TreeListNode childNode)
        {
            if (childNode.ParentNode != null)
            {
                int counter = 0;
                for (int i = 0; i < childNode.ParentNode.Nodes.Count; i++)
                {
                    if (childNode.ParentNode.Nodes[i].GetValue("USEFLAG").NullString() == "Y")
                    {
                        counter++;
                    }
                }
                if (counter == 0)
                {
                    childNode.ParentNode.SetValue("USEFLAG", "N");
                    childNode.ParentNode.SetValue("FORMROLE", string.Empty);
                }
                else if (counter == childNode.ParentNode.Nodes.Count)
                {
                    childNode.ParentNode.SetValue("USEFLAG", "Y");
                    childNode.ParentNode.SetValue("FORMROLE", "W");
                }
                else
                {
                    childNode.ParentNode.SetValue("USEFLAG", "Y");
                    childNode.ParentNode.SetValue("FORMROLE", "W");
                }

                SetParentCheckChange(childNode.ParentNode);
            }
        }





        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strXml = string.Empty;

                strXml = Converter.GetDataTableToXml((tlMenuList.DataSource as DataTable).Copy());

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM004.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_USERROLE",
                        "A_XML",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        gvRoleList.GetDataRow(gvRoleList.FocusedRowHandle)["USERROLE"].NullString(),
                        strXml,
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
                    base.m_BindData.BindTreeList(tlMenuList,
                    "PKG_SYSTEM004.GET_LIST",
                    new string[] { "A_PLANT",
                        "A_USERROLE",
                        "A_LANG",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    },
                    new string[] { Consts.PLANT,
                        gvRoleList.GetDataRow(e.RowHandle)["USERROLE"].NullString(),
                        Consts.USER_INFO.Language,
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    },
                    false,
                    "MENUSEQ, UPRSEQ"
                    );


                    tlMenuList.Columns["USEFLAG"].Width = 100;
                    tlMenuList.Columns["MENUNAME"].Width = 200;
                    tlMenuList.Columns["FORMROLE"].Width = 300;

                    tlMenuList.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

    }
}
