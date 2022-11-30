
namespace Wisol.MES.Forms.REPORT
{
    partial class REPORT_DRAFT
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.sltCategory = new Wisol.AceGridLookUpEdit(this.components);
            this.aceGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xLayoutControl3 = new Wisol.XLayoutControl();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.xGridControl1 = new Wisol.XGridControl();
            this.xGridView1 = new Wisol.XGridView();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtStartDate = new DevExpress.XtraEditors.DateEdit();
            this.dtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sltCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl3)).BeginInit();
            this.xLayoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.sltCategory);
            this.xLayoutControl1.Controls.Add(this.xLayoutControl3);
            this.xLayoutControl1.Controls.Add(this.dtStartDate);
            this.xLayoutControl1.Controls.Add(this.dtEndDate);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(583, 359, 650, 400);
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(1213, 759);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // sltCategory
            // 
            this.sltCategory.EditValue = "";
            this.sltCategory.Location = new System.Drawing.Point(120, 45);
            this.sltCategory.Name = "sltCategory";
            this.sltCategory.Properties.Appearance.Options.UseTextOptions = true;
            this.sltCategory.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sltCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sltCategory.Properties.ImmediatePopup = true;
            this.sltCategory.Properties.NullText = "";
            this.sltCategory.Properties.PopupView = this.aceGridLookUpEdit1View;
            this.sltCategory.Size = new System.Drawing.Size(290, 20);
            this.sltCategory.StyleController = this.xLayoutControl1;
            this.sltCategory.TabIndex = 7;
            // 
            // aceGridLookUpEdit1View
            // 
            this.aceGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.aceGridLookUpEdit1View.Name = "aceGridLookUpEdit1View";
            this.aceGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.aceGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.aceGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // xLayoutControl3
            // 
            this.xLayoutControl3.Controls.Add(this.chart1);
            this.xLayoutControl3.Controls.Add(this.xGridControl1);
            this.xLayoutControl3.Location = new System.Drawing.Point(24, 114);
            this.xLayoutControl3.Name = "xLayoutControl3";
            this.xLayoutControl3.Root = this.layoutControlGroup2;
            this.xLayoutControl3.Size = new System.Drawing.Size(1165, 621);
            this.xLayoutControl3.TabIndex = 5;
            this.xLayoutControl3.Text = "xLayoutControl3";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1141, 345);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // xGridControl1
            // 
            this.xGridControl1.Location = new System.Drawing.Point(12, 361);
            this.xGridControl1.MainView = this.xGridView1;
            this.xGridControl1.Name = "xGridControl1";
            this.xGridControl1.Size = new System.Drawing.Size(1141, 248);
            this.xGridControl1.TabIndex = 4;
            this.xGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.xGridView1});
            // 
            // xGridView1
            // 
            this.xGridView1.GridControl = this.xGridControl1;
            this.xGridView1.Name = "xGridView1";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem6});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1165, 621);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.xGridControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 349);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1145, 252);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chart1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1145, 349);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // dtStartDate
            // 
            this.dtStartDate.EditValue = null;
            this.dtStartDate.Location = new System.Drawing.Point(510, 45);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dtStartDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartDate.Properties.DisplayFormat.FormatString = "";
            this.dtStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartDate.Properties.EditFormat.FormatString = "";
            this.dtStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartDate.Properties.Mask.EditMask = "";
            this.dtStartDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtStartDate.Size = new System.Drawing.Size(289, 20);
            this.dtStartDate.StyleController = this.xLayoutControl1;
            this.dtStartDate.TabIndex = 5;
            // 
            // dtEndDate
            // 
            this.dtEndDate.EditValue = null;
            this.dtEndDate.Location = new System.Drawing.Point(899, 45);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dtEndDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.DisplayFormat.FormatString = "";
            this.dtEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEndDate.Properties.EditFormat.FormatString = "";
            this.dtEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEndDate.Properties.Mask.EditMask = "";
            this.dtEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtEndDate.Size = new System.Drawing.Size(290, 20);
            this.dtEndDate.StyleController = this.xLayoutControl1;
            this.dtEndDate.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1213, 759);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "CONDITION";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1193, 69);
            this.layoutControlGroup3.Text = "CONDITION";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dtStartDate;
            this.layoutControlItem4.CustomizationFormText = "START_DATE";
            this.layoutControlItem4.Location = new System.Drawing.Point(390, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(389, 24);
            this.layoutControlItem4.Text = "START_DATE";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(93, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtEndDate;
            this.layoutControlItem5.CustomizationFormText = "END_DATE";
            this.layoutControlItem5.Location = new System.Drawing.Point(779, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(390, 24);
            this.layoutControlItem5.Text = "END_DATE";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(93, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.sltCategory;
            this.layoutControlItem1.CustomizationFormText = "CATEGORY_DRAFT";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(390, 24);
            this.layoutControlItem1.Text = "CATEGORY_DRAFT";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(93, 13);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 69);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1193, 670);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.xLayoutControl3;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1169, 625);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // REPORT_DRAFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xLayoutControl1);
            this.Name = "REPORT_DRAFT";
            this.Size = new System.Drawing.Size(1213, 759);
            this.Controls.SetChildIndex(this.xLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sltCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl3)).EndInit();
            this.xLayoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XLayoutControl xLayoutControl1;
        private XLayoutControl xLayoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.DateEdit dtStartDate;
        private DevExpress.XtraEditors.DateEdit dtEndDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private AceGridLookUpEdit sltCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView aceGridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private XGridControl xGridControl1;
        private XGridView xGridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
