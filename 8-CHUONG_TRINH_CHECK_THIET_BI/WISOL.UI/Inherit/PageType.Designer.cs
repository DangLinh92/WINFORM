namespace Wisol.MES.Inherit
{
    partial class PageType
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageType));
            this.barSubItemExport = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemXls = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemXlsx = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHtml = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPdf = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemTxt = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemCopy = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemAllCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLineCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCellCopy = new DevExpress.XtraBars.BarButtonItem();
            this.myBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.myImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItemNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barLargeButtonItem1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSubItemSummery = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemSummery = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAverage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMax = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMin = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCount = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClearSummery = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemBold = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemTopTenItems = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemBottomTenItems = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemTopTenPercent = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemBottomTenPercent = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAboveAverage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemBlowAverage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClearBlod = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemBar = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemBlue = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRed = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGreen = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOrange = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemViolet = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPupple = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemBarClear = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.myPath = new DevExpress.XtraEditors.TextEdit();
            this.myTitlePanel = new DevExpress.XtraEditors.PanelControl();
            this.mySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.myRadialMenu = new DevExpress.XtraBars.Ribbon.RadialMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.myBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTitlePanel)).BeginInit();
            this.myTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myRadialMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // barSubItemExport
            // 
            this.barSubItemExport.Caption = "EXPORT";
            this.barSubItemExport.Id = 0;
            this.barSubItemExport.ImageOptions.ImageIndex = 0;
            this.barSubItemExport.ImageOptions.LargeImageIndex = 0;
            this.barSubItemExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemXls),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemXlsx),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemHtml),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPdf),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemTxt)});
            this.barSubItemExport.Name = "barSubItemExport";
            this.barSubItemExport.Tag = "EXPORT";
            // 
            // barButtonItemXls
            // 
            this.barButtonItemXls.Caption = "XLS";
            this.barButtonItemXls.Id = 6;
            this.barButtonItemXls.ImageOptions.ImageIndex = 2;
            this.barButtonItemXls.ImageOptions.LargeImageIndex = 2;
            this.barButtonItemXls.Name = "barButtonItemXls";
            this.barButtonItemXls.Tag = "XLS";
            this.barButtonItemXls.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemXlsx
            // 
            this.barButtonItemXlsx.Caption = "XLSX";
            this.barButtonItemXlsx.Id = 7;
            this.barButtonItemXlsx.ImageOptions.ImageIndex = 3;
            this.barButtonItemXlsx.ImageOptions.LargeImageIndex = 3;
            this.barButtonItemXlsx.Name = "barButtonItemXlsx";
            this.barButtonItemXlsx.Tag = "XLSX";
            this.barButtonItemXlsx.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemHtml
            // 
            this.barButtonItemHtml.Caption = "HTML";
            this.barButtonItemHtml.Id = 8;
            this.barButtonItemHtml.ImageOptions.ImageIndex = 4;
            this.barButtonItemHtml.ImageOptions.LargeImageIndex = 4;
            this.barButtonItemHtml.Name = "barButtonItemHtml";
            this.barButtonItemHtml.Tag = "HTML";
            this.barButtonItemHtml.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemPdf
            // 
            this.barButtonItemPdf.Caption = "PDF";
            this.barButtonItemPdf.Id = 9;
            this.barButtonItemPdf.ImageOptions.ImageIndex = 5;
            this.barButtonItemPdf.ImageOptions.LargeImageIndex = 5;
            this.barButtonItemPdf.Name = "barButtonItemPdf";
            this.barButtonItemPdf.Tag = "PDF";
            this.barButtonItemPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemTxt
            // 
            this.barButtonItemTxt.Caption = "TXT";
            this.barButtonItemTxt.Id = 10;
            this.barButtonItemTxt.ImageOptions.ImageIndex = 6;
            this.barButtonItemTxt.ImageOptions.LargeImageIndex = 6;
            this.barButtonItemTxt.Name = "barButtonItemTxt";
            this.barButtonItemTxt.Tag = "TXT";
            this.barButtonItemTxt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItemCopy
            // 
            this.barSubItemCopy.Caption = "COPY";
            this.barSubItemCopy.Id = 1;
            this.barSubItemCopy.ImageOptions.ImageIndex = 1;
            this.barSubItemCopy.ImageOptions.LargeImageIndex = 1;
            this.barSubItemCopy.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAllCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLineCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCellCopy)});
            this.barSubItemCopy.Name = "barSubItemCopy";
            this.barSubItemCopy.Tag = "COPY";
            // 
            // barButtonItemAllCopy
            // 
            this.barButtonItemAllCopy.Caption = "ALL_COPY";
            this.barButtonItemAllCopy.Id = 11;
            this.barButtonItemAllCopy.ImageOptions.ImageIndex = 7;
            this.barButtonItemAllCopy.ImageOptions.LargeImageIndex = 7;
            this.barButtonItemAllCopy.Name = "barButtonItemAllCopy";
            this.barButtonItemAllCopy.Tag = "ALL_COPY";
            this.barButtonItemAllCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemLineCopy
            // 
            this.barButtonItemLineCopy.Caption = "LINE_COPY";
            this.barButtonItemLineCopy.Id = 12;
            this.barButtonItemLineCopy.ImageOptions.ImageIndex = 8;
            this.barButtonItemLineCopy.ImageOptions.LargeImageIndex = 8;
            this.barButtonItemLineCopy.Name = "barButtonItemLineCopy";
            this.barButtonItemLineCopy.Tag = "LINE_COPY";
            this.barButtonItemLineCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemCellCopy
            // 
            this.barButtonItemCellCopy.Caption = "CELL_COPY";
            this.barButtonItemCellCopy.Id = 13;
            this.barButtonItemCellCopy.ImageOptions.ImageIndex = 9;
            this.barButtonItemCellCopy.ImageOptions.LargeImageIndex = 9;
            this.barButtonItemCellCopy.Name = "barButtonItemCellCopy";
            this.barButtonItemCellCopy.Tag = "CELL_COPY";
            this.barButtonItemCellCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // myBarManager
            // 
            this.myBarManager.DockControls.Add(this.barDockControlTop);
            this.myBarManager.DockControls.Add(this.barDockControlBottom);
            this.myBarManager.DockControls.Add(this.barDockControlLeft);
            this.myBarManager.DockControls.Add(this.barDockControlRight);
            this.myBarManager.DockWindowTabFont = new System.Drawing.Font("Tahoma", 9F);
            this.myBarManager.Form = this;
            this.myBarManager.Images = this.myImageCollection;
            this.myBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemNew,
            this.barButtonItemEdit,
            this.barSubItemExport,
            this.barSubItemCopy,
            this.barButtonItemPrint,
            this.barButtonItemXls,
            this.barButtonItemXlsx,
            this.barButtonItemHtml,
            this.barButtonItemPdf,
            this.barButtonItemTxt,
            this.barButtonItemAllCopy,
            this.barButtonItemLineCopy,
            this.barButtonItemCellCopy,
            this.barLargeButtonItem1,
            this.barSubItemSummery,
            this.barButtonItemSummery,
            this.barButtonItemAverage,
            this.barButtonItemMax,
            this.barButtonItemMin,
            this.barButtonItemCount,
            this.barSubItem1,
            this.barSubItem2,
            this.barButtonItemClearSummery,
            this.barButtonItem1,
            this.barSubItemBold,
            this.barButtonItemTopTenItems,
            this.barButtonItemBottomTenItems,
            this.barButtonItemTopTenPercent,
            this.barButtonItemBottomTenPercent,
            this.barButtonItemAboveAverage,
            this.barButtonItemBlowAverage,
            this.barButtonItemClearBlod,
            this.barSubItemBar,
            this.barButtonItemBlue,
            this.barButtonItemRed,
            this.barButtonItemGreen,
            this.barButtonItemOrange,
            this.barButtonItemViolet,
            this.barButtonItemPupple,
            this.barButtonItemBarClear,
            this.barButtonItem2});
            this.myBarManager.LargeImages = this.myImageCollection;
            this.myBarManager.MaxItemId = 54;
            this.myBarManager.ShowCloseButton = true;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.myBarManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1366, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 768);
            this.barDockControlBottom.Manager = this.myBarManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1366, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.myBarManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 768);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1366, 0);
            this.barDockControlRight.Manager = this.myBarManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 768);
            // 
            // myImageCollection
            // 
            this.myImageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.myImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("myImageCollection.ImageStream")));
            this.myImageCollection.InsertGalleryImage("export_32x32.png", "images/export/export_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/export_32x32.png"), 0);
            this.myImageCollection.Images.SetKeyName(0, "export_32x32.png");
            this.myImageCollection.InsertGalleryImage("copy_32x32.png", "images/edit/copy_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/edit/copy_32x32.png"), 1);
            this.myImageCollection.Images.SetKeyName(1, "copy_32x32.png");
            this.myImageCollection.InsertGalleryImage("exporttoxls_32x32.png", "images/export/exporttoxls_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxls_32x32.png"), 2);
            this.myImageCollection.Images.SetKeyName(2, "exporttoxls_32x32.png");
            this.myImageCollection.InsertGalleryImage("exporttoxlsx_32x32.png", "images/export/exporttoxlsx_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxlsx_32x32.png"), 3);
            this.myImageCollection.Images.SetKeyName(3, "exporttoxlsx_32x32.png");
            this.myImageCollection.InsertGalleryImage("exporttohtml_32x32.png", "images/export/exporttohtml_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttohtml_32x32.png"), 4);
            this.myImageCollection.Images.SetKeyName(4, "exporttohtml_32x32.png");
            this.myImageCollection.InsertGalleryImage("exporttopdf_32x32.png", "images/export/exporttopdf_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_32x32.png"), 5);
            this.myImageCollection.Images.SetKeyName(5, "exporttopdf_32x32.png");
            this.myImageCollection.InsertGalleryImage("exporttotxt_32x32.png", "images/export/exporttotxt_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttotxt_32x32.png"), 6);
            this.myImageCollection.Images.SetKeyName(6, "exporttotxt_32x32.png");
            this.myImageCollection.Images.SetKeyName(7, "SelectAll_32x32.png");
            this.myImageCollection.Images.SetKeyName(8, "SelectRow_32x32.png");
            this.myImageCollection.Images.SetKeyName(9, "SelectCell_32x32.png");
            this.myImageCollection.InsertGalleryImage("summary_32x32.png", "images/data/summary_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/data/summary_32x32.png"), 10);
            this.myImageCollection.Images.SetKeyName(10, "summary_32x32.png");
            this.myImageCollection.Images.SetKeyName(11, "SUM.png");
            this.myImageCollection.Images.SetKeyName(12, "AVG.png");
            this.myImageCollection.Images.SetKeyName(13, "MAX.png");
            this.myImageCollection.Images.SetKeyName(14, "MIN.png");
            this.myImageCollection.Images.SetKeyName(15, "COUNT.png");
            this.myImageCollection.InsertGalleryImage("clearformatting_32x32.png", "images/actions/clearformatting_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/clearformatting_32x32.png"), 16);
            this.myImageCollection.Images.SetKeyName(16, "clearformatting_32x32.png");
            this.myImageCollection.Images.SetKeyName(17, "Top10Items_32x32.png");
            this.myImageCollection.Images.SetKeyName(18, "Bottom10Items_32x32.png");
            this.myImageCollection.Images.SetKeyName(19, "Top10Percent_32x32.png");
            this.myImageCollection.Images.SetKeyName(20, "Bottom10Percent_32x32.png");
            this.myImageCollection.Images.SetKeyName(21, "AboveAverage_32x32.png");
            this.myImageCollection.Images.SetKeyName(22, "BelowAverage_32x32.png");
            this.myImageCollection.Images.SetKeyName(23, "GradientLightBlueDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(24, "GradientBlueDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(25, "GradientCoralDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(26, "GradientGreenDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(27, "GradientOrangeDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(28, "GradientVioletDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(29, "GradientRaspberryDataBar_32x32.png");
            this.myImageCollection.Images.SetKeyName(30, "1452168531_bold.png");
            // 
            // barButtonItemNew
            // 
            this.barButtonItemNew.Caption = "New";
            this.barButtonItemNew.Id = 2;
            this.barButtonItemNew.ImageOptions.ImageIndex = 0;
            this.barButtonItemNew.Name = "barButtonItemNew";
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "Edit";
            this.barButtonItemEdit.Id = 3;
            this.barButtonItemEdit.ImageOptions.ImageIndex = 1;
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            // 
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.Caption = "Print";
            this.barButtonItemPrint.Id = 5;
            this.barButtonItemPrint.ImageOptions.ImageIndex = 4;
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            // 
            // barLargeButtonItem1
            // 
            this.barLargeButtonItem1.Caption = "Export";
            this.barLargeButtonItem1.Id = 14;
            this.barLargeButtonItem1.Name = "barLargeButtonItem1";
            // 
            // barSubItemSummery
            // 
            this.barSubItemSummery.Caption = "SUMMERY";
            this.barSubItemSummery.Id = 16;
            this.barSubItemSummery.ImageOptions.ImageIndex = 10;
            this.barSubItemSummery.ImageOptions.LargeImageIndex = 10;
            this.barSubItemSummery.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSummery),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAverage),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemMax),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemMin),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCount),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemClearSummery)});
            this.barSubItemSummery.Name = "barSubItemSummery";
            this.barSubItemSummery.Tag = "SUMMERY";
            // 
            // barButtonItemSummery
            // 
            this.barButtonItemSummery.Caption = "SUM";
            this.barButtonItemSummery.Id = 17;
            this.barButtonItemSummery.ImageOptions.ImageIndex = 11;
            this.barButtonItemSummery.ImageOptions.LargeImageIndex = 11;
            this.barButtonItemSummery.Name = "barButtonItemSummery";
            this.barButtonItemSummery.Tag = "SUM";
            this.barButtonItemSummery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemAverage
            // 
            this.barButtonItemAverage.Caption = "AVG";
            this.barButtonItemAverage.Id = 18;
            this.barButtonItemAverage.ImageOptions.ImageIndex = 12;
            this.barButtonItemAverage.ImageOptions.LargeImageIndex = 12;
            this.barButtonItemAverage.Name = "barButtonItemAverage";
            this.barButtonItemAverage.Tag = "AVG";
            this.barButtonItemAverage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemMax
            // 
            this.barButtonItemMax.Caption = "MAX";
            this.barButtonItemMax.Id = 19;
            this.barButtonItemMax.ImageOptions.ImageIndex = 13;
            this.barButtonItemMax.ImageOptions.LargeImageIndex = 13;
            this.barButtonItemMax.Name = "barButtonItemMax";
            this.barButtonItemMax.Tag = "MAX";
            this.barButtonItemMax.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemMin
            // 
            this.barButtonItemMin.Caption = "MIN";
            this.barButtonItemMin.Id = 20;
            this.barButtonItemMin.ImageOptions.ImageIndex = 14;
            this.barButtonItemMin.ImageOptions.LargeImageIndex = 14;
            this.barButtonItemMin.Name = "barButtonItemMin";
            this.barButtonItemMin.Tag = "MIN";
            this.barButtonItemMin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemCount
            // 
            this.barButtonItemCount.Caption = "COUNT";
            this.barButtonItemCount.Id = 21;
            this.barButtonItemCount.ImageOptions.ImageIndex = 15;
            this.barButtonItemCount.ImageOptions.LargeImageIndex = 15;
            this.barButtonItemCount.Name = "barButtonItemCount";
            this.barButtonItemCount.Tag = "COUNT";
            this.barButtonItemCount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemClearSummery
            // 
            this.barButtonItemClearSummery.Caption = "CLEAR";
            this.barButtonItemClearSummery.Id = 34;
            this.barButtonItemClearSummery.ImageOptions.ImageIndex = 16;
            this.barButtonItemClearSummery.ImageOptions.LargeImageIndex = 16;
            this.barButtonItemClearSummery.Name = "barButtonItemClearSummery";
            this.barButtonItemClearSummery.Tag = "CLEAR_S";
            this.barButtonItemClearSummery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "INSERT";
            this.barSubItem1.Id = 27;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "CHANGE";
            this.barSubItem2.Id = 28;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "BOLD";
            this.barButtonItem1.Id = 35;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItemBold
            // 
            this.barSubItemBold.Caption = "BOLD";
            this.barSubItemBold.Id = 37;
            this.barSubItemBold.ImageOptions.ImageIndex = 30;
            this.barSubItemBold.ImageOptions.LargeImageIndex = 30;
            this.barSubItemBold.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemTopTenItems),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBottomTenItems),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemTopTenPercent),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBottomTenPercent),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAboveAverage),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBlowAverage),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemClearBlod)});
            this.barSubItemBold.Name = "barSubItemBold";
            // 
            // barButtonItemTopTenItems
            // 
            this.barButtonItemTopTenItems.Caption = "TOP_TEN_ITEMS";
            this.barButtonItemTopTenItems.Id = 38;
            this.barButtonItemTopTenItems.ImageOptions.ImageIndex = 17;
            this.barButtonItemTopTenItems.ImageOptions.LargeImageIndex = 17;
            this.barButtonItemTopTenItems.Name = "barButtonItemTopTenItems";
            this.barButtonItemTopTenItems.Tag = "TOP_TEN_ITEMS";
            this.barButtonItemTopTenItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBottomTenItems
            // 
            this.barButtonItemBottomTenItems.Caption = "BOTTOM_TEN_ITEMS";
            this.barButtonItemBottomTenItems.Id = 39;
            this.barButtonItemBottomTenItems.ImageOptions.ImageIndex = 18;
            this.barButtonItemBottomTenItems.ImageOptions.LargeImageIndex = 18;
            this.barButtonItemBottomTenItems.Name = "barButtonItemBottomTenItems";
            this.barButtonItemBottomTenItems.Tag = "BOTTOM_TEN_ITEMS";
            this.barButtonItemBottomTenItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemTopTenPercent
            // 
            this.barButtonItemTopTenPercent.Caption = "TOP_TEN_PERCENT";
            this.barButtonItemTopTenPercent.Id = 40;
            this.barButtonItemTopTenPercent.ImageOptions.ImageIndex = 19;
            this.barButtonItemTopTenPercent.ImageOptions.LargeImageIndex = 19;
            this.barButtonItemTopTenPercent.Name = "barButtonItemTopTenPercent";
            this.barButtonItemTopTenPercent.Tag = "TOP_TEN_PERCENT";
            this.barButtonItemTopTenPercent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBottomTenPercent
            // 
            this.barButtonItemBottomTenPercent.Caption = "BOTTOM_TEN_PERCENT";
            this.barButtonItemBottomTenPercent.Id = 41;
            this.barButtonItemBottomTenPercent.ImageOptions.ImageIndex = 20;
            this.barButtonItemBottomTenPercent.ImageOptions.LargeImageIndex = 20;
            this.barButtonItemBottomTenPercent.Name = "barButtonItemBottomTenPercent";
            this.barButtonItemBottomTenPercent.Tag = "BOTTOM_TEN_PERCENT";
            this.barButtonItemBottomTenPercent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemAboveAverage
            // 
            this.barButtonItemAboveAverage.Caption = "ABOVE_AVERAGE";
            this.barButtonItemAboveAverage.Id = 42;
            this.barButtonItemAboveAverage.ImageOptions.ImageIndex = 21;
            this.barButtonItemAboveAverage.ImageOptions.LargeImageIndex = 21;
            this.barButtonItemAboveAverage.Name = "barButtonItemAboveAverage";
            this.barButtonItemAboveAverage.Tag = "ABOVE_AVERAGE";
            this.barButtonItemAboveAverage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBlowAverage
            // 
            this.barButtonItemBlowAverage.Caption = "BLOW_AVERAGE";
            this.barButtonItemBlowAverage.Id = 43;
            this.barButtonItemBlowAverage.ImageOptions.ImageIndex = 22;
            this.barButtonItemBlowAverage.ImageOptions.LargeImageIndex = 22;
            this.barButtonItemBlowAverage.Name = "barButtonItemBlowAverage";
            this.barButtonItemBlowAverage.Tag = "BLOW_AVERAGE";
            this.barButtonItemBlowAverage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemClearBlod
            // 
            this.barButtonItemClearBlod.Caption = "CLEAR";
            this.barButtonItemClearBlod.Id = 44;
            this.barButtonItemClearBlod.ImageOptions.ImageIndex = 16;
            this.barButtonItemClearBlod.ImageOptions.LargeImageIndex = 16;
            this.barButtonItemClearBlod.Name = "barButtonItemClearBlod";
            this.barButtonItemClearBlod.Tag = "CLEAR_B";
            this.barButtonItemClearBlod.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItemBar
            // 
            this.barSubItemBar.Caption = "COND_BAR";
            this.barSubItemBar.Id = 45;
            this.barSubItemBar.ImageOptions.ImageIndex = 23;
            this.barSubItemBar.ImageOptions.LargeImageIndex = 23;
            this.barSubItemBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBlue),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemRed),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGreen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemOrange),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemViolet),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPupple),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemBarClear)});
            this.barSubItemBar.Name = "barSubItemBar";
            // 
            // barButtonItemBlue
            // 
            this.barButtonItemBlue.Caption = "BLUE";
            this.barButtonItemBlue.Id = 46;
            this.barButtonItemBlue.ImageOptions.ImageIndex = 24;
            this.barButtonItemBlue.ImageOptions.LargeImageIndex = 24;
            this.barButtonItemBlue.Name = "barButtonItemBlue";
            this.barButtonItemBlue.Tag = "BAR_BLUE";
            this.barButtonItemBlue.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemRed
            // 
            this.barButtonItemRed.Caption = "RED";
            this.barButtonItemRed.Id = 47;
            this.barButtonItemRed.ImageOptions.ImageIndex = 25;
            this.barButtonItemRed.ImageOptions.LargeImageIndex = 25;
            this.barButtonItemRed.Name = "barButtonItemRed";
            this.barButtonItemRed.Tag = "BAR_RED";
            this.barButtonItemRed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemGreen
            // 
            this.barButtonItemGreen.Caption = "GREEN";
            this.barButtonItemGreen.Id = 48;
            this.barButtonItemGreen.ImageOptions.ImageIndex = 26;
            this.barButtonItemGreen.ImageOptions.LargeImageIndex = 26;
            this.barButtonItemGreen.Name = "barButtonItemGreen";
            this.barButtonItemGreen.Tag = "BAR_GREEN";
            this.barButtonItemGreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemOrange
            // 
            this.barButtonItemOrange.Caption = "ORANGE";
            this.barButtonItemOrange.Id = 49;
            this.barButtonItemOrange.ImageOptions.ImageIndex = 27;
            this.barButtonItemOrange.ImageOptions.LargeImageIndex = 27;
            this.barButtonItemOrange.Name = "barButtonItemOrange";
            this.barButtonItemOrange.Tag = "BAR_ORANGE";
            this.barButtonItemOrange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemViolet
            // 
            this.barButtonItemViolet.Caption = "VIOLET";
            this.barButtonItemViolet.Id = 50;
            this.barButtonItemViolet.ImageOptions.ImageIndex = 28;
            this.barButtonItemViolet.ImageOptions.LargeImageIndex = 28;
            this.barButtonItemViolet.Name = "barButtonItemViolet";
            this.barButtonItemViolet.Tag = "BAR_VIOLET";
            this.barButtonItemViolet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemPupple
            // 
            this.barButtonItemPupple.Caption = "PUPPLE";
            this.barButtonItemPupple.Id = 51;
            this.barButtonItemPupple.ImageOptions.ImageIndex = 29;
            this.barButtonItemPupple.ImageOptions.LargeImageIndex = 29;
            this.barButtonItemPupple.Name = "barButtonItemPupple";
            this.barButtonItemPupple.Tag = "BAR_PUPPLE";
            this.barButtonItemPupple.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBarClear
            // 
            this.barButtonItemBarClear.Caption = "CLEAR";
            this.barButtonItemBarClear.Id = 52;
            this.barButtonItemBarClear.ImageOptions.ImageIndex = 16;
            this.barButtonItemBarClear.ImageOptions.LargeImageIndex = 16;
            this.barButtonItemBarClear.Name = "barButtonItemBarClear";
            this.barButtonItemBarClear.Tag = "CLEAR_BAR";
            this.barButtonItemBarClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "CLEAR";
            this.barButtonItem2.Id = 53;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Tag = "CLEAR";
            // 
            // myPath
            // 
            this.myPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPath.EditValue = "Path";
            this.myPath.Location = new System.Drawing.Point(0, 0);
            this.myPath.Name = "myPath";
            this.myPath.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myPath.Properties.Appearance.Options.UseFont = true;
            this.myPath.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.myPath.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.myPath.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.myPath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.myPath.Properties.ReadOnly = true;
            this.myPath.Size = new System.Drawing.Size(1366, 22);
            this.myPath.TabIndex = 0;
            // 
            // myTitlePanel
            // 
            this.myTitlePanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.myTitlePanel.Appearance.Options.UseBackColor = true;
            this.myTitlePanel.AutoSize = true;
            this.myTitlePanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.myTitlePanel.Controls.Add(this.myPath);
            this.myTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.myTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.myTitlePanel.MinimumSize = new System.Drawing.Size(0, 22);
            this.myTitlePanel.Name = "myTitlePanel";
            this.myTitlePanel.Size = new System.Drawing.Size(1366, 22);
            this.myTitlePanel.TabIndex = 0;
            // 
            // myRadialMenu
            // 
            this.myRadialMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemSummery),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemBold),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemBar)});
            this.myRadialMenu.Manager = this.myBarManager;
            this.myRadialMenu.MenuRadius = 150;
            this.myRadialMenu.Name = "myRadialMenu";
            // 
            // PageType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.myTitlePanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PageType";
            this.Size = new System.Drawing.Size(1366, 768);
            this.Load += new System.EventHandler(this.PageType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myTitlePanel)).EndInit();
            this.myTitlePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myRadialMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private DevExpress.XtraBars.BarSubItem barSubItemExport;
        private DevExpress.XtraBars.BarButtonItem barButtonItemXls;
        private DevExpress.XtraBars.BarButtonItem barButtonItemXlsx;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHtml;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPdf;
        private DevExpress.XtraBars.BarButtonItem barButtonItemTxt;
        private DevExpress.XtraBars.BarSubItem barSubItemCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAllCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLineCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCellCopy;
        private DevExpress.XtraBars.BarManager myBarManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl myTitlePanel;
        private DevExpress.XtraEditors.TextEdit myPath;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNew;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private System.Windows.Forms.SaveFileDialog mySaveFileDialog;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem1;
        private DevExpress.XtraBars.Ribbon.RadialMenu myRadialMenu;
        private DevExpress.Utils.ImageCollection myImageCollection;
        private DevExpress.XtraBars.BarSubItem barSubItemSummery;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSummery;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAverage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMax;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMin;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCount;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClearSummery;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarSubItem barSubItemBold;
        private DevExpress.XtraBars.BarButtonItem barButtonItemTopTenItems;
        private DevExpress.XtraBars.BarButtonItem barButtonItemBottomTenItems;
        private DevExpress.XtraBars.BarButtonItem barButtonItemTopTenPercent;
        private DevExpress.XtraBars.BarButtonItem barButtonItemBottomTenPercent;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAboveAverage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemBlowAverage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClearBlod;
        private DevExpress.XtraBars.BarSubItem barSubItemBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemBlue;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRed;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGreen;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOrange;
        private DevExpress.XtraBars.BarButtonItem barButtonItemViolet;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPupple;
        private DevExpress.XtraBars.BarButtonItem barButtonItemBarClear;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    }
}
