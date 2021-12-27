using DevExpress.XtraLayout;
using System.Windows.Forms;

namespace Wisol
{
    public class XLayoutControl : LayoutControl
    {
        public XLayoutControl()
        {

        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            SetControlItem(e.Control);
            e.Control.DataBindings.CollectionChanged += (x, y) => { SetControlItem(e.Control); };
        }

        private void SetControlItem(Control control)
        {
            var layoutControlItem = GetItemByControl(control);
            if (layoutControlItem != null)
            {
                layoutControlItem.Text = control.DataBindings["EditValue"]?.BindingMemberInfo.BindingMember;
            }
        }
    }
}
