using System;
using System.Xml;
using System.Xml.XPath;

namespace Gold.Forms
{
    public partial class ListControl : PropertyBase, IFormProperty
    {
        public ListControl()
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

            this.comboBox.DisplayMember = "Label";
            this.comboBox.ValueMember = "Value";

            foreach ( XmlElement option in propertyElem.SelectNodes( " s:options/s:option[ @label and @value ] ", FormsConfiguration.Current.NsManager ) )
            {
                ListItem item = new ListItem()
                {
                    Label = option.Attributes[ "label" ].Value,
                    Value = option.Attributes[ "value" ].Value
                };

                this.comboBox.Items.Add( item );
            }


            /*
             * 
             */
            this.label.Text = this.Label;
        }


        public string Value
        {
            get
            {
                ListItem item = (ListItem) this.comboBox.SelectedItem;

                if ( item == null )
                    return null;

                return item.Value;
            }

            set
            {
                foreach ( ListItem item in this.comboBox.Items )
                {
                    if ( item.Value == value )
                    {
                        this.comboBox.SelectedItem = item;
                    }
                }
            }
        }


        public bool FocusEx()
        {
            return this.comboBox.Focus();
        }


        private void comboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            OnRebuildConditionals();
        }


        private void comboBox_Validating( object sender, System.ComponentModel.CancelEventArgs e )
        {
            if ( this.IsVisible == false )
                return;

            /*
             * Validate required.
             */
            if ( this.IsRequired == false )
                return;

            if ( this.comboBox.SelectedIndex < 0 )
            {
                e.Cancel = true;
                MarkInvalid( this.label, this.comboBox );
            }
        }

        private void comboBox_Validated( object sender, EventArgs e )
        {
            MarkValid( this.label, this.comboBox );
        }
    }
}

/* eof */