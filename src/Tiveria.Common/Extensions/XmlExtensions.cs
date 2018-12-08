using System;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;

namespace Tiveria.Common.Extensions
{
    public static class XmlExtensions
    {
        public static XmlNode AppendAttribute(this XmlNode node, string attribute, string value)
        {
            XmlAttribute attrib = node.OwnerDocument.CreateAttribute(attribute);
            attrib.InnerText = value;
            node.Attributes.Append(attrib);
            return node;
        }

        public static XmlNode AppendElement(this XmlNode node, string name, string value)
        {
            XmlNode newnode = node.OwnerDocument.CreateElement(name);
            newnode.InnerText = value;
            node.AppendChild(newnode);
            return node;
        }

        public static T AttributeValue<T>(this XElement element, string attributename, T defaultvalue)
        {
            if (element == null)
                return defaultvalue;

            XAttribute attribute = element.Attribute(attributename);

            if (attribute == null)
                return defaultvalue;

            try
            {
                T value = (T)Convert.ChangeType(attribute.Value, typeof(T));
                return value;
            }
            catch
            { 
                return defaultvalue;
            }
        }

        public static T ElementAttributeValue<T>(this XElement element, string subelementname, string attributename, T defaultvalue)
        {
            if (element == null)
                return defaultvalue;

            element = element.Element(subelementname);

            if (element == null)
                return defaultvalue;

            XAttribute attribute = element.Attribute(attributename);

            if (attribute == null)
                return defaultvalue;

            try
            {
                T value = (T)Convert.ChangeType(attribute.Value, typeof(T));
                return value;
            }
            catch
            {
                return defaultvalue;
            }
        }

        public static T Value<T>(this XElement element, T defaultvalue)
        {
            if (element == null)
                return defaultvalue;

            try
            {
                T value = (T)Convert.ChangeType(element.Value, typeof(T));
                return value;
            }
            catch
            {
                return defaultvalue;
            }
        }

        public static T ElementValue<T>(this XElement element, string subelementname, T defaultvalue)
        {
            if (element == null)
                return defaultvalue;

            element = element.Element(subelementname);

            if (element == null)
                return defaultvalue;

            try
            {
                T value = (T)Convert.ChangeType(element.Value, typeof(T));
                return value;
            }
            catch
            {
                return defaultvalue;
            }
        }

    }
}
