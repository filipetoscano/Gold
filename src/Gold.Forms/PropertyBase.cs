using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Gold.Forms
{
    public class PropertyBase : UserControl
    {
        private Dictionary<string, string> _conditionals;


        public virtual void Initialize( IXPathNavigable propertyDefinition, string mode )
        {
            #region Validations

            if ( propertyDefinition == null )
                throw new ArgumentNullException( "propertyDefinition" );

            if ( mode == null )
                throw new ArgumentNullException( "mode" );

            #endregion

            XmlElement propertyElem = (XmlElement) propertyDefinition;
            var config = FormsConfiguration.Current;


            /*
             * 
             */
            this.Id = propertyElem.Attributes[ "id" ].Value;
            this.Name = this.Id;
            this.Label = propertyElem.Attributes[ "name" ].Value;

            
            /*
             * s:required/
             */
            XmlElement requiredElem = (XmlElement) propertyElem.SelectSingleNode( " s:required ", config.NsManager );

            if ( requiredElem != null )
            {
                string forMode = requiredElem.GetAttribute( "for" );

                if ( string.IsNullOrEmpty( forMode ) == true || forMode == "#all" )
                {
                    this.IsRequired = true;
                }
                else
                {
                    // TODO
                    //int ixCurrent = ModelConfiguration.ModeValue( mode );
                    //int ixDemand = ModelConfiguration.ModeValue( forMode );

                    if ( true )
                    {
                        this.IsRequired = true;
                    }
                }
            }


            /*
             * s:hasConditionals/
             */
            XmlElement hasConditionalsElem = (XmlElement) propertyElem.SelectSingleNode( " s:hasConditionals ", config.NsManager );

            if ( hasConditionalsElem != null )
                this.HasConditionals = true;


            /*
             * s:conditional/
             */
            XmlNodeList conditionals = propertyElem.SelectNodes( " s:conditional[ @ref and @value ] ", config.NsManager );

            if ( conditionals != null && conditionals.Count > 0 )
            {
                this.IsConditional = true;
                _conditionals = new Dictionary<string, string>();

                foreach ( XmlElement condElem in conditionals )
                {
                    string @ref = condElem.Attributes[ "ref" ].Value;
                    string @value = condElem.Attributes[ "value" ].Value;

                    _conditionals.Add( @ref, @value );
                }
            }
        }


        public string Id
        {
            get;
            private set;
        }

        public string Label
        {
            get;
            private set;
        }

        public bool IsConditional
        {
            get;
            private set;
        }


        public bool IsRequired
        {
            get;
            private set;
        }


        public bool IsVisible
        {
            get;
            private set;
        }


        public bool HasConditionals
        {
            get;
            private set;
        }


        public Control Control
        {
            get { return this; }
        }


        public event EventHandler RebuildConditionals;


        public void OnRebuildConditionals()
        {
            if ( this.HasConditionals == false )
                return;

            if ( RebuildConditionals != null )
                RebuildConditionals( this, new EventArgs() );
        }


        public void EvaluateConditional( Dictionary<string,string> properties )
        {
            #region Validations

            if ( properties == null )
                throw new ArgumentNullException( nameof( properties ) );

            #endregion


            /*
             * 
             */
            if ( _conditionals == null || _conditionals.Count == 0 )
            {
                this.IsVisible = true;
                this.Visible = true;

                return;
            }
            

            /*
             * 
             */
            bool isVisible = false;

            foreach ( string key in _conditionals.Keys )
            {
                if ( properties[ key ] == _conditionals[ key ] )
                {
                    isVisible = true;
                    break;
                }
            }

            this.Visible = isVisible;
            this.IsVisible = isVisible;
        }



        protected static void MarkInvalid( Control label, params Control[] controls )
        {
            #region Validations

            if ( label == null )
                throw new ArgumentNullException( "label" );

            #endregion

            label.ForeColor = Color.Red;

            foreach ( Control c in controls )
            {
                c.BackColor = Color.Pink;
            }
        }


        protected static void MarkValid( Control label, params Control[] controls )
        {
            #region Validations

            if ( label == null )
                throw new ArgumentNullException( "label" );

            #endregion

            label.ForeColor = Color.Black;

            foreach ( Control c in controls )
            {
                c.BackColor = Color.White;
            }
        }


        [SuppressMessage( "Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode" )]
        protected static int? RestrictionGet( XmlElement property, string xp )
        {
            #region Validations

            if ( property == null )
                throw new ArgumentNullException( "property" );

            if ( xp == null )
                throw new ArgumentNullException( "xp" );

            #endregion

            XmlAttribute attr = (XmlAttribute) property.SelectSingleNode( xp, FormsConfiguration.Current.NsManager );

            if ( attr == null )
                return null;

            return int.Parse( attr.Value, CultureInfo.InvariantCulture );
        }
    }
}

/* eof */
