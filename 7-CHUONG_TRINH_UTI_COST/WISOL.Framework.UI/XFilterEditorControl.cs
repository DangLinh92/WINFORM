using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using System.ComponentModel;

namespace Wisol
{
    [ToolboxItem(true)]
    public class XFilterEditorControl : DevExpress.DataAccess.UI.FilterEditorControl
    {
        static XFilterEditorControl() { }

        public XFilterEditorControl() { }

        protected override FilterControl CreateTreeControl()
        {
            return new XFilterControl();
        }
    }

    [ToolboxItem(true)]
    public class XFilterControl : DevExpress.XtraEditors.FilterControl
    {
        public XFilterControl() { }

        protected override WinFilterTreeNodeModel CreateModel()
        {
            return new XWinFilterTreeNodeModel(this);
        }
    }

    internal class XWinFilterTreeNodeModel : DevExpress.XtraEditors.Filtering.WinFilterTreeNodeModel
    {
        public XWinFilterTreeNodeModel(XFilterControl control) : base(control)
        {
        }

        public override GroupNode CreateGroupNode()
        {
            GroupNode node = base.CreateGroupNode();
            node.NodeType = GroupType.Or;
            return node;
        }

        public override void OnVisualChange(FilterChangedActionInternal action, Node node)
        {
            if (action == FilterChangedActionInternal.NodeAdded && node != null)
            {
                if (node.GetPrevNode() is ClauseNode prevNode)
                {
                    SetDefaultProperty(prevNode.Property);
                }
            }
            base.OnVisualChange(action, node);
        }

        public override ClauseType GetDefaultOperation(IBoundPropertyCollection properties, OperandProperty operandProperty)
        {
            IBoundProperty property = properties.GetProperty(operandProperty);
            if (property == null)
            {
                return ClauseType.Equals;
            }
            switch (GetClauseClass(property))
            {
                case FilterColumnClauseClass.Generic:
                    return ClauseType.Contains;
                case FilterColumnClauseClass.DateTime:
                    return ClauseType.Equals;
                case FilterColumnClauseClass.String:
                    return ClauseType.Contains;
                case FilterColumnClauseClass.Lookup:
                    return ClauseType.Contains;
                case FilterColumnClauseClass.Blob:
                    return ClauseType.IsNotNull;
                default:
                    return ClauseType.Contains;
            }
        }
    }
}
