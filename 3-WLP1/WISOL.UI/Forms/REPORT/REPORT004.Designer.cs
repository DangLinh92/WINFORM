namespace Wisol.MES.Forms.REPORT
{
    partial class REPORT004
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(REPORT004));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cbYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbModel = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExportToExcel = new Wisol.XSimpleButton(this.components);
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.txtFromMonth = new DevExpress.XtraEditors.TextEdit();
            this.dtpFromMonth = new DevExpress.XtraEditors.DateEdit();
            this.dtpToMonth = new DevExpress.XtraEditors.DateEdit();
            this.txtToMonth = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlFromMonth = new DevExpress.XtraLayout.LayoutControlItem();
            this.controltomonth = new DevExpress.XtraLayout.LayoutControlItem();
            this.controltxtToMonth = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFromMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controltomonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controltxtToMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbYear);
            this.layoutControl1.Controls.Add(this.cbModel);
            this.layoutControl1.Controls.Add(this.gcList);
            this.layoutControl1.Controls.Add(this.btnExportToExcel);
            this.layoutControl1.Controls.Add(this.chartControl1);
            this.layoutControl1.Controls.Add(this.txtFromMonth);
            this.layoutControl1.Controls.Add(this.dtpFromMonth);
            this.layoutControl1.Controls.Add(this.dtpToMonth);
            this.layoutControl1.Controls.Add(this.txtToMonth);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 22);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1008, 371, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1116, 787);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cbYear
            // 
            this.cbYear.Location = new System.Drawing.Point(98, 49);
            this.cbYear.Name = "cbYear";
            this.cbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbYear.Size = new System.Drawing.Size(112, 20);
            this.cbYear.StyleController = this.layoutControl1;
            this.cbYear.TabIndex = 34;
            // 
            // cbModel
            // 
            this.cbModel.Location = new System.Drawing.Point(636, 49);
            this.cbModel.Name = "cbModel";
            this.cbModel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbModel.Size = new System.Drawing.Size(112, 20);
            this.cbModel.StyleController = this.layoutControl1;
            this.cbModel.TabIndex = 33;
            // 
            // gcList
            // 
            this.gcList.Location = new System.Drawing.Point(24, 565);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(1068, 198);
            this.gcList.TabIndex = 32;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.ImageOptions.Image")));
            this.btnExportToExcel.Location = new System.Drawing.Point(963, 49);
            this.btnExportToExcel.MaximumSize = new System.Drawing.Size(129, 24);
            this.btnExportToExcel.MinimumSize = new System.Drawing.Size(129, 24);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(129, 24);
            this.btnExportToExcel.StyleController = this.layoutControl1;
            this.btnExportToExcel.TabIndex = 29;
            this.btnExportToExcel.Text = "EXPORT TO EXCEL";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // chartControl1
            // 
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(24, 126);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.SeriesTemplate.TopNOptions.Mode = DevExpress.XtraCharts.TopNMode.ThresholdValue;
            this.chartControl1.Size = new System.Drawing.Size(1068, 435);
            this.chartControl1.TabIndex = 20;
            // 
            // txtFromMonth
            // 
            this.txtFromMonth.Location = new System.Drawing.Point(334, 49);
            this.txtFromMonth.MaximumSize = new System.Drawing.Size(50, 22);
            this.txtFromMonth.MinimumSize = new System.Drawing.Size(50, 22);
            this.txtFromMonth.Name = "txtFromMonth";
            this.txtFromMonth.Properties.MaxLength = 3;
            this.txtFromMonth.Size = new System.Drawing.Size(50, 22);
            this.txtFromMonth.StyleController = this.layoutControl1;
            this.txtFromMonth.TabIndex = 34;
            // 
            // dtpFromMonth
            // 
            this.dtpFromMonth.EditValue = null;
            this.dtpFromMonth.Location = new System.Drawing.Point(288, 49);
            this.dtpFromMonth.Name = "dtpFromMonth";
            this.dtpFromMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromMonth.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dtpFromMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpFromMonth.Size = new System.Drawing.Size(42, 20);
            this.dtpFromMonth.StyleController = this.layoutControl1;
            this.dtpFromMonth.TabIndex = 33;
            // 
            // dtpToMonth
            // 
            this.dtpToMonth.EditValue = null;
            this.dtpToMonth.Location = new System.Drawing.Point(462, 49);
            this.dtpToMonth.Name = "dtpToMonth";
            this.dtpToMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToMonth.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dtpToMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpToMonth.Size = new System.Drawing.Size(42, 20);
            this.dtpToMonth.StyleController = this.layoutControl1;
            this.dtpToMonth.TabIndex = 33;
            // 
            // txtToMonth
            // 
            this.txtToMonth.Location = new System.Drawing.Point(508, 49);
            this.txtToMonth.MaximumSize = new System.Drawing.Size(50, 22);
            this.txtToMonth.MinimumSize = new System.Drawing.Size(50, 22);
            this.txtToMonth.Name = "txtToMonth";
            this.txtToMonth.Properties.MaxLength = 3;
            this.txtToMonth.Size = new System.Drawing.Size(50, 22);
            this.txtToMonth.StyleController = this.layoutControl1;
            this.txtToMonth.TabIndex = 34;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup4});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1116, 787);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "SHIP_LIST";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 77);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1096, 690);
            this.layoutControlGroup2.Text = "INFO";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chartControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1072, 439);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 439);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1072, 202);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem8,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.controlFromMonth,
            this.controltomonth,
            this.controltxtToMonth});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1096, 77);
            this.layoutControlGroup4.Text = "CONDITION";
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(728, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(211, 28);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnExportToExcel;
            this.layoutControlItem8.Location = new System.Drawing.Point(939, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(133, 28);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cbModel;
            this.layoutControlItem5.Location = new System.Drawing.Point(538, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(190, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(190, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(190, 28);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "MODEL";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbYear;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(190, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(190, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(190, 28);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "YEAR";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtFromMonth;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(310, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(54, 28);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // controlFromMonth
            // 
            this.controlFromMonth.Control = this.dtpFromMonth;
            this.controlFromMonth.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controlFromMonth.CustomizationFormText = "From Month";
            this.controlFromMonth.Location = new System.Drawing.Point(190, 0);
            this.controlFromMonth.MaxSize = new System.Drawing.Size(120, 24);
            this.controlFromMonth.MinSize = new System.Drawing.Size(120, 24);
            this.controlFromMonth.Name = "controlFromMonth";
            this.controlFromMonth.Size = new System.Drawing.Size(120, 28);
            this.controlFromMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controlFromMonth.Text = "FROM_MONTH";
            this.controlFromMonth.TextSize = new System.Drawing.Size(71, 13);
            // 
            // controltomonth
            // 
            this.controltomonth.Control = this.dtpToMonth;
            this.controltomonth.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controltomonth.CustomizationFormText = "From Month";
            this.controltomonth.Location = new System.Drawing.Point(364, 0);
            this.controltomonth.MaxSize = new System.Drawing.Size(120, 24);
            this.controltomonth.MinSize = new System.Drawing.Size(120, 24);
            this.controltomonth.Name = "controltomonth";
            this.controltomonth.Size = new System.Drawing.Size(120, 28);
            this.controltomonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.controltomonth.Text = "TO_MONTH";
            this.controltomonth.TextSize = new System.Drawing.Size(71, 13);
            // 
            // controltxtToMonth
            // 
            this.controltxtToMonth.Control = this.txtToMonth;
            this.controltxtToMonth.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.controltxtToMonth.CustomizationFormText = "layoutControlItem4";
            this.controltxtToMonth.Location = new System.Drawing.Point(484, 0);
            this.controltxtToMonth.Name = "controltxtToMonth";
            this.controltxtToMonth.Size = new System.Drawing.Size(54, 28);
            this.controltxtToMonth.Text = "layoutControlItem4";
            this.controltxtToMonth.TextSize = new System.Drawing.Size(0, 0);
            this.controltxtToMonth.TextVisible = false;
            // 
            // REPORT004
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.layoutControl1);
            this.Name = "REPORT004";
            this.Size = new System.Drawing.Size(1116, 809);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlFromMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controltomonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controltxtToMonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private XSimpleButton btnExportToExcel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.ComboBoxEdit cbModel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.ComboBoxEdit cbYear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit txtFromMonth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.DateEdit dtpFromMonth;
        private DevExpress.XtraLayout.LayoutControlItem controlFromMonth;
        private DevExpress.XtraEditors.DateEdit dtpToMonth;
        private DevExpress.XtraLayout.LayoutControlItem controltomonth;
        private DevExpress.XtraEditors.TextEdit txtToMonth;
        private DevExpress.XtraLayout.LayoutControlItem controltxtToMonth;
    }
}