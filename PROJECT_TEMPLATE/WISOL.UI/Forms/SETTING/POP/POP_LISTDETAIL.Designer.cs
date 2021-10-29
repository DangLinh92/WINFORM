
namespace Wisol.MES.Forms.SETTING.POP
{
    partial class POP_LISTDETAIL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POP_LISTDETAIL));
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.gcStockOut = new Wisol.XGridControl();
            this.gvStockOut = new Wisol.XGridView();
            this.xLayoutControl2 = new Wisol.XLayoutControl();
            this.sltMaterial = new Wisol.AceGridLookUpEdit(this.components);
            this.aceGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcStockIn = new Wisol.XGridControl();
            this.gvStockIn = new Wisol.XGridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl2)).BeginInit();
            this.xLayoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sltMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            this.SuspendLayout();
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.gcStockOut);
            this.xLayoutControl1.Controls.Add(this.xLayoutControl2);
            this.xLayoutControl1.Controls.Add(this.gcStockIn);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(386, 300, 650, 400);
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(1312, 662);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // gcStockOut
            // 
            this.gcStockOut.Location = new System.Drawing.Point(24, 367);
            this.gcStockOut.MainView = this.gvStockOut;
            this.gcStockOut.Name = "gcStockOut";
            this.gcStockOut.Size = new System.Drawing.Size(1264, 271);
            this.gcStockOut.TabIndex = 6;
            this.gcStockOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockOut});
            // 
            // gvStockOut
            // 
            this.gvStockOut.GridControl = this.gcStockOut;
            this.gvStockOut.Name = "gvStockOut";
            // 
            // xLayoutControl2
            // 
            this.xLayoutControl2.Controls.Add(this.sltMaterial);
            this.xLayoutControl2.Location = new System.Drawing.Point(12, 12);
            this.xLayoutControl2.Name = "xLayoutControl2";
            this.xLayoutControl2.Root = this.layoutControlGroup1;
            this.xLayoutControl2.Size = new System.Drawing.Size(1288, 44);
            this.xLayoutControl2.TabIndex = 5;
            this.xLayoutControl2.Text = "xLayoutControl2";
            // 
            // sltMaterial
            // 
            this.sltMaterial.EditValue = "";
            this.sltMaterial.Location = new System.Drawing.Point(65, 12);
            this.sltMaterial.Name = "sltMaterial";
            this.sltMaterial.Properties.Appearance.Options.UseTextOptions = true;
            this.sltMaterial.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sltMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sltMaterial.Properties.ImmediatePopup = true;
            this.sltMaterial.Properties.NullText = "";
            this.sltMaterial.Properties.PopupView = this.aceGridLookUpEdit1View;
            this.sltMaterial.Size = new System.Drawing.Size(354, 20);
            this.sltMaterial.StyleController = this.xLayoutControl2;
            this.sltMaterial.TabIndex = 4;
            this.sltMaterial.EditValueChanged += new System.EventHandler(this.sltMaterial_EditValueChanged);
            this.sltMaterial.Validating += new System.ComponentModel.CancelEventHandler(this.sltMaterial_Validating);
            // 
            // aceGridLookUpEdit1View
            // 
            this.aceGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.aceGridLookUpEdit1View.Name = "aceGridLookUpEdit1View";
            this.aceGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.aceGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.aceGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1288, 44);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(411, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(857, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sltMaterial;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(411, 24);
            this.layoutControlItem3.Text = "MATERIAL";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 13);
            // 
            // gcStockIn
            // 
            this.gcStockIn.Location = new System.Drawing.Point(24, 93);
            this.gcStockIn.MainView = this.gvStockIn;
            this.gcStockIn.Name = "gcStockIn";
            this.gcStockIn.Size = new System.Drawing.Size(1264, 225);
            this.gcStockIn.TabIndex = 4;
            this.gcStockIn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockIn});
            // 
            // gvStockIn
            // 
            this.gvStockIn.GridControl = this.gcStockIn;
            this.gvStockIn.Name = "gvStockIn";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1312, 662);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcStockIn;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1268, 229);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.xLayoutControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsTableLayoutItem.ColumnIndex = 1;
            this.layoutControlItem2.Size = new System.Drawing.Size(1292, 48);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcStockOut;
            this.layoutControlItem4.CustomizationFormText = "DETAIL_STOCKOUT";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.OptionsTableLayoutItem.RowIndex = 1;
            this.layoutControlItem4.Size = new System.Drawing.Size(1268, 275);
            this.layoutControlItem4.Text = "DETAIL_STOCKOUT";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "DETAIL_STOCKIN";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1292, 274);
            this.layoutControlGroup2.Text = "DETAIL_STOCKIN";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "DETAIL_STOCKOUT";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 322);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1292, 320);
            this.layoutControlGroup3.Text = "DETAIL_STOCKOUT";
            // 
            // POP_LISTDETAIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 662);
            this.Controls.Add(this.xLayoutControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("POP_LISTDETAIL.IconOptions.Icon")));
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("POP_LISTDETAIL.IconOptions.Image")));
            this.Name = "POP_LISTDETAIL";
            this.Text = "POP_LISTDETAIL";
            this.Controls.SetChildIndex(this.xLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStockOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl2)).EndInit();
            this.xLayoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sltMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XLayoutControl xLayoutControl1;
        private XGridControl gcStockIn;
        private XGridView gvStockIn;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private XLayoutControl xLayoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private AceGridLookUpEdit sltMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView aceGridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private XGridControl gcStockOut;
        private XGridView gvStockOut;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
    }
}