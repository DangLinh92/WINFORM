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
    public partial class POP_SMT005_2 : FormType
    {
        private string headerColumn = string.Empty;
        public POP_SMT005_2()
        {
            InitializeComponent();
        }

        public POP_SMT005_2(string date_event, string model, string lot_no, string nghi_ngo_sw, string nghi_ngo_lna, string nghi_ngo_sw_lna) : this()
        {
            txtDate_Event.EditValue = date_event;
            txtModel.EditValue = model;
            txtLot_No.EditValue = lot_no;
            txtColumn.EditValue = Convert.ToInt32(nghi_ngo_sw);
            //txtLNA.EditValue = Convert.ToInt32(nghi_ngo_lna);
            //txtSW_LNA.EditValue = Convert.ToInt32(nghi_ngo_sw_lna);
        }

        public POP_SMT005_2(string date_event, string model, string lot_no, string column, string value) : this()
        {
            string hyperlink = string.Empty;

            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT005.POP_GET_LIST",
                    new string[]
                    {
                        "A_PLANT", "A_DATE_EVENT", "A_MODEL",
                        "A_LOT_NO", "A_COLUMN"
                    },
                    new string[]
                    {
                        Consts.PLANT, date_event, model, lot_no, column
                    }
                );
                if (base.mResultDB.ReturnInt == 0)
                {
                    if (base.mResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        hyperlink = base.mResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }


            headerColumn = column;
            txtDate_Event.EditValue = date_event;
            txtModel.EditValue = model;
            txtLot_No.EditValue = lot_no;
            this.layoutControlItem7.Text = column;


            //if(column.Equals("NGHI_NGO_SW") || column.Equals("NGHI_NGO_LNA") || column.Equals("NGHI_NGO_SW_LNA"))
            //{
            //    txtColumn.ReadOnly = false;
            //    txtHyperLink.ReadOnly = false;
            //    if (!string.IsNullOrWhiteSpace(value))
            //    {
            //        txtColumn.EditValue = Convert.ToInt32(value);
            //    }
            //    txtHyperLink.EditValue = hyperlink;
            //}
            //else
            //{
            //    txtColumn.ReadOnly = true;
            //    if (!string.IsNullOrWhiteSpace(value))
            //    {
            //        if (Convert.ToInt32(value) > 0)
            //        {
            //            txtHyperLink.ReadOnly = false;
            //            txtHyperLink.EditValue = hyperlink;
            //            txtColumn.EditValue = Convert.ToInt32(value);
            //        }
            //        else
            //        {
            //            txtHyperLink.ReadOnly = true;
            //        }
            //    }
            //}

            if (!string.IsNullOrWhiteSpace(value))
            {
                txtColumn.EditValue = Convert.ToInt32(value);
            }
            txtHyperLink.EditValue = hyperlink;

            //txtLNA.EditValue = Convert.ToInt32(nghi_ngo_lna);
            //txtSW_LNA.EditValue = Convert.ToInt32(nghi_ngo_sw_lna);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            if (txtColumn.ReadOnly && txtHyperLink.ReadOnly)
                return;

            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT005.POP_PUT_ITEM_2",
                    new string[]
                    {
                        "A_PLANT", "A_DATE_EVENT", "A_MODEL", "A_LOT_NO",
                        "A_COLUMN", "A_VALUE", "A_HYPER_LINK", "A_UPDATE_USER"
                    },
                    new string[]
                    {
                        Consts.PLANT, txtDate_Event.EditValue.NullString(), txtModel.EditValue.NullString(), txtLot_No.EditValue.NullString(),
                        headerColumn, txtColumn.EditValue.ToString(), txtHyperLink.EditValue.ToString(), Consts.USER_INFO.Id
                    }
                );
                if (base.mResultDB.ReturnInt == 0)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
