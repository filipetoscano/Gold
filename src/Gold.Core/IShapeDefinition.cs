using System.Xml;

namespace Gold
{
    public interface IShapeDefinition
    {
        string Name
        {
            get;
        }


        string FriendlyName
        {
            get;
        }


        string ShapeCodePrefix
        {
            get;
        }


        string ShapeCodeFormat
        {
            get;
        }


        XmlDocument FormDefinition
        {
            get;
        }


        string TextGet( string shapeCode, string xml );


        IShape InstanceCreate();
    }
}
