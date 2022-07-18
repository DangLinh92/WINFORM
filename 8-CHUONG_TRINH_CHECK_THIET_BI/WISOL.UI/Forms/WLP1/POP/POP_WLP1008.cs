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
    public partial class POP_WLP1008 : FormType
    {
        public string luongchuanhap { get; set; }
        public string soluongwafer { get; set; }

        public POP_WLP1008()
        {
            InitializeComponent();
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public POP_WLP1008(string month, string soluongwafer) : this()
        {
            this.txtMonth.Text = month;
            //this.txtLuongChuaNhap.Text = luongchuanhap;
            this.txtSoLuongWafer.Text = soluongwafer;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void POP_WLP1008_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.luongchuanhap = txtLuongChuaNhap.EditValue.ToString();
            this.soluongwafer = txtSoLuongWafer.EditValue.ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
