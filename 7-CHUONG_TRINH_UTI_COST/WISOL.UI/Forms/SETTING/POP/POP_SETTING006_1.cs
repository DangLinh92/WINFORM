using System;
using System.Data;
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
    public partial class POP_SETTING006_1 : FormType
    {
        public string luongchuanhap { get; set; }
        public string soluongwafer { get; set; }

        public POP_SETTING006_1()
        {
            InitializeComponent();
        }

        public POP_SETTING006_1(string draft_type, string draft_number, string caption, string category) : this()
        {
            this.lctCost1.Text = caption.ToUpper();
            this.lctCost2.Text = caption.ToUpper();
            this.lctCost3.Text = caption.ToUpper();

            this.layoutControlItem5.Text = " ";
            this.layoutControlItem10.Text = " ";
            this.layoutControlItem13.Text = " ";
            this.lctFactory1.Text = " ";
            this.lctFactory2.Text = " ";
            this.layoutControlItem22.Text = " ";

            dtPaymentDate.EditValue = string.Empty;

            this.lctFactory1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lctFactory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lctDraftRefer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lctNoPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.lctPaymentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            radioGroup1.EditValue = "";

            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING006_1.POP_INT_LIST"
                    , new string[] { "A_CATEGORY", "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_USER_ID",
                        "A_LANG",
                        "A_DRAFT_TYPE",
                        "A_DRAFT_NUMBER",
                        "A_CAPTION"
                    }
                    , new string[] { aceCtgDraft.EditValue.NullString(), Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        draft_type,
                        draft_number,
                        caption

                    }
                    );
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridLookEdit(aceType, base.mResultDB.ReturnDataSet.Tables[0], "CODE", "TYPE");
                    base.mBindData.BindGridLookEdit(aceDepartment1, base.mResultDB.ReturnDataSet.Tables[1], "CODE", "DEPARTMENT");
                    base.mBindData.BindGridLookEdit(aceDepartment2, base.mResultDB.ReturnDataSet.Tables[1], "CODE", "DEPARTMENT");
                    base.mBindData.BindGridLookEdit(aceDepartment3, base.mResultDB.ReturnDataSet.Tables[1], "CODE", "DEPARTMENT");
                    base.mBindData.BindGridLookEdit(aceMaker1, base.mResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    base.mBindData.BindGridLookEdit(aceMaker2, base.mResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    base.mBindData.BindGridLookEdit(aceMaker3, base.mResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
                    base.mBindData.BindGridLookEdit(aceDraftReference, base.mResultDB.ReturnDataSet.Tables[3], "DRAFT_NUMBER", "DRAFT_NAME");
                    base.mBindData.BindGridLookEdit(aceCtgDraft, base.mResultDB.ReturnDataSet.Tables[4], "CODE_CTG", "NAME_CTG");

                    aceCtgDraft.EditValue = category.ToString();
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
                    lblMessage.Text = "Danh muc khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrEmpty(txtDraftNumber.EditValue.NullString()))
                {
                    lblMessage.Text = "DRAFT NUMBER khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (string.IsNullOrEmpty(txtHangMuc1.EditValue.NullString()) && string.IsNullOrEmpty(txtHangMuc2.EditValue.NullString()) && string.IsNullOrEmpty(txtHangMuc3.EditValue.NullString()))
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
                else if (string.IsNullOrEmpty(aceDepartment1.EditValue.NullString())&& string.IsNullOrEmpty(aceDepartment2.EditValue.NullString())&& string.IsNullOrEmpty(aceDepartment3.EditValue.NullString()))
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
                else if (spinEdit1.EditValue.NullString() != "0" && string.IsNullOrWhiteSpace(aceMaker1.EditValue.NullString()))
                {
                    lblMessage.Text = "Maker 1 khong duoc de trong".Translation().ToUpper();
                    this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    return;
                }
                else if (aceType.EditValue.ToString() == "1")
                {
                    if (string.IsNullOrWhiteSpace(aceDraftReference.EditValue.NullString()))
                    {
                        lblMessage.Text = "Draft Reference khong duoc de trong".Translation().ToUpper();
                        this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(radioGroup1.EditValue.NullString()))
                    {
                        lblMessage.Text = "Lan Thanh Toan khong duoc de trong".Translation().ToUpper();
                        this.layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        return;
                    }
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
                Console.WriteLine(paymentDate);
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING006.POP_PUT_ITEM"
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
                        "A_PAYMENT_DATE","A_NOTE_01","A_NOTE_02","A_NOTE_03"
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
                        paymentDate,
                        "","",""
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

            DateTime dateTime = DateTime.Today;
            dtSignTime.EditValue = dateTime.ToString("yyyy-MM-dd");

            switch (aceType.EditValue.ToString())
            {
                case "0":
                    this.lctDraftRefer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctNoPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctFactory1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctFactory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctMaker3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctCost3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctDepart2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctDepart3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctHangMuc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    dtPaymentDate.EditValue = string.Empty;
                    break;
                case "1":
                    this.lctDraftRefer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctNoPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctFactory1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctFactory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctMaker3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctCost3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctDepart3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctHangMuc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctPaymentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    dtPaymentDate.EditValue = dateTime.ToString("yyyy-MM-dd");
                    break;
                case "2":
                    this.lctDraftRefer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctNoPay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctFactory1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctFactory2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    this.lctMaker3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctCost3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctDepart3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctHangMuc3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.lctPaymentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    aceMaker1.EditValue = "G101-0002";
                    aceMaker2.EditValue = "G101-0002";
                    dtPaymentDate.EditValue = dateTime.ToString("yyyy-MM-dd");

                    break;
                default:

                    break;
            }
        }

        private void Reset()
        {
            
            spinEdit1.EditValue = "0";
            spinEdit2.EditValue = "0";
            spinEdit3.EditValue = "0";
            aceMaker1.ReadOnly = false;
            aceMaker2.ReadOnly = false;
            aceMaker3.ReadOnly = false;
            spinEdit1.ReadOnly = false;
            spinEdit2.ReadOnly = false;
            spinEdit3.ReadOnly = false;
            txtHangMuc1.ReadOnly = false;
            txtHangMuc2.ReadOnly = false;
            txtHangMuc3.ReadOnly = false;
            aceDepartment1.ReadOnly = false;
            aceDepartment2.ReadOnly = false;
            aceDepartment3.ReadOnly = false;

            aceMaker1.EditValue = string.Empty;
            aceMaker2.EditValue = string.Empty;
            aceMaker3.EditValue = string.Empty;
            txtDraftNumber.EditValue = string.Empty;
            txtHangMuc1.EditValue = string.Empty;
            txtHangMuc2.EditValue = string.Empty;
            txtHangMuc3.EditValue = string.Empty;
            txtDraftName.EditValue = string.Empty;
            aceDepartment1.EditValue = string.Empty;
            aceDepartment2.EditValue = string.Empty;
            aceDepartment3.EditValue = string.Empty;
            dtSignTime.EditValue = string.Empty;
            txtCreateDraftPerson.EditValue = string.Empty;
        }
        private void aceDraftReference_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(aceDraftReference.EditValue.NullString()) || aceDraftReference.EditValue.NullString().ToUpper().Trim() == "NO REFERENCE")
            {
                Reset();
                txtHangMuc1.EditValue = string.Empty;
                aceDepartment1.EditValue = string.Empty;
                txtCreateDraftPerson.EditValue = string.Empty;
                aceMaker1.EditValue = string.Empty;
                aceMaker1.ReadOnly = false;
                spinEdit1.EditValue = "0";
                aceMaker2.ReadOnly = false;
                aceMaker2.EditValue = string.Empty;
                aceMaker3.ReadOnly = false;
                aceMaker3.ReadOnly = false;
                spinEdit2.ReadOnly = false;
                spinEdit2.EditValue = "0";
                spinEdit3.ReadOnly = false;
                spinEdit3.EditValue = "0";


                return;
            }

            string draft_number = aceDraftReference.EditValue.NullString();

            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING006.GET_LIST_DETAIL"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_LANG", "A_USER_ID", "A_DRAFT_NUMBER"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Language, Consts.USER_INFO.Id, draft_number
                    }
                    );
                if (base.mResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.mResultDB.ReturnDataSet.Tables[0].Copy();
                    int count = dt.Rows.Count;

                    string noPay = base.mResultDB.ReturnDataSet.Tables[1].Rows[0]["NO_PAY"].ToString();
                    Console.WriteLine(count);
                    if (noPay.IsNullOrEmpty())
                    {
                        radioGroup1.EditValue = "1";
                    }
                    else
                        radioGroup1.EditValue = (noPay.ToInt() + 1).ToString();

                    //txtDraftNumber.EditValue = dt.Rows[0]["DRAFT_NUMBER"].ToString();
                    //aceDraftReference.EditValue = dt.Rows[0]["DRAFT_REFERENCE"].ToString();
                    //dtSignTime.EditValue = dt.Rows[0]["DRAFT_SIGN_TIME"].ToString();

                    switch (count)
                    {
                        case 0:
                            txtHangMuc1.EditValue = string.Empty;
                            txtHangMuc2.EditValue = string.Empty;
                            txtHangMuc3.EditValue = string.Empty;
                            aceDepartment1.EditValue = string.Empty;
                            aceDepartment2.EditValue = string.Empty;
                            aceDepartment3.EditValue = string.Empty;
                            break;
                        case 1:
                            txtHangMuc1.EditValue = dt.Rows[0]["HANG_MUC"].ToString();
                            aceDepartment1.EditValue = dt.Rows[0]["DEPARTMENT_CODE"].ToString();

                            txtHangMuc2.EditValue = string.Empty;
                            txtHangMuc3.EditValue = string.Empty;
                            aceDepartment2.EditValue = string.Empty;
                            aceDepartment3.EditValue = string.Empty;

                            txtHangMuc2.ReadOnly = true;
                            txtHangMuc3.ReadOnly = true;
                            aceDepartment2.ReadOnly = true;
                            aceDepartment3.ReadOnly = true;

                            spinEdit1.ReadOnly = false;
                            spinEdit2.ReadOnly = true;
                            spinEdit3.ReadOnly = true;

                            break;
                        case 2:
                            txtHangMuc1.EditValue = dt.Rows[0]["HANG_MUC"].ToString();
                            txtHangMuc2.EditValue = dt.Rows[1]["HANG_MUC"].ToString();
                            aceDepartment1.EditValue = dt.Rows[0]["DEPARTMENT_CODE"].ToString();
                            aceDepartment2.EditValue = dt.Rows[1]["DEPARTMENT_CODE"].ToString();
                            aceMaker2.EditValue = dt.Rows[1]["MAKER_CODE"].ToString();

                            txtHangMuc3.EditValue = string.Empty;
                            aceDepartment3.EditValue = string.Empty;

                            txtHangMuc2.ReadOnly = false;
                            txtHangMuc3.ReadOnly = true;

                            aceDepartment2.ReadOnly = false;
                            aceDepartment3.ReadOnly = true;

                            spinEdit2.ReadOnly = false;
                            spinEdit3.ReadOnly = true;

                            break;
                        case 3:
                            txtHangMuc1.EditValue = dt.Rows[0]["HANG_MUC"].ToString();
                            txtHangMuc2.EditValue = dt.Rows[1]["HANG_MUC"].ToString();
                            txtHangMuc3.EditValue = dt.Rows[2]["HANG_MUC"].ToString();
                            aceDepartment1.EditValue = dt.Rows[0]["DEPARTMENT_CODE"].ToString();
                            aceDepartment2.EditValue = dt.Rows[1]["DEPARTMENT_CODE"].ToString();
                            aceDepartment3.EditValue = dt.Rows[2]["DEPARTMENT_CODE"].ToString();

                            aceMaker2.EditValue = dt.Rows[1]["MAKER_CODE"].ToString();
                            aceMaker3.EditValue = dt.Rows[2]["MAKER_CODE"].ToString();

                            spinEdit2.ReadOnly = false;
                            spinEdit3.ReadOnly = false;


                            txtHangMuc3.ReadOnly = false;
                            txtHangMuc1.ReadOnly = false;
                            txtHangMuc2.ReadOnly = false;
                            aceDepartment3.ReadOnly = false;
                            aceDepartment2.ReadOnly = false;

                            break;
                        default:
                            txtHangMuc1.EditValue = string.Empty;
                            txtHangMuc2.EditValue = string.Empty;
                            txtHangMuc3.EditValue = string.Empty;
                            aceDepartment1.EditValue = string.Empty;
                            aceDepartment2.EditValue = string.Empty;
                            aceDepartment3.EditValue = string.Empty;

                            spinEdit1.ReadOnly = false;
                            spinEdit2.ReadOnly = false;
                            spinEdit3.ReadOnly = false;
                            txtHangMuc1.ReadOnly = false;
                            txtHangMuc2.ReadOnly = false;
                            txtHangMuc3.ReadOnly = false;
                            aceDepartment1.ReadOnly = false;
                            aceDepartment2.ReadOnly = false;
                            aceDepartment3.ReadOnly = false;
                            break;
                    }

                    aceMaker1.EditValue = dt.Rows[0]["MAKER_CODE"].ToString();

                    txtCreateDraftPerson.EditValue = dt.Rows[0]["CREATE_DRAFT_PERSON"].ToString();


                    aceMaker1.ReadOnly = true;
                    aceMaker2.ReadOnly = true;
                    aceMaker3.ReadOnly = true;

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void aceCtgDraft_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING006_1.GET_REFER"
                    , new string[] { "A_CATEGORY"
                    }
                    , new string[] { aceCtgDraft.EditValue.NullString()}
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridLookEdit(aceDraftReference, base.mResultDB.ReturnDataSet.Tables[0], "DRAFT_NUMBER", "DRAFT_NAME");
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
    }
}
