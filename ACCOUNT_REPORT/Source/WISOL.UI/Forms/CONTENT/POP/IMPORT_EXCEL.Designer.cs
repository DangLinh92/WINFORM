
namespace Wisol.MES.Forms.CONTENT.POP
{
    partial class IMPORT_EXCEL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMPORT_EXCEL));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtFilePath = new Wisol.XTextEdit();
            this.btnImport = new Wisol.XSimpleButton(this.components);
            this.btnLoadData = new Wisol.XSimpleButton(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcList = new Wisol.XGridControl();
            this.gvList = new Wisol.XGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblMsg);
            this.panelControl1.Controls.Add(this.txtFilePath);
            this.panelControl1.Controls.Add(this.btnImport);
            this.panelControl1.Controls.Add(this.btnLoadData);
            this.panelControl1.Location = new System.Drawing.Point(0, 380);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 69);
            this.panelControl1.TabIndex = 4;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(12, 41);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(49, 13);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "Message";
            // 
            // txtFilePath
            // 
            this.txtFilePath.IsRequire = false;
            this.txtFilePath.Location = new System.Drawing.Point(12, 14);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.NullBackColor = System.Drawing.Color.Lime;
            this.txtFilePath.NullValidation = false;
            this.txtFilePath.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtFilePath.Properties.Appearance.Options.UseBackColor = true;
            this.txtFilePath.Size = new System.Drawing.Size(506, 20);
            this.txtFilePath.TabIndex = 3;
            // 
            // btnImport
            // 
            this.btnImport.FormId = null;
            this.btnImport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.ImageOptions.Image")));
            this.btnImport.isFormType = false;
            this.btnImport.Location = new System.Drawing.Point(659, 9);
            this.btnImport.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnImport.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(129, 30);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import Excel";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.FormId = null;
            this.btnLoadData.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadData.ImageOptions.Image")));
            this.btnLoadData.isFormType = false;
            this.btnLoadData.Location = new System.Drawing.Point(524, 9);
            this.btnLoadData.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnLoadData.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(129, 30);
            this.btnLoadData.TabIndex = 1;
            this.btnLoadData.Text = "Upload File";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcList);
            this.groupControl1.Location = new System.Drawing.Point(0, 26);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(800, 348);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "List";
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(2, 23);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(796, 323);
            this.gcList.TabIndex = 0;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            // 
            // IMPORT_EXCEL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("IMPORT_EXCEL.IconOptions.Icon")));
            this.Name = "IMPORT_EXCEL";
            this.Text = "IMPORT_EXCEL";
            this.Load += new System.EventHandler(this.IMPORT_EXCEL_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.groupControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private XSimpleButton btnImport;
        private XSimpleButton btnLoadData;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private XGridControl gcList;
        private XGridView gvList;
        private XTextEdit txtFilePath;
        private System.Windows.Forms.Label lblMsg;
    }
}