
using DevExpress.XtraEditors;

namespace Wisol.MES.Forms.CONTENT
{
    partial class FRM_FINACING_STATEMENT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_FINACING_STATEMENT));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xLayoutControl1 = new Wisol.XLayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateCloseMonth = new DevExpress.XtraEditors.DateEdit();
            this.btnCloseData = new Wisol.XSimpleButton(this.components);
            this.cheDaily = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.btnDownloadFile = new Wisol.XSimpleButton(this.components);
            this.btnLoadFileData = new Wisol.XSimpleButton(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateLoad = new DevExpress.XtraEditors.DateEdit();
            this.spreadsheetFormulaBar1 = new DevExpress.XtraSpreadsheet.SpreadsheetFormulaBar();
            this.spreadsheetMain = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Wisol.MES.FrmWaitForm), true, true, typeof(System.Windows.Forms.UserControl));
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).BeginInit();
            this.xLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheDaily.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateLoad.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateLoad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xLayoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1649, 860);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Financing Statement";
            // 
            // xLayoutControl1
            // 
            this.xLayoutControl1.Controls.Add(this.panelControl1);
            this.xLayoutControl1.Controls.Add(this.spreadsheetFormulaBar1);
            this.xLayoutControl1.Controls.Add(this.spreadsheetMain);
            this.xLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayoutControl1.Location = new System.Drawing.Point(2, 23);
            this.xLayoutControl1.Name = "xLayoutControl1";
            this.xLayoutControl1.Root = this.Root;
            this.xLayoutControl1.Size = new System.Drawing.Size(1645, 835);
            this.xLayoutControl1.TabIndex = 0;
            this.xLayoutControl1.Text = "xLayoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateCloseMonth);
            this.panelControl1.Controls.Add(this.btnCloseData);
            this.panelControl1.Controls.Add(this.cheDaily);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dateTo);
            this.panelControl1.Controls.Add(this.btnDownloadFile);
            this.panelControl1.Controls.Add(this.btnLoadFileData);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.dateLoad);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1621, 40);
            this.panelControl1.TabIndex = 6;
            // 
            // dateCloseMonth
            // 
            this.dateCloseMonth.EditValue = null;
            this.dateCloseMonth.Location = new System.Drawing.Point(1286, 10);
            this.dateCloseMonth.Name = "dateCloseMonth";
            this.dateCloseMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCloseMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCloseMonth.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dateCloseMonth.Size = new System.Drawing.Size(253, 20);
            this.dateCloseMonth.TabIndex = 8;
            // 
            // btnCloseData
            // 
            this.btnCloseData.FormId = null;
            this.btnCloseData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseData.ImageOptions.Image")));
            this.btnCloseData.isFormType = false;
            this.btnCloseData.Location = new System.Drawing.Point(1151, 5);
            this.btnCloseData.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnCloseData.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnCloseData.Name = "btnCloseData";
            this.btnCloseData.Size = new System.Drawing.Size(129, 30);
            this.btnCloseData.TabIndex = 7;
            this.btnCloseData.Text = "Close Data Monthly";
            this.btnCloseData.Click += new System.EventHandler(this.btnCloseData_Click);
            // 
            // cheDaily
            // 
            this.cheDaily.Location = new System.Drawing.Point(535, 8);
            this.cheDaily.Name = "cheDaily";
            this.cheDaily.Properties.Caption = "Daily/일일";
            this.cheDaily.Size = new System.Drawing.Size(75, 20);
            this.cheDaily.TabIndex = 6;
            this.cheDaily.CheckedChanged += new System.EventHandler(this.cheDaily_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(287, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(11, 17);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "~";
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(304, 9);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Size = new System.Drawing.Size(211, 20);
            this.dateTo.TabIndex = 4;
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.FormId = null;
            this.btnDownloadFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadFile.ImageOptions.Image")));
            this.btnDownloadFile.isFormType = false;
            this.btnDownloadFile.Location = new System.Drawing.Point(751, 5);
            this.btnDownloadFile.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnDownloadFile.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.Size = new System.Drawing.Size(129, 30);
            this.btnDownloadFile.TabIndex = 3;
            this.btnDownloadFile.Text = "DOWNLOAD REPORT";
            this.btnDownloadFile.Click += new System.EventHandler(this.btnDownloadFile_Click);
            // 
            // btnLoadFileData
            // 
            this.btnLoadFileData.FormId = null;
            this.btnLoadFileData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadFileData.ImageOptions.Image")));
            this.btnLoadFileData.isFormType = false;
            this.btnLoadFileData.Location = new System.Drawing.Point(616, 5);
            this.btnLoadFileData.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnLoadFileData.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnLoadFileData.Name = "btnLoadFileData";
            this.btnLoadFileData.Size = new System.Drawing.Size(129, 30);
            this.btnLoadFileData.TabIndex = 2;
            this.btnLoadFileData.Text = "LOAD FILE";
            this.btnLoadFileData.Click += new System.EventHandler(this.btnLoadFileData_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "NGÀY [DATE]";
            // 
            // dateLoad
            // 
            this.dateLoad.EditValue = null;
            this.dateLoad.Location = new System.Drawing.Point(98, 9);
            this.dateLoad.Name = "dateLoad";
            this.dateLoad.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateLoad.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateLoad.Size = new System.Drawing.Size(183, 20);
            this.dateLoad.TabIndex = 0;
            // 
            // spreadsheetFormulaBar1
            // 
            this.spreadsheetFormulaBar1.Expanded = true;
            this.spreadsheetFormulaBar1.Location = new System.Drawing.Point(12, 56);
            this.spreadsheetFormulaBar1.MinimumSize = new System.Drawing.Size(0, 24);
            this.spreadsheetFormulaBar1.Name = "spreadsheetFormulaBar1";
            this.spreadsheetFormulaBar1.Size = new System.Drawing.Size(1621, 31);
            this.spreadsheetFormulaBar1.SpreadsheetControl = this.spreadsheetMain;
            this.spreadsheetFormulaBar1.TabIndex = 5;
            // 
            // spreadsheetMain
            // 
            this.spreadsheetMain.Location = new System.Drawing.Point(12, 91);
            this.spreadsheetMain.Name = "spreadsheetMain";
            this.spreadsheetMain.Options.Culture = new System.Globalization.CultureInfo("en-US");
            this.spreadsheetMain.Size = new System.Drawing.Size(1621, 732);
            this.spreadsheetMain.TabIndex = 4;
            this.spreadsheetMain.Text = "spreadsheetControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1645, 835);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.spreadsheetMain;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1625, 736);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spreadsheetFormulaBar1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1625, 35);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1625, 44);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // FRM_FINACING_STATEMENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "FRM_FINACING_STATEMENT";
            this.Size = new System.Drawing.Size(1649, 860);
            this.Controls.SetChildIndex(this.groupControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xLayoutControl1)).EndInit();
            this.xLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cheDaily.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateLoad.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateLoad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private XLayoutControl xLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraSpreadsheet.SpreadsheetFormulaBar spreadsheetFormulaBar1;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateLoad;
        private XSimpleButton btnLoadFileData;
        private XSimpleButton btnDownloadFile;
        private LabelControl labelControl2;
        private DateEdit dateTo;
        private CheckEdit cheDaily;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private XSimpleButton btnCloseData;
        private DateEdit dateCloseMonth;
    }
}