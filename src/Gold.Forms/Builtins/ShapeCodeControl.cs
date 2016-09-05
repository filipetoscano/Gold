using System.Xml;
using System.Xml.XPath;

namespace Gold.Forms
{
    public partial class ShapeIdControl : PropertyBase, IFormProperty
    {
        public ShapeIdControl()
        {
            InitializeComponent();
        }


        public override void Initialize( IXPathNavigable propertyDefinition, string mode )
        {
            base.Initialize( propertyDefinition, mode );


            /*
             * 
             */
            this.label.Text = this.Label;
        }


        public string Value
        {
            get
            {
                return this.textBox.Text;
            }

            set
            {
                this.textBox.Text = value;
            }
        }


        public bool FocusEx()
        {
            return this.textBox.Focus();
        }
    }
}

/* eof */