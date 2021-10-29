namespace Wisol.MES.Dialog
{
    partial class SplashScreen
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
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblMsg = new DevExpress.XtraEditors.LabelControl();
            this.lblCorporation = new DevExpress.XtraEditors.LabelControl();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.picBackground = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(69, 133);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(249, 32);
            this.lblTitle.TabIndex = 21;
            this.lblTitle.Text = "WHC법인_SPARE PART";
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblMsg.Appearance.Options.UseFont = true;
            this.lblMsg.Appearance.Options.UseForeColor = true;
            this.lblMsg.Location = new System.Drawing.Point(21, 236);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 15);
            this.lblMsg.TabIndex = 18;
            // 
            // lblCorporation
            // 
            this.lblCorporation.Appearance.Options.UseFont = true;
            this.lblCorporation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCorporation.Location = new System.Drawing.Point(24, 286);
            this.lblCorporation.Name = "lblCorporation";
            this.lblCorporation.Size = new System.Drawing.Size(121, 13);
            this.lblCorporation.TabIndex = 17;
            this.lblCorporation.Text = "Wisol Hanoi Co.,Ltd 2020";
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(21, 255);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(404, 12);
            this.marqueeProgressBarControl1.TabIndex = 16;
            // 
            // picBackground
            // 
            this.picBackground.Location = new System.Drawing.Point(10, 13);
            this.picBackground.Name = "picBackground";
            this.picBackground.Properties.AllowFocused = false;
            this.picBackground.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picBackground.Properties.Appearance.Options.UseBackColor = true;
            this.picBackground.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picBackground.Properties.NullText = " ";
            this.picBackground.Properties.ShowMenu = false;
            this.picBackground.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picBackground.Size = new System.Drawing.Size(426, 180);
            this.picBackground.TabIndex = 20;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(337, 286);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 13);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "Power-by PI Team";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(447, 327);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picBackground);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblCorporation);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Name = "SplashScreen";
            this.Text = "FrmSplshScreen";
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.PictureEdit picBackground;
        private DevExpress.XtraEditors.LabelControl lblMsg;
        private DevExpress.XtraEditors.LabelControl lblCorporation;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}