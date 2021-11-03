﻿
namespace Wisol.MES.Forms.SETTING.POP
{
    partial class POP_CANCEL_STOCKIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POP_CANCEL_STOCKIN));
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.btnSave = new Wisol.XSimpleButton(this.components);
            this.txtSave = new Wisol.XSimpleButton(this.components);
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCost = new DevExpress.XtraEditors.TextEdit();
            this.txtUnit = new DevExpress.XtraEditors.TextEdit();
            this.txtQuantityStockin = new DevExpress.XtraEditors.TextEdit();
            this.sltMaterial = new Wisol.AceGridLookUpEdit(this.components);
            this.aceGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtValidDate = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.CODE = new DevExpress.XtraLayout.LayoutControlItem();
            this.COST = new DevExpress.XtraLayout.LayoutControlItem();
            this.QUANTITYONHAND = new DevExpress.XtraLayout.LayoutControlItem();
            this.MATERIAL = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.NAME = new DevExpress.XtraLayout.LayoutControlItem();
            this.UNIT = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.QUANTITYONHAND1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantityStockin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sltMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValidDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CODE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.COST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QUANTITYONHAND)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MATERIAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UNIT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QUANTITYONHAND1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.btnSave);
            this.xLayoutControl1.Controls.Add(this.txtSave);
            this.xLayoutControl1.Controls.Add(this.txtCode);
            this.xLayoutControl1.Controls.Add(this.txtName);
            this.xLayoutControl1.Controls.Add(this.txtCost);
            this.xLayoutControl1.Controls.Add(this.txtUnit);
            this.xLayoutControl1.Controls.Add(this.txtQuantityStockin);
            this.xLayoutControl1.Controls.Add(this.sltMaterial);
            this.xLayoutControl1.Controls.Add(this.txtValidDate);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.xLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(824, 388, 650, 400);
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(517, 341);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(376, 299);
            this.btnSave.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 30);
            this.btnSave.StyleController = this.xLayoutControl1;
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSave
            // 
            this.txtSave.Location = new System.Drawing.Point(12, 157);
            this.txtSave.MaximumSize = new System.Drawing.Size(129, 30);
            this.txtSave.MinimumSize = new System.Drawing.Size(129, 30);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(129, 30);
            this.txtSave.StyleController = this.xLayoutControl1;
            this.txtSave.TabIndex = 11;
            this.txtSave.Text = "SAVE";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(119, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(137, 20);
            this.txtCode.StyleController = this.xLayoutControl1;
            this.txtCode.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(367, 12);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(138, 20);
            this.txtName.StyleController = this.xLayoutControl1;
            this.txtName.TabIndex = 7;
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(119, 36);
            this.txtCost.Name = "txtCost";
            this.txtCost.Properties.DisplayFormat.FormatString = "{0:#,##0.00}";
            this.txtCost.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCost.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCost.Properties.Mask.EditMask = "n";
            this.txtCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCost.Properties.ReadOnly = true;
            this.txtCost.Size = new System.Drawing.Size(137, 20);
            this.txtCost.StyleController = this.xLayoutControl1;
            this.txtCost.TabIndex = 8;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(367, 36);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Properties.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(138, 20);
            this.txtUnit.StyleController = this.xLayoutControl1;
            this.txtUnit.TabIndex = 9;
            // 
            // txtQuantityStockin
            // 
            this.txtQuantityStockin.Location = new System.Drawing.Point(119, 60);
            this.txtQuantityStockin.Name = "txtQuantityStockin";
            this.txtQuantityStockin.Properties.ReadOnly = true;
            this.txtQuantityStockin.Size = new System.Drawing.Size(137, 20);
            this.txtQuantityStockin.StyleController = this.xLayoutControl1;
            this.txtQuantityStockin.TabIndex = 7;
            // 
            // sltMaterial
            // 
            this.sltMaterial.EditValue = "";
            this.sltMaterial.Location = new System.Drawing.Point(119, 85);
            this.sltMaterial.Name = "sltMaterial";
            this.sltMaterial.Properties.Appearance.Options.UseTextOptions = true;
            this.sltMaterial.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sltMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sltMaterial.Properties.ImmediatePopup = true;
            this.sltMaterial.Properties.NullText = "";
            this.sltMaterial.Properties.PopupView = this.aceGridLookUpEdit1View;
            this.sltMaterial.Size = new System.Drawing.Size(386, 20);
            this.sltMaterial.StyleController = this.xLayoutControl1;
            this.sltMaterial.TabIndex = 1;
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
            // txtValidDate
            // 
            this.txtValidDate.Location = new System.Drawing.Point(367, 60);
            this.txtValidDate.Name = "txtValidDate";
            this.txtValidDate.Properties.ReadOnly = true;
            this.txtValidDate.Size = new System.Drawing.Size(138, 20);
            this.txtValidDate.StyleController = this.xLayoutControl1;
            this.txtValidDate.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtSave;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 145);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(605, 179);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.CODE,
            this.COST,
            this.QUANTITYONHAND,
            this.MATERIAL,
            this.simpleSeparator1,
            this.NAME,
            this.UNIT,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.QUANTITYONHAND1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(517, 341);
            this.Root.TextVisible = false;
            // 
            // CODE
            // 
            this.CODE.Control = this.txtCode;
            this.CODE.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.CODE.CustomizationFormText = "CODE";
            this.CODE.Location = new System.Drawing.Point(0, 0);
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(248, 24);
            this.CODE.TextLocation = DevExpress.Utils.Locations.Left;
            this.CODE.TextSize = new System.Drawing.Size(104, 13);
            // 
            // COST
            // 
            this.COST.Control = this.txtCost;
            this.COST.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.COST.CustomizationFormText = "COST";
            this.COST.Location = new System.Drawing.Point(0, 24);
            this.COST.Name = "COST";
            this.COST.Size = new System.Drawing.Size(248, 24);
            this.COST.TextLocation = DevExpress.Utils.Locations.Left;
            this.COST.TextSize = new System.Drawing.Size(104, 13);
            // 
            // QUANTITYONHAND
            // 
            this.QUANTITYONHAND.Control = this.txtQuantityStockin;
            this.QUANTITYONHAND.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.QUANTITYONHAND.CustomizationFormText = "QUANTITY_ STOCKIN";
            this.QUANTITYONHAND.Location = new System.Drawing.Point(0, 48);
            this.QUANTITYONHAND.Name = "QUANTITYONHAND";
            this.QUANTITYONHAND.Size = new System.Drawing.Size(248, 24);
            this.QUANTITYONHAND.Text = "QUANTITY_ STOCKIN";
            this.QUANTITYONHAND.TextSize = new System.Drawing.Size(104, 13);
            // 
            // MATERIAL
            // 
            this.MATERIAL.Control = this.sltMaterial;
            this.MATERIAL.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.MATERIAL.CustomizationFormText = "MATERIAL";
            this.MATERIAL.Location = new System.Drawing.Point(0, 73);
            this.MATERIAL.Name = "MATERIAL";
            this.MATERIAL.Size = new System.Drawing.Size(497, 24);
            this.MATERIAL.TextSize = new System.Drawing.Size(104, 13);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 72);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(497, 1);
            // 
            // NAME
            // 
            this.NAME.Control = this.txtName;
            this.NAME.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.NAME.CustomizationFormText = "NAME";
            this.NAME.Location = new System.Drawing.Point(248, 0);
            this.NAME.Name = "NAME";
            this.NAME.Size = new System.Drawing.Size(249, 24);
            this.NAME.TextLocation = DevExpress.Utils.Locations.Left;
            this.NAME.TextSize = new System.Drawing.Size(104, 13);
            // 
            // UNIT
            // 
            this.UNIT.Control = this.txtUnit;
            this.UNIT.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.UNIT.CustomizationFormText = "UNIT";
            this.UNIT.Location = new System.Drawing.Point(248, 24);
            this.UNIT.Name = "UNIT";
            this.UNIT.Size = new System.Drawing.Size(249, 24);
            this.UNIT.TextLocation = DevExpress.Utils.Locations.Left;
            this.UNIT.TextSize = new System.Drawing.Size(104, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 97);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(497, 190);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            this.layoutControlItem2.Location = new System.Drawing.Point(364, 287);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(133, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 287);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(364, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // QUANTITYONHAND1
            // 
            this.QUANTITYONHAND1.Control = this.txtValidDate;
            this.QUANTITYONHAND1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.QUANTITYONHAND1.CustomizationFormText = "IMPORT_DATE";
            this.QUANTITYONHAND1.Location = new System.Drawing.Point(248, 48);
            this.QUANTITYONHAND1.Name = "QUANTITYONHAND1";
            this.QUANTITYONHAND1.Size = new System.Drawing.Size(249, 24);
            this.QUANTITYONHAND1.Text = "IMPORT_DATE";
            this.QUANTITYONHAND1.TextSize = new System.Drawing.Size(104, 13);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // POP_CANCEL_STOCKIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 341);
            this.Controls.Add(this.xLayoutControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("POP_CANCEL_STOCKIN.IconOptions.Icon")));
            this.Name = "POP_CANCEL_STOCKIN";
            this.Text = "POP_CANCEL_STOCKIN";
            this.Controls.SetChildIndex(this.xLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantityStockin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sltMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValidDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CODE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.COST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QUANTITYONHAND)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MATERIAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UNIT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QUANTITYONHAND1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XLayoutControl xLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlItem CODE;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlItem NAME;
        private DevExpress.XtraEditors.TextEdit txtCost;
        private DevExpress.XtraLayout.LayoutControlItem COST;
        private DevExpress.XtraEditors.TextEdit txtUnit;
        private DevExpress.XtraLayout.LayoutControlItem UNIT;
        private DevExpress.XtraEditors.TextEdit txtQuantityStockin;
        private DevExpress.XtraLayout.LayoutControlItem QUANTITYONHAND;
        private AceGridLookUpEdit sltMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView aceGridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem MATERIAL;
        private XSimpleButton txtSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private XSimpleButton btnSave;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit txtValidDate;
        private DevExpress.XtraLayout.LayoutControlItem QUANTITYONHAND1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}