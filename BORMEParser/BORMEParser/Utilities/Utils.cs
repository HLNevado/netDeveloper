using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Xml;
using BORME.Libraries;

namespace BORMEParser.Utilities
{
    public static class Utils
    {
        public static bool ProcessBORMEData(DateTime _BORMEDate, int _userId)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GetConf("BORMEURLBaseSummaryXML") + _BORMEDate.ToString("yyyy") + _BORMEDate.ToString("MM") + _BORMEDate.ToString("dd"));
                XmlElement _parentElement = doc.DocumentElement;
                XmlNodeList _nodeList = _parentElement.SelectNodes("//seccion");
                foreach (XmlNode _node in _nodeList)
                {
                    StringReader _reader = new StringReader(_nodeList[0].InnerXml.ToString());
                    DataSet _ds = new DataSet();
                    _ds.ReadXml(_reader);
                    string _section = _ds.Tables[0].Rows[0].ItemArray[2].ToString();
                    if (_section == "a1")
                    {
                        new ClsCompanies().ProcessDataDwn(_ds.Tables[1], _BORMEDate, GetConf("BORMEURLBasePDF"), _userId);
                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error al procesar los datos");
            }
        }
        public static string GetConf(string _key)
        {
            return ConfigurationManager.AppSettings[_key];
        }
    }
}
