using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.BindDatas;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;
using DialogType = Wisol.Components.DialogType;

namespace Wisol.MES.Inherit
{
    [ToolboxItem(false)]
    public partial class FormType : XtraForm
    {
        protected Dictionary<string, Dictionary<string, Control>> BindInfo = new Dictionary<string, Dictionary<string, Control>>();     
        private GridView MouseDownGridView;           
        public DBAccess mDBaccess = null;
        public BindData mBindData = null;
        public ResultDB mResultDB = null;


        public FormType()
        {
            InitializeComponent();

            this.KeyPreview = true;

            ServerInfo service = new ServerInfo();
            service.ServerIp = Consts.SERVICE_INFO.ServiceIp;
            service.ClientIp = Consts.LOCAL_SYSTEM_INFO.IpAddress;
            service.ServicePort = Converter.ParseValue<int>(Consts.SERVICE_INFO.ServicePort);
            service.ServiceID = Consts.SERVICE_INFO.UserId;
            service.ServicePassword = Consts.SERVICE_INFO.Password;

            mDBaccess = new DBAccess(service);
            mBindData = new BindData(service, Consts.USER_INFO.Language, Consts.GLOSSARY.Copy());
            mResultDB = new ResultDB();
        }

        private void FormType_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Text = this.Text.Translation();
            ControlsInitialize();
        }

        private void ControlsInitialize()
        {
            var controls = GetAllControls(this);

            foreach (var control in controls)
            {
                if (control.Name == "") continue;

                var baseControl = control as BaseControl;
                if (baseControl != null) { baseControl.ToolTip = baseControl.Text.Translation("KOR"); }    

                if (control is LabelControl || control is SimpleButton || control is GroupControl || control is CheckEdit || control is TabPage)
                {
                    control.Text = control.Text.Translation();
                }
                else if (control is RadioGroup)  
                {
                    RadioGroup radioGroup = control as RadioGroup;

                    foreach (RadioGroupItem item in radioGroup.Properties.Items)
                    {
                        item.Description = item.Description.Translation();
                    }
                }
                else if (control is SpinEdit)
                {
                    SpinEdit spinEdit = control as SpinEdit;

                    spinEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    spinEdit.Properties.MinValue = 0;
                    spinEdit.Properties.MaxValue = decimal.MaxValue;
                }
                else if (control is LayoutControl)   
                {
                    LayoutControl layoutCtrl = control as LayoutControl;
                    layoutCtrl.AllowCustomization = false;
                    layoutCtrl.BeginUpdate();

                    foreach (BaseLayoutItem item in layoutCtrl.Items)
                    {
                        item.OptionsToolTip.ToolTip = item.Text.Translation("KOR");    
                        item.Text = item.Text.Translation();
                        item.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;

                        if (item.IsGroup)      
                        {
                            LayoutControlGroup layoutCtrlGrp = item as LayoutControlGroup;
                            layoutCtrlGrp.AppearanceGroup.Font = new Font(layoutCtrlGrp.AppearanceGroup.Font.Name, layoutCtrlGrp.AppearanceGroup.Font.Size, FontStyle.Bold);
                            layoutCtrlGrp.Padding = new DevExpress.XtraLayout.Utils.Padding(3);
                        }
                    }
                    layoutCtrl.EndUpdate();
                }
                if (control is GridControl) 
                {
                    GridControl gc = control as GridControl;

                    if (gc.DefaultView is GridView)
                    {
                        GridView gv = gc.DefaultView as GridView;

                        gv.BestFitMaxRowCount = 40;
                        gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
                        gv.IndicatorWidth = 50;

                        gv.Appearance.FocusedCell.BackColor = Color.FromArgb(205, 230, 247);
                        gv.Appearance.SelectedRow.BackColor = Color.FromArgb(100, 140, 145, 255);
                        gv.Appearance.FocusedRow.BackColor = Color.FromArgb(100, 140, 145, 255);

                        gv.OptionsBehavior.Editable = false;                          
                        gv.OptionsBehavior.AutoExpandAllGroups = false;                                  
                        gv.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;                
                        gv.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;                   

                        gv.OptionsMenu.EnableFooterMenu = true;
                        gv.OptionsMenu.ShowGroupSummaryEditorItem = true;

                        gv.OptionsPrint.AutoWidth = false;
                        gv.OptionsPrint.PrintDetails = true;                  

                        gv.OptionsSelection.MultiSelect = true;                                           
                        gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;      

                        gv.OptionsView.ShowGroupPanel = false;
                        gv.OptionsView.ShowAutoFilterRow = true;          
                        gv.OptionsView.ShowGroupedColumns = true;            
                        gv.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                        gv.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;            


                        gv.MouseDown += new MouseEventHandler(gv_MouseDown);
                        gv.DoubleClick += new EventHandler(gv_DoubleClick);
                        gv.EndGrouping += new EventHandler(gv_EndGrouping);
                        gv.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gv_CustomDrawRowIndicator);
                        gv.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gv_CustomDrawCell);
                    }
                }

                if (control is GridLookUpEdit)
                {
                    GridLookUpEdit gridLookUpEdit = control as GridLookUpEdit;

                    gridLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    gridLookUpEdit.Properties.View.OptionsView.ShowAutoFilterRow = true;

                    GridLookUpPopupFormSizeHelper glePopSizeHelper = new GridLookUpPopupFormSizeHelper(gridLookUpEdit);
                }

                if (control is DateEdit)
                {
                    DateEdit dateEdit = control as DateEdit;

                    dateEdit.Properties.DisplayFormat.FormatString = "d";
                    dateEdit.Properties.DisplayFormat.FormatType = FormatType.DateTime;
                    dateEdit.Properties.EditFormat.FormatString = "d";
                    dateEdit.Properties.EditFormat.FormatType = FormatType.DateTime;


                    dateEdit.Properties.AllowNullInput = DefaultBoolean.False;
                    dateEdit.Properties.Mask.EditMask = "yyyy-MM-dd";
                    dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                    dateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                }
                if (control is GroupControl)
                {
                    GroupControl groupControl = control as GroupControl;

                    groupControl.AppearanceCaption.Font = new Font(groupControl.AppearanceCaption.Font.Name, groupControl.AppearanceCaption.Font.Size, FontStyle.Bold);
                    groupControl.Padding = new Padding(3);
                }

                if (control is XtraTabPage)
                {
                    XtraTabPage xtraTabPage = control as XtraTabPage;
                    xtraTabPage.Text = xtraTabPage.Text.Translation();
                    xtraTabPage.Padding = new Padding(3);
                }
            }
            barSubItemExport.Caption = barSubItemExport.Caption.Translation();
            barSubItemCopy.Caption = barSubItemCopy.Caption.Translation();
            barSubItemSummery.Caption = barSubItemSummery.Caption.Translation();
            barSubItemBold.Caption = barSubItemBold.Caption.Translation();
            barSubItemBar.Caption = barSubItemBar.Caption.Translation();

            barButtonItemXls.Caption = barButtonItemXls.Caption.Translation();
            barButtonItemXlsx.Caption = barButtonItemXlsx.Caption.Translation();
            barButtonItemHtml.Caption = barButtonItemHtml.Caption.Translation();
            barButtonItemPdf.Caption = barButtonItemPdf.Caption.Translation();
            barButtonItemTxt.Caption = barButtonItemTxt.Caption.Translation();
            barButtonItemAllCopy.Caption = barButtonItemAllCopy.Caption.Translation();
            barButtonItemLineCopy.Caption = barButtonItemLineCopy.Caption.Translation();
            barButtonItemCellCopy.Caption = barButtonItemCellCopy.Caption.Translation();

            barButtonItemSummery.Caption = barButtonItemSummery.Caption.Translation();
            barButtonItemAverage.Caption = barButtonItemAverage.Caption.Translation();
            barButtonItemMax.Caption = barButtonItemMax.Caption.Translation();
            barButtonItemMin.Caption = barButtonItemMin.Caption.Translation();
            barButtonItemCount.Caption = barButtonItemCount.Caption.Translation();
            barButtonItemClearSummery.Caption = barButtonItemClearSummery.Caption.Translation();

            barButtonItemTopTenItems.Caption = barButtonItemTopTenItems.Caption.Translation();
            barButtonItemBottomTenItems.Caption = barButtonItemBottomTenItems.Caption.Translation();
            barButtonItemTopTenPercent.Caption = barButtonItemTopTenPercent.Caption.Translation();
            barButtonItemBottomTenPercent.Caption = barButtonItemBottomTenPercent.Caption.Translation();
            barButtonItemAboveAverage.Caption = barButtonItemAboveAverage.Caption.Translation();
            barButtonItemBlowAverage.Caption = barButtonItemBlowAverage.Caption.Translation();
            barButtonItemClearBlod.Caption = barButtonItemClearBlod.Caption.Translation();

            barButtonItemBlue.Caption = barButtonItemBlue.Caption.Translation();
            barButtonItemRed.Caption = barButtonItemRed.Caption.Translation();
            barButtonItemOrange.Caption = barButtonItemOrange.Caption.Translation();
            barButtonItemViolet.Caption = barButtonItemViolet.Caption.Translation();
            barButtonItemGreen.Caption = barButtonItemGreen.Caption.Translation();
            barButtonItemPupple.Caption = barButtonItemPupple.Caption.Translation();
            barButtonItemBarClear.Caption = barButtonItemBarClear.Caption.Translation();
        }





        void gv_EndGrouping(object sender, EventArgs e)
        {
            GridView gridview = sender as GridView;

            if (gridview.GroupedColumns.Count <= 0)
            {
                return;
            }
            if (gridview.GroupSummary.Count == 0)
            {
                gridview.GroupSummary.Clear();
                GridGroupSummaryItem item = new GridGroupSummaryItem();
                item.FieldName = gridview.GroupedColumns[0].FieldName;
                item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                gridview.GroupSummary.Add(item);
            }

            gridview.VisibleColumns[0].BestFit();


            for (int i = 0; i < gridview.Columns.Count; i++)
            {
                if (gridview.Columns[i].ColumnType == typeof(Decimal) ||
                    gridview.Columns[i].ColumnType == typeof(Double) ||
                    gridview.Columns[i].ColumnType == typeof(Single) ||
                    gridview.Columns[i].ColumnType == typeof(Int32) ||
                    gridview.Columns[i].ColumnType == typeof(Int64) ||
                    gridview.Columns[i].ColumnType == typeof(long) ||
                    gridview.Columns[i].ColumnType == typeof(Byte))
                {
                    if (gridview.GroupSummary.Count == 1)
                    {
                        GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                        item1.FieldName = gridview.Columns[i].FieldName;
                        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        item1.ShowInGroupColumnFooter = gridview.Columns[i];
                        item1.DisplayFormat = gridview.Columns[i].SummaryItem.DisplayFormat;
                        gridview.GroupSummary.Add(item1);
                    }
                    else
                    {
                        for (int j = gridview.GroupSummary.Count - 1; j >= 0; j--)
                        {
                            GridGroupSummaryItem temp = gridview.GroupSummary[j] as GridGroupSummaryItem;
                            if (gridview.GroupSummary[j].FieldName == gridview.Columns[i].FieldName && temp.ShowInGroupColumnFooter != null)
                            {
                                gridview.GroupSummary.RemoveAt(j);
                                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                                item1.FieldName = temp.FieldName;
                                item1.SummaryType = temp.SummaryType;
                                item1.ShowInGroupColumnFooter = gridview.Columns[i];
                                item1.DisplayFormat = gridview.Columns[i].SummaryItem.DisplayFormat;
                                gridview.GroupSummary.Add(temp);
                            }
                        }
                    }

                }
            }
        }
        void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView gvView = sender as GridView;

            if (e.Column.OptionsColumn.AllowEdit == true && gvView.Editable == true)
            {
                e.Appearance.BackColor = Color.FromArgb(218, 255, 218);
            }
        }

        void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            GridView gridview = sender as GridView;

            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            }
        }

        void gv_DoubleClick(object sender, EventArgs e)
        {
            MouseDownGridView = sender as GridView;

            if (MouseDownGridView.OptionsView.ShowGroupPanel == true)
            {
                Point pt = MouseDownGridView.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo gridHitInfo = MouseDownGridView.CalcHitInfo(pt);

                if (gridHitInfo.InColumn == true)
                {
                    gridHitInfo.Column.Group();
                }
            }
        }

        void gv_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                MouseDownGridView = sender as GridView;

                Point point = new Point(e.X, e.Y);
                GridHitInfo gridHitInfo = MouseDownGridView.CalcHitInfo(point);

                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        if (gridHitInfo.InColumn == true && gridHitInfo.Column.ColumnEdit != null && gridHitInfo.Column.ColumnEdit.EditorTypeName == "CheckEdit")
                        {
                            RepositoryItemCheckEdit checkedEdit = (RepositoryItemCheckEdit)gridHitInfo.Column.ColumnEdit;
                            DataTable dt = MouseDownGridView.GridControl.DataSource as DataTable;

                            object IsCheck = checkedEdit.ValueUnchecked;
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dt.Select(gridHitInfo.Column.FieldName + " = '" + IsCheck.NullString() + "'").Length > 0)
                                    IsCheck = checkedEdit.ValueChecked;

                                for (int i = 0; i < MouseDownGridView.RowCount; i++)
                                {
                                    MouseDownGridView.SetRowCellValue(i, gridHitInfo.Column.FieldName, IsCheck);
                                }
                            }
                        }
                        break;

                    case System.Windows.Forms.MouseButtons.Right:
                        if (gridHitInfo.InRowCell == true)
                        {
                            radialMenu1.ShowPopup(Cursor.Position);
                            radialMenu1.Expand();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void FormType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dialogResult = MsgBox.Show("YOU ARE WANT CLOSE SCREEN!!!", MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string OpenFile = string.Empty;

            GridFormatRule gridFormatRule = null;
            FormatConditionRuleTopBottom formatConditionRuleTopBottom = null;
            FormatConditionRuleAboveBelowAverage formatConditionRuleAboveBelowAverage = null;
            FormatConditionRuleDataBar formatConditionRuleDataBar = null;

            try
            {
                switch (e.Item.Tag.NullString())
                {
                    case "New":
                        break;
                    case "Edit":
                        break;
                    case "Print":
                        break;
                    case "XLS":
                        OpenFile = ShowExportFileMessage("xls");
                        if (OpenFile != null)
                        {
                            if (MouseDownGridView is BandedGridView)            
                            {
                                BandedGridView gvExportView = this.CopyBandedGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsPrint.PrintHeader = false;        
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.OptionsView.ShowColumnHeaders = true;          
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToXls(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                            else
                            {
                                GridView gvExportView = this.CopyGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToXls(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }

                        }
                        break;
                    case "XLSX":
                        OpenFile = ShowExportFileMessage("xlsx");
                        if (OpenFile != null)
                        {
                            if (MouseDownGridView is BandedGridView)            
                            {
                                BandedGridView gvExportView = this.CopyBandedGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsPrint.PrintHeader = false;        
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.OptionsView.ShowColumnHeaders = true;          
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToXlsx(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                            else
                            {
                                GridView gvExportView = this.CopyGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                MouseDownGridView.ExportToXlsx(OpenFile);
                                gvExportView.ExportToXlsx(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                        }
                        break;
                    case "HTML":
                        OpenFile = ShowExportFileMessage("html");
                        if (OpenFile != null)
                        {
                            if (MouseDownGridView is BandedGridView)            
                            {
                                BandedGridView gvExportView = this.CopyBandedGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsPrint.PrintHeader = false;        
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.OptionsView.ShowColumnHeaders = true;          
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToHtml(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                            else
                            {
                                GridView gvExportView = this.CopyGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToHtml(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                        }
                        break;
                    case "PDF":
                        OpenFile = ShowExportFileMessage("pdf");
                        if (OpenFile != null)
                        {
                            if (MouseDownGridView is BandedGridView)            
                            {
                                BandedGridView gvExportView = this.CopyBandedGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsPrint.PrintHeader = false;        
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.OptionsView.ShowColumnHeaders = true;          
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToPdf(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                            else
                            {
                                GridView gvExportView = this.CopyGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToPdf(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                        }
                        break;
                    case "TXT":
                        OpenFile = ShowExportFileMessage("txt");
                        if (OpenFile != null)
                        {
                            if (MouseDownGridView is BandedGridView)            
                            {
                                BandedGridView gvExportView = this.CopyBandedGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.OptionsPrint.PrintHeader = false;        
                                gvExportView.OptionsView.AllowCellMerge = false;
                                gvExportView.OptionsView.ShowColumnHeaders = true;          
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToText(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                            else
                            {
                                GridView gvExportView = this.CopyGridViewByValue(MouseDownGridView);

                                gvExportView.OptionsPrint.AutoWidth = false;
                                gvExportView.BestFitMaxRowCount = 40;
                                gvExportView.BestFitColumns();

                                gvExportView.ExportToText(OpenFile);
                                ShowOpenFileMessage(OpenFile);
                            }
                        }
                        break;
                    case "ALL_COPY":
                        MouseDownGridView.SelectAll();
                        MouseDownGridView.CopyToClipboard();
                        break;
                    case "LINE_COPY":
                        int[] rowIdxs = MouseDownGridView.GetSelectedRows();

                        StringBuilder sb = new StringBuilder();

                        foreach (int i in rowIdxs)
                        {
                            for (int colIdx = 0; colIdx < MouseDownGridView.Columns.Count; colIdx++)
                            {
                                sb.Append(String.Format("{0}\t", MouseDownGridView.GetDataRow(i)[colIdx]));
                            }
                            sb.Append(Environment.NewLine);
                        }

                        Clipboard.SetDataObject(sb.ToString());
                        break;
                    case "CELL_COPY":
                        Clipboard.SetDataObject(MouseDownGridView.GetFocusedValue());
                        break;
                    case "SUM":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                            MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                            MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                            MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                            MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                            MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                            MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            MouseDownGridView.FocusedColumn.Summary.Clear();
                            MouseDownGridView.FocusedColumn.Summary.Add(DevExpress.Data.SummaryItemType.Sum);
                            MouseDownGridView.FocusedColumn.SummaryItem.DisplayFormat = "SUM={0:n0}";
                        }
                        break;
                    case "AVG":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            MouseDownGridView.FocusedColumn.Summary.Clear();
                            MouseDownGridView.FocusedColumn.Summary.Add(DevExpress.Data.SummaryItemType.Average);
                            MouseDownGridView.FocusedColumn.SummaryItem.DisplayFormat = "AVG={0:n0}";
                        }
                        break;
                    case "MAX":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            MouseDownGridView.FocusedColumn.Summary.Clear();
                            MouseDownGridView.FocusedColumn.Summary.Add(DevExpress.Data.SummaryItemType.Max);
                            MouseDownGridView.FocusedColumn.SummaryItem.DisplayFormat = "MAX={0:n0}";
                        }
                        break;
                    case "MIN":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            MouseDownGridView.FocusedColumn.Summary.Clear();
                            MouseDownGridView.FocusedColumn.Summary.Add(DevExpress.Data.SummaryItemType.Min);
                            MouseDownGridView.FocusedColumn.SummaryItem.DisplayFormat = "MIN={0:n0}";
                        }
                        break;
                    case "COUNT":
                        MouseDownGridView.FocusedColumn.Summary.Clear();
                        MouseDownGridView.FocusedColumn.Summary.Add(DevExpress.Data.SummaryItemType.Count);
                        MouseDownGridView.FocusedColumn.SummaryItem.DisplayFormat = "CNT={0:n0}";
                        break;
                    case "CLEAR_S":
                        MouseDownGridView.FocusedColumn.Summary.Clear();
                        break;
                    case "TOP_TEN_ITEMS":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        (MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleTopBottom" ||
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleAboveBelowAverage"))
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleTopBottom = new FormatConditionRuleTopBottom();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleTopBottom.PredefinedName = "Bold Text";
                            formatConditionRuleTopBottom.Rank = 10;
                            formatConditionRuleTopBottom.RankType = FormatConditionValueType.Number;
                            formatConditionRuleTopBottom.TopBottom = FormatConditionTopBottomType.Top;
                            gridFormatRule.Rule = formatConditionRuleTopBottom;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BOTTOM_TEN_ITEMS":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        (MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleTopBottom" ||
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleAboveBelowAverage"))
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleTopBottom = new FormatConditionRuleTopBottom();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleTopBottom.PredefinedName = "Bold Text";
                            formatConditionRuleTopBottom.Rank = 10;
                            formatConditionRuleTopBottom.RankType = FormatConditionValueType.Number;
                            formatConditionRuleTopBottom.TopBottom = FormatConditionTopBottomType.Bottom;
                            gridFormatRule.Rule = formatConditionRuleTopBottom;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "TOP_TEN_PERCENT":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        (MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleTopBottom" ||
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleAboveBelowAverage"))
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleTopBottom = new FormatConditionRuleTopBottom();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleTopBottom.PredefinedName = "Bold Text";
                            formatConditionRuleTopBottom.Rank = 10;
                            formatConditionRuleTopBottom.RankType = FormatConditionValueType.Percent;
                            formatConditionRuleTopBottom.TopBottom = FormatConditionTopBottomType.Top;
                            gridFormatRule.Rule = formatConditionRuleTopBottom;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BOTTOM_TEN_PERCENT":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        (MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleTopBottom" ||
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleAboveBelowAverage"))
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleTopBottom = new FormatConditionRuleTopBottom();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleTopBottom.PredefinedName = "Bold Text";
                            formatConditionRuleTopBottom.Rank = 10;
                            formatConditionRuleTopBottom.RankType = FormatConditionValueType.Percent;
                            formatConditionRuleTopBottom.TopBottom = FormatConditionTopBottomType.Bottom;
                            gridFormatRule.Rule = formatConditionRuleTopBottom;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "ABOVE_AVERAGE":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        (MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleTopBottom" ||
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleAboveBelowAverage"))
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleAboveBelowAverage = new FormatConditionRuleAboveBelowAverage();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleAboveBelowAverage.PredefinedName = "Bold Text";
                            formatConditionRuleAboveBelowAverage.AverageType = FormatConditionAboveBelowType.Above;
                            gridFormatRule.Rule = formatConditionRuleAboveBelowAverage;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BLOW_AVERAGE":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        (MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleTopBottom" ||
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleAboveBelowAverage"))
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleAboveBelowAverage = new FormatConditionRuleAboveBelowAverage();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleAboveBelowAverage.PredefinedName = "Bold Text";
                            formatConditionRuleAboveBelowAverage.AverageType = FormatConditionAboveBelowType.Below;
                            gridFormatRule.Rule = formatConditionRuleAboveBelowAverage;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "CLEAR_B":
                        for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                        {
                            if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleAboveBelowAverage ||
                                    MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleTopBottom)
                            {
                                MouseDownGridView.FormatRules.RemoveAt(i);
                            }
                        }
                        break;
                    case "BAR_BLUE":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleDataBar")
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleDataBar = new FormatConditionRuleDataBar();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleDataBar.PredefinedName = "Blue Gradient";
                            gridFormatRule.Rule = formatConditionRuleDataBar;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BAR_RED":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleDataBar")
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleDataBar = new FormatConditionRuleDataBar();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleDataBar.PredefinedName = "Coral Gradient";
                            gridFormatRule.Rule = formatConditionRuleDataBar;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BAR_GREEN":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleDataBar")
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleDataBar = new FormatConditionRuleDataBar();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleDataBar.PredefinedName = "Green Gradient";
                            gridFormatRule.Rule = formatConditionRuleDataBar;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BAR_ORANGE":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleDataBar")
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleDataBar = new FormatConditionRuleDataBar();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleDataBar.PredefinedName = "Orange Gradient";
                            gridFormatRule.Rule = formatConditionRuleDataBar;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BAR_VIOLET":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleDataBar")
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleDataBar = new FormatConditionRuleDataBar();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleDataBar.PredefinedName = "Violet Gradient";
                            gridFormatRule.Rule = formatConditionRuleDataBar;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "BAR_PUPPLE":
                        if (MouseDownGridView.FocusedColumn.ColumnType == typeof(Decimal) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Double) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Single) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int32) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Int64) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(long) ||
                           MouseDownGridView.FocusedColumn.ColumnType == typeof(Byte))
                        {
                            for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                            {
                                if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                                {
                                    if (MouseDownGridView.FormatRules[i].Column == MouseDownGridView.FocusedColumn &&
                                        MouseDownGridView.FormatRules[i].Rule.GetType().Name == "FormatConditionRuleDataBar")
                                    {
                                        MouseDownGridView.FormatRules.RemoveAt(i);
                                    }
                                }
                            }
                            gridFormatRule = new GridFormatRule();
                            formatConditionRuleDataBar = new FormatConditionRuleDataBar();
                            gridFormatRule.Column = MouseDownGridView.FocusedColumn;
                            formatConditionRuleDataBar.PredefinedName = "Raspberry Gradient";
                            gridFormatRule.Rule = formatConditionRuleDataBar;
                            MouseDownGridView.FormatRules.Add(gridFormatRule);
                        }
                        break;
                    case "CLEAR_BAR":
                        for (int i = MouseDownGridView.FormatRules.Count - 1; i >= 0; i--)
                        {
                            if (MouseDownGridView.FormatRules[i].Rule is FormatConditionRuleDataBar)
                            {
                                MouseDownGridView.FormatRules.RemoveAt(i);
                            }
                        }
                        break;
                    default:
                        break;
                }

                radialMenu1.Collapse(true);
            }
            catch
            {
            }
        }


        private Control[] GetAllControls(Control containerControl)
        {
            List<Control> allControls = new List<Control>();
            Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
            queue.Enqueue(containerControl.Controls);

            Task task = new Task(() =>
            {
                while (queue.Count > 0)
                {
                    Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();
                    if (controls == null || controls.Count == 0) continue;

                    foreach (Control control in controls)
                    {
                        if (control is GridControl)
                        {
                            BindInfo.Add(control.Name, new Dictionary<string, Control>());
                        }

                        allControls.Add(control);
                        queue.Enqueue(control.Controls);
                    }
                }
            });

            task.Start();
            task.Wait();

            return allControls.ToArray();
        }



        public string ShowExportFileMessage(string FileExtension)
        {

            saveFileDialog.RestoreDirectory = false;
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + FileExtension;
            saveFileDialog.Filter = string.Format("files (*.{0})|*.{0}", FileExtension);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return saveFileDialog.FileName;
                }
                catch  
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public void ShowOpenFileMessage(string fileName)
        {
            this.TopMost = false;
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    MessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.", "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.TopMost = true;
        }



        private GridView CopyGridViewByValue(GridView _FromView)
        {
            GridControl gcToCtrl = new GridControl();
            gcToCtrl.Parent = this;
            gcToCtrl.DataSource = (_FromView.GridControl.DataSource as DataTable).Copy();

            GridView gvToView = new GridView(gcToCtrl);
            gcToCtrl.MainView = gvToView;
            gcToCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvToView });

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                _FromView.SaveLayoutToStream(ms, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                gvToView.RestoreLayoutFromStream(ms, DevExpress.Utils.OptionsLayoutBase.FullLayout);
            }

            return gvToView;
        }

        private BandedGridView CopyBandedGridViewByValue(GridView _FromView)
        {
            GridControl gcToCtrl = new GridControl();
            gcToCtrl.Parent = this;
            gcToCtrl.DataSource = (_FromView.GridControl.DataSource as DataTable).Copy();

            BandedGridView gvToView = new BandedGridView(gcToCtrl);
            gcToCtrl.MainView = gvToView;
            gcToCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvToView });

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                (_FromView as BandedGridView).SaveLayoutToStream(ms, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                gvToView.RestoreLayoutFromStream(ms, DevExpress.Utils.OptionsLayoutBase.FullLayout);
            }

            return gvToView;
        }



        private class GridLookUpPopupFormSizeHelper
        {
            private GridLookUpEdit mGridLookUpEdit = null;

            public GridLookUpPopupFormSizeHelper(GridLookUpEdit gridLookUpEdit)
            {
                mGridLookUpEdit = gridLookUpEdit;
                mGridLookUpEdit.Properties.View.DataSourceChanged += new EventHandler(gridLookUpEditView_DataSourceChanged);
            }

            private void gridLookUpEditView_DataSourceChanged(object sender, EventArgs e)
            {
                GridViewInfo vInfo = mGridLookUpEdit.Properties.View.GetViewInfo() as GridViewInfo;
                mGridLookUpEdit.Properties.PopupFormSize = new Size(mGridLookUpEdit.Properties.PopupFormSize.Width, vInfo.CalcRealViewHeight(new Rectangle(0, 0, 500, 300)));
            }
        }



    }
}
