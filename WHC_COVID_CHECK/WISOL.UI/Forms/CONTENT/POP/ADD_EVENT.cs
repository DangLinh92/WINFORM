using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT.POP
{
    public partial class ADD_EVENT : FormType
    {
        public string Event { get; set; }
        public ADD_EVENT()
        {
            InitializeComponent();
            Classes.Common.SetFormIdToButton(null, "ADD_EVENT", this);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Event = txtEvent.Text.NullString();
            this.Close();
        }
    }
}
