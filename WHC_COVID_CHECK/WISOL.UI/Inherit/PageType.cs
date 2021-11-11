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
using DevExpress.XtraTreeList;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.BindDatas;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;
using Wisol.MES.Interfaces;

namespace Wisol.MES.Inherit
{
    [ToolboxItem(false)]
    public partial class PageType : XtraUserControl, IButton
    {
        public const string A_FROM_DATE = "A_FROM_DATE";
        public const string A_LABEL_DATE = "A_LABEL_DATE";
        public const string A_LANG = "A_LANG";
        public const string A_LOT_QTY = "A_LOT_QTY";
        public const string A_MAKER_MAT_ID = "A_MAKER_MAT_ID";
        public const string A_MAKER = "A_MAKER";
        public const string A_MAT_ID = "A_MAT_ID";
        public const string A_MSL_FLAG = "A_MSL_FLAG";
        public const string A_PACKING_DATE = "A_PACKING_DATE";
        public const string A_PLANT = "A_PLANT";
        public const string A_QTY = "A_QTY";
        public const string A_REVISION = "A_REVISION";
        public const string A_SIP_CONFIG = "A_SIP_CONFIG";
        public const string A_TO_DATE = "A_TO_DATE";
        public const string A_TRAN_USER_ID = "A_TRAN_USER_ID";
        public const string A_UNIT = "A_UNIT";
        public const string A_VENDOR_LOT_ID = "A_VENDOR_LOT_ID";
        public const string A_WH_CODE = "A_WH_CODE";
        public const string A_XML = "A_XML";

        public const string WH_CODE = "WH_CODE";
        public const string WH_DESC = "WH_DESC";
        public const string MAT_ID = "MAT_ID";
        public const string UNIT = "UNIT";
        public const string COMMCODE = "COMMCODE";
        public const string COMMNAME = "COMMNAME";
        public const string ROUND_VALUE = "ROUND_VALUE";
        public const string VENDOR_CODE = "VENDOR_CODE";
        public const string MAKER = "MAKER";
        public const string MAT_OUT_GUBUN = "MAT_OUT_GUBUN";
        public const string MSL = "MSL";
        public const string STOCK_CHG_QTY = "STOCK_CHG_QTY";
        public const string VENDOR_LOT_ID = "VENDOR_LOT_ID";
        public const string LABEL_DATE = "LABEL_DATE";
        public const string PACKING_DATE = "PACKING_DATE";
        public const string VENDOR_MAT_ID = "VENDOR_MAT_ID";
        public const string SIP_CONFIG = "SIP_CONFIG";

        protected Dictionary<string, Dictionary<string, Control>> BindInfo = new Dictionary<string, Dictionary<string, Control>>();     
        private GridView MouseDownGridView;           
        private string MouseFooterColumn = string.Empty;

        public DBAccess m_DBaccess = null;
        public BindData m_BindData = null;
        public ResultDB m_ResultDB = null;

        public object MainID { get; set; }

        public string ModuleCode { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public string ModuleAuth { get; set; } = string.Empty;
        public PageType()
        {
            InitializeComponent();

            try
            {
                var serviceinfo = new ServerInfo
                {
                    ServerIp = Consts.SERVICE_INFO.ServiceIp,
                    ClientIp = Consts.LOCAL_SYSTEM_INFO.IpAddress,
                    ServicePort = Converter.ParseValue<int>(Consts.SERVICE_INFO.ServicePort),
                    ServiceID = Consts.SERVICE_INFO.UserId,
                    ServicePassword = Consts.SERVICE_INFO.Password
                };

                m_DBaccess = new DBAccess(serviceinfo, Consts.USER_INFO.Id);
                m_BindData = new BindData(serviceinfo, Consts.USER_INFO.Language, Consts.GLOSSARY.Copy(), Consts.USER_INFO.Id);
                m_ResultDB = new ResultDB();
            }
            catch
            {
            }
        }
        private void PageType_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            //string value = this.ModuleCode.Substring(this.ModuleCode.LastIndexOf('>') + 1).Trim();
            string value = this.ModuleName;
            m_DBaccess.ExcuteProc(
                "PKG_COMM.PUT_LOG",
                new string[] { "A_PLANT", "A_USER_ID", "A_FORM_CODE", "A_DEPARTMENT" },
                new string[] { Consts.PLANT, Consts.USER_INFO.Id, value, Consts.DEPARTMENT }
                );

            ControlsInitialize();
            if (Consts.USER_INFO.Id.ToUpper() != "H2002001" && Consts.USER_INFO.Id.ToUpper() != "231017")
            {
                myTitlePanel.Visible = false;
            }
            else
            {
                myTitlePanel.Visible = true;
            }
        }

        private void ControlsInitialize()
        {
            var controls = GetAllControls(this, true);
            int tabIndex = 0;

            foreach (var control in controls)
            {
                if (control.Name == String.Empty) continue;

                var baseControl = control as BaseControl;
                if (baseControl != null) { baseControl.ToolTip = baseControl.Text.Translation("KOR"); }    

                if (control is LabelControl || control is SimpleButton || control is GroupControl || control is CheckEdit || control is TabPage)
                {
                    if (ModuleAuth == "R" && control is SimpleButton)
                    {
                        SimpleButton btn = control as SimpleButton;
                        btn.Enabled = false;
                    }
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
                    spinEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
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

                        gv.RowHeight = 21;

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

                        if (this.Name.ToString() != "PRODCT007" || gv.Name.ToString() != "gvToolList")
                        {
                            gv.MouseDown += new MouseEventHandler(gv_MouseDown);
                        }

                        gv.DoubleClick += new EventHandler(gv_DoubleClick);
                        gv.EndGrouping += new EventHandler(gv_EndGrouping);
                        gv.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gv_CustomDrawRowIndicator);
                        gv.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gv_CustomDrawCell);
                    }
                }

                if (control is TreeList)
                {
                    TreeList tl = control as TreeList;

                    tl.IndicatorWidth = 50;

                    tl.Appearance.FocusedCell.BackColor = Color.FromArgb(205, 230, 247);
                    tl.Appearance.SelectedRow.BackColor = Color.FromArgb(100, 140, 145, 255);
                    tl.Appearance.FocusedRow.BackColor = Color.FromArgb(100, 140, 145, 255);

                    tl.BeginSort();
                    tl.EndSort();
                    tl.OptionsView.ShowAutoFilterRow = false;
                    tl.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(tl_CustomDrawNodeCell);
                }

                if (control is GridLookUpEdit)
                {
                    GridLookUpEdit gridLookUpEdit = control as GridLookUpEdit;

                    gridLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    gridLookUpEdit.Properties.View.OptionsView.ShowAutoFilterRow = true;

                    GridLookUpPopupFormSizeHelper glePopSizeHelper = new GridLookUpPopupFormSizeHelper(gridLookUpEdit);
                }



                if (control is XDateEdit xdateEdit)
                {
                    xdateEdit.DateTime = DateTime.Now;
                    xdateEdit.Properties.DisplayFormat.FormatString = "y";
                    xdateEdit.Properties.DisplayFormat.FormatType = FormatType.DateTime;
                    xdateEdit.Properties.EditFormat.FormatString = "y";
                    xdateEdit.Properties.EditFormat.FormatType = FormatType.DateTime;
                    xdateEdit.Properties.Mask.EditMask = "y";
                    xdateEdit.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearView;
                    xdateEdit.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                }
                else if (control is DateEdit dateEdit)
                {
                    dateEdit.Properties.AllowNullInput = DefaultBoolean.False;
                    dateEdit.Properties.DisplayFormat.FormatString = "d";
                    dateEdit.Properties.DisplayFormat.FormatType = FormatType.DateTime;
                    dateEdit.Properties.EditFormat.FormatString = "d";
                    dateEdit.Properties.EditFormat.FormatType = FormatType.DateTime;
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
                if (control is SimpleButton || control is CheckEdit || control is RadioGroup || control is GridLookUpEdit || control is DateEdit || control is TextEdit || control is SpinEdit || control is MemoEdit || control is TimeEdit)
                {
                    control.TabIndex = tabIndex;
                    tabIndex++;
                }
                else
                {
                    control.TabIndex = tabIndex * 100;
                }
                if (control is TextEdit)
                {
                    TextEdit textEdit = control as TextEdit;
                    textEdit.GotFocus += textEdit_GotFocus;
                }
                if (control is SpinEdit)
                {
                    SpinEdit spinEdit = control as SpinEdit;
                    spinEdit.GotFocus += spinEdit_GotFocus;
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

        void textEdit_GotFocus(object sender, EventArgs e)
        {
            try
            {
                TextEdit edit = sender as TextEdit;
                edit.SelectAll();
            }
            catch
            {

            }
        }

        void spinEdit_GotFocus(object sender, EventArgs e)
        {
            try
            {
                TextEdit edit = sender as TextEdit;
                edit.SelectAll();
            }
            catch
            {

            }
        }

        void tl_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeList tl = sender as TreeList;

            if (e.Column.OptionsColumn.AllowEdit == true && tl.OptionsBehavior.Editable == true)
            {
                e.Appearance.BackColor = Color.FromArgb(218, 255, 218);
            }
        }
        public virtual void Form_Show()
        {
            Text = Text.Translation();
            //myPath.Text = "＊ " + ModuleCode;
            myPath.Text =  ModuleCode;
            //this.myPath.Visible = false;
        }



        public virtual void InitializePage()
        {

        }
        public virtual void SearchPage()
        {

        }
        public virtual void SaveCodes()
        {
            
        }

        public virtual void LoadCodes()
        {

        }

        public virtual void LoadLayout()
        {

        }

        public virtual void SaveLayout()
        {

        }
        public virtual void ClosePage()
        {

        }
        public virtual void CloseAll()
        {

        }
        public virtual void OpenChart()
        {

        }
        public virtual void OpenManual()
        {
            try
            {
                m_ResultDB = m_DBaccess.ExcuteProc("PKG_COMM.GET_MANUAL",
                        new string[] { "A_PLANT",
                            "A_FORM_CODE"
                        },
                        new string[] { Consts.PLANT,
                            ModuleCode
                        }
                        );
                if (m_ResultDB.ReturnInt == 0)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.FileName = m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["MANUAL_FILE"].NullString();
                        sfd.Title = "Save Excel File";
                        DialogResult result = sfd.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            Wisol.DataAcess.FileAccess fileAccess = new Wisol.DataAcess.FileAccess(Consts.SERVICE_INFO.ServiceIp);
                            FileObject fileObject = fileAccess.GetFile("MANUAL/" + m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["MANUAL_FILE"].NullString());
                            File.WriteAllBytes(sfd.FileName, fileObject.FileContent);
                        }
                    }
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
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
        void View_DataSourceChanged(object sender, EventArgs e)
        {
            GridViewInfo vInfo = (sender as GridLookUpEdit).Properties.View.GetViewInfo() as GridViewInfo;
            (sender as GridLookUpEdit).Properties.PopupFormSize = new Size((sender as GridLookUpEdit).Properties.PopupFormSize.Width, vInfo.CalcRealViewHeight(new Rectangle(0, 0, 500, 300)));
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
                if (gridHitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowFooter)
                {
                    MouseFooterColumn = gridHitInfo.Column.FieldName;
                }
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
                            myRadialMenu.ShowPopup(Cursor.Position);
                            myRadialMenu.Expand();

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        public void AddGrid(GridControl gc)
        {
            try
            {
                GridView gv = gc.DefaultView as GridView;

                gv.RowHeight = 21;

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
            catch
            {

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
                                gvExportView.OptionsPrint.PrintHeader = true;        
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

                myRadialMenu.Collapse(true);
            }
            catch
            {
            }
        }



        private Control[] GetAllControls(Control containerControl, bool addFlag)
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
                            if (addFlag)
                            {
                                BindInfo.Add(control.Name, new Dictionary<string, Control>());
                            }
                        }

                        allControls.Add(control);
                        queue.Enqueue(control.Controls);
                    }
                }
            });
            for (int i = 0; i < allControls.Count - 1; i++)
            {
                for (int j = 0; j < allControls.Count - 1; j++)
                {
                    if (allControls[j].Location.Y > allControls[j + 1].Location.Y)
                    {
                        Control temp = allControls[j];
                        allControls[j] = allControls[j + 1];
                        allControls[j + 1] = temp;
                    }
                }
            }
            for (int i = 0; i < allControls.Count - 1; i++)
            {
                for (int j = 0; j < allControls.Count - 1; j++)
                {
                    if (allControls[j].Location.Y == allControls[j + 1].Location.Y && allControls[j].Location.X > allControls[j].Location.X)
                    {
                        Control temp = allControls[j];
                        allControls[j] = allControls[j + 1];
                        allControls[j + 1] = temp;
                    }
                }
            }


            task.Start();
            task.Wait();

            return allControls.ToArray();
        }
        public string ShowExportFileMessage(string FileExtension)
        {

            mySaveFileDialog.RestoreDirectory = false;
            mySaveFileDialog.FileName = ModuleName.Replace("/", "_") + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + FileExtension;
            mySaveFileDialog.Filter = string.Format("files (*.{0})|*.{0}", FileExtension);

            if (mySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return mySaveFileDialog.FileName;
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


        protected DialogResult MessageBoxShow(string text, MsgType type)
        {
            if (ParentForm is MainForm)
            {
                var form = ParentForm as MainForm;
                if (form != null) form.SplashScreenToggle(SplashScreenStatus.Off);
            }
            return MsgBox.Show(text, type);
        }

        protected bool GetExcelFileName(ref string @ref)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel Files(*.xls;*.xlsx;*xlsm)|*.xls;*.xlsx;*.xlsm";
                    dialog.RestoreDirectory = true;
                    dialog.Title = "Open Excel File";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        @ref = dialog.FileName;
                        return true;
                    }
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
            return false;
        }


        public virtual void InitializeControl()
        {
            try
            {
                InitializeControl(Controls);
            }
            catch (Exception err) { MsgBox.Show(err.Message, MsgType.Error); }
        }

        public void InitializeControl(ControlCollection controls)
        {
            try
            {
                foreach (Control control in controls)
                {
                    if (control is BaseEdit baseEdit)
                    {
                        baseEdit.EditValue = "";
                    }

                    if (control.HasChildren)
                    {
                        InitializeControl(control.Controls);
                    }
                }
            }
            catch { throw; }
        }

        public virtual void ReloadData() { }
    }
}
