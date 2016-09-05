using Gold;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using WForm = System.Windows.Forms.Form;

namespace Gold.Forms
{
    public partial class XmlForm : WForm
    {
        private Dictionary<string, IFormProperty> _properties;
        private List<IFormProperty> _propertiesList;


        public XmlForm()
        {
            InitializeComponent();


            /*
             * 
             */
            if ( Properties.Settings.Default.Size.IsEmpty == false )
            {
                this.Height = Properties.Settings.Default.Size.Height;
                this.Width = Properties.Settings.Default.Size.Width;
            }

            if ( Properties.Settings.Default.Position.IsEmpty == false )
            {
                this.Location = Properties.Settings.Default.Position;
            }
        }


        public void Initialize( IShapeDefinition shape, string mode, string shapeCode, string shapeXml )
        {
            #region Validations

            if ( shape == null )
                throw new ArgumentNullException( nameof( shape ) );

            if ( mode == null )
                throw new ArgumentNullException( nameof( mode ) );

            #endregion


            /*
             *
             */
            if ( string.IsNullOrEmpty( shapeCode ) == true )
                this.Text = shape.FriendlyName;
            else
                this.Text = string.Concat( shape.FriendlyName, ": ", shapeCode );


            /*
             * 
             */
            Dictionary<string,string> values = new Dictionary<string, string>();

            if ( string.IsNullOrEmpty( shapeXml ) == false )
            {
                XmlDocument docShapeXml = new XmlDocument();
                docShapeXml.LoadXml( shapeXml );

                foreach ( XmlElement elem in docShapeXml.SelectNodes( " /r/* " ) )
                {
                    values.Add( elem.LocalName, elem.InnerText );
                }
            }


            /*
             * 
             */
            var config = FormsConfiguration.Current;

            XmlDocument document = shape.FormDefinition;
            _properties = new Dictionary<string, IFormProperty>();
            _propertiesList = new List<IFormProperty>();

            foreach ( XmlElement section in document.SelectNodes( " /xf:shape/xf:section ", config.NsManager ) )
            {
                string sectionName = section.GetAttribute( "name" );


                TabPage tabPage = new TabPage( sectionName );
                tabPage.Name = sectionName;

                Panel tabPanel = new Panel();
                tabPanel.Dock = DockStyle.Fill;
                tabPanel.Padding = new Padding( 5 );

                tabPage.Controls.Add( tabPanel );


                /*
                 * 
                 */
                foreach ( XmlElement property in section.SelectNodes( " *[ @id and @name ] ", config.NsManager ) )
                {
                    string propertyId = property.GetAttribute( "id" );


                    /*
                     * 
                     */
                    var propertyConfig = config.Find( property );

                    if ( propertyConfig == null )
                        continue;


                    /*
                     * 
                     */
                    IFormProperty prop = Platinum.Activator.Create<IFormProperty>( propertyConfig.Moniker );
                    prop.Control.Dock = DockStyle.Top;
                    prop.Initialize( property, mode );
                    prop.Value = values[ propertyId ];
                    prop.EvaluateConditional( values );
                    prop.RebuildConditionals += new EventHandler( RebuildConditionals );

                    tabPanel.Controls.Add( prop.Control );

                    _properties.Add( prop.Id, prop );
                    _propertiesList.Add( prop );
                }


                /*
                 * This effectively re-orders the children, so that the z-Index
                 * matches their position. This will allow the user to tab
                 * correctly from field to field.
                 */
                foreach ( Control c in tabPanel.Controls )
                {
                    c.BringToFront();
                }

                tabs.TabPages.Add( tabPage );
            }


            /*
             * 
             */
            _propertiesList.Reverse();


            /*
             * Ergonomy: keep a preference of the last used tab, and automatically
             * set the current selected tab. This will allow an end-user to keep
             * focussed on the most relevant tab.
             */
            if ( string.IsNullOrEmpty( Properties.Settings.Default.SelectedTab ) == false )
                tabs.SelectTab( Properties.Settings.Default.SelectedTab );
        }



        private void Form_Shown( object sender, EventArgs e )
        {
            FocusFirstCompositeControl();
        }


        private void Tabs_SelectedIndexChanged( object sender, EventArgs e )
        {
            FocusFirstCompositeControl();
        }


        private void FocusFirstCompositeControl()
        {
            if ( tabs.SelectedTab == null )
                return;

            Panel panel = (Panel) tabs.SelectedTab.Controls[ 0 ];

            if ( panel.Controls.Count == 0 )
                return;

            for ( int i = 0; i < panel.Controls.Count; i++ )
            {
                Control c = panel.Controls[ panel.Controls.Count - i - 1 ];

                if ( c.Visible == false )
                    continue;

                if ( _properties.ContainsKey( c.Name ) == false )
                    continue;

                _properties[ c.Name ].FocusEx();
                break;
            }
        }


        [SuppressMessage( "Microsoft.Reliability", "CA2002:DoNotLockOnObjectsWithWeakIdentity" )]
        private void RebuildConditionals( object sender, EventArgs e )
        {
            lock ( this )
            {
                Dictionary<string, string> propertyValues = PropertyValuesGet();

                foreach ( IFormProperty property in _propertiesList )
                {
                    if ( property.IsConditional == false )
                        continue;

                    property.EvaluateConditional( propertyValues );
                }
            }
        }


        private void buttonOk_Click( object sender, EventArgs e )
        {
            /*
             * 
             */
            if ( this.ValidateChildren() == false )
                return;


            /*
             * 
             */
            var values = PropertyValuesGet();
            this.ShapeXml = ToXml( values );


            /*
             * 
             */
            Size size = new Size();
            size.Height = this.Height;
            size.Width = this.Width;

            Point point = new Point();
            point.X = this.Location.X;
            point.Y = this.Location.Y;

            Properties.Settings.Default.Size = size;
            Properties.Settings.Default.Position = point;

            if ( this.tabs.TabCount > 0 )
                Properties.Settings.Default.SelectedTab = this.tabs.SelectedTab.Name;

            Properties.Settings.Default.Save();


            /*
             * 
             */
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        /// <summary>
        /// Gets the new/updated shape XML.
        /// </summary>
        /// <remarks>
        /// Value is only set when the DialogResult is set to OK.
        /// </remarks>
        public string ShapeXml
        {
            get;
            private set;
        }


        /// <summary>
        /// Iterates through all of the properties and constructs a bag
        /// with all of the property values.
        /// </summary>
        /// <returns>NVC.</returns>
        private Dictionary<string, string> PropertyValuesGet()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            foreach ( IFormProperty property in _properties.Values )
            {
                if ( property.IsVisible == false )
                    continue;

                string value = property.Value;

                if ( value == null )
                    continue;

                values.Add( property.Id, value );
            }

            return values;
        }


        /// <summary>
        /// Converts the property bag from the <see cref="NameValueCollection"/> container
        /// to the specific XML format.
        /// </summary>
        /// <param name="properties">Property bag.</param>
        /// <returns>XML equivalent.</returns>
        private static string ToXml( Dictionary<string, string> properties )
        {
            #region Validations

            if ( properties == null )
                throw new ArgumentNullException( nameof( properties ) );

            #endregion

            XDocument doc = XDocument.Parse( "<r/>" );

            foreach ( string key in properties.Keys )
            {
                string value = properties[ key ];
                doc.Root.Add( new XElement( key, new XText( value ) ) );
            }

            return doc.ToString( SaveOptions.DisableFormatting );
        }
    }
}

/* eof */