using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SMC_24_Reader.Parameter
{
    public class XmlRead
    {
        public XmlNodeList ReadXml(string fileName)
        {
            string file = System.Windows.Forms.Application.StartupPath + "/Parameter/" + fileName;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(file);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("parameter").ChildNodes;//獲取Parent節點的所有子節點
            return nodeList;
        }

        public List<XmlNode> SearchNode(XmlNodeList xnl)
        {
            List<XmlNode> lxn = new List<XmlNode>();

            int count = xnl.Count;

            for (int i = 0; i < count; i++)
            {
                lxn.Add(xnl[i]);
            }

            return lxn;
        }

        public XmlNode LoadNode(string fileName, string nodeName)
        {
            foreach (XmlNode xn in LoadXmlFile(fileName))
            {
                if (GetNodeValue(xn, "name") == nodeName) return xn;
            }

            return null;
        }

        public string GetNodeValue(XmlNode xn, string name)
        {
            foreach (XmlNode cxn in xn.ChildNodes)
            {
                if (cxn.Name == name) return cxn.InnerText;
            }

            return null;
        }

        public List<XmlNode> LoadXmlFile(string fileName)
        {
            string file = System.Windows.Forms.Application.StartupPath + "/Parameter/" + fileName;
            XmlNodeList cm = ReadXml(file);
            return SearchNode(cm);
        }
    }
}
