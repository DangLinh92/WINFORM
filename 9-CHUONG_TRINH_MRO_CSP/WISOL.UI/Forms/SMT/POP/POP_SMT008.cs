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

namespace Wisol.MES.Forms.SMT.POP
{
    public partial class POP_SMT008 : FormType
    {
        public string comment { get; set; }
        public POP_SMT008()
        {
            InitializeComponent();
            txtComment.Focus();
        }

        public POP_SMT008(string lot, string model, string cereal, string barcode, string reason) : this()
        {
            txtLot.Text = lot;
            txtModel.Text = model;
            txtCerealCounter.Text = cereal;
            txtBarcode.Text = barcode;
            txtComment.Text = reason;
            txtComment.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void POP_SMT008_FormClosed(object sender, FormClosedEventArgs e)
        {
            comment = txtComment.EditValue.ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
