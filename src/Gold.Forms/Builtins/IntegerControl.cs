using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace Gold.Forms
{
    public partial class IntegerControl : PropertyBase, IFormProperty
    {
        protected int? MinExclusive
        {
            get;
            private set;
        }

        protected int? MinInclusive
        {
            get;
            private set;
        }

        protected int? MaxExclusive
        {
            get;
            private set;
        }

        protected int? MaxInclusive
        {
            get;
            private set;
        }


        /// <summary>
        /// Initializes a new instance of the IntegerControl class.
        /// </summary>
        public IntegerControl()
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

            this.MinExclusive = RestrictionGet( propertyElem, " xf:range/@minExclusive " );
            this.MinInclusive = RestrictionGet( propertyElem, " xf:range/@minInclusive " );
            this.MaxExclusive = RestrictionGet( propertyElem, " xf:range/@maxExclusive " );
            this.MaxInclusive = RestrictionGet( propertyElem, " xf:range/@maxInclusive " );


            /*
             * 
             */
            this.label.Text = this.Label;
        }


        public string Value
        {
            get
            {
                int v;

                if ( int.TryParse( this.textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out v ) == false )
                    return null;

                return v.ToString( CultureInfo.InvariantCulture );
            }

            set
            {
                int v;

                if ( int.TryParse( value, NumberStyles.Integer, CultureInfo.InvariantCulture, out v ) == false )
                    this.textBox.Text = "";
                else
                    this.textBox.Text = v.ToString( CultureInfo.InvariantCulture );
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


            /*
             * Validate according to type.
             */
            if ( string.IsNullOrEmpty( this.textBox.Text.Trim() ) == false )
            {
                int v;

                if ( int.TryParse( this.textBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out v ) == false )
                {
                    e.Cancel = true;
                    this.textBox.SelectAll();
                    MarkInvalid( this.label, this.textBox );
                }
                else
                {
                    if ( this.MinExclusive.HasValue == true )
                    {
                        if ( v <= this.MinExclusive )
                        {
                            e.Cancel = true;
                            this.textBox.SelectAll();
                            MarkInvalid( this.label, this.textBox );
                        }
                    }

                    if ( this.MinInclusive.HasValue == true )
                    {
                        if ( v < this.MinInclusive )
                        {
                            e.Cancel = true;
                            this.textBox.SelectAll();
                            MarkInvalid( this.label, this.textBox );
                        }
                    }

                    if ( this.MaxExclusive.HasValue == true )
                    {
                        if ( v >= this.MaxExclusive )
                        {
                            e.Cancel = true;
                            this.textBox.SelectAll();
                            MarkInvalid( this.label, this.textBox );
                        }
                    }

                    if ( this.MaxInclusive.HasValue == true )
                    {
                        if ( v > this.MaxInclusive )
                        {
                            e.Cancel = true;
                            this.textBox.SelectAll();
                            MarkInvalid( this.label, this.textBox );
                        }
                    }
                }
            }


            /*
             * Validate required.
             */
            if ( this.IsRequired == false )
                return;

            if ( string.IsNullOrEmpty( this.textBox.Text.Trim() ) == true )
            {
                e.Cancel = true;
                this.textBox.SelectAll();
                MarkInvalid( this.label, this.textBox );
            }
        }


        private void textBox_Validated( object sender, EventArgs e )
        {
            MarkValid( this.label, this.textBox );
        }
    }
}

/* eof */