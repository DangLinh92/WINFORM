using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Wisol.Common;
using Wisol.Components;
using Wisol.Objects;

using Wisol.MES.Inherit;
using Wisol.MES.Classes;
using Wisol.MES.Dialog;
using System.Text.RegularExpressions;

namespace Wisol.MES.Forms.WLP1.POP
{
    public partial class POP_WLP1008_2 : FormType
    {
        public string luongchuanhap { get; set; }

        public POP_WLP1008_2()
        {
            InitializeComponent();
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public POP_WLP1008_2(string luongchuanhap) : this()
        {
            //this.txtLuongChuaNhap.Text = luongchuanhap;
            this.txtLuongChuaNhap.Text = luongchuanhap;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void POP_WLP1008_2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.luongchuanhap = txtLuongChuaNhap.EditValue.ToString();
            this.luongchuanhap = txtLuongChuaNhap.EditValue.ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
