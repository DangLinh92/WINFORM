namespace Wisol.MES
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barLogin = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barFavorite = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barNewExcute = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSettings = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnInit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem6 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnCloseAll = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnSearch = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.barBtnSaveCDT = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnLoadCDT = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnLoadLayout = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnSaveLayout = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barBtnManual = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnGo = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barUsr = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSkin = new DevExpress.XtraBars.SkinBarSubItem();
            this.myTextNotify = new DevExpress.XtraBars.BarStaticItem();
            this.m_SearchMenuBarItem = new DevExpress.XtraBars.BarEditItem();
            this.m_SearchMenuEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSearchControl();
            this.myTextVersion = new DevExpress.XtraBars.BarStaticItem();
            this.skinBarSubItem2 = new DevExpress.XtraBars.SkinBarSubItem();
            this.largeButtonImageList = new System.Windows.Forms.ImageList(this.components);
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.m_SplashScreenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Wisol.MES.FrmWaitForm), false, false);
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tabForm = new DevExpress.XtraTab.XtraTabControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_SearchMenuEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLogin,
            this.barFavorite,
            this.barNewExcute,
            this.barSettings,
            this.barBtnInit,
            this.barLargeButtonItem6,
            this.barBtnClose,
            this.barBtnCloseAll,
            this.barBtnSearch,
            this.barSubItem2,
            this.barSubItem4,
            this.barButtonItem1,
            this.barBtnSaveCDT,
            this.barBtnLoadCDT,
            this.barBtnLoadLayout,
            this.barBtnSaveLayout,
            this.barBtnManual,
            this.barButtonItem2,
            this.barBtnGo,
            this.barUsr,
            this.barSkin,
            this.myTextNotify,
            this.m_SearchMenuBarItem,
            this.myTextVersion,
            this.skinBarSubItem2});
            this.barManager1.LargeImages = this.largeButtonImageList;
            this.barManager1.MaxItemId = 42;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemHyperLinkEdit1,
            this.m_SearchMenuEdit});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 730);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 730);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1008, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 730);
            // 
            // barLogin
            // 
            this.barLogin.Caption = "LOGIN";
            this.barLogin.Id = 1;
            this.barLogin.ImageOptions.LargeImageIndex = 0;
            this.barLogin.Name = "barLogin";
            this.barLogin.ShowCaptionOnBar = false;
            this.barLogin.Tag = "LOGIN";
            this.barLogin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLogin_ItemClick);
            // 
            // barFavorite
            // 
            this.barFavorite.Caption = "FAVORITE";
            this.barFavorite.Id = 2;
            this.barFavorite.ImageOptions.LargeImageIndex = 1;
            this.barFavorite.Name = "barFavorite";
            this.barFavorite.ShowCaptionOnBar = false;
            this.barFavorite.Tag = "FAVORITE";
            // 
            // barNewExcute
            // 
            this.barNewExcute.Caption = "NEW_EXCUTE";
            this.barNewExcute.Id = 3;
            this.barNewExcute.ImageOptions.LargeImageIndex = 3;
            this.barNewExcute.Name = "barNewExcute";
            this.barNewExcute.ShowCaptionOnBar = false;
            this.barNewExcute.Tag = "NEW_EXCUTE";
            this.barNewExcute.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNewExcute_ItemClick);
            // 
            // barSettings
            // 
            this.barSettings.Caption = "SETTINGS";
            this.barSettings.Id = 4;
            this.barSettings.ImageOptions.LargeImageIndex = 2;
            this.barSettings.Name = "barSettings";
            this.barSettings.ShowCaptionOnBar = false;
            this.barSettings.Tag = "SETTINGS";
            this.barSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSettings_ItemClick);
            // 
            // barBtnInit
            // 
            this.barBtnInit.Caption = "INIT[F2]";
            this.barBtnInit.Id = 5;
            this.barBtnInit.ImageOptions.LargeImageIndex = 4;
            this.barBtnInit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this.barBtnInit.Name = "barBtnInit";
            this.barBtnInit.ShowCaptionOnBar = false;
            this.barBtnInit.Tag = "INIT[F2]";
            this.barBtnInit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barLargeButtonItem6
            // 
            this.barLargeButtonItem6.Caption = "SEARCH";
            this.barLargeButtonItem6.Id = 6;
            this.barLargeButtonItem6.Name = "barLargeButtonItem6";
            // 
            // barBtnClose
            // 
            this.barBtnClose.Caption = "CLOSE[F9]";
            this.barBtnClose.Id = 7;
            this.barBtnClose.ImageOptions.LargeImageIndex = 10;
            this.barBtnClose.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9));
            this.barBtnClose.Name = "barBtnClose";
            this.barBtnClose.ShowCaptionOnBar = false;
            this.barBtnClose.Tag = "CLOSE[F9]";
            this.barBtnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barBtnCloseAll
            // 
            this.barBtnCloseAll.Caption = "CLOSE_ALL[F10]";
            this.barBtnCloseAll.Id = 8;
            this.barBtnCloseAll.ImageOptions.LargeImageIndex = 11;
            this.barBtnCloseAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F10));
            this.barBtnCloseAll.Name = "barBtnCloseAll";
            this.barBtnCloseAll.ShowCaptionOnBar = false;
            this.barBtnCloseAll.Tag = "CLOSE_ALL[F10]";
            this.barBtnCloseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barBtnSearch
            // 
            this.barBtnSearch.Caption = "SEARCH[F3]";
            this.barBtnSearch.Id = 10;
            this.barBtnSearch.ImageOptions.LargeImageIndex = 5;
            this.barBtnSearch.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.barBtnSearch.Name = "barBtnSearch";
            this.barBtnSearch.ShowCaptionOnBar = false;
            this.barBtnSearch.Tag = "SEARCH[F3]";
            this.barBtnSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "barSubItem2";
            this.barSubItem2.Id = 13;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 16;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem4
            // 
            this.barSubItem4.Caption = "barSubItem4";
            this.barSubItem4.Id = 15;
            this.barSubItem4.Name = "barSubItem4";
            // 
            // barBtnSaveCDT
            // 
            this.barBtnSaveCDT.Caption = "SAVE_CDT[F6]";
            this.barBtnSaveCDT.Id = 17;
            this.barBtnSaveCDT.ImageOptions.LargeImageIndex = 7;
            this.barBtnSaveCDT.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6));
            this.barBtnSaveCDT.Name = "barBtnSaveCDT";
            this.barBtnSaveCDT.ShowCaptionOnBar = false;
            this.barBtnSaveCDT.Tag = "SAVE_CDT[F6]";
            this.barBtnSaveCDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barBtnLoadCDT
            // 
            this.barBtnLoadCDT.Caption = "LOAD_CDT[F4]";
            this.barBtnLoadCDT.Id = 18;
            this.barBtnLoadCDT.ImageOptions.LargeImageIndex = 6;
            this.barBtnLoadCDT.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4));
            this.barBtnLoadCDT.Name = "barBtnLoadCDT";
            this.barBtnLoadCDT.ShowCaptionOnBar = false;
            this.barBtnLoadCDT.Tag = "LOAD_CDT[F4]";
            this.barBtnLoadCDT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barBtnLoadLayout
            // 
            this.barBtnLoadLayout.Caption = "LOAD_LAYOUT[F7]";
            this.barBtnLoadLayout.Id = 19;
            this.barBtnLoadLayout.ImageOptions.LargeImageIndex = 8;
            this.barBtnLoadLayout.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F7));
            this.barBtnLoadLayout.Name = "barBtnLoadLayout";
            this.barBtnLoadLayout.ShowCaptionOnBar = false;
            this.barBtnLoadLayout.Tag = "LOAD_LAYOUT[F7]";
            this.barBtnLoadLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barBtnSaveLayout
            // 
            this.barBtnSaveLayout.Caption = "SAVE_LAYOUT[F8]";
            this.barBtnSaveLayout.Id = 20;
            this.barBtnSaveLayout.ImageOptions.LargeImageIndex = 9;
            this.barBtnSaveLayout.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F8));
            this.barBtnSaveLayout.Name = "barBtnSaveLayout";
            this.barBtnSaveLayout.ShowCaptionOnBar = false;
            this.barBtnSaveLayout.Tag = "SAVE_LAYOUT[F8]";
            this.barBtnSaveLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barBtnManual
            // 
            this.barBtnManual.Caption = "MANUAL[F12]";
            this.barBtnManual.Id = 22;
            this.barBtnManual.ImageOptions.LargeImageIndex = 12;
            this.barBtnManual.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
            this.barBtnManual.Name = "barBtnManual";
            this.barBtnManual.ShowCaptionOnBar = false;
            this.barBtnManual.Tag = "MANUAL[F12]";
            this.barBtnManual.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "GO";
            this.barButtonItem2.Id = 24;
            this.barButtonItem2.ImageOptions.LargeImageIndex = 13;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barBtnGo
            // 
            this.barBtnGo.Caption = "GO[F1]";
            this.barBtnGo.Id = 25;
            this.barBtnGo.ImageOptions.LargeImageIndex = 13;
            this.barBtnGo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.barBtnGo.Name = "barBtnGo";
            this.barBtnGo.ShowCaptionOnBar = false;
            this.barBtnGo.Tag = "GO[F1]";
            this.barBtnGo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnGo_ItemClick);
            // 
            // barUsr
            // 
            this.barUsr.Caption = "USR";
            this.barUsr.Id = 26;
            this.barUsr.ImageOptions.LargeImageIndex = 14;
            this.barUsr.Name = "barUsr";
            this.barUsr.ShowCaptionOnBar = false;
            this.barUsr.Tag = "USR";
            this.barUsr.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUsr_ItemClick);
            // 
            // barSkin
            // 
            this.barSkin.Caption = "THEME_CHANGE";
            this.barSkin.Id = 27;
            this.barSkin.Name = "barSkin";
            this.barSkin.Tag = "THEME_CHANGE";
            // 
            // myTextNotify
            // 
            this.myTextNotify.Id = 35;
            this.myTextNotify.ItemAppearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.myTextNotify.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.myTextNotify.ItemAppearance.Normal.Options.UseFont = true;
            this.myTextNotify.ItemAppearance.Normal.Options.UseForeColor = true;
            this.myTextNotify.Name = "myTextNotify";
            this.myTextNotify.Size = new System.Drawing.Size(1000, 0);
            this.myTextNotify.Width = 1000;
            this.myTextNotify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.txtnotify_ItemClick);
            // 
            // m_SearchMenuBarItem
            // 
            this.m_SearchMenuBarItem.Caption = "SEARCH";
            this.m_SearchMenuBarItem.Edit = this.m_SearchMenuEdit;
            this.m_SearchMenuBarItem.Id = 36;
            this.m_SearchMenuBarItem.Name = "m_SearchMenuBarItem";
            this.m_SearchMenuBarItem.EditValueChanged += new System.EventHandler(this.SearchMenuBarItem_EditValueChanged);
            this.m_SearchMenuBarItem.ItemPress += new DevExpress.XtraBars.ItemClickEventHandler(this.SearchMenuBarItem_ItemPress);
            // 
            // m_SearchMenuEdit
            // 
            this.m_SearchMenuEdit.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_SearchMenuEdit.Appearance.Options.UseFont = true;
            this.m_SearchMenuEdit.AutoHeight = false;
            this.m_SearchMenuEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.m_SearchMenuEdit.CaseSensitiveSearch = true;
            this.m_SearchMenuEdit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_SearchMenuEdit.HideSelection = false;
            this.m_SearchMenuEdit.Name = "m_SearchMenuEdit";
            this.m_SearchMenuEdit.NullValuePrompt = "화면명 입력\\Tên màn hình";
            // 
            // myTextVersion
            // 
            this.myTextVersion.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.myTextVersion.Caption = "barStaticItem1";
            this.myTextVersion.Id = 40;
            this.myTextVersion.Name = "myTextVersion";
            // 
            // skinBarSubItem2
            // 
            this.skinBarSubItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.skinBarSubItem2.Caption = "skinBarSubItem2";
            this.skinBarSubItem2.Id = 41;
            this.skinBarSubItem2.Name = "skinBarSubItem2";
            // 
            // largeButtonImageList
            // 
            this.largeButtonImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeButtonImageList.ImageStream")));
            this.largeButtonImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeButtonImageList.Images.SetKeyName(0, "key-icon.png");
            this.largeButtonImageList.Images.SetKeyName(1, "star-rating-icon.png");
            this.largeButtonImageList.Images.SetKeyName(2, "Properties_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(3, "Refresh_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(4, "New_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(5, "Zoom_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(6, "loadCDT.png");
            this.largeButtonImageList.Images.SetKeyName(7, "saveCDT.png");
            this.largeButtonImageList.Images.SetKeyName(8, "Loadlayout.png");
            this.largeButtonImageList.Images.SetKeyName(9, "SaveLayout.png");
            this.largeButtonImageList.Images.SetKeyName(10, "Cancel_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(11, "Close_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(12, "Question_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(13, "Find_32x32.png");
            this.largeButtonImageList.Images.SetKeyName(14, "Notes_32x32.png");
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // accordionControl1
            // 
            this.accordionControl1.AllowItemSelection = true;
            this.accordionControl1.Appearance.AccordionControl.BackColor = System.Drawing.Color.LightGray;
            this.accordionControl1.Appearance.AccordionControl.Options.UseBackColor = true;
            this.accordionControl1.Appearance.Item.Hovered.BackColor = System.Drawing.Color.LightSkyBlue;
            this.accordionControl1.Appearance.Item.Hovered.Options.UseBackColor = true;
            this.accordionControl1.Appearance.Item.Pressed.BackColor = System.Drawing.Color.LightGray;
            this.accordionControl1.Appearance.Item.Pressed.Options.UseBackColor = true;
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement2,
            this.accordionControlElement3,
            this.accordionControlElement4});
            this.accordionControl1.Location = new System.Drawing.Point(0, 32);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
            this.accordionControl1.Size = new System.Drawing.Size(48, 698);
            this.accordionControl1.TabIndex = 10;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.accordionControl1.Click += new System.EventHandler(this.accordionControl1_Click);
            this.accordionControl1.MouseHover += new System.EventHandler(this.accordionControl1_MouseHover);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement1.Appearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.accordionControlElement1.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement1.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Left),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Right)});
            this.accordionControlElement1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accordionControlElement1.ImageOptions.SvgImage")));
            this.accordionControlElement1.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement1.Tag = "SYSTEM";
            this.accordionControlElement1.Text = "SYSTEM";
            this.accordionControlElement1.Click += new System.EventHandler(this.accordionControlElement1_Click);
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement2.Appearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.accordionControlElement2.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement2.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement2.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Right)});
            this.accordionControlElement2.ImageOptions.Image = global::Wisol.MES.Properties.Resources.icons8_scan_stock_32;
            this.accordionControlElement2.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement2.Tag = "CONTENT";
            this.accordionControlElement2.Text = "CONTENT";
            this.accordionControlElement2.Click += new System.EventHandler(this.accordionControlElement2_Click);
            // 
            // accordionControlElement3
            // 
            this.accordionControlElement3.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement3.Appearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.accordionControlElement3.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement3.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement3.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Right)});
            this.accordionControlElement3.ImageOptions.Image = global::Wisol.MES.Properties.Resources.icons8_combo_chart_32;
            this.accordionControlElement3.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            this.accordionControlElement3.Name = "accordionControlElement3";
            this.accordionControlElement3.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement3.Tag = "REPORT";
            this.accordionControlElement3.Text = "REPORT";
            this.accordionControlElement3.Click += new System.EventHandler(this.accordionControlElement3_Click);
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.accordionControlElement4.Appearance.Normal.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.accordionControlElement4.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement4.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement4.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text, DevExpress.XtraBars.Navigation.HeaderElementAlignment.Right)});
            this.accordionControlElement4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("accordionControlElement4.ImageOptions.Image")));
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement4.Tag = "EQUIPMENT";
            this.accordionControlElement4.Text = "EQUIPMENT";
            this.accordionControlElement4.Click += new System.EventHandler(this.accordionControlElement4_Click);
            // 
            // tabForm
            // 
            this.tabForm.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tabForm.Appearance.ForeColor = System.Drawing.Color.White;
            this.tabForm.Appearance.Options.UseBackColor = true;
            this.tabForm.Appearance.Options.UseForeColor = true;
            this.tabForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabForm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tabForm.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.tabForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabForm.HeaderButtons = ((DevExpress.XtraTab.TabButtons)((((DevExpress.XtraTab.TabButtons.Prev | DevExpress.XtraTab.TabButtons.Next) 
            | DevExpress.XtraTab.TabButtons.Close) 
            | DevExpress.XtraTab.TabButtons.Default)));
            this.tabForm.Location = new System.Drawing.Point(48, 32);
            this.tabForm.Name = "tabForm";
            this.tabForm.Size = new System.Drawing.Size(960, 698);
            this.tabForm.TabIndex = 11;
            this.tabForm.TabStop = false;
            this.tabForm.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabForm_SelectedPageChanged);
            this.tabForm.CloseButtonClick += new System.EventHandler(this.tabForm_CloseButtonClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.RibbonCaptionAlignment = DevExpress.XtraBars.Ribbon.RibbonCaptionAlignment.Center;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(1008, 32);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tabForm);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("MainForm.IconOptions.Icon")));
            this.IconOptions.Image = global::Wisol.MES.Properties.Resources.bdcd801c_194a_44e0_b02d_a3164d977a3c_200x200;
            this.InactiveGlowColor = System.Drawing.Color.Black;
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_SearchMenuEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarLargeButtonItem barLogin;
        private DevExpress.XtraBars.BarLargeButtonItem barFavorite;
        private DevExpress.XtraBars.BarLargeButtonItem barNewExcute;
        private DevExpress.XtraBars.BarLargeButtonItem barSettings;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnInit;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnClose;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnCloseAll;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem6;
        private System.Windows.Forms.ImageList largeButtonImageList;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnSearch;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnSaveCDT;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnLoadCDT;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnLoadLayout;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnSaveLayout;
        public DevExpress.XtraSplashScreen.SplashScreenManager m_SplashScreenManager;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnManual;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarLargeButtonItem barBtnGo;
        private DevExpress.XtraBars.BarLargeButtonItem barUsr;
        private DevExpress.XtraBars.SkinBarSubItem barSkin;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarStaticItem myTextNotify;
        private DevExpress.XtraBars.BarEditItem m_SearchMenuBarItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchControl m_SearchMenuEdit;
        private DevExpress.XtraTab.XtraTabControl tabForm;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.BarStaticItem myTextVersion;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItem2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
    }
}

