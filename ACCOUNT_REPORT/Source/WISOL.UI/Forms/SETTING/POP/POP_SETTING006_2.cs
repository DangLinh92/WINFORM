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

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_SETTING006_2 : FormType
    {
        public string luongchuanhap { get; set; }
        public string soluongwafer { get; set; }

        public POP_SETTING006_2()
        {
            InitializeComponent();
        }

        public POP_SETTING006_2(string draft_number) : this()
        {
            string caption = string.Empty;
            //this.layoutControlItem7.Text = caption.ToUpper();
            //this.layoutControlItem12.Text = caption.ToUpper();
            //this.layoutControlItem15.Text = caption.ToUpper();

            //this.layoutControlItem5.Text = " ";
            //this.layoutControlItem10.Text = " ";
            //this.layoutControlItem13.Text = " ";
            //this.layoutControlItem18.Text = " ";
            //this.layoutControlItem19.Text = " ";
            //this.layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //this.layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //if(caption != "COST")
            //{
            //    this.txtDraftNumber.ReadOnly = true;
            //    txtDraftName.ReadOnly = true;
            //    txtHangMuc.ReadOnly = true;
            //    aceMaker2.ReadOnly = true;
            //    spinEdit2.ReadOnly = true;
            //    aceMaker3.ReadOnly = true;
            //    spinEdit3.ReadOnly = true;
            //}

            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING006_2.POP_INT_LIST"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_USER_ID",
                        "A_LANG",
                        "A_DRAFT_NUMBER"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        draft_number

                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridLookEdit(aceType, base.mResultDB.ReturnDataSet.Tables[0], "CODE", "TYPE");
                    base.mBindData.BindGridLookEdit(aceDepartment1, base.mResultDB.ReturnDataSet.Tables[1], "CODE", "DEPARTMENT");
                    base.mBindData.BindGridLookEdit(aceDepartment2, base.mResultDB.ReturnDataSet.Tables[1], "CODE", "DEPARTMENT");
                    base.mBindData.BindGridLookEdit(aceDepartment3, base.mResultDB.ReturnDataSet.Tables[1], "CODE", "DEPARTMENT");
                    base.mBindData.BindGridLookEdit(aceMaker1, base.mResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    base.mBindData.BindGridLookEdit(aceMaker2, base.mResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    base.mBindData.BindGridLookEdit(aceMaker3, base.mResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    base.mBindData.BindGridLookEdit(aceDraftReference, base.mResultDB.ReturnDataSet.Tables[4], "DRAFT_NUMBER", "DRAFT_NAME");
                    base.mBindData.BindGridLookEdit(aceCtgDraft, base.mResultDB.ReturnDataSet.Tables[5], "CODE_CTG", "NAME_CTG");
                    //string Draft_Ref = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["DRAFT_ROOT"].ToString();
                    //aceDraftReference.EditValue = Draft_Ref;

                    string ltt = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["LAN_THANH_TOAN"].ToString();
                    if (ltt == "0") caption = "COST";
                    if (ltt == "1") caption = "PRE_PAID";
                    if (ltt == "2") caption = "PAY_1";
                    if (ltt == "3") caption = "PAY_2";
                    if (ltt == "4") caption = "PAY_3";
                    if (ltt == "5") caption = "PAY_4";

                    if (ltt != "0")
                    {
                        radioGroup1.EditValue = ltt;
                    }

                    aceType.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["TYPE_CODE"].ToString();
                    aceCtgDraft.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["CATEGORY_CODE"].ToString();
                    txtDraftNumber.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["DRAFT_NUMBER"].ToString();
                    aceDraftReference.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["DRAFT_ROOT"].ToString();
                    string signtime = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["SIGN_TIME"].ToString();
                    if (!string.IsNullOrWhiteSpace(signtime))
                    {
                        dtSignTime.EditValue = signtime.Substring(0, 4) + "-" + signtime.Substring(4, 2) + "-" + signtime.Substring(6, 2);
                    }
                    string paymentDate = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["PAYMENT_DATE"].ToString();
                    if (!string.IsNullOrWhiteSpace(paymentDate))
                    {
                        dtPaymentDate.EditValue = paymentDate.Substring(0, 4) + "-" + paymentDate.Substring(4, 2) + "-" + paymentDate.Substring(6, 2);
                    }
                    txtHangMuc1.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["HANG_MUC"].ToString();
                    txtDraftName.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["DRAFT_NAME"].ToString();
                    aceDepartment1.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["DEPARTMENT_CODE"].ToString();
                    txtCreateDraftPerson.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["CREATE_DRAFT_PERSON"].ToString();
                    aceMaker1.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["MAKER_CODE"].ToString();

                    spinEdit1.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[0][caption].ToString();


                    this.layoutControlItem7.Text = caption.ToUpper();
                    this.layoutControlItem12.Text = caption.ToUpper();
                    this.layoutControlItem15.Text = caption.ToUpper();

                    this.layoutControlItem5.Text = " ";
                    this.layoutControlItem10.Text = " ";
                    this.layoutControlItem13.Text = " ";
                    this.layoutControlItem18.Text = " ";
                    this.layoutControlItem19.Text = " ";
                    this.layoutControlItem22.Text = " ";
                    this.layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    if (base.mResultDB.ReturnDataSet.Tables[3].Rows[0]["TYPE_CODE"].ToString() == "2")
                    {
                        this.layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }


                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    //aceCtgDraft.ReadOnly = true;

                    if (caption != "COST")
                    {
                        this.txtDraftNumber.ReadOnly = true;
                        txtDraftName.ReadOnly = true;
                        txtHangMuc1.ReadOnly = true;
                        aceMaker2.ReadOnly = true;
                        spinEdit2.ReadOnly = true;
                        aceMaker3.ReadOnly = true;
                        spinEdit3.ReadOnly = true;
                    }


                    if (base.mResultDB.ReturnDataSet.Tables[3].Rows.Count == 2)
                    {
                        aceDepartment2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[1]["DEPARTMENT_CODE"].ToString();
                        txtHangMuc2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[1]["HANG_MUC"].ToString();
                        aceMaker2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[1]["MAKER_CODE"].ToString();
                        spinEdit2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[1][caption].ToString();

                        aceMaker2.ReadOnly = false;
                        spinEdit2.ReadOnly = false;
                    }

                    if (base.mResultDB.ReturnDataSet.Tables[3].Rows.Count == 3)
                    {
                        aceDepartment2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[2]["DEPARTMENT_CODE"].ToString();
                        aceDepartment3.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[2]["DEPARTMENT_CODE"].ToString();
                        txtHangMuc2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[2]["HANG_MUC"].ToString();
                        txtHangMuc3.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[2]["HANG_MUC"].ToString();
                        aceMaker2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[1]["MAKER_CODE"].ToString();
                        spinEdit2.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[1][caption].ToString();

                        aceMaker3.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[2]["MAKER_CODE"].ToString();
                        spinEdit3.EditValue = base.mResultDB.ReturnDataSet.Tables[3].Rows[2][caption].ToString();

                        aceMaker2.ReadOnly = false;
                        spinEdit2.ReadOnly = false;
                        aceMaker3.ReadOnly = false;
                        spinEdit3.ReadOnly = false;
                    }
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(aceType.EditValue.NullString()))
                {
                    lblMessage.Text = "TYPE khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //MsgBox.Show("TYPE khong duoc de trong".Translation(), MsgType.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(aceCtgDraft.EditValue.NullString()))
                {
                    lblMessage.Text = "CATEGORY khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrEmpty(txtDraftNumber.EditValue.NullString()))
                {
                    lblMessage.Text = "DRAFT NUMBER khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrEmpty(txtHangMuc1.EditValue.NullString()))
                {
                    lblMessage.Text = "Hang muc khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrEmpty(txtDraftName.EditValue.NullString()))
                {
                    lblMessage.Text = "Draft name khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrEmpty(aceDepartment1.EditValue.NullString()))
                {
                    lblMessage.Text = "Department khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrWhiteSpace(aceMaker1.EditValue.NullString()))
                {
                    lblMessage.Text = "Maker 1 khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (spinEdit2.EditValue.NullString() != "0" && string.IsNullOrWhiteSpace(aceMaker2.EditValue.NullString()))
                {
                    lblMessage.Text = "Maker 2 khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (spinEdit3.EditValue.NullString() != "0" && string.IsNullOrWhiteSpace(aceMaker3.EditValue.NullString()))
                {
                    lblMessage.Text = "Maker 3 khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }

                string sign_time = string.Empty;
                if (!string.IsNullOrWhiteSpace(dtSignTime.EditValue.NullString()))
                {
                    sign_time = dtSignTime.DateTime.ToString("yyyyMMdd");
                }

                string paymentDate = string.Empty;
                if (!string.IsNullOrWhiteSpace(dtPaymentDate.EditValue.NullString()))
                {
                    paymentDate = dtPaymentDate.DateTime.ToString("yyyyMMdd");
                }

                string lan_thanh_toan = string.Empty;
                if (aceType.EditValue.NullString() == "0")
                {
                    lan_thanh_toan = "0";
                }
                else if (aceType.EditValue.NullString() == "2")
                {
                    lan_thanh_toan = "2";
                }
                else
                {
                    lan_thanh_toan = radioGroup1.EditValue.ToString();
                }

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING006_2.POP_PUT_ITEM"
                     , new string[] {
                        "A_PLAN",
                        "A_DEPARTMENT",
                        "A_LANGUAGE",
                        "A_USER_ID",
                        "A_TYPE",
                        "A_CATEGORY",
                        "A_DRAFT_NUMBER",
                        "A_DRAFT_ROOT",
                        "A_SIGN_TIME",
                        "A_HANG_MUC1",
                        "A_HANG_MUC2",
                        "A_HANG_MUC3",
                        "A_DRAFT_NAME",
                        "A_DRAFT_DEPARTMENT1",
                        "A_DRAFT_DEPARTMENT2",
                        "A_DRAFT_DEPARTMENT3",
                        "A_CREATE_DRAFT_PERSON",
                        "A_LAN_THANH_TOAN",
                        "A_MAKER_1",
                        "A_FACTORY_1",
                        "A_SPIN_1",
                        "A_MAKER_2",
                        "A_FACTORY_2",
                        "A_SPIN_2",
                        "A_MAKER_3",
                        "A_SPIN_3",
                        "A_PAYMENT_DATE"
                    }
                    , new string[] {
                        Consts.PLANT,
                        Consts.DEPARTMENT,
                        Consts.USER_INFO.Language,
                        Consts.USER_INFO.Id,
                        aceType.EditValue.NullString(),
                        aceCtgDraft.EditValue.NullString(),
                        txtDraftNumber.EditValue.NullString(),
                        aceDraftReference.EditValue.NullString(),
                        sign_time,
                        txtHangMuc1.EditValue.NullString(),
                        txtHangMuc2.EditValue.NullString(),
                        txtHangMuc3.EditValue.NullString(),
                        txtDraftName.EditValue.NullString(),
                        aceDepartment1.EditValue.NullString(),
                        aceDepartment2.EditValue.NullString(),
                        aceDepartment3.EditValue.NullString(),
                        txtCreateDraftPerson.EditValue.NullString(),
                        lan_thanh_toan,
                        aceMaker1.EditValue.NullString(),
                        txtFactory1.EditValue.NullString(),
                        spinEdit1.EditValue.NullString(),
                        aceMaker2.EditValue.NullString(),
                        txtFactory2.EditValue.NullString(),
                        spinEdit2.EditValue.NullString(),
                        aceMaker3.EditValue.NullString(),
                        spinEdit3.EditValue.NullString(),
                        paymentDate
                    }
                    ); ; ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Information);
                    this.Close();
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void POP_SETTING001_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.soluongwafer = txtSoLuongWafer.EditValue.ToString();
            //DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void aceType_EditValueChanged(object sender, EventArgs e)
        {
            this.Reset();
            if (aceType.EditValue.ToString() == "1")
            {
                this.layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                this.layoutControlItem21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            if (aceType.EditValue.ToString() == "2")
            {
                this.layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lctDepart3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.lctHangMuc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.dtPaymentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                aceMaker1.EditValue = "G101-0002";
                aceMaker2.EditValue = "G101-0002";
            }
            else
            {
                this.layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void Reset()
        {
            aceMaker1.EditValue = string.Empty;
            aceMaker2.EditValue = string.Empty;
            aceMaker3.EditValue = string.Empty;
            spinEdit1.EditValue = "0";
            spinEdit2.EditValue = "0";
            spinEdit3.EditValue = "0";
            aceMaker1.ReadOnly = false;
            aceMaker2.ReadOnly = false;
            aceMaker3.ReadOnly = false;
            spinEdit1.ReadOnly = false;
            spinEdit2.ReadOnly = false;
            spinEdit3.ReadOnly = false;
            txtDraftNumber.EditValue = string.Empty;
            txtHangMuc1.EditValue = string.Empty;
            txtDraftName.EditValue = string.Empty;
            aceDepartment1.EditValue = string.Empty;
            dtSignTime.EditValue = string.Empty;
            txtCreateDraftPerson.EditValue = string.Empty;
            aceDraftReference.EditValue = string.Empty;
        }
    }
}
