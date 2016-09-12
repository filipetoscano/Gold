using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Gold.Forms
{
    public partial class StringControl : PropertyBase, IFormProperty
    {
        protected int? MinLength
        {
            get;
            private set;
        }

        protected int? MaxLength
        {
            get;
            private set;
        }


        /// <summary>
        /// Initializes a new instance of the StringControl class.
        /// </summary>
        public StringControl()
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
            string multiline = propertyElem.GetAttribute( "multiline" );

            if ( multiline == "true" || multiline == "1" )
            {
                this.textBox.Multiline = true;
                this.Height = 110;
            }


            /*
             * 
             */
            this.MinLength = RestrictionGet( propertyElem, " xf:length/@min " );
            this.MaxLength = RestrictionGet( propertyElem, " xf:length/@max " );


            /*
             * 
             */
            this.label.Text = this.Label;
        }


        public string Value
        {
            get
            {
                string v = this.textBox.Text.TrimEnd().Replace( "\r\n", "\n" );

                if ( v.Length == 0 )
                    return null;
                else
                    return v;
            }

            set
            {
                if ( this.textBox.Multiline == true )
                {
                    Regex regex = new Regex( "(?<!\r)\n" );
                    this.textBox.Text = regex.Replace( value ?? "", "\r\n" );
                }
                else
                {
                    this.textBox.Text = value ?? "";
                }
            }
        }


        public bool FocusEx()
        {
            return this.textBox.Focus();
        }


        private void textBox_Validating( object sender, CancelEventArgs e )
        {
            if ( this.IsVisible == false )
                return;

            if ( this.IsRequired == true )
            {
                if ( string.IsNullOrEmpty( this.Value ) == true )
                {
                    e.Cancel = true;
                    this.textBox.SelectAll();
                    MarkInvalid( this.label, this.textBox );
                }
            }

            if ( this.Value == null )
                return;

            if ( this.MinLength.HasValue == true )
            {
                if ( this.Value.Length < this.MinLength )
                {
                    e.Cancel = true;
                    this.textBox.SelectAll();
                    MarkInvalid( this.label, this.textBox );
                }
            }

            if ( this.MaxLength.HasValue == true )
            {
                if ( this.Value.Length > this.MaxLength )
                {
                    e.Cancel = true;
                    this.textBox.SelectAll();
                    MarkInvalid( this.label, this.textBox );
                }
            }
        }


        private void textBox_Validated( object sender, EventArgs e )
        {
            MarkValid( this.label, this.textBox );
        }


        private void textBox_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.A && e.Control )
            {
                this.textBox.SelectAll();
            }
        }
    }
}

/* eof */