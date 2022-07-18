using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Wisol
{
    [ToolboxItem(true)]
    public partial class AceGridLookUpEdit : GridLookUpEdit
    {
        public AceGridLookUpEdit()
        {
            this.Text = string.Empty;
            this.Properties.NullText = string.Empty;
            this.Properties.View.OptionsView.ShowAutoFilterRow = true;
            this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Properties.ImmediatePopup = true;
        }

        public AceGridLookUpEdit(IContainer container)
        {
            container.Add(this);

            this.Text = string.Empty;
            this.Properties.NullText = string.Empty;
            this.Properties.View.OptionsView.ShowAutoFilterRow = true;
            this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Properties.ImmediatePopup = true;
        }

        private void AceGridLookUpEdit_EditValueChanging(object sender, ChangingEventArgs e)
        {
            this.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
            {
                FilterLookup(sender);
            }));
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            this.EditValueChanging -= new DevExpress.XtraEditors.Controls.ChangingEventHandler(AceGridLookUpEdit_EditValueChanging);
            this.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(AceGridLookUpEdit_EditValueChanging);
        }

        protected override void OnPopupShown()
        {
            base.OnPopupShown();
            FilterLookup(this);
        }

        private void FilterLookup(object sender)
        {
            DevExpress.XtraEditors.GridLookUpEdit edit = this;
            DevExpress.XtraGrid.Views.Grid.GridView gv = edit.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;

            FieldInfo fi = gv.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
            var op2 = new FunctionOperator("Like", new OperandProperty(base.Properties.ValueMember), new OperandValue("%" + edit.AutoSearchText + "%"));
            var op1 = new FunctionOperator("Like", new OperandProperty(base.Properties.DisplayMember), new OperandValue("%" + edit.AutoSearchText + "%"));
            string filterCondition = new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] { op1, op2 }).ToString();
            fi.SetValue(gv, filterCondition);

            MethodInfo mi = gv.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);

            mi.Invoke(gv, null);
        }
    }
}
