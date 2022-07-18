namespace Wisol
{
    partial class XFormBaseGeneric<TDataSet>
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
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControlGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // m_LayoutControl
            // 
            this.m_LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(952, 487, 250, 350);
            // 
            // BaseGeneric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Name = "BaseGeneric";
            this.Text = "XtraForm1";
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_LayoutControlGroup)).EndInit();
            this.ResumeLayout(false);

        }

    }
}