namespace Wisol.Components
{
    partial class MsgType1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgType1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblMsg = new DevExpress.XtraEditors.LabelControl();
            this.imgList = new DevExpress.Utils.ImageCollection(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.marqueeProgressBarControl1);
            this.panel2.Controls.Add(this.picImage);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(337, 224);
            this.panel2.TabIndex = 1;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(139, 136);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(171, 12);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(10, 169);
            this.marqueeProgressBarControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
            this.marqueeProgressBarControl1.Properties.EndColor = System.Drawing.SystemColors.HotTrack;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(318, 10);
            this.marqueeProgressBarControl1.TabIndex = 18;
            this.marqueeProgressBarControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(10, 9);
            this.picImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(50, 50);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImage.TabIndex = 17;
            this.picImage.TabStop = false;
            this.picImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseBorderColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(139, 188);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(61, 28);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "CLOSE";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblMsg.Appearance.Options.UseFont = true;
            this.lblMsg.Appearance.Options.UseForeColor = true;
            this.lblMsg.Appearance.Options.UseTextOptions = true;
            this.lblMsg.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblMsg.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblMsg.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMsg.Location = new System.Drawing.Point(58, 9);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(270, 151);
            this.lblMsg.TabIndex = 14;
            this.lblMsg.Text = "Msg";
            this.lblMsg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
            // 
            // imgList
            // 
            this.imgList.ImageSize = new System.Drawing.Size(50, 50);
            this.imgList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.Images.SetKeyName(0, "ERROR.png");
            this.imgList.Images.SetKeyName(1, "INFORMATION.png");
            this.imgList.Images.SetKeyName(2, "WARNING.png");
            // 
            // MsgType1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 232);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgType1";
            this.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgList)).EndInit();
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl lblMsg;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public System.Windows.Forms.PictureBox picImage;
        private DevExpress.Utils.ImageCollection imgList;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}