using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using DevExpress.XtraEditors;

namespace Dreamtech
{
    [ToolboxItem(true)]
    public partial class AceMonthEdit : DateEdit
    {
        public AceMonthEdit()
        {
            InitializeComponent();
        }

        public AceMonthEdit(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

        }
    }
}
