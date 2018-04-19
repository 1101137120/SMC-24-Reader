using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SMC_24_Reader.Parameter
{
    public class ParameterList
    {
        public List<ParameterValue> GotParameterValueList(string parameterFileName) {
            List<ParameterValue> lp = new List<ParameterValue>();

            XmlRead xr = new XmlRead();
            XmlNodeList xnl = xr.ReadXml(parameterFileName);
            List<XmlNode> lx = xr.SearchNode(xnl);
            ParameterValue pv;

            foreach(XmlNode x in lx){
                pv = new ParameterValue();
                pv.name = xr.GetNodeValue(x, "name");
                pv.value = Convert.ToByte(xr.GetNodeValue(x,"value"));
                lp.Add(pv);
            }
            return lp;
        }

        public List<ReaderParameter> GotTagParameter() {
            return GotParameter("TagParameter.xml");
        }

        public List<ParameterValue> GotReaderId()
        {
            return GotParameterValueList("ReaderId.xml");
        }

        private List<ReaderParameter> GotParameter(string file)
        {
            List<ReaderParameter> list_ReaderParameter = new List<ReaderParameter>();

            XmlRead xr = new XmlRead();
            XmlNodeList xnl = xr.ReadXml(file);
            List<XmlNode> lx = xr.SearchNode(xnl);

            ReaderParameter rp;
            foreach (XmlNode x in lx)
            {
                rp = new ReaderParameter();
                rp.name = xr.GetNodeValue(x, "name");
                rp.address = Convert.ToByte(xr.GetNodeValue(x, "address"), 10);
                rp.fileName = xr.GetNodeValue(x, "file");
                rp.Bytes = Convert.ToByte(xr.GetNodeValue(x, "bytes"), 10);

                list_ReaderParameter.Add(rp);
            }

            return list_ReaderParameter;
        }

        public List<ReaderParameter> GotReaderParameter()
        {
            return GotParameter("ReaderParameter.xml");
        }
    }
}
