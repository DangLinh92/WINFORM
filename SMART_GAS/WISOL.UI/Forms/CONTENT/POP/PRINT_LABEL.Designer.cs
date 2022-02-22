
namespace Wisol.MES.Forms.CONTENT.POP
{
    partial class PRINT_LABEL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRINT_LABEL));
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.cboPrinter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnPrint = new Wisol.XSimpleButton(this.components);
            this.stlGas = new Wisol.AceGridLookUpEdit(this.components);
            this.aceGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtQuantity = new Wisol.XTextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stlGas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.txtQuantity);
            this.xLayoutControl1.Controls.Add(this.cboPrinter);
            this.xLayoutControl1.Controls.Add(this.btnPrint);
            this.xLayoutControl1.Controls.Add(this.stlGas);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(631, 239);
            this.xLayoutControl1.TabIndex = 4;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // cboPrinter
            // 
            this.cboPrinter.Location = new System.Drawing.Point(82, 36);
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPrinter.Size = new System.Drawing.Size(537, 20);
            this.cboPrinter.StyleController = this.xLayoutControl1;
            this.cboPrinter.TabIndex = 6;
            this.cboPrinter.SelectedIndexChanged += new System.EventHandler(this.cboPrinter_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.FormId = null;
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.isFormType = false;
            this.btnPrint.Location = new System.Drawing.Point(12, 84);
            this.btnPrint.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnPrint.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(129, 30);
            this.btnPrint.StyleController = this.xLayoutControl1;
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // stlGas
            // 
            this.stlGas.EditValue = "";
            this.stlGas.Location = new System.Drawing.Point(82, 12);
            this.stlGas.Name = "stlGas";
            this.stlGas.Properties.Appearance.Options.UseTextOptions = true;
            this.stlGas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.stlGas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.stlGas.Properties.ImmediatePopup = true;
            this.stlGas.Properties.NullText = "";
            this.stlGas.Properties.PopupView = this.aceGridLookUpEdit1View;
            this.stlGas.Size = new System.Drawing.Size(537, 20);
            this.stlGas.StyleController = this.xLayoutControl1;
            this.stlGas.TabIndex = 4;
            // 
            // aceGridLookUpEdit1View
            // 
            this.aceGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.aceGridLookUpEdit1View.Name = "aceGridLookUpEdit1View";
            this.aceGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.aceGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.aceGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(631, 239);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.stlGas;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(611, 24);
            this.layoutControlItem1.Text = "Mã Khí Gas";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(58, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 106);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(611, 113);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnPrint;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(611, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cboPrinter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(611, 24);
            this.layoutControlItem3.Text = "PRINTER";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(58, 13);
            // 
            // txtQuantity
            // 
            this.txtQuantity.IsRequire = false;
            this.txtQuantity.Location = new System.Drawing.Point(82, 60);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.NullBackColor = System.Drawing.Color.Lime;
            this.txtQuantity.NullValidation = false;
            this.txtQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtQuantity.Properties.Appearance.Options.UseBackColor = true;
            this.txtQuantity.Size = new System.Drawing.Size(537, 20);
            this.txtQuantity.StyleController = this.xLayoutControl1;
            this.txtQuantity.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtQuantity;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(611, 24);
            this.layoutControlItem4.Text = "Số Lượng In";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(58, 13);
            // 
            // PRINT_LABEL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 239);
            this.Controls.Add(this.xLayoutControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("PRINT_LABEL.IconOptions.Icon")));
            this.Name = "PRINT_LABEL";
            this.Text = "PRINT_LABEL";
            this.Controls.SetChildIndex(this.xLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboPrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stlGas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XLayoutControl xLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private AceGridLookUpEdit stlGas;
        private DevExpress.XtraGrid.Views.Grid.GridView aceGridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private XSimpleButton btnPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.ComboBoxEdit cboPrinter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private XTextEdit txtQuantity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}