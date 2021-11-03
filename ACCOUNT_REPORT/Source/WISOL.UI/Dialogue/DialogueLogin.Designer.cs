namespace Wisol.MES.Dialog
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.lblSystem = new DevExpress.XtraEditors.LabelControl();
            this.gleLanguage = new Wisol.AceGridLookUpEdit(this.components);
            this.aceGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gleDepartment = new Wisol.AceGridLookUpEdit(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pciVtn = new DevExpress.XtraEditors.PictureEdit();
            this.picKor = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCanccel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gleLanguage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gleDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pciVtn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSystem
            // 
            this.lblSystem.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystem.Appearance.Options.UseFont = true;
            this.lblSystem.Appearance.Options.UseTextOptions = true;
            this.lblSystem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSystem.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSystem.Location = new System.Drawing.Point(-91, 16);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(412, 86);
            this.lblSystem.TabIndex = 14;
            this.lblSystem.Tag = "PROJECT_NAME";
            this.lblSystem.Text = "PROJECT_NAME";
            // 
            // gleLanguage
            // 
            this.gleLanguage.EditValue = "";
            this.gleLanguage.Location = new System.Drawing.Point(197, 135);
            this.gleLanguage.Name = "gleLanguage";
            this.gleLanguage.Properties.Appearance.Options.UseTextOptions = true;
            this.gleLanguage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gleLanguage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gleLanguage.Properties.NullText = "";
            this.gleLanguage.Properties.PopupView = this.aceGridLookUpEdit1View;
            this.gleLanguage.Size = new System.Drawing.Size(137, 20);
            this.gleLanguage.TabIndex = 13;
            this.gleLanguage.Visible = false;
            this.gleLanguage.EditValueChanged += new System.EventHandler(this.gleLanguage_EditValueChanged);
            // 
            // aceGridLookUpEdit1View
            // 
            this.aceGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.aceGridLookUpEdit1View.Name = "aceGridLookUpEdit1View";
            this.aceGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.aceGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.aceGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Location = new System.Drawing.Point(7, 299);
            this.lblInfo.MaximumSize = new System.Drawing.Size(300, 11);
            this.lblInfo.MinimumSize = new System.Drawing.Size(300, 11);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(300, 11);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "Wisol Hanoi Co.,Ltd 2021";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(197, 209);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(137, 22);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(197, 160);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Properties.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.Properties.Appearance.Options.UseFont = true;
            this.txtUserId.Size = new System.Drawing.Size(137, 22);
            this.txtUserId.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.gleDepartment);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.pciVtn);
            this.panel1.Controls.Add(this.picKor);
            this.panel1.Controls.Add(this.labelControl5);
            this.panel1.Controls.Add(this.lblSystem);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.gleLanguage);
            this.panel1.Controls.Add(this.btnCanccel);
            this.panel1.Controls.Add(this.txtUserId);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 317);
            this.panel1.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(344, 299);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(81, 11);
            this.labelControl3.TabIndex = 19;
            this.labelControl3.Text = "Power-by PI Team";
            // 
            // gleDepartment
            // 
            this.gleDepartment.EditValue = "";
            this.gleDepartment.Location = new System.Drawing.Point(197, 185);
            this.gleDepartment.Name = "gleDepartment";
            this.gleDepartment.Properties.Appearance.Options.UseTextOptions = true;
            this.gleDepartment.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gleDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gleDepartment.Properties.ImmediatePopup = true;
            this.gleDepartment.Properties.NullText = "";
            this.gleDepartment.Properties.PopupView = this.gridView1;
            this.gleDepartment.Size = new System.Drawing.Size(137, 20);
            this.gleDepartment.TabIndex = 18;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(11, 187);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(180, 17);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Tag = "DEPARTMENT";
            this.labelControl2.Text = "DEPARTMENT";
            // 
            // pciVtn
            // 
            this.pciVtn.EditValue = global::Wisol.MES.Properties.Resources.vietnam;
            this.pciVtn.Location = new System.Drawing.Point(198, 136);
            this.pciVtn.Name = "pciVtn";
            this.pciVtn.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pciVtn.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pciVtn.Size = new System.Drawing.Size(30, 20);
            this.pciVtn.TabIndex = 16;
            this.pciVtn.Click += new System.EventHandler(this.pciVtn_Click);
            // 
            // picKor
            // 
            this.picKor.EditValue = global::Wisol.MES.Properties.Resources.korea;
            this.picKor.Location = new System.Drawing.Point(231, 136);
            this.picKor.Name = "picKor";
            this.picKor.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picKor.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picKor.Size = new System.Drawing.Size(30, 20);
            this.picKor.TabIndex = 16;
            this.picKor.Click += new System.EventHandler(this.picKor_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Location = new System.Drawing.Point(11, 210);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(180, 17);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Tag = "PASSWORD";
            this.labelControl5.Text = "PASSWORD";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Location = new System.Drawing.Point(11, 163);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(180, 18);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Tag = "USER_ID";
            this.labelControl4.Text = "USER_ID";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureEdit1.EditValue = global::Wisol.MES.Properties.Resources.WisolLogo;
            this.pictureEdit1.Location = new System.Drawing.Point(351, 16);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.True;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(133, 86);
            this.pictureEdit1.TabIndex = 4;
            this.pictureEdit1.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(11, 138);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(180, 18);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Tag = "LANGUAGE";
            this.labelControl1.Text = "LANGUAGE";
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.ImageOptions.Image")));
            this.btnLogin.Location = new System.Drawing.Point(125, 245);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(108, 20);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Tag = "LOG_IN";
            this.btnLogin.Text = "LOG_IN";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCanccel
            // 
            this.btnCanccel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanccel.Appearance.Options.UseFont = true;
            this.btnCanccel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCanccel.ImageOptions.Image")));
            this.btnCanccel.Location = new System.Drawing.Point(239, 245);
            this.btnCanccel.Name = "btnCanccel";
            this.btnCanccel.Size = new System.Drawing.Size(108, 20);
            this.btnCanccel.TabIndex = 7;
            this.btnCanccel.Tag = "CANCEL";
            this.btnCanccel.Text = "CANCEL";
            this.btnCanccel.Click += new System.EventHandler(this.btnCanccel_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(437, 317);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("FrmLogin.IconOptions.Icon")));
            this.IconOptions.Image = global::Wisol.MES.Properties.Resources.money_bag_200px;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.Text = "Login";
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gleLanguage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aceGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gleDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pciVtn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private Wisol.AceGridLookUpEdit gleLanguage;
        private DevExpress.XtraGrid.Views.Grid.GridView aceGridLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.SimpleButton btnCanccel;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUserId;
        private DevExpress.XtraEditors.LabelControl lblSystem;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pciVtn;
        private DevExpress.XtraEditors.PictureEdit picKor;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private AceGridLookUpEdit gleDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}