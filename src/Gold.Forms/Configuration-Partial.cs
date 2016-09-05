using System;
using System.Linq;
using System.Xml;

namespace Gold.Forms
{
    public partial class FormsConfiguration
    {
        private XmlNamespaceManager _manager;

        public XmlNamespaceManager NsManager
        {
            get
            {
                if ( _manager == null )
                {
                    lock ( this )
                    {
                        if ( _manager == null )
                        {
                            XmlNamespaceManager manager = new XmlNamespaceManager( new NameTable() );
                            manager.AddNamespace( "xf", "urn:gold/forms" );
                            
                            foreach ( var ns in this.Namespaces )
                                manager.AddNamespace( ns.Prefix, ns.Namespace );

                            _manager = manager;
                        }
                    }
                }

                return _manager;
            }
        }


        internal FormsPropertyConfiguration Find( XmlElement element )
        {
            #region Validations

            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );

            #endregion

            string prefix = this.NsManager.LookupPrefix( element.NamespaceURI );

            if ( prefix == null )
                return null;

            string needle = string.Concat( prefix, ":", element.LocalName );

            return this.FormProperties.Where( x => x.Name == needle ).FirstOrDefault();
        }
    }
}
