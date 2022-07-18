using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING006 : PageType
    {
        private string G_DRAFT_NUMBER = string.Empty;

        public SETTING006()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            //this.layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //this.layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {
            try
            {
                rdoStateDraft.EditValue = "2";

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_SELECT", new string[] { }, new string[] { });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridLookEdit(aceCtgDraft, base.m_ResultDB.ReturnDataSet.Tables[0], "CODE", "NAME_CATEGORY");
                    aceCtgDraft.EditValue = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["CODE"].ToString();
                    Init_Control(true);
                }

                /**base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING006.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_LANG", "A_USER_ID"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Language, Consts.USER_INFO.Id
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    Init_Control(true);
                }**/
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING006.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_LANG", "A_USER_ID", "A_CATEGORY", "A_STATE"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Language, Consts.USER_INFO.Id, aceCtgDraft.EditValue.NullString(), rdoStateDraft.EditValue.NullString()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    gvList.Columns["COST"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["COST"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["PRE_PAID"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PRE_PAID"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["PAY_1"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PAY_1"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["PAY_2"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PAY_2"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["PAY_3"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PAY_3"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["PAY_4"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["PAY_4"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["REMAIN"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["REMAIN"].DisplayFormat.FormatString = "n0";
                    //gvList.Columns["PAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //gvList.Columns["PAY"].DisplayFormat.FormatString = "n0";

                    //gvList.Columns["ID"].Visible = false;
                    gvList.Columns["FINISH_STATUS"].Visible = false;

                    gvList.Columns["DRAFT_ROOT"].Group();
                    gvList.ExpandAllGroups();

                    gvList.Columns["DRAFT_ROOT"].Visible = false;

                    //Init_Control(true);

                    gvList.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;


                    GridGroupSummaryItem item = new GridGroupSummaryItem();
                    item.FieldName = "COST";
                    item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item.DisplayFormat = "SUM = {0:n0}";
                    item.ShowInGroupColumnFooter = gvList.Columns["COST"];
                    gvList.GroupSummary.Add(item);

                    GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                    item1.FieldName = "PRE_PAID";
                    item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item1.DisplayFormat = "SUM = {0:n0}";
                    item1.ShowInGroupColumnFooter = gvList.Columns["PRE_PAID"];
                    gvList.GroupSummary.Add(item1);

                    GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                    item2.FieldName = "PAY_1";
                    item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item2.DisplayFormat = "SUM = {0:n0}";
                    item2.ShowInGroupColumnFooter = gvList.Columns["PAY_1"];
                    gvList.GroupSummary.Add(item2);

                    GridGroupSummaryItem item3 = new GridGroupSummaryItem();
                    item3.FieldName = "PAY_2";
                    item3.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item3.DisplayFormat = "SUM = {0:n0}";
                    item3.ShowInGroupColumnFooter = gvList.Columns["PAY_2"];
                    gvList.GroupSummary.Add(item3);

                    GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                    item4.FieldName = "PAY_3";
                    item4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item4.DisplayFormat = "SUM = {0:n0}";
                    item4.ShowInGroupColumnFooter = gvList.Columns["PAY_3"];
                    gvList.GroupSummary.Add(item4);

                    GridGroupSummaryItem item5 = new GridGroupSummaryItem();
                    item5.FieldName = "PAY_4";
                    item5.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item5.DisplayFormat = "SUM = {0:n0}";
                    item5.ShowInGroupColumnFooter = gvList.Columns["PAY_4"];
                    gvList.GroupSummary.Add(item5);

                    GridGroupSummaryItem item6 = new GridGroupSummaryItem();
                    item6.FieldName = "REMAIN";
                    item6.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    item6.DisplayFormat = "SUM = {0:n0}";
                    item6.ShowInGroupColumnFooter = gvList.Columns["REMAIN"];
                    gvList.GroupSummary.Add(item6);

                    gvList.Columns["DRAFT_NUMBER"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    //gvList.Columns["TYPE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["HANG_MUC"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["DEPARTMENT"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["SIGN_TIME"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["PAYMENT_DATE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    //gvList.Columns["DRAFT_REFERENCE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            

        }



        private void Init_Control(bool condFlag)
        {

        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;

            if (indexRow >= 0)
            {
                G_DRAFT_NUMBER = gvList.GetDataRow(indexRow)["DRAFT_NUMBER"].NullString();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            string draft_number = string.Empty;
            string draft_type = string.Empty;

            string caption = "COST";

            string category = aceCtgDraft.EditValue.NullString();
            Console.WriteLine(category);
            POP.POP_SETTING006_1 popup = new POP.POP_SETTING006_1(draft_type, draft_number, caption, category);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(G_DRAFT_NUMBER))
            {
                return;
            }

            //DialogResult dialogResult = MsgBox.Show("Bạn chắc chắn muốn xóa?",MsgType.Warning, DialogType.OkCancel);
            //if (dialogResult == DialogResult.OK)
            //{
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING006_2.POP_INT_LIST"
                        , new string[] { "A_PLANT" ,
                            "A_DEPARTMENT",
                            "A_USER_ID",
                            "A_LANG",
                            "A_DRAFT_NUMBER"
                        }
                        , new string[] { Consts.PLANT ,
                            "",
                            Consts.USER_INFO.Id,
                            Consts.USER_INFO.Language,
                            G_DRAFT_NUMBER
                        }
                    );

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        string maker_1 = "NguyenDemLongThanhKinh";
                        string maker_2 = "GuiTheoDamMayHuong";
                        string maker_3 = "PhangPhatKhapMuoiPhuong";
                        string type = base.m_ResultDB.ReturnDataSet.Tables[3].Rows[0]["TYPE_CODE"].ToString();
                        DialogResult dialogResult = MsgBox.Show(type != "0"?"Bạn chắc chắn muốn xóa?" : "Xóa Draft Đề Nghị sẽ xóa các Draft Thanh Toán liên quan.\r\nBạn chắc chắn muốn xóa?", MsgType.Warning, DialogType.OkCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            string root = base.m_ResultDB.ReturnDataSet.Tables[3].Rows[0]["DRAFT_ROOT"].ToString();
                            maker_1 = base.m_ResultDB.ReturnDataSet.Tables[3].Rows[0]["MAKER_CODE"].ToString();
                            if (base.m_ResultDB.ReturnDataSet.Tables[3].Rows.Count == 2)
                            {
                                maker_2 = base.m_ResultDB.ReturnDataSet.Tables[3].Rows[1]["MAKER_CODE"].ToString();
                            }
                            if (base.m_ResultDB.ReturnDataSet.Tables[3].Rows.Count == 3)
                            {
                                maker_2 = base.m_ResultDB.ReturnDataSet.Tables[3].Rows[1]["MAKER_CODE"].ToString();
                                maker_3 = base.m_ResultDB.ReturnDataSet.Tables[3].Rows[2]["MAKER_CODE"].ToString();
                            }


                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING006.DELETE_ITEM"
                                , new string[] { "A_PLAN", "A_DEPARTMENT", "A_USER", "A_TYPE", "A_DRAFT_NUMBER", "A_DRAFT_ROOT","A_MAKER_1", "A_MAKER_2", "A_MAKER_3"
                                }
                                , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Id, type, G_DRAFT_NUMBER, root, maker_1, maker_2, maker_3
                                }
                                );
                            if (base.m_ResultDB.ReturnInt == 0)
                            {
                                MsgBox.Show("Xóa thành công.", MsgType.Information);
                            }
                            else
                            {
                                MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
                SearchPage();
            //}
        }

        private void gvList_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if ((e.Column.FieldName == "DRAFT_NUMBER") && (e.Column.FieldName == "SIGN_TIME") && (e.Column.FieldName == "HANG_MUC")
                    && (e.Column.FieldName == "DEPARTMENT"))
                {
                    int value1 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle1, e.Column));
                    int value2 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle2, e.Column));

                    e.Merge = (value1 == value2);
                    e.Handled = true;

                    return;
                }
            }
            catch
            {
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Column.FieldName == "FINISH_STATUS" || e.Column.FieldName == "TYPE" || e.Column.FieldName == "DRAFT_NUMBER"
              ||e.Column.FieldName == "SIGN_TIME" || e.Column.FieldName == "HANG_MUC" || e.Column.FieldName == "DEPARTMENT"
              ||e.Column.FieldName == "COST" || e.Column.FieldName == "PRE_PAID" || e.Column.FieldName == "PAY_1"
              ||e.Column.FieldName == "PAY_2" ||e.Column.FieldName == "PAY_3" || e.Column.FieldName == "PAY_4"
              ||e.Column.FieldName == "REMAIN" ||e.Column.FieldName == "FACTORY" || e.Column.FieldName == "MAKER_NAME"
              ||e.Column.FieldName == "DRAFT_NAME" || e.Column.FieldName == "CREATE_DRAFT_PERSON")
            {
                if (!string.IsNullOrWhiteSpace(gvList.GetDataRow(e.RowHandle)["FINISH_STATUS"].NullString()))
                {
                    string status = gvList.GetDataRow(e.RowHandle)["FINISH_STATUS"].NullString();
                    if(status == "0")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                }
            }
        }

        private void btnDraftRef_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtDraft.EditValue.NullString()))
            //{
            //    this.SearchPage();
            //}
            //else
            //{
            //    try
            //    {
            //        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING006.GET_REF"
            //            , new string[] { "A_PLANT", "A_DEPARTMENT", "A_LANG", "A_USER_ID", "A_DRAFT_NUMBER"
            //            }
            //            , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Language, Consts.USER_INFO.Id, txtDraft.EditValue.NullString()
            //            }
            //            );
            //        if (base.m_ResultDB.ReturnInt == 0)
            //        {
            //            base.m_BindData.BindGridView(gcList,
            //                base.m_ResultDB.ReturnDataSet.Tables[0]
            //                );

            //            gvList.Columns["COST"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            gvList.Columns["COST"].DisplayFormat.FormatString = "n0";

            //            gvList.Columns["PAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            gvList.Columns["PAY"].DisplayFormat.FormatString = "n0";

            //            gvList.Columns["ID"].Visible = false;
            //            gvList.Columns["FINISH_STATUS"].Visible = false;

            //            Init_Control(true);

            //            gvList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvList_CustomDrawCell);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgBox.Show(ex.Message, MsgType.Error);
            //    }

            //    gvList.Columns["DRAFT_NUMBER"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //    gvList.Columns["DRAFT_REFERENCE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //}
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            this.SearchPage();
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    int indexRow = gvList.FocusedRowHandle;

            //    //if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //    if (indexRow >= 0)
            //    {
            //        string draft_number = gvList.GetDataRow(indexRow)["DRAFT_NUMBER"].NullString();
            //        string draft_type = gvList.GetDataRow(indexRow)["TYPE"].NullString();

            //        GridView view = gcList.FocusedView as GridView;
            //        GridHitInfo info = view.CalcHitInfo(gcList.PointToClient(MousePosition));
            //        string caption = info.Column.Caption;
            //        if (caption == "COST" || caption == "PRE_PAID" || caption == "PAY_1" ||
            //           caption == "PAY_2" || caption == "PAY_3" || caption == "PAY_4")
            //        {
            //            POP.POP_SETTING006_2 popup = new POP.POP_SETTING006_2(draft_type, draft_number, caption);
            //            popup.ShowDialog();
            //            this.SearchPage();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            //if(e.Info == null && e.SelectedControl == gcList)
            //{
            //    GridView view = gcList.FocusedView as GridView;
            //    GridHitInfo info = view.CalcHitInfo(e.ControlMousePosition);
            //    if (info.RowHandle < 1) return;
            //    if (view == null)
            //        return;
            //    if (info.InRowCell)
            //    {
            //        if(info.Column.Caption == "PRE_PAID" || info.Column.Caption == "PAY_1" || info.Column.Caption == "PAY_2" ||
            //           info.Column.Caption == "PAY_3" || info.Column.Caption == "PAY_4")
            //        {
            //            if (!string.IsNullOrWhiteSpace(view.GetRowCellValue(info.RowHandle, view.Columns[info.Column.Caption.ToString()]).ToString()))
            //            {
            //                string text = string.Empty;
            //                string draft_number = view.GetRowCellValue(info.RowHandle, view.Columns["DRAFT_NUMBER"]).ToString();
            //                string pay_tt = info.Column.Caption;

            //                try
            //                {
            //                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING006.GET_TOOLTIP"
            //                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_LANG", "A_USER_ID", "A_DRAFT_NUMBER", "A_PAY_TT"
            //                        }
            //                        , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Language, Consts.USER_INFO.Id, draft_number, pay_tt
            //                        }
            //                        );
            //                    if (base.m_ResultDB.ReturnInt == 0 && base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["DRAFT_NUMBER"].ToString()))
            //                        {
            //                            text = "DRAFT_NUMBER: " + base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["DRAFT_NUMBER"].ToString() + "\n"
            //                                   + "SIGN_TIME: " + base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["SIGN_TIME"].ToString();
            //                        }
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    MsgBox.Show(ex.Message, MsgType.Error);
            //                }

            //                string cellKey = info.RowHandle.ToString() + " - " + info.Column.ToString();
            //                e.Info = new DevExpress.Utils.ToolTipControlInfo(cellKey, text);
            //            }
            //        }
            //    }
            //}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(G_DRAFT_NUMBER))
            {
                return;
            }

            POP.POP_SETTING006_2 popup = new POP.POP_SETTING006_2(G_DRAFT_NUMBER);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void aceCtgDraft_EditValueChanged(object sender, EventArgs e)
        {
            SearchPage();
        }

        private void rdoStateDraft_EditValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Radio state: " + rdoStateDraft.EditValue.NullString());
            SearchPage();
        }
    }
}
