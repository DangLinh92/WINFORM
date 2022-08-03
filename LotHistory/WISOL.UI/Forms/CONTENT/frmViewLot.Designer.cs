
using DevExpress.XtraEditors;

namespace Wisol.MES.Forms.CONTENT
{
    partial class frmViewLot
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewLot));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.txtReelId = new Wisol.XTextEdit();
            this.txtMarkingNo = new Wisol.XTextEdit();
            this.btnGetFileTemp = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new Wisol.XSimpleButton(this.components);
            this.txtLotID = new Wisol.XTextEdit();
            this.txtFileUrl = new Wisol.XTextEdit();
            this.gcList = new Wisol.XGridControl();
            this.gvList = new Wisol.XGridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Wisol.MES.FrmWaitForm), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReelId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkingNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileUrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xLayoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1685, 856);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "View Lot History";
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.radioGroup1);
            this.xLayoutControl1.Controls.Add(this.txtReelId);
            this.xLayoutControl1.Controls.Add(this.txtMarkingNo);
            this.xLayoutControl1.Controls.Add(this.btnGetFileTemp);
            this.xLayoutControl1.Controls.Add(this.btnImportFile);
            this.xLayoutControl1.Controls.Add(this.btnSearch);
            this.xLayoutControl1.Controls.Add(this.txtLotID);
            this.xLayoutControl1.Controls.Add(this.txtFileUrl);
            this.xLayoutControl1.Controls.Add(this.gcList);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(2, 23);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(1681, 831);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = 1;
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Lot Id", true, null, "cheLotId"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Marking No", true, null, "cheMarking No"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Reel Id", true, null, "cheReelId")});
            this.radioGroup1.Size = new System.Drawing.Size(372, 34);
            this.radioGroup1.StyleController = this.xLayoutControl1;
            this.radioGroup1.TabIndex = 14;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // txtReelId
            // 
            this.txtReelId.Enabled = false;
            this.txtReelId.IsRequire = false;
            this.txtReelId.Location = new System.Drawing.Point(1241, 12);
            this.txtReelId.Name = "txtReelId";
            this.txtReelId.NullBackColor = System.Drawing.Color.Lime;
            this.txtReelId.NullValidation = false;
            this.txtReelId.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtReelId.Properties.Appearance.Options.UseBackColor = true;
            this.txtReelId.Size = new System.Drawing.Size(428, 20);
            this.txtReelId.StyleController = this.xLayoutControl1;
            this.txtReelId.TabIndex = 11;
            // 
            // txtMarkingNo
            // 
            this.txtMarkingNo.Enabled = false;
            this.txtMarkingNo.IsRequire = false;
            this.txtMarkingNo.Location = new System.Drawing.Point(850, 12);
            this.txtMarkingNo.Name = "txtMarkingNo";
            this.txtMarkingNo.NullBackColor = System.Drawing.Color.Lime;
            this.txtMarkingNo.NullValidation = false;
            this.txtMarkingNo.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtMarkingNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtMarkingNo.Size = new System.Drawing.Size(322, 20);
            this.txtMarkingNo.StyleController = this.xLayoutControl1;
            this.txtMarkingNo.TabIndex = 10;
            // 
            // btnGetFileTemp
            // 
            this.btnGetFileTemp.Location = new System.Drawing.Point(12, 50);
            this.btnGetFileTemp.Name = "btnGetFileTemp";
            this.btnGetFileTemp.Size = new System.Drawing.Size(127, 22);
            this.btnGetFileTemp.StyleController = this.xLayoutControl1;
            this.btnGetFileTemp.TabIndex = 9;
            this.btnGetFileTemp.Text = "File Mẫu";
            this.btnGetFileTemp.Click += new System.EventHandler(this.btnGetFileTemp_Click);
            // 
            // btnImportFile
            // 
            this.btnImportFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFile.ImageOptions.Image")));
            this.btnImportFile.Location = new System.Drawing.Point(143, 50);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(130, 22);
            this.btnImportFile.StyleController = this.xLayoutControl1;
            this.btnImportFile.TabIndex = 8;
            this.btnImportFile.Text = "Import";
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FormId = null;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.isFormType = false;
            this.btnSearch.Location = new System.Drawing.Point(12, 76);
            this.btnSearch.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnSearch.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(129, 30);
            this.btnSearch.StyleController = this.xLayoutControl1;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLotID
            // 
            this.txtLotID.IsRequire = false;
            this.txtLotID.Location = new System.Drawing.Point(453, 12);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.NullBackColor = System.Drawing.Color.Lime;
            this.txtLotID.NullValidation = false;
            this.txtLotID.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLotID.Properties.Appearance.Options.UseBackColor = true;
            this.txtLotID.Size = new System.Drawing.Size(328, 20);
            this.txtLotID.StyleController = this.xLayoutControl1;
            this.txtLotID.TabIndex = 6;
            // 
            // txtFileUrl
            // 
            this.txtFileUrl.IsRequire = false;
            this.txtFileUrl.Location = new System.Drawing.Point(342, 50);
            this.txtFileUrl.Name = "txtFileUrl";
            this.txtFileUrl.NullBackColor = System.Drawing.Color.Lime;
            this.txtFileUrl.NullValidation = false;
            this.txtFileUrl.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtFileUrl.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileUrl.Size = new System.Drawing.Size(1327, 20);
            this.txtFileUrl.StyleController = this.xLayoutControl1;
            this.txtFileUrl.TabIndex = 5;
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(12, 110);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(1657, 709);
            this.gcList.TabIndex = 4;
            this.gcList.UseEmbeddedNavigator = true;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsView.AllowCellMerge = true;
            this.gvList.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.gvList_CellMerge);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1681, 831);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1661, 713);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtLotID;
            this.layoutControlItem3.Location = new System.Drawing.Point(376, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(397, 38);
            this.layoutControlItem3.Text = "Lot ID";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSearch;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1661, 34);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtFileUrl;
            this.layoutControlItem2.Location = new System.Drawing.Point(265, 38);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1396, 26);
            this.layoutControlItem2.Text = "File Url";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnImportFile;
            this.layoutControlItem5.Location = new System.Drawing.Point(131, 38);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnGetFileTemp;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtMarkingNo;
            this.layoutControlItem7.Location = new System.Drawing.Point(773, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(391, 38);
            this.layoutControlItem7.Text = "Marking No";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtReelId;
            this.layoutControlItem8.Location = new System.Drawing.Point(1164, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(497, 38);
            this.layoutControlItem8.Text = "Reel Id";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.radioGroup1;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(376, 38);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmViewLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "frmViewLot";
            this.Size = new System.Drawing.Size(1685, 856);
            this.Controls.SetChildIndex(this.groupControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReelId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarkingNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileUrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupControl groupControl1;
        private XLayoutControl xLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private SimpleButton btnImportFile;
        private XSimpleButton btnSearch;
        private XTextEdit txtLotID;
        private XTextEdit txtFileUrl;
        private XGridControl gcList;
        private XGridView gvList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private SimpleButton btnGetFileTemp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private XTextEdit txtReelId;
        private XTextEdit txtMarkingNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}