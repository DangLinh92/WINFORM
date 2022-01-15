using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using PROJ_B_DLL.DataAcess;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;

namespace Wisol.BindDatas
{
    public class BindData
    {
        private ServerInfo _ServerInfo;

        public ResultDB _Result = null;
        public DataTable _Glossary = null;
        public string _Language = string.Empty;
        public string _UserId = string.Empty;

        private Dictionary<string, Dictionary<string, Control>> _ParentBindingInfo = new Dictionary<string, Dictionary<string, Control>>();     


        public Dictionary<string, Dictionary<string, Control>> ParentBindInfo
        {
            set { _ParentBindingInfo = value; }
        }

        public BindData(ServerInfo servierInfo)
        {
            _ServerInfo = servierInfo;
            _Result = new ResultDB();
        }

        public BindData(ServerInfo servierInfo, String language, DataTable glossary)
        {
            _ServerInfo = servierInfo;
            _Glossary = new DataTable();
            _Glossary = glossary.Copy();
            _Language = language;
            _Result = new ResultDB();
        }

        public BindData(ServerInfo servierInfo, String language, DataTable glossary, string userId)
        {
            _ServerInfo = servierInfo;
            _Glossary = new DataTable();
            _Glossary = glossary.Copy();
            _Language = language;
            _UserId = userId;
            _Result = new ResultDB();
        }



        public void BindTreeList(TreeList tl, string procName, string[] argument, string[] parameter, bool repaint = false, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                if (argument.Length != parameter.Length)
                {
                    _Result.ReturnInt = -1;
                    _Result.ReturnString = "파라메터 수와 아규먼트 수가 틀려 바인딩 할 수 없습니다.";
                    _Result.ReturnDataSet = new DataSet();
                    return;
                }
                Dictionary<string, string> dicParam = new Dictionary<string, string>();

                for (int i = 0; i < argument.Length; i++)
                {
                    dicParam.Add(argument[i], parameter[i]);
                }
                BindTreeList(tl, procName, dicParam, repaint, hidden, dbAccesstype, rfcTableCount);
            }
            catch
            {
            }
        }
        public void BindTreeList(TreeList tl, string procName, Dictionary<string, string> param, bool repaint = false, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteProc(procName, param, dbAccesstype, rfcTableCount);
                if (_Result.ReturnInt == 0)
                {
                    BindTreeList(tl, _Result.ReturnDataSet.Tables[0], repaint, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                    return;
                }
            }
            catch
            {
            }
        }
        public void BindTreeList(TreeList tl, string query, bool repaint = false, string hidden = "")
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteQuery(query);
                if (_Result.ReturnInt == 0)
                {
                    BindTreeList(tl, _Result.ReturnDataSet.Tables[0], repaint, hidden);

                    Clipboard.SetText(query);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                    return;
                }
            }
            catch
            {
            }
        }
        public void BindTreeList(TreeList tl, DataTable dataTable, bool repaint = false, string hidden = "", string v = null)
        {
            try
            {
                if (dataTable == null || dataTable.Columns.Count == 0)
                {
                    tl.DataSource = null;
                    return;
                }

                if (repaint == false)
                {
                    this.AddColumns(tl, dataTable, hidden);
                }

                tl.OptionsView.EnableAppearanceEvenRow = true;
                tl.OptionsView.EnableAppearanceOddRow = true;

                tl.DataSource = dataTable.Copy();
            }
            catch
            {
            }
        }



        public void BindGridView(GridControl gridControl, string procedureName, string[] arguments, string[] parameters, bool repaint = false, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                if (arguments.Length != parameters.Length)
                {
                    _Result.ReturnInt = -1;
                    _Result.ReturnString = "파라메터 수와 아규먼트 수가 틀려 바인딩 할 수 없습니다.";
                    _Result.ReturnDataSet = new DataSet();
                    return;
                }
                Dictionary<string, string> dicParam = new Dictionary<string, string>();

                for (int i = 0; i < arguments.Length; i++)
                {
                    dicParam.Add(arguments[i], parameters[i]);
                }

                BindGridView(gridControl, procedureName, dicParam, repaint, hidden, dbAccesstype, rfcTableCount);
            }
            catch
            {
            }
        }
        public void BindGridView(GridControl gridControl, string procedureName, Dictionary<string, string> parameters, bool repaint = false, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteProc(procedureName, parameters, dbAccesstype, rfcTableCount);

                if (_Result.ReturnInt == 0)
                {
                    BindGridView(gridControl, _Result.ReturnDataSet.Tables[0], repaint, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                    return;
                }
            }
            catch
            {
            }
        }
        public void BindGridView(GridControl gridControl, string query, bool repaint = false, string hidden = "")
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteQuery(query);
                if (_Result.ReturnInt == 0)
                {
                    BindGridView(gridControl, _Result.ReturnDataSet.Tables[0], repaint, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                    return;
                }

                Clipboard.SetText(query);
            }
            catch
            {
            }
        }
        public void BindGridView(GridControl gridControl, DataTable dataTable, bool repaint = false, string hidden = "")
        {
            try
            {
                if (dataTable == null || dataTable.Columns.Count == 0)
                {
                    gridControl.DataSource = null;
                    return;
                }

                var view = gridControl.MainView as GridView;

                if (repaint == false)
                {
                    view.OptionsView.ShowFooter = true;
                    if (view is BandedGridView)    
                    {
                        BandedGridView bandedGridView = view as BandedGridView;

                        bandedGridView.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
                        bandedGridView.OptionsBehavior.AutoExpandAllGroups = true;
                        bandedGridView.OptionsBehavior.Editable = false;
                        bandedGridView.OptionsFilter.UseNewCustomFilterDialog = true;
                        bandedGridView.OptionsLayout.StoreAppearance = true;
                        bandedGridView.OptionsLayout.StoreAllOptions = true;
                        bandedGridView.OptionsPrint.AutoWidth = false;
                        bandedGridView.OptionsView.ShowColumnHeaders = false;
                        bandedGridView.OptionsView.ShowAutoFilterRow = true;
                        bandedGridView.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                        bandedGridView.OptionsView.ShowGroupPanel = false;

                        SetBandedGridViewColumn(bandedGridView, dataTable);
                    }
                    else        
                    {
                        AddColumns(view, dataTable, hidden);
                    }
                }
                else
                {
                    if (view.OptionsView.ColumnAutoWidth == true)
                    {
                        foreach (GridColumn gridColumn in view.Columns)
                        {
                            gridColumn.Width = (view.GridControl.ClientRectangle.Width - view.IndicatorWidth) / view.VisibleColumns.Count;
                        }
                    }
                }

                gridControl.DataSource = dataTable.Copy();
                if (_ParentBindingInfo.ContainsKey(gridControl.Name) == true)
                {
                    Dictionary<string, Control> BindInfo = _ParentBindingInfo[gridControl.Name];

                    if (BindInfo != null && BindInfo.Count > 0)
                    {
                        for (int idx = 0; idx < dataTable.Columns.Count; idx++)
                        {
                            DataColumn dc = dataTable.Columns[idx];

                            if (BindInfo.ContainsKey(dc.ColumnName))
                            {
                                this.BindGrid(gridControl, BindInfo[dc.ColumnName], dc.ColumnName);
                            }
                        }
                    }
                }

                view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.EnableAppearanceEvenRow = true;
                view.OptionsView.EnableAppearanceOddRow = true;
                view.BestFitMaxRowCount = 20;
                if (view.OptionsView.ShowColumnHeaders == false)
                {
                    view.OptionsView.ShowColumnHeaders = true;
                    view.BestFitColumns();
                    view.OptionsView.ShowColumnHeaders = false;
                }
                else
                {
                    view.BestFitColumns();
                }
                view.Appearance.FooterPanel.Font = new Font(view.Appearance.FooterPanel.Font.Name, view.Appearance.FooterPanel.Font.Size, FontStyle.Bold);
                view.FocusedRowHandle = -1;
            }
            catch
            {
            }
        }



        public void BindGridLookEdit(GridLookUpEdit gridLookUpEdit, string procedureName, string[] arguments, string[] parameters, string valueMember, string displayMember, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                if (arguments.Length != parameters.Length)
                {
                    _Result.ReturnInt = -1;
                    _Result.ReturnString = "파라메터 수와 아규먼트 수가 틀려 바인딩 할 수 없습니다.";
                    _Result.ReturnDataSet = new DataSet();
                    return;
                }
                Dictionary<string, string> dicParam = new Dictionary<string, string>();

                for (int i = 0; i < arguments.Length; i++)
                {
                    dicParam.Add(arguments[i], parameters[i]);
                }

                BindGridLookEdit(gridLookUpEdit, procedureName, dicParam, valueMember, displayMember, hidden, dbAccesstype, rfcTableCount);
            }
            catch
            {
            }
        }
        public void BindGridLookEdit(GridLookUpEdit gridLookUpEdit, string procedureName, Dictionary<string, string> parameters, string valueMember, string displayMember, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteProc(procedureName, parameters, dbAccesstype, rfcTableCount);

                if (_Result.ReturnInt == 0)
                {
                    BindGridLookEdit(gridLookUpEdit, _Result.ReturnDataSet.Tables[0], valueMember, displayMember, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                    return;
                }
            }
            catch
            {
            }
        }
        public void BindGridLookEdit(GridLookUpEdit gridLookUpEdit, string query, string valueMember, string displayMember, string hidden = "")
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteQuery(query);

                if (_Result.ReturnInt == 0)
                {
                    BindGridLookEdit(gridLookUpEdit, _Result.ReturnDataSet.Tables[0], valueMember, displayMember, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                }
            }
            catch
            {
            }
        }
        public void BindGridLookEdit(GridLookUpEdit gridLookUpEdit, DataTable dataTable, string valueMember, string displayMember, string hidden = "")
        {
            gridLookUpEdit.Text = string.Empty;
            gridLookUpEdit.Properties.NullText = string.Empty;
            DataTable dtTemp = dataTable.Copy();
            DataRow dr = dtTemp.NewRow();
            dtTemp.Rows.InsertAt(dr, 0);
            dtTemp.AcceptChanges();
            GridView gvView = gridLookUpEdit.Properties.View;

            AddColumns(gvView, dtTemp, hidden);

            gridLookUpEdit.Properties.DataSource = dtTemp.Copy();
            gridLookUpEdit.Properties.ValueMember = valueMember;
            gridLookUpEdit.Properties.DisplayMember = displayMember;
            gridLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            gvView.GridControl.Refresh();
            gridLookUpEdit.Refresh();
        }
        public void BindGridLookEdit(RepositoryItemGridLookUpEdit gridLookUpEdit, string procedureName, string[] arguments, string[] parameters, string valueMember, string displayMember, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                if (arguments.Length != parameters.Length)
                {
                    _Result.ReturnInt = -1;
                    _Result.ReturnString = "파라메터 수와 아규먼트 수가 틀려 바인딩 할 수 없습니다.";
                    _Result.ReturnDataSet = new DataSet();
                    return;
                }
                Dictionary<string, string> dicParam = new Dictionary<string, string>();

                for (int i = 0; i < arguments.Length; i++)
                {
                    dicParam.Add(arguments[i], parameters[i]);
                }

                BindGridLookEdit(gridLookUpEdit, procedureName, dicParam, valueMember, displayMember, hidden, dbAccesstype, rfcTableCount);
            }
            catch
            {
            }
        }
        public void BindGridLookEdit(RepositoryItemGridLookUpEdit gridLookUpEdit, string procedureName, Dictionary<string, string> parameters, string valueMember, string displayMember, string hidden = "", DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteProc(procedureName, parameters, dbAccesstype, rfcTableCount);

                if (_Result.ReturnInt == 0)
                {
                    BindGridLookEdit(gridLookUpEdit, _Result.ReturnDataSet.Tables[0], valueMember, displayMember, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                    return;
                }
            }
            catch
            {
            }
        }
        public void BindGridLookEdit(RepositoryItemGridLookUpEdit gridLookUpEdit, string query, string valueMember, string displayMember, string hidden = "")
        {
            try
            {
                DBAccess access = new DBAccess(_ServerInfo.ClientIp, _ServerInfo.ServerIp, _ServerInfo.ServicePort, _ServerInfo.ServiceID, _ServerInfo.ServicePassword, _UserId);

                _Result = access.ExcuteQuery(query);

                if (_Result.ReturnInt == 0)
                {
                    BindGridLookEdit(gridLookUpEdit, _Result.ReturnDataSet.Tables[0], valueMember, displayMember, hidden);
                }
                else
                {
                    MsgBox.Show(this.Translation(_Result.ReturnString), MsgType.Warning);
                }
            }
            catch
            {
            }
        }
        public void BindGridLookEdit(RepositoryItemGridLookUpEdit gridLookUpEdit, DataTable dataTable, string valueMember, string displayMember, string hidden = "")
        {

            gridLookUpEdit.NullText = string.Empty;
            DataTable dtTemp = dataTable.Copy();
            DataRow dr = dtTemp.NewRow();
            dtTemp.Rows.InsertAt(dr, 0);
            dtTemp.AcceptChanges();
            GridView gvView = gridLookUpEdit.View;

            AddColumns(gvView, dtTemp, hidden);

            gridLookUpEdit.DataSource = dtTemp.Copy();
            gridLookUpEdit.ValueMember = valueMember;
            gridLookUpEdit.DisplayMember = displayMember;
            gridLookUpEdit.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            gvView.GridControl.Refresh();
        }



        private void AddColumns(GridView gridView, DataTable dataTable, string hiddenFieldNames)
        {
            try
            {
                string[] fieldNames = hiddenFieldNames.Replace(" ", "").Split(',');

                var columnInfo = new Dictionary<String, GridColumn>();
                if (gridView.Columns.Count > 0)
                {
                    foreach (GridColumn column in gridView.Columns)
                    {
                        columnInfo.Add(column.FieldName, column);
                    }
                }

                gridView.Columns.Clear();
                gridView.GroupCount = 0;
                gridView.FormatConditions.Clear();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    DataColumn dataColumn = dataTable.Columns[i];
                    GridColumn gridColumn = gridView.Columns.AddField(dataColumn.ColumnName);

                    if (columnInfo.ContainsKey(gridColumn.FieldName) == true)
                    {
                        gridColumn.OptionsColumn.AllowEdit = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowEdit;
                        gridColumn.OptionsColumn.AllowMerge = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowMerge;
                        gridColumn.OptionsColumn.AllowSort = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowSort;
                        gridColumn.OptionsColumn.AllowGroup = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowGroup;
                        gridColumn.Fixed = (columnInfo[gridColumn.FieldName]).Fixed;
                        gridColumn.ColumnEdit = (columnInfo[gridColumn.FieldName]).ColumnEdit;
                    }
                    else
                    {
                        gridColumn.OptionsColumn.AllowEdit = false;
                        gridColumn.OptionsColumn.AllowMerge = DefaultBoolean.False;
                    }

                    gridColumn.Caption = this.Translation(dataColumn.ColumnName);
                    gridColumn.Tag = gridColumn.Width;
                    gridColumn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                    gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    switch (dataColumn.DataType.NullString())
                    {
                        case "System.DateTime":
                            if (gridColumn.DisplayFormat.FormatString == string.Empty)
                            {
                                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                            }
                            break;
                        case "System.Decimal":
                        case "System.Double":
                        case "System.Single":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Long":
                        case "System.Byte":
                            if (gridColumn.DisplayFormat.FormatString == string.Empty)
                            {
                                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                gridColumn.DisplayFormat.FormatString = "n2";
                                gridColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                                gridColumn.SummaryItem.DisplayFormat = "SUM={0:n0}";
                            }
                            break;
                        default:
                            break;
                    }
                    if (gridColumn.FieldName == "SEL")
                    {
                        var checkEdit = new RepositoryItemCheckEdit
                        {
                            Tag = gridView,
                            ValueChecked = "Y",
                            ValueUnchecked = "N",
                            ValueGrayed = "N"
                        };
                        checkEdit.CheckedChanged += new EventHandler(CheckEdit_CheckedChanged);

                        gridColumn.ColumnEdit = checkEdit;
                        gridColumn.OptionsColumn.AllowEdit = true;
                        gridColumn.OptionsColumn.AllowSort = DefaultBoolean.False;
                        gridColumn.Width = 40;

                        gridView.OptionsBehavior.Editable = true;
                    }
                }

                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    gridView.Columns[i].Visible = true;
                    for (int j = 0; j < fieldNames.Length; j++)
                    {
                        if (gridView.Columns[i].FieldName == fieldNames[j])
                        {
                            gridView.Columns[i].Visible = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void AddColumns(TreeList treeList, DataTable dataTable, string hiddenFieldNames)
        {
            try
            {
                string[] fieldNames = hiddenFieldNames.Replace(" ", "").Split(',');

                Dictionary<string, TreeListColumn> TreeColumnInfo = new Dictionary<string, TreeListColumn>();
                if (treeList.Columns.Count > 0)
                {
                    foreach (TreeListColumn tc in treeList.Columns)
                    {
                        TreeColumnInfo.Add(tc.FieldName, tc);
                    }
                }

                treeList.Columns.Clear();
                treeList.FormatConditions.Clear();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    DataColumn dc = dataTable.Columns[i];
                    TreeListColumn tc = treeList.Columns.AddField(dc.ColumnName);

                    if (TreeColumnInfo.ContainsKey(tc.FieldName) == true)
                    {
                        tc.OptionsColumn.AllowEdit = (TreeColumnInfo[tc.FieldName]).OptionsColumn.AllowEdit;
                        tc.OptionsColumn.AllowSort = (TreeColumnInfo[tc.FieldName]).OptionsColumn.AllowSort;
                        tc.Fixed = (TreeColumnInfo[tc.FieldName]).Fixed;
                        tc.ColumnEdit = (TreeColumnInfo[tc.FieldName]).ColumnEdit;
                    }
                    else
                    {
                        tc.OptionsColumn.AllowEdit = false;
                    }

                    tc.Caption = this.Translation(dc.ColumnName);
                    tc.FieldName = dc.ColumnName;
                    tc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
                for (int i = 0; i < treeList.Columns.Count; i++)
                {
                    treeList.Columns[i].Visible = true;
                    for (int j = 0; j < fieldNames.Length; j++)
                    {
                        if (treeList.Columns[i].FieldName == fieldNames[j])
                        {
                            treeList.Columns[i].Visible = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void SetBandedGridViewColumn(BandedGridView bandedGridView, DataTable dataTable)
        {
            foreach (GridBand gridBand in bandedGridView.Bands)
            {
                this.SetGridBand(bandedGridView, gridBand, dataTable);
            }
        }
        private void SetGridBand(BandedGridView bandedGridView, GridBand gridBand, DataTable dataTable)
        {
            if (gridBand == null)
                return;

            if (dataTable == null)
                return;

            gridBand.Caption = this.Translation(gridBand.Caption);
            gridBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            if (gridBand.HasChildren == true)
            {
                for (int i = 0; i < gridBand.Children.Count; i++)
                {
                    this.SetGridBand(bandedGridView, gridBand.Children[i], dataTable);
                }
            }

            if (gridBand.Columns.Count > 0)
            {
                for (int i = 0; i < gridBand.Columns.Count; i++)
                {
                    gridBand.Columns[i].Caption = this.Translation(gridBand.Columns[i].Caption);           
                    gridBand.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;    
                    gridBand.Columns[i].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;     

                    if (dataTable.Columns.Contains(gridBand.Columns[i].FieldName) == true)
                    {
                        switch (dataTable.Columns[gridBand.Columns[i].FieldName].DataType.NullString())
                        {
                            case "System.DateTime":
                                if (gridBand.Columns[i].DisplayFormat.FormatString == "")
                                {
                                    gridBand.Columns[i].DisplayFormat.FormatType = FormatType.DateTime;
                                    gridBand.Columns[i].DisplayFormat.FormatString = "yyyy-MM-dd";
                                }
                                break;

                            case "System.Decimal":
                            case "System.Double":
                            case "System.Single":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Long":
                            case "System.Byte":
                                if (gridBand.Columns[i].DisplayFormat.FormatString == "")
                                {
                                    gridBand.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                                    gridBand.Columns[i].DisplayFormat.FormatString = "n2";
                                    gridBand.Columns[i].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                                    gridBand.Columns[i].SummaryItem.DisplayFormat = "{0:n0}";
                                }
                                break;
                        }
                    }

                    if (gridBand.Columns[i].FieldName == "SEL")
                    {
                        var checkEdit = new RepositoryItemCheckEdit
                        {
                            Tag = bandedGridView,
                            ValueChecked = "Y",
                            ValueUnchecked = "N",
                            ValueGrayed = "N"
                        };
                        checkEdit.CheckedChanged += new EventHandler(CheckEdit_CheckedChanged);

                        gridBand.Columns[i].ColumnEdit = checkEdit;
                        gridBand.Columns[i].OptionsColumn.AllowEdit = true;
                        gridBand.Columns[i].OptionsColumn.AllowSort = DefaultBoolean.False;          
                        gridBand.Columns[i].Width = 40;
                        bandedGridView.OptionsBehavior.Editable = true;
                    }
                }
            }
        }


        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit)
            {
                if (checkEdit.Tag is RepositoryItemCheckEdit repositoryEdit)
                {
                    if (repositoryEdit.Tag is GridView gridView)
                    {
                        gridView.PostEditor();
                    }
                }
            }
        }

        private void BindGrid(GridControl gridControl, Control control, string dataMember)
        {
            try
            {
                control.DataBindings.Clear();
                control.DataBindings.Add("EditValue", gridControl.DataSource, dataMember, false, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch
            {
            }
        }

        public string Translation(string glossaryCode)
        {
            string returnString = glossaryCode;

            try
            {
                if (string.IsNullOrEmpty(returnString)) return String.Empty;

                if (_Glossary == null || _Glossary.Rows.Count == 0) return returnString;

                DataRow[] rows = _Glossary.Select(string.Format("GLSR = '{0}'", glossaryCode));

                if (rows.Length > 0)
                {
                    switch (_Language)
                    {
                        case "KOR":
                            returnString = rows[0]["KOR"].ToString();
                            break;
                        //case "ENG":
                        //    returnString = rows[0]["ENG"].ToString();
                        //    break;
                        //case "CHN":
                        //    returnString = rows[0]["CHN"].ToString();
                        //    break;
                        case "VTN":
                            returnString = rows[0]["VTN"].ToString();
                            break;
                    }

                    if (returnString == "") returnString = glossaryCode;
                }
                else
                {
                    returnString = glossaryCode;
                }
            }
            catch
            {
                returnString = glossaryCode;
            }
            return returnString.Replace("\\r", "\r").Replace("\\n", "\n");
        }
    }
}
