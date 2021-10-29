
namespace Wisol.MES.Forms.SETTING
{
    partial class SETTING008
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SETTING008));
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.txtDesc = new DevExpress.XtraEditors.MemoEdit();
            this.btnDelete = new Wisol.XSimpleButton(this.components);
            this.btnCancel = new Wisol.XSimpleButton(this.components);
            this.btnSave = new Wisol.XSimpleButton(this.components);
            this.txtCode = new Wisol.XTextEdit();
            this.txtName = new Wisol.XTextEdit();
            this.gcList = new Wisol.XGridControl();
            this.gvList = new Wisol.XGridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgList = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgInputData = new DevExpress.XtraLayout.LayoutControlGroup();
            this.NAME = new DevExpress.XtraLayout.LayoutControlItem();
            this.CODE = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.DESC = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInputData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CODE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DESC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.txtDesc);
            this.xLayoutControl1.Controls.Add(this.btnDelete);
            this.xLayoutControl1.Controls.Add(this.btnCancel);
            this.xLayoutControl1.Controls.Add(this.btnSave);
            this.xLayoutControl1.Controls.Add(this.txtCode);
            this.xLayoutControl1.Controls.Add(this.txtName);
            this.xLayoutControl1.Controls.Add(this.gcList);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(345, 333, 650, 400);
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(1166, 767);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(967, 93);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(175, 111);
            this.txtDesc.StyleController = this.xLayoutControl1;
            this.txtDesc.TabIndex = 13;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(1025, 725);
            this.btnDelete.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(129, 30);
            this.btnDelete.StyleController = this.xLayoutControl1;
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(892, 725);
            this.btnCancel.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnCancel.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 30);
            this.btnCancel.StyleController = this.xLayoutControl1;
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(759, 725);
            this.btnSave.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 30);
            this.btnSave.StyleController = this.xLayoutControl1;
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(967, 45);
            this.txtCode.Name = "txtCode";
            this.txtCode.NullBackColor = System.Drawing.Color.Lime;
            this.txtCode.NullValidation = false;
            this.txtCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtCode.Size = new System.Drawing.Size(175, 20);
            this.txtCode.StyleController = this.xLayoutControl1;
            this.txtCode.TabIndex = 8;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(967, 69);
            this.txtName.Name = "txtName";
            this.txtName.NullBackColor = System.Drawing.Color.Lime;
            this.txtName.NullValidation = false;
            this.txtName.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Size = new System.Drawing.Size(175, 20);
            this.txtName.StyleController = this.xLayoutControl1;
            this.txtName.TabIndex = 7;
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(24, 45);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(824, 664);
            this.gcList.TabIndex = 6;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvList_RowCellClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgList,
            this.lcgInputData,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1166, 767);
            // 
            // lcgList
            // 
            this.lcgList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.lcgList.Location = new System.Drawing.Point(0, 0);
            this.lcgList.Name = "lcgList";
            this.lcgList.Size = new System.Drawing.Size(852, 713);
            this.lcgList.Text = "LIST";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(828, 668);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcgInputData
            // 
            this.lcgInputData.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.NAME,
            this.CODE,
            this.emptySpaceItem1,
            this.DESC});
            this.lcgInputData.Location = new System.Drawing.Point(852, 0);
            this.lcgInputData.Name = "lcgInputData";
            this.lcgInputData.OptionsTableLayoutItem.ColumnIndex = 1;
            this.lcgInputData.Size = new System.Drawing.Size(294, 713);
            this.lcgInputData.Text = "INPUT_DATA";
            // 
            // NAME
            // 
            this.NAME.Control = this.txtName;
            this.NAME.Location = new System.Drawing.Point(0, 24);
            this.NAME.Name = "NAME";
            this.NAME.Size = new System.Drawing.Size(270, 24);
            this.NAME.Text = "NAME_CATEGORY";
            this.NAME.TextSize = new System.Drawing.Size(88, 13);
            // 
            // CODE
            // 
            this.CODE.Control = this.txtCode;
            this.CODE.Location = new System.Drawing.Point(0, 0);
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(270, 24);
            this.CODE.TextSize = new System.Drawing.Size(88, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 163);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(270, 505);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DESC
            // 
            this.DESC.Control = this.txtDesc;
            this.DESC.Location = new System.Drawing.Point(0, 48);
            this.DESC.Name = "DESC";
            this.DESC.Size = new System.Drawing.Size(270, 115);
            this.DESC.TextSize = new System.Drawing.Size(88, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 713);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(747, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            this.layoutControlItem5.CustomizationFormText = "SAVE";
            this.layoutControlItem5.Location = new System.Drawing.Point(747, 713);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(133, 34);
            this.layoutControlItem5.Text = "SAVE";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "CANCEL";
            this.layoutControlItem6.Location = new System.Drawing.Point(880, 713);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(133, 34);
            this.layoutControlItem6.Text = "CANCEL";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnDelete;
            this.layoutControlItem7.CustomizationFormText = "DELETE";
            this.layoutControlItem7.Location = new System.Drawing.Point(1013, 713);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(133, 34);
            this.layoutControlItem7.Text = "DELETE";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // SETTING008
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xLayoutControl1);
            this.Name = "SETTING008";
            this.Size = new System.Drawing.Size(1166, 767);
            this.Controls.SetChildIndex(this.xLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInputData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CODE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DESC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XLayoutControl xLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private XTextEdit txtCode;
        private XTextEdit txtName;
        private XGridControl gcList;
        private XGridView gvList;
        private DevExpress.XtraLayout.LayoutControlGroup lcgList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInputData;
        private DevExpress.XtraLayout.LayoutControlItem NAME;
        private DevExpress.XtraLayout.LayoutControlItem CODE;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private XSimpleButton btnDelete;
        private XSimpleButton btnCancel;
        private XSimpleButton btnSave;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.MemoEdit txtDesc;
        private DevExpress.XtraLayout.LayoutControlItem DESC;
    }
}
