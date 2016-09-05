using System.Xml;

namespace Gold
{
    public interface IShapeDefinition
    {
        string PublicName
        {
            get;
        }


        XmlDocument FormDefinition
        {
            get;
        }
    }
}
