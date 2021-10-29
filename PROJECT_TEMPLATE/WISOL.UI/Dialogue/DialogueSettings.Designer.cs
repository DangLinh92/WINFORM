namespace Wisol.MES.Dialog
{
    partial class DialogueSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogueSettings));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rdgMenuType = new DevExpress.XtraEditors.RadioGroup();
            this.txtFileVersion = new DevExpress.XtraEditors.TextEdit();
            this.btnChgPwd = new DevExpress.XtraEditors.SimpleButton();
            this.txtUserIp = new DevExpress.XtraEditors.TextEdit();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.txtFactory = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdgMenuType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFactory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.rdgMenuType);
            this.layoutControl1.Controls.Add(this.txtFileVersion);
            this.layoutControl1.Controls.Add(this.btnChgPwd);
            this.layoutControl1.Controls.Add(this.txtUserIp);
            this.layoutControl1.Controls.Add(this.txtUserName);
            this.layoutControl1.Controls.Add(this.txtUserId);
            this.layoutControl1.Controls.Add(this.txtFactory);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(552, 56, 250, 350);
            this.layoutControl1.Padding = new System.Windows.Forms.Padding(3);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(436, 208);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // rdgMenuType
            // 
            this.rdgMenuType.EditValue = "T";
            this.rdgMenuType.Location = new System.Drawing.Point(84, 134);
            this.rdgMenuType.Name = "rdgMenuType";
            this.rdgMenuType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("T", "TOP"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("L", "LEFT")});
            this.rdgMenuType.Size = new System.Drawing.Size(340, 36);
            this.rdgMenuType.StyleController = this.layoutControl1;
            this.rdgMenuType.TabIndex = 20;
            // 
            // txtFileVersion
            // 
            this.txtFileVersion.Location = new System.Drawing.Point(84, 110);
            this.txtFileVersion.Name = "txtFileVersion";
            this.txtFileVersion.Properties.ReadOnly = true;
            this.txtFileVersion.Size = new System.Drawing.Size(340, 20);
            this.txtFileVersion.StyleController = this.layoutControl1;
            this.txtFileVersion.TabIndex = 18;
            // 
            // btnChgPwd
            // 
            this.btnChgPwd.Location = new System.Drawing.Point(298, 36);
            this.btnChgPwd.Name = "btnChgPwd";
            this.btnChgPwd.Size = new System.Drawing.Size(126, 22);
            this.btnChgPwd.StyleController = this.layoutControl1;
            this.btnChgPwd.TabIndex = 17;
            this.btnChgPwd.Text = "CHG_PWD";
            this.btnChgPwd.Click += new System.EventHandler(this.btnChgPwd_Click);
            // 
            // txtUserIp
            // 
            this.txtUserIp.Location = new System.Drawing.Point(84, 86);
            this.txtUserIp.Name = "txtUserIp";
            this.txtUserIp.Properties.ReadOnly = true;
            this.txtUserIp.Size = new System.Drawing.Size(340, 20);
            this.txtUserIp.StyleController = this.layoutControl1;
            this.txtUserIp.TabIndex = 15;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(84, 62);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(340, 20);
            this.txtUserName.StyleController = this.layoutControl1;
            this.txtUserName.TabIndex = 14;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(84, 36);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Properties.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(210, 20);
            this.txtUserId.StyleController = this.layoutControl1;
            this.txtUserId.TabIndex = 12;
            // 
            // txtFactory
            // 
            this.txtFactory.Location = new System.Drawing.Point(84, 12);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Properties.ReadOnly = true;
            this.txtFactory.Size = new System.Drawing.Size(340, 20);
            this.txtFactory.StyleController = this.layoutControl1;
            this.txtFactory.TabIndex = 10;
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.ImageOptions.Image")));
            this.btnOk.Location = new System.Drawing.Point(324, 174);
            this.btnOk.MaximumSize = new System.Drawing.Size(100, 22);
            this.btnOk.MinimumSize = new System.Drawing.Size(100, 22);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem10,
            this.layoutControlItem8,
            this.layoutControlItem11,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(436, 208);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnOk;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(312, 162);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(104, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtFactory;
            this.layoutControlItem1.CustomizationFormText = "FACTORY";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem1.Text = "FACTORY";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtUserId;
            this.layoutControlItem3.CustomizationFormText = "USER_ID";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(286, 26);
            this.layoutControlItem3.Text = "USER_ID";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtUserName;
            this.layoutControlItem5.CustomizationFormText = "USER_NAME";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem5.Text = "USER_NAME";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtUserIp;
            this.layoutControlItem6.CustomizationFormText = "USER_IP";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem6.Text = "USER_IP";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnChgPwd;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(286, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(130, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtFileVersion;
            this.layoutControlItem8.CustomizationFormText = "FILE_VERSiON";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem8.Text = "FILE_VERSiON";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.rdgMenuType;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 122);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(416, 40);
            this.layoutControlItem11.Text = "MENU_TYPE";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(69, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 162);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(312, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(440, 212);
            this.panelControl1.TabIndex = 1;
            // 
            // DialogueSettings
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(440, 212);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("DialogueSettings.IconOptions.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogueSettings";
            this.Text = "SETTINGS";
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdgMenuType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFactory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnChgPwd;
        private DevExpress.XtraEditors.TextEdit txtUserIp;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtUserId;
        private DevExpress.XtraEditors.TextEdit txtFactory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.TextEdit txtFileVersion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.RadioGroup rdgMenuType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}