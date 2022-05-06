using System.Data;

namespace Wisol
{
    public partial class XFormBaseGeneric<TDataSet> : XFormBase where TDataSet : DataSet
    {
        private TDataSet dataSet;

        public TDataSet DataSet
        {
            get => dataSet;
            set
            {
                dataSet = value;
            }
        }

        public XFormBaseGeneric()
        {
            InitializeComponent();
        }
    }
}