﻿using Platinum.Resolver;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

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


        public string TextGet( string shapeCode, string xml )
        {
            #region Validations

            if ( xml == null )
                throw new ArgumentNullException( nameof( xml ) );

            #endregion

            XmlSerializer ser = new XmlSerializer( typeof( T ) );
            T instance;

            using ( var tr = new StringReader( xml ) )
            {
                using ( var xr = XmlReader.Create( tr ) )
                {
                    instance = (T) ser.Deserialize( xr );
                }
            }

            instance.ShapeCode = shapeCode;

            return instance.TextGet();
        }
    }
}
