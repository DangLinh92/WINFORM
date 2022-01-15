using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.CONTENT;
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
            this.Load += SYSTEM004_Load;
        }

        private void SYSTEM004_Load(object sender, EventArgs e)
        {
            DataControls = new DataTable();
            DataControls.Columns.Add("Text", typeof(string));
            DataControls.Columns.Add("Name", typeof(string));
            DataControls.Columns.Add("FormId", typeof(string));
            DataControls.Columns.Add("IsActive", typeof(bool));

            Classes.Common.SetFormIdToButton(this, "SYSTEM004");
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

                    gvRoleList.OptionsSelection.MultiSelect = true;
                    gvRoleList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

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
                    tlMenuList.Columns["FORMROLE"].OptionsColumn.AllowEdit = false;
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
                // update last view source
                if (gcList.DataSource != null)
                {
                    DataTable dataOld = gcList.DataSource as DataTable;
                    bool isCheck = false;
                    DataRow rowNew;
                    foreach (DataRow row in dataOld.Rows)
                    {
                        foreach (DataRow item in DataControls.Rows)
                        {
                            if (row["FormId"].NullString() == item["FormId"].NullString() && row["Name"].NullString() == item["Name"].NullString())
                            {
                                item["IsActive"] = row["IsActive"];
                                isCheck = true;
                            }
                        }

                        if (!isCheck)
                        {
                            rowNew = DataControls.NewRow();
                            rowNew["FormId"] = row["FormId"];
                            rowNew["Name"] = row["Name"];
                            rowNew["Text"] = row["Text"];
                            rowNew["IsActive"] = row["IsActive"];
                            DataControls.Rows.Add(rowNew);
                        }
                    }
                }

                string strXml = string.Empty;

                strXml = Converter.GetDataTableToXml((tlMenuList.DataSource as DataTable).Copy());

                base.m_ResultDB = base.m_DBaccess.ExcuteProcWithTableParam("PKG_SYSTEM004.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_USERROLE",
                        "A_XML",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    }, "A_DATA"
                    , new string[] { Consts.PLANT,
                        gvRoleList.GetDataRow(gvRoleList.FocusedRowHandle)["USERROLE"].NullString(),
                        strXml,
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    }, DataControls
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataControls.Rows.Clear();
                    gcList.DataSource = null;
                    tlMenuList.DataSource = null;
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    // SearchPage();
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
                    foreach (int i in gvRoleList.GetSelectedRows())
                    {
                        if (i == e.RowHandle) continue;

                        gvRoleList.UnselectRow(i);
                    }

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
                        gvRoleList.GetDataRow(e.RowHandle)["DEPARTMENT"].NullString()
                    },
                    false,
                    "MENUSEQ, UPRSEQ"
                    );

                    tlMenuList.Columns["USEFLAG"].Width = 100;
                    tlMenuList.Columns["MENUNAME"].Width = 200;
                    tlMenuList.Columns["FORMROLE"].Width = 300;
                    tlMenuList.Columns["FORM_NAME"].Width = 150;

                    tlMenuList.ExpandAll();
                    gcList.DataSource = null;

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM004.GET_LIST_CONTROLS", new string[] { "A_PLANT",
                        "A_USERROLE",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] {
                        Consts.PLANT,
                        gvRoleList.GetDataRow(e.RowHandle)["USERROLE"].NullString(),
                        Consts.USER_INFO.Id,
                       gvRoleList.GetDataRow(e.RowHandle)["DEPARTMENT"].NullString()
                    });

                    if (m_ResultDB.ReturnInt == 0)
                    {
                        DataTable controlTb = m_ResultDB.ReturnDataSet.Tables[0];
                        if (controlTb.Rows.Count > 0)
                        {
                            int count = 0;
                            foreach (DataRow row in controlTb.Rows)
                            {
                                if (row["FormId"].NullString() == "")
                                {
                                    count++;
                                }
                            }

                            if (count == controlTb.Rows.Count)
                            {
                                DataControls.Rows.Clear();
                            }
                            else
                            {
                                DataControls = controlTb;
                            }
                        }
                        else
                        {
                            DataControls.Rows.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        DataTable DataControls = new DataTable();
        private void tlMenuList_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "FORM_NAME" || e.Column.FieldName == "MENUNAME")
                {
                    if (gcList.DataSource != null)
                    {
                        DataTable dataOld = gcList.DataSource as DataTable;
                        bool isCheck = false;
                        DataRow rowNew;
                        foreach (DataRow row in dataOld.Rows)
                        {
                            foreach (DataRow item in DataControls.Rows)
                            {
                                if (row["FormId"].NullString() == item["FormId"].NullString() && row["Name"].NullString() == item["Name"].NullString())
                                {
                                    item["IsActive"] = row["IsActive"];
                                    isCheck = true;
                                }
                            }

                            if (!isCheck)
                            {
                                rowNew = DataControls.NewRow();
                                rowNew["FormId"] = row["FormId"];
                                rowNew["Name"] = row["Name"];
                                rowNew["Text"] = row["Text"];
                                rowNew["IsActive"] = row["IsActive"];
                                DataControls.Rows.Add(rowNew);
                            }
                        }
                    }

                    string value = tlMenuList.GetRowCellValue(e.Node, "FORM_NAME").NullString();
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    var type = assembly.GetTypes().FirstOrDefault(x => x.Name == value);
                    string menuSeq = tlMenuList.GetRowCellValue(e.Node, "MENUSEQ").NullString();

                    if (type != null)
                    {
                        PageType pageType = assembly.CreateInstance(type.FullName, true) as PageType;
                        var controls = Classes.Common.GetAllButton(pageType);

                        bool isCheck1 = false;
                        foreach (var item in controls)
                        {
                            item.FormId = menuSeq;
                            foreach (DataRow row in DataControls.Rows)
                            {
                                if (row["FormId"].NullString() == menuSeq && item.Name == row["Name"].NullString())
                                {
                                    item.IsActive = bool.Parse(row["IsActive"].NullString() == "" ? "False" : row["IsActive"].NullString());
                                    isCheck1 = true;
                                }
                            }

                            if (!isCheck1)
                            {
                                item.IsActive = false;
                            }
                        }

                        gcList.DataSource = Classes.Common.ToDataTable(controls, DataControls);
                        gvList.OptionsBehavior.Editable = true;
                        gvList.Columns["Text"].OptionsColumn.AllowEdit = false;
                        gvList.Columns["Name"].OptionsColumn.AllowEdit = false;
                        gvList.Columns["FormId"].OptionsColumn.AllowEdit = false;
                    }
                    else
                    {
                        gcList.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void cheAll_CheckedChanged(object sender, EventArgs e)
        {
            cheUncheckAll.Checked = !cheAll.Checked;
            CheckUncheckControl(cheAll.Checked);
        }

        private void cheUncheckAll_CheckedChanged(object sender, EventArgs e)
        {
            cheAll.Checked = !cheUncheckAll.Checked;
            CheckUncheckControl(cheAll.Checked);
        }

        private void CheckUncheckControl(bool isCheckAll)
        {
            DataTable data = gcList.DataSource as DataTable;
            foreach (DataRow row in data.Rows)
            {
                row["IsActive"] = isCheckAll;
            }
        }

        private void cheCustome_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
