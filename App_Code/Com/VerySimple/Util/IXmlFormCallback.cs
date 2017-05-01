using System.Collections;
using System.Xml;

public interface IXmlFormCallback
{
    Hashtable GetFormOptions(XmlNode field, string param);
    string GetFormDefault(XmlNode field, string param);
}
