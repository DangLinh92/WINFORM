namespace Wisol.MES.Inherit
{
    partial class FormType
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormType));
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollectionLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItemNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemSummery = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemSummery = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAverage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMax = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMin = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCount = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClearSummery = new DevExpress.XtraBars.BarButtonItem();
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.radialMenu1 = new DevExpress.XtraBars.Ribbon.RadialMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // barSubItemExport
            // 
            this.barSubItemExport.Caption = "EXPORT";
            this.barSubItemExport.Id = 0;
            this.barSubItemExport.ImageIndex = 0;
            this.barSubItemExport.LargeImageIndex = 0;
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
            this.barButtonItemXls.ImageIndex = 3;
            this.barButtonItemXls.LargeImageIndex = 3;
            this.barButtonItemXls.Name = "barButtonItemXls";
            this.barButtonItemXls.Tag = "XLS";
            this.barButtonItemXls.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemXlsx
            // 
            this.barButtonItemXlsx.Caption = "XLSX";
            this.barButtonItemXlsx.Id = 7;
            this.barButtonItemXlsx.ImageIndex = 4;
            this.barButtonItemXlsx.LargeImageIndex = 4;
            this.barButtonItemXlsx.Name = "barButtonItemXlsx";
            this.barButtonItemXlsx.Tag = "XLSX";
            this.barButtonItemXlsx.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemHtml
            // 
            this.barButtonItemHtml.Caption = "HTML";
            this.barButtonItemHtml.Id = 8;
            this.barButtonItemHtml.ImageIndex = 4;
            this.barButtonItemHtml.LargeImageIndex = 4;
            this.barButtonItemHtml.Name = "barButtonItemHtml";
            this.barButtonItemHtml.Tag = "HTML";
            this.barButtonItemHtml.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemPdf
            // 
            this.barButtonItemPdf.Caption = "PDF";
            this.barButtonItemPdf.Id = 9;
            this.barButtonItemPdf.ImageIndex = 5;
            this.barButtonItemPdf.LargeImageIndex = 5;
            this.barButtonItemPdf.Name = "barButtonItemPdf";
            this.barButtonItemPdf.Tag = "PDF";
            this.barButtonItemPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemTxt
            // 
            this.barButtonItemTxt.Caption = "TXT";
            this.barButtonItemTxt.Id = 10;
            this.barButtonItemTxt.ImageIndex = 6;
            this.barButtonItemTxt.LargeImageIndex = 6;
            this.barButtonItemTxt.Name = "barButtonItemTxt";
            this.barButtonItemTxt.Tag = "TXT";
            this.barButtonItemTxt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItemCopy
            // 
            this.barSubItemCopy.Caption = "COPY";
            this.barSubItemCopy.Id = 1;
            this.barSubItemCopy.ImageIndex = 1;
            this.barSubItemCopy.LargeImageIndex = 1;
            this.barSubItemCopy.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAllCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLineCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCellCopy)});
            this.barSubItemCopy.Name = "barSubItemCopy";
            this.barSubItemCopy.Tag = "COPY";
            // 
            // barButtonItemAllCopy
            // 
            this.barButtonItemAllCopy.Caption = "All_COPY";
            this.barButtonItemAllCopy.Id = 11;
            this.barButtonItemAllCopy.ImageIndex = 7;
            this.barButtonItemAllCopy.LargeImageIndex = 7;
            this.barButtonItemAllCopy.Name = "barButtonItemAllCopy";
            this.barButtonItemAllCopy.Tag = "ALL_COPY";
            this.barButtonItemAllCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemLineCopy
            // 
            this.barButtonItemLineCopy.Caption = "LINE_COPY";
            this.barButtonItemLineCopy.Id = 12;
            this.barButtonItemLineCopy.ImageIndex = 8;
            this.barButtonItemLineCopy.LargeImageIndex = 8;
            this.barButtonItemLineCopy.Name = "barButtonItemLineCopy";
            this.barButtonItemLineCopy.Tag = "LINE_COPY";
            this.barButtonItemLineCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemCellCopy
            // 
            this.barButtonItemCellCopy.Caption = "CELL_COPY";
            this.barButtonItemCellCopy.Id = 13;
            this.barButtonItemCellCopy.ImageIndex = 9;
            this.barButtonItemCellCopy.LargeImageIndex = 9;
            this.barButtonItemCellCopy.Name = "barButtonItemCellCopy";
            this.barButtonItemCellCopy.Tag = "CELL_COPY";
            this.barButtonItemCellCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollectionLarge;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
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
            this.barSubItemSummery,
            this.barButtonItemSummery,
            this.barButtonItemAverage,
            this.barButtonItemMax,
            this.barButtonItemMin,
            this.barButtonItemCount,
            this.barButtonItemClearSummery,
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
            this.barButtonItemBarClear});
            this.barManager1.LargeImages = this.imageCollectionLarge;
            this.barManager1.MaxItemId = 18;
            this.barManager1.ShowCloseButton = true;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(284, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 306);
            this.barDockControlBottom.Size = new System.Drawing.Size(284, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 306);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(284, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 306);
            // 
            // imageCollectionLarge
            // 
            this.imageCollectionLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollectionLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionLarge.ImageStream")));
            this.imageCollectionLarge.InsertGalleryImage("export_32x32.png", "images/export/export_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/export_32x32.png"), 0);
            this.imageCollectionLarge.Images.SetKeyName(0, "export_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("copy_32x32.png", "images/edit/copy_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/edit/copy_32x32.png"), 1);
            this.imageCollectionLarge.Images.SetKeyName(1, "copy_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("exporttoxls_32x32.png", "images/export/exporttoxls_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxls_32x32.png"), 2);
            this.imageCollectionLarge.Images.SetKeyName(2, "exporttoxls_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("exporttoxlsx_32x32.png", "images/export/exporttoxlsx_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxlsx_32x32.png"), 3);
            this.imageCollectionLarge.Images.SetKeyName(3, "exporttoxlsx_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("exporttohtml_32x32.png", "images/export/exporttohtml_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttohtml_32x32.png"), 4);
            this.imageCollectionLarge.Images.SetKeyName(4, "exporttohtml_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("exporttopdf_32x32.png", "images/export/exporttopdf_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_32x32.png"), 5);
            this.imageCollectionLarge.Images.SetKeyName(5, "exporttopdf_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("exporttotxt_32x32.png", "images/export/exporttotxt_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttotxt_32x32.png"), 6);
            this.imageCollectionLarge.Images.SetKeyName(6, "exporttotxt_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(7, "SelectAll_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(8, "SelectRow_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(9, "SelectCell_32x32.png");
            this.imageCollectionLarge.InsertGalleryImage("summary_32x32.png", "images/data/summary_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/data/summary_32x32.png"), 10);
            this.imageCollectionLarge.Images.SetKeyName(10, "summary_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(11, "SUM.png");
            this.imageCollectionLarge.Images.SetKeyName(12, "AVG.png");
            this.imageCollectionLarge.Images.SetKeyName(13, "MAX.png");
            this.imageCollectionLarge.Images.SetKeyName(14, "MIN.png");
            this.imageCollectionLarge.Images.SetKeyName(15, "COUNT.png");
            this.imageCollectionLarge.InsertGalleryImage("clearformatting_32x32.png", "images/actions/clearformatting_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/clearformatting_32x32.png"), 16);
            this.imageCollectionLarge.Images.SetKeyName(16, "clearformatting_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(17, "Top10Items_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(18, "Bottom10Items_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(19, "Top10Percent_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(20, "Bottom10Percent_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(21, "AboveAverage_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(22, "BelowAverage_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(23, "GradientLightBlueDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(24, "GradientBlueDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(25, "GradientCoralDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(26, "GradientGreenDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(27, "GradientOrangeDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(28, "GradientVioletDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(29, "GradientRaspberryDataBar_32x32.png");
            this.imageCollectionLarge.Images.SetKeyName(30, "1452168531_bold.png");
            // 
            // barButtonItemNew
            // 
            this.barButtonItemNew.Caption = "New";
            this.barButtonItemNew.Id = 2;
            this.barButtonItemNew.ImageIndex = 0;
            this.barButtonItemNew.Name = "barButtonItemNew";
            this.barButtonItemNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemEdit
            // 
            this.barButtonItemEdit.Caption = "Edit";
            this.barButtonItemEdit.Id = 3;
            this.barButtonItemEdit.ImageIndex = 1;
            this.barButtonItemEdit.Name = "barButtonItemEdit";
            this.barButtonItemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.Caption = "Print";
            this.barButtonItemPrint.Id = 5;
            this.barButtonItemPrint.ImageIndex = 4;
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItemSummery
            // 
            this.barSubItemSummery.Caption = "SUMMERY";
            this.barSubItemSummery.Id = 16;
            this.barSubItemSummery.ImageIndex = 10;
            this.barSubItemSummery.LargeImageIndex = 10;
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
            this.barButtonItemSummery.ImageIndex = 11;
            this.barButtonItemSummery.LargeImageIndex = 11;
            this.barButtonItemSummery.Name = "barButtonItemSummery";
            this.barButtonItemSummery.Tag = "SUM";
            this.barButtonItemSummery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemAverage
            // 
            this.barButtonItemAverage.Caption = "AVG";
            this.barButtonItemAverage.Id = 18;
            this.barButtonItemAverage.ImageIndex = 12;
            this.barButtonItemAverage.LargeImageIndex = 12;
            this.barButtonItemAverage.Name = "barButtonItemAverage";
            this.barButtonItemAverage.Tag = "AVG";
            this.barButtonItemAverage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemMax
            // 
            this.barButtonItemMax.Caption = "MAX";
            this.barButtonItemMax.Id = 19;
            this.barButtonItemMax.ImageIndex = 13;
            this.barButtonItemMax.LargeImageIndex = 13;
            this.barButtonItemMax.Name = "barButtonItemMax";
            this.barButtonItemMax.Tag = "MAX";
            this.barButtonItemMax.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemMin
            // 
            this.barButtonItemMin.Caption = "MIN";
            this.barButtonItemMin.Id = 20;
            this.barButtonItemMin.ImageIndex = 14;
            this.barButtonItemMin.LargeImageIndex = 14;
            this.barButtonItemMin.Name = "barButtonItemMin";
            this.barButtonItemMin.Tag = "MIN";
            this.barButtonItemMin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemCount
            // 
            this.barButtonItemCount.Caption = "COUNT";
            this.barButtonItemCount.Id = 21;
            this.barButtonItemCount.ImageIndex = 15;
            this.barButtonItemCount.LargeImageIndex = 15;
            this.barButtonItemCount.Name = "barButtonItemCount";
            this.barButtonItemCount.Tag = "COUNT";
            this.barButtonItemCount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemClearSummery
            // 
            this.barButtonItemClearSummery.Caption = "CLEAR";
            this.barButtonItemClearSummery.Id = 34;
            this.barButtonItemClearSummery.ImageIndex = 16;
            this.barButtonItemClearSummery.LargeImageIndex = 16;
            this.barButtonItemClearSummery.Name = "barButtonItemClearSummery";
            this.barButtonItemClearSummery.Tag = "CLEAR_S";
            this.barButtonItemClearSummery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItemBold
            // 
            this.barSubItemBold.Caption = "BOLD";
            this.barSubItemBold.Id = 37;
            this.barSubItemBold.ImageIndex = 30;
            this.barSubItemBold.LargeImageIndex = 30;
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
            this.barButtonItemTopTenItems.ImageIndex = 17;
            this.barButtonItemTopTenItems.LargeImageIndex = 17;
            this.barButtonItemTopTenItems.Name = "barButtonItemTopTenItems";
            this.barButtonItemTopTenItems.Tag = "TOP_TEN_ITEMS";
            this.barButtonItemTopTenItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBottomTenItems
            // 
            this.barButtonItemBottomTenItems.Caption = "BOTTOM_TEN_ITEMS";
            this.barButtonItemBottomTenItems.Id = 39;
            this.barButtonItemBottomTenItems.ImageIndex = 18;
            this.barButtonItemBottomTenItems.LargeImageIndex = 18;
            this.barButtonItemBottomTenItems.Name = "barButtonItemBottomTenItems";
            this.barButtonItemBottomTenItems.Tag = "BOTTOM_TEN_ITEMS";
            this.barButtonItemBottomTenItems.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemTopTenPercent
            // 
            this.barButtonItemTopTenPercent.Caption = "TOP_TEN_PERCENT";
            this.barButtonItemTopTenPercent.Id = 40;
            this.barButtonItemTopTenPercent.ImageIndex = 19;
            this.barButtonItemTopTenPercent.LargeImageIndex = 19;
            this.barButtonItemTopTenPercent.Name = "barButtonItemTopTenPercent";
            this.barButtonItemTopTenPercent.Tag = "TOP_TEN_PERCENT";
            this.barButtonItemTopTenPercent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBottomTenPercent
            // 
            this.barButtonItemBottomTenPercent.Caption = "BOTTOM_TEN_PERCENT";
            this.barButtonItemBottomTenPercent.Id = 41;
            this.barButtonItemBottomTenPercent.ImageIndex = 20;
            this.barButtonItemBottomTenPercent.LargeImageIndex = 20;
            this.barButtonItemBottomTenPercent.Name = "barButtonItemBottomTenPercent";
            this.barButtonItemBottomTenPercent.Tag = "BOTTOM_TEN_PERCENT";
            this.barButtonItemBottomTenPercent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemAboveAverage
            // 
            this.barButtonItemAboveAverage.Caption = "ABOVE_AVERAGE";
            this.barButtonItemAboveAverage.Id = 42;
            this.barButtonItemAboveAverage.ImageIndex = 21;
            this.barButtonItemAboveAverage.LargeImageIndex = 21;
            this.barButtonItemAboveAverage.Name = "barButtonItemAboveAverage";
            this.barButtonItemAboveAverage.Tag = "ABOVE_AVERAGE";
            this.barButtonItemAboveAverage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBlowAverage
            // 
            this.barButtonItemBlowAverage.Caption = "BLOW_AVERAGE";
            this.barButtonItemBlowAverage.Id = 43;
            this.barButtonItemBlowAverage.ImageIndex = 22;
            this.barButtonItemBlowAverage.LargeImageIndex = 22;
            this.barButtonItemBlowAverage.Name = "barButtonItemBlowAverage";
            this.barButtonItemBlowAverage.Tag = "BLOW_AVERAGE";
            this.barButtonItemBlowAverage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemClearBlod
            // 
            this.barButtonItemClearBlod.Caption = "CLEAR";
            this.barButtonItemClearBlod.Id = 44;
            this.barButtonItemClearBlod.ImageIndex = 16;
            this.barButtonItemClearBlod.LargeImageIndex = 16;
            this.barButtonItemClearBlod.Name = "barButtonItemClearBlod";
            this.barButtonItemClearBlod.Tag = "CLEAR_B";
            this.barButtonItemClearBlod.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barSubItemBar
            // 
            this.barSubItemBar.Caption = "COND_BAR";
            this.barSubItemBar.Id = 45;
            this.barSubItemBar.ImageIndex = 23;
            this.barSubItemBar.LargeImageIndex = 23;
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
            this.barButtonItemBlue.ImageIndex = 24;
            this.barButtonItemBlue.LargeImageIndex = 24;
            this.barButtonItemBlue.Name = "barButtonItemBlue";
            this.barButtonItemBlue.Tag = "BAR_BLUE";
            this.barButtonItemBlue.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemRed
            // 
            this.barButtonItemRed.Caption = "RED";
            this.barButtonItemRed.Id = 47;
            this.barButtonItemRed.ImageIndex = 25;
            this.barButtonItemRed.LargeImageIndex = 25;
            this.barButtonItemRed.Name = "barButtonItemRed";
            this.barButtonItemRed.Tag = "BAR_RED";
            this.barButtonItemRed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemGreen
            // 
            this.barButtonItemGreen.Caption = "GREEN";
            this.barButtonItemGreen.Id = 48;
            this.barButtonItemGreen.ImageIndex = 26;
            this.barButtonItemGreen.LargeImageIndex = 26;
            this.barButtonItemGreen.Name = "barButtonItemGreen";
            this.barButtonItemGreen.Tag = "BAR_GREEN";
            this.barButtonItemGreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemOrange
            // 
            this.barButtonItemOrange.Caption = "ORANGE";
            this.barButtonItemOrange.Id = 49;
            this.barButtonItemOrange.ImageIndex = 27;
            this.barButtonItemOrange.LargeImageIndex = 27;
            this.barButtonItemOrange.Name = "barButtonItemOrange";
            this.barButtonItemOrange.Tag = "BAR_ORANGE";
            this.barButtonItemOrange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemViolet
            // 
            this.barButtonItemViolet.Caption = "VIOLET";
            this.barButtonItemViolet.Id = 50;
            this.barButtonItemViolet.ImageIndex = 28;
            this.barButtonItemViolet.LargeImageIndex = 28;
            this.barButtonItemViolet.Name = "barButtonItemViolet";
            this.barButtonItemViolet.Tag = "BAR_VIOLET";
            this.barButtonItemViolet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemPupple
            // 
            this.barButtonItemPupple.Caption = "PUPPLE";
            this.barButtonItemPupple.Id = 51;
            this.barButtonItemPupple.ImageIndex = 29;
            this.barButtonItemPupple.LargeImageIndex = 29;
            this.barButtonItemPupple.Name = "barButtonItemPupple";
            this.barButtonItemPupple.Tag = "BAR_PUPPLE";
            this.barButtonItemPupple.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // barButtonItemBarClear
            // 
            this.barButtonItemBarClear.Caption = "CLEAR";
            this.barButtonItemBarClear.Id = 52;
            this.barButtonItemBarClear.ImageIndex = 16;
            this.barButtonItemBarClear.LargeImageIndex = 16;
            this.barButtonItemBarClear.Name = "barButtonItemBarClear";
            this.barButtonItemBarClear.Tag = "CLEAR_BAR";
            this.barButtonItemBarClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_ItemClick);
            // 
            // radialMenu1
            // 
            this.radialMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemSummery),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemBold),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemBar)});
            this.radialMenu1.Manager = this.barManager1;
            this.radialMenu1.MenuRadius = 150;
            this.radialMenu1.Name = "radialMenu1";
            // 
            // FormType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(284, 306);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormType";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormType_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormType_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).EndInit();
            this.ResumeLayout(false);

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
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNew;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.XtraBars.Ribbon.RadialMenu radialMenu1;
        private DevExpress.XtraBars.BarSubItem barSubItemSummery;
        private DevExpress.Utils.ImageCollection imageCollectionLarge;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSummery;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAverage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMax;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMin;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCount;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClearSummery;
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
    }
}