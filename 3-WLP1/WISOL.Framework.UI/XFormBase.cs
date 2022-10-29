using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Wisol
{
    public partial class XFormBase : XtraForm
    {
        private BindingSource dataSource;
        private string dataMember = string.Empty;

        [Category("Data")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        [Description("BindingSourceDataSourceDescr")]
        public BindingSource DataSource
        {
            get { return dataSource; }
            set
            {
                if (dataSource != value)
                {
                    dataSource = value;
                    if (!IsInitializing) InitializeControls();
                }
            }
        }

        public XFormBase()
        {
            InitializeComponent();
        }

        private void DataMemberChanged(object sender, EventArgs e)
        {
        }

        private void InitializeControls()
        {
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void Cancel()
        {
            throw new NotImplementedException();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            throw new NotImplementedException();
        }
    }
}
