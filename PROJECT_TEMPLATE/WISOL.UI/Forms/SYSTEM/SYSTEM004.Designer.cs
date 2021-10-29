namespace Wisol.MES.Forms.SYSTEM
{
    partial class SYSTEM004
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SYSTEM004));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tlMenuList = new DevExpress.XtraTreeList.TreeList();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.cheAll = new DevExpress.XtraEditors.CheckEdit();
            this.cheUncheckAll = new DevExpress.XtraEditors.CheckEdit();
            this.gcList = new Wisol.XGridControl();
            this.gvList = new Wisol.XGridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcRoleList = new DevExpress.XtraGrid.GridControl();
            this.gvRoleList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSave = new Wisol.XSimpleButton(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlMenuList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cheAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheUncheckAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRoleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tlMenuList);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Controls.Add(this.gcRoleList);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(800, 398, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1366, 768);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tlMenuList
            // 
            this.tlMenuList.Location = new System.Drawing.Point(356, 45);
            this.tlMenuList.Name = "tlMenuList";
            this.tlMenuList.Size = new System.Drawing.Size(571, 665);
            this.tlMenuList.TabIndex = 21;
            this.tlMenuList.RowCellClick += new DevExpress.XtraTreeList.RowCellClickEventHandler(this.tlMenuList_RowCellClick);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xLayoutControl1);
            this.groupControl1.Location = new System.Drawing.Point(943, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(411, 710);
            this.groupControl1.TabIndex = 22;
            this.groupControl1.Text = "Controls";
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.cheAll);
            this.xLayoutControl1.Controls.Add(this.cheUncheckAll);
            this.xLayoutControl1.Controls.Add(this.gcList);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(2, 23);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(407, 685);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // cheAll
            // 
            this.cheAll.Location = new System.Drawing.Point(12, 12);
            this.cheAll.Name = "cheAll";
            this.cheAll.Properties.Caption = "Check All";
            this.cheAll.Size = new System.Drawing.Size(200, 20);
            this.cheAll.StyleController = this.xLayoutControl1;
            this.cheAll.TabIndex = 5;
            this.cheAll.CheckedChanged += new System.EventHandler(this.cheAll_CheckedChanged);
            // 
            // cheUncheckAll
            // 
            this.cheUncheckAll.Location = new System.Drawing.Point(216, 12);
            this.cheUncheckAll.Name = "cheUncheckAll";
            this.cheUncheckAll.Properties.Caption = "Uncheck All";
            this.cheUncheckAll.Size = new System.Drawing.Size(179, 20);
            this.cheUncheckAll.StyleController = this.xLayoutControl1;
            this.cheUncheckAll.TabIndex = 4;
            this.cheUncheckAll.CheckedChanged += new System.EventHandler(this.cheUncheckAll_CheckedChanged);
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(12, 36);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(383, 637);
            this.gcList.TabIndex = 0;
            this.gcList.UseEmbeddedNavigator = true;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(407, 685);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcList;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(387, 641);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cheUncheckAll;
            this.layoutControlItem5.Location = new System.Drawing.Point(204, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(183, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cheAll;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(204, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // gcRoleList
            // 
            this.gcRoleList.Location = new System.Drawing.Point(24, 45);
            this.gcRoleList.MainView = this.gvRoleList;
            this.gcRoleList.Name = "gcRoleList";
            this.gcRoleList.Size = new System.Drawing.Size(294, 665);
            this.gcRoleList.TabIndex = 20;
            this.gcRoleList.UseEmbeddedNavigator = true;
            this.gcRoleList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRoleList});
            // 
            // gvRoleList
            // 
            this.gvRoleList.GridControl = this.gcRoleList;
            this.gvRoleList.Name = "gvRoleList";
            this.gvRoleList.OptionsSelection.MultiSelect = true;
            this.gvRoleList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvRoleList.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
            this.gvRoleList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvRoleList_RowCellClick);
            // 
            // btnSave
            // 
            this.btnSave.ButtonType = Wisol.ButtonTypes.Save;
            this.btnSave.FormId = null;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.isFormType = false;
            this.btnSave.Location = new System.Drawing.Point(1225, 726);
            this.btnSave.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 30);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem13,
            this.emptySpaceItem2,
            this.layoutControlGroup3,
            this.splitterItem1,
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1366, 768);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "LIST";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(322, 714);
            this.layoutControlGroup2.Text = "LIST";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcRoleList;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(298, 669);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnSave;
            this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
            this.layoutControlItem13.Location = new System.Drawing.Point(1213, 714);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(133, 34);
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 714);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1213, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "MENU_LIST";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(332, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(599, 714);
            this.layoutControlGroup3.Text = "MENU_LIST";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tlMenuList;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(575, 669);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(322, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(10, 714);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.groupControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(931, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(415, 714);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // SYSTEM004
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.layoutControl1);
            this.Name = "SYSTEM004";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlMenuList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cheAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheUncheckAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRoleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private Wisol.XSimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gcRoleList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRoleList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraTreeList.TreeList tlMenuList;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private XGridControl gcList;
        private XGridView gvList;
        private XLayoutControl xLayoutControl1;
        private DevExpress.XtraEditors.CheckEdit cheAll;
        private DevExpress.XtraEditors.CheckEdit cheUncheckAll;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}