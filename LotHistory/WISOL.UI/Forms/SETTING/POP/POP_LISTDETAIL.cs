using System;
using System.Data;
using System.Linq;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_LISTDETAIL : FormType
    {
        public POP_LISTDETAIL()
        {
            InitializeComponent();
        }

        public POP_LISTDETAIL (string ID) : this()
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.GET_MATERIAL", new string[] { }, new string[] { });
                Console.WriteLine(base.mResultDB.ReturnInt);
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridLookEdit(sltMaterial, base.mResultDB.ReturnDataSet.Tables[0], "CODE", "MATERIAL_NAME");
                    sltMaterial.Select();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void sltMaterial_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(sltMaterial.EditValue.NullString()))
            {
                e.Cancel = true;
                sltMaterial.Focus();
                dxErrorProvider1.SetError(sltMaterial, "Not be left blank!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.ClearErrors();
            }
        }

        private void sltMaterial_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.LIST_DETAIL", new string[] {
                    "A_CODE"
                }, new string[] {
                    sltMaterial.EditValue.NullString()
                });
                if(base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridView(gcStockIn, base.mResultDB.ReturnDataSet.Tables[0]);
                    base.mBindData.BindGridView(gcStockOut, base.mResultDB.ReturnDataSet.Tables[1]);
                    
                    gvStockIn.Columns["CODE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockIn.Columns["NAME_MATERIAL"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockIn.Columns["UNIT"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockIn.Columns["COST"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockIn.Columns["VALID_DATE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                    gvStockIn.Columns["COST"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvStockIn.Columns["COST"].DisplayFormat.FormatString = "n0";
                    gvStockIn.Columns["QUANTITY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvStockIn.Columns["QUANTITY"].DisplayFormat.FormatString = "n0";
                    gvStockIn.Columns["AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvStockIn.Columns["AMOUNT"].DisplayFormat.FormatString = "n0";

                    gvStockOut.Columns["CODE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockOut.Columns["NAME_MATERIAL"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockOut.Columns["UNIT"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockOut.Columns["COST"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvStockOut.Columns["VALID_DATE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                    gvStockOut.Columns["COST"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvStockOut.Columns["COST"].DisplayFormat.FormatString = "n0";
                    gvStockOut.Columns["QUANTITY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvStockOut.Columns["QUANTITY"].DisplayFormat.FormatString = "n0";
                    gvStockOut.Columns["AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvStockOut.Columns["AMOUNT"].DisplayFormat.FormatString = "n0";
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void Init_Control(bool v)
        {
            throw new NotImplementedException();
        }
    }
}