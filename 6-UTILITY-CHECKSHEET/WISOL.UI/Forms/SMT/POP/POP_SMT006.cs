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
    public partial class POP_SMT006 : FormType
    {
        public string thuathiec { get; set; }
        public string thieuthiec { get; set; }
        public string divat { get; set; }
        public string vai { get; set; }
        public string _short { get; set; }
        public string mat { get; set; }
        public string kenh { get; set; }
        public string lech { get; set; }
        public string nguoc { get; set; }
        public string dung { get; set; }
        public string lat { get; set; }
        public string pcb_loss { get; set; }
        public string vo { get; set; }
        public string dst { get; set; }
        public string loi_khac { get; set; }
        public string sample { get; set; }
        public string comment { get; set; }

        public POP_SMT006()
        {
            InitializeComponent();
        }

        public POP_SMT006(string line, string lot, string position, string thuathiec, string thieuthiec, string divat,
                          string vai, string _short, string mat, string kenh, string lech, string nguoc, string dung,
                          string lat, string pcb_loss, string vo, string dst, string loi_khac, string sample, string comment) : this()
        {
            this.txtLine.Text = line;
            this.txtLot.Text = lot;
            this.txtPosition.Text = position;
            this.txtThuaThiec.Text = thuathiec;
            this.txtThieuThiec.Text = thieuthiec;
            this.txtDiVat.Text = divat;
            this.txtVai.Text = vai;
            this.txtShort.Text = _short;
            this.txtMat.Text = mat;
            this.txtKenh.Text = kenh;
            this.txtLech.Text = lech;
            this.txtNguoc.Text = nguoc;
            this.txtDung.Text = dung;
            this.txtLat.Text = lat;
            this.txtPcbLoss.Text = pcb_loss;
            this.txtVo.Text = vo;
            this.txtDst.Text = dst;
            this.txtLoiKhac.Text = loi_khac;
            this.txtSample.Text = sample;
            this.txtComment.Text = comment;

            this.txtThuaThiec.ReadOnly = false;
            this.txtThieuThiec.ReadOnly = false;
            this.txtDiVat.ReadOnly = false;
            this.txtVai.ReadOnly = false;
            this.txtShort.ReadOnly = false;
            this.txtMat.ReadOnly = false;
            this.txtKenh.ReadOnly = false;
            this.txtLech.ReadOnly = false;
            this.txtNguoc.ReadOnly = false;
            this.txtDung.ReadOnly = false;
            this.txtLat.ReadOnly = false;
            this.txtPcbLoss.ReadOnly = false;
            this.txtVo.ReadOnly = false;
            this.txtLoiKhac.ReadOnly = false;
            this.txtSample.ReadOnly = false;

            if (position == "DST")
            {
                this.txtThuaThiec.ReadOnly = true;
                this.txtThieuThiec.ReadOnly = true;
                this.txtDiVat.ReadOnly = true;
                this.txtVai.ReadOnly = true;
                this.txtShort.ReadOnly = true;
                this.txtMat.ReadOnly = true;
                this.txtKenh.ReadOnly = true;
                this.txtLech.ReadOnly = true;
                this.txtNguoc.ReadOnly = true;
                this.txtDung.ReadOnly = true;
                this.txtLat.ReadOnly = true;
                this.txtPcbLoss.ReadOnly = true;
                this.txtVo.ReadOnly = true;
                this.txtLoiKhac.ReadOnly = true;
                this.txtSample.ReadOnly = true;
            }
            if(position == "Sample")
            {
                this.txtThuaThiec.ReadOnly = true;
                this.txtThieuThiec.ReadOnly = true;
                this.txtDiVat.ReadOnly = true;
                this.txtVai.ReadOnly = true;
                this.txtShort.ReadOnly = true;
                this.txtMat.ReadOnly = true;
                this.txtKenh.ReadOnly = true;
                this.txtLech.ReadOnly = true;
                this.txtNguoc.ReadOnly = true;
                this.txtDung.ReadOnly = true;
                this.txtLat.ReadOnly = true;
                //this.txtPcbLoss.ReadOnly = true;
                this.txtVo.ReadOnly = true;
                this.txtLoiKhac.ReadOnly = true;
                this.txtDst.ReadOnly = true;
            }
            if(position == "SPI")
            {
                this.txtVai.ReadOnly = true;
                this.txtMat.ReadOnly = true;
                this.txtKenh.ReadOnly = true;
                this.txtLech.ReadOnly = true;
                this.txtNguoc.ReadOnly = true;
                this.txtDung.ReadOnly = true;
                this.txtLat.ReadOnly = true;
                this.txtPcbLoss.ReadOnly = true;
                this.txtVo.ReadOnly = true;
                this.txtDst.ReadOnly = true;
                this.txtLoiKhac.ReadOnly = true;
                this.txtSample.ReadOnly = true;
            }
            if(position != "DST" && position != "Sample")
            {
                this.txtDst.ReadOnly = true;
                this.txtSample.ReadOnly = true;
            }
            //this.txtPosition.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            //this.txtPosition.Properties.Appearance.BorderColor = Color.Red;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //DialogResult = System.Windows.Forms.DialogResult.No;
            //if (txtColumn.ReadOnly && txtHyperLink.ReadOnly)
            //    return;

            //try
            //{
            //    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT005.POP_PUT_ITEM_2",
            //        new string[]
            //        {
            //            "A_PLANT", "A_DATE_EVENT", "A_MODEL", "A_LOT_NO",
            //            "A_COLUMN", "A_VALUE", "A_HYPER_LINK", "A_UPDATE_USER"
            //        },
            //        new string[]
            //        {
            //            Consts.PLANT, txtDate_Event.EditValue.NullString(), txtModel.EditValue.NullString(), txtLot_No.EditValue.NullString(),
            //            headerColumn, txtColumn.EditValue.ToString(), txtHyperLink.EditValue.ToString(), Consts.USER_INFO.Id
            //        }
            //    );
            //    if (base.mResultDB.ReturnInt == 0)
            //    {
            //        DialogResult = System.Windows.Forms.DialogResult.OK;
            //    }
            //    else
            //    {
            //        MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
            //    }
            //}
            //catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
            //this.thuathiec = txtThuaThiec.EditValue.ToString();
            //this.thieuthiec = txtThieuThiec.EditValue.ToString();
            //this.divat = txtDiVat.EditValue.ToString();
            //this.vai = txtVai.EditValue.ToString();
            //this._short = txtShort.EditValue.ToString();
            //this.mat = txtMat.EditValue.ToString();
            //this.kenh = txtKenh.EditValue.ToString();
            //this.lech = txtLech.EditValue.ToString();
            //this.nguoc = txtNguoc.EditValue.ToString();
            //this.dung = txtDung.EditValue.ToString();
            //this.lat = txtLat.EditValue.ToString();
            //this.pcb_loss = txtPcbLoss.EditValue.ToString();
            //this.vo = txtVo.EditValue.ToString();
            //this.dst = txtDst.EditValue.ToString();
            //this.loi_khac = txtLoiKhac.EditValue.ToString();
            //this.sample = txtSample.EditValue.ToString();
            //if (txtComment.EditValue != DBNull.Value)
            //{
            //    this.comment = txtComment.EditValue.ToString();
            //}
            //else
            //{
            //    this.comment = "";
            //}
            //DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void POP_SMT006_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.thuathiec = txtThuaThiec.EditValue.ToString();
            this.thieuthiec = txtThieuThiec.EditValue.ToString();
            this.divat = txtDiVat.EditValue.ToString();
            this.vai = txtVai.EditValue.ToString();
            this._short = txtShort.EditValue.ToString();
            this.mat = txtMat.EditValue.ToString();
            this.kenh = txtKenh.EditValue.ToString();
            this.lech = txtLech.EditValue.ToString();
            this.nguoc = txtNguoc.EditValue.ToString();
            this.dung = txtDung.EditValue.ToString();
            this.lat = txtLat.EditValue.ToString();
            this.pcb_loss = txtPcbLoss.EditValue.ToString();
            this.vo = txtVo.EditValue.ToString();
            this.dst = txtDst.EditValue.ToString();
            this.loi_khac = txtLoiKhac.EditValue.ToString();
            this.sample = txtSample.EditValue.ToString();
            if (txtComment.EditValue != DBNull.Value)
            {
                this.comment = txtComment.EditValue.ToString();
            }
            else
            {
                this.comment = "";
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
