namespace Wisol.MES.Forms.REPORT.POP
{
    partial class POP_REPORT001_CHART
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POP_REPORT001_CHART));
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.rgDay = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgDay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(12, 51);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1251, 505);
            this.chartControl1.TabIndex = 4;
            // 
            // rgDay
            // 
            this.rgDay.Location = new System.Drawing.Point(541, 12);
            this.rgDay.Name = "rgDay";
            this.rgDay.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("30", "30 Days"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("365", "1 Year")});
            this.rgDay.Size = new System.Drawing.Size(184, 33);
            this.rgDay.TabIndex = 5;
            this.rgDay.EditValueChanged += new System.EventHandler(this.rgDay_EditValueChanged);
            // 
            // POP_REPORT001_CHART
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 573);
            this.Controls.Add(this.rgDay);
            this.Controls.Add(this.chartControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("POP_REPORT001_CHART.IconOptions.Icon")));
            this.Name = "POP_REPORT001_CHART";
            this.Text = "CHART";
            this.Controls.SetChildIndex(this.chartControl1, 0);
            this.Controls.SetChildIndex(this.rgDay, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgDay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.RadioGroup rgDay;
    }
}