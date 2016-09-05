using Platinum.Resolver;
using System;
using System.Xml;

namespace Gold
{
    public abstract class ShapeDefinitionBase<T>
        where T : IShape
    {
        protected XmlDocument FormDefinitionLoad()
        {
            Type type = typeof( T );

            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.XmlResolver = new UrlResolver();

            XmlDocument doc = new XmlDocument();

            using ( XmlReader xr = XmlReader.Create( $"assembly:///{ type.Namespace }/~/{ type.Name }.xml", xrs ) )
            {
                doc.Load( xr );
            }

            return doc;
        }


        public virtual string ShapeCodePrefix
        {
            get { return null; }
        }


        public virtual string ShapeCodeFormat
        {
            get { return null; }
        }


        public IShape InstanceCreate()
        {
            return Platinum.Activator.Create<T>( typeof( T ) );
        }


        public string TextGet( string shapeCode, string xml )
        {
            // TODO
            return "TEXT";
        }
    }
}
