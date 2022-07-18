namespace Wisol
{
    partial class XFormBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFormBase));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new Wisol.XSimpleButton();
            this.btnCancel = new Wisol.XSimpleButton();
            this.m_LayoutControl = new Wisol.XLayoutControl();
            this.m_LayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.eWIPMSTMATBindingSource = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eWIPMSTMATBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 228);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(384, 33);
            this.panelControl1.TabIndex = 66;
            // 
            // btnSave
            // 
            this.btnSave.ButtonType = Wisol.ButtonTypes.Save;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(124, 2);
            this.btnSave.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonType = Wisol.ButtonTypes.Cancel;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(253, 2);
            this.btnCancel.MaximumSize = new System.Drawing.Size(129, 30);
            this.btnCancel.MinimumSize = new System.Drawing.Size(129, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 30);
            this.btnCancel.TabIndex = 65;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // m_LayoutControl
            // 
            this.m_LayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LayoutControl.Location = new System.Drawing.Point(0, 0);
            this.m_LayoutControl.Name = "m_LayoutControl";
            this.m_LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(952, 487, 250, 350);
            this.m_LayoutControl.Root = this.m_LayoutControlGroup;
            this.m_LayoutControl.Size = new System.Drawing.Size(384, 228);
            this.m_LayoutControl.TabIndex = 4;
            this.m_LayoutControl.Text = "layoutControl1";
            // 
            // m_LayoutControlGroup
            // 
            this.m_LayoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.m_LayoutControlGroup.Name = "m_LayoutControlGroup1";
            this.m_LayoutControlGroup.Size = new System.Drawing.Size(384, 228);
            this.m_LayoutControlGroup.Text = "m_LayoutControlGroup";
            // 
            // eWIPMSTMATBindingSource
            // 
            this.eWIPMSTMATBindingSource.DataMember = "EWIPMSTMAT";
            // 
            // XFormBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.m_LayoutControl);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "XFormBase";
            this.Text = "BaseDialogue";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eWIPMSTMATBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        private Wisol.XSimpleButton btnSave;
        private XSimpleButton btnCancel;
        protected XLayoutControl m_LayoutControl;
        protected DevExpress.XtraLayout.LayoutControlGroup m_LayoutControlGroup;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.BindingSource eWIPMSTMATBindingSource;
    }
}