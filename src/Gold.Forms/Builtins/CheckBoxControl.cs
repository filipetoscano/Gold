using System.Xml;
using System.Xml.XPath;

namespace Gold.Forms.Form
{
    public partial class CheckBoxControl : PropertyBase, IFormProperty
    {
        public CheckBoxControl()
        {
            InitializeComponent();
        }


        public override void Initialize( IXPathNavigable propertyDefinition, string mode )
        {
            base.Initialize( propertyDefinition, mode );


            /*
             * 
             */
            XmlElement propertyElem = (XmlElement) propertyDefinition;

            string prompt = propertyElem.GetAttribute( "prompt" );

            if ( string.IsNullOrEmpty( prompt ) == false )
            {
                this.label.Text = this.Label;
                this.checkBox.Text = prompt;
            }
            else
            {
                this.label.Text = "";
                this.checkBox.Text = this.Label;
            }
        }


        public string Value
        {
            get
            {
                if ( this.checkBox.Checked == true )
                    return "true";
                else
                    return "false";
            }

            set
            {
                if ( value == "true" )
                    this.checkBox.Checked = true;
                else
                    this.checkBox.Checked = false;
            }
        }


        public bool FocusEx()
        {
            return this.checkBox.Focus();
        }
    }
}

/* eof */