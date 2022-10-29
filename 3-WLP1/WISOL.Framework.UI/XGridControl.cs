using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.FilterEditor;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System.ComponentModel;
using System.Windows.Forms;

namespace Wisol
{
    [ToolboxItem(true)]
    public class XGridControl : GridControl
    {
        protected override BaseView CreateDefaultView()
        {
            return CreateView("XGridView");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new XGridViewInfoRegistrator());
        }
    }

    public class XGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName => "XGridView";

        public override BaseView CreateView(GridControl grid)
        {
            return new XGridView(grid);
        }

        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new XGridViewInfo(view as XGridView);
        }

        public override BaseViewHandler CreateHandler(BaseView view)
        {
            return new XGridViewHandler(view as XGridView);
        }

        public override BaseViewPainter CreatePainter(BaseView view)
        {
            return new XGridViewPainter(view as XGridView);
        }
    }

    public class XGridView : DevExpress.XtraGrid.Views.Grid.GridView
    {
        public XGridView()
        {
        }

        public XGridView(GridControl grid) : base(grid)
        {
        }

        protected override Form CreateFilterBuilderDialog(FilterColumnCollection filterColumns, FilterColumn defaultFilterColumn)
        {
            return new XFilterBuilder(filterColumns, GridControl.MenuManager, GridControl.LookAndFeel, this, defaultFilterColumn);
        }

        protected override void OnFilterEditorCreated(FilterControlEventArgs e)
        {
            base.OnFilterEditorCreated(e);
        }

        public override void ShowFilterEditor(GridColumn defaultColumn)
        {
            base.ShowFilterEditor(defaultColumn is null ? FocusedColumn : defaultColumn);
        }

        protected override string ViewName => "XGridView";
    }

    class XFilterBuilder : FilterBuilder
    {
        public XFilterBuilder(
            DevExpress.XtraEditors.Filtering.FilterColumnCollection columns,
            DevExpress.Utils.Menu.IDXMenuManager manager,
            DevExpress.LookAndFeel.UserLookAndFeel lookAndFeel,
            DevExpress.XtraGrid.Views.Base.ColumnView view,
            DevExpress.XtraEditors.Filtering.FilterColumn fColumn) : base(columns, manager, lookAndFeel, view, fColumn)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            FormBorderEffect = FormBorderEffect.Shadow;

            foreach (var filterControl in Controls.FindAll<XFilterControl>())
            {
                filterControl.Dock = DockStyle.Fill;
                filterControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            }
            foreach (var panel in Controls.FindAll<SidePanel>())
            {
                panel.BorderThickness = 1;
            }
        }

        protected override void OnFilterControlCreated(DevExpress.XtraEditors.IFilterControl filterControl)
        {
            var filterCtrl = new XFilterControl();
            filterCtrl.UseMenuForOperandsAndOperators = fcMain.UseMenuForOperandsAndOperators;
            filterCtrl.AllowAggregateEditing = fcMain.AllowAggregateEditing;
            fcMain = filterCtrl;
        }
    }

    public class XGridViewInfo : DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo
    {
        public XGridViewInfo(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }

    public class XGridViewHandler : DevExpress.XtraGrid.Views.Grid.Handler.GridHandler
    {
        public XGridViewHandler(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }

    public class XGridViewPainter : DevExpress.XtraGrid.Views.Grid.Drawing.GridPainter
    {
        public XGridViewPainter(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }
}
