using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronXml;
using Crestron.SimplSharp.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ThydenUtils;

namespace Roku
{
    public class RokuTcp
    {
        public string BaseUrl = "http://192.168.1.56";
        public List<RokuApp> Apps = new List<RokuApp>();
        public ushort Appnum;
        public string[] AppName;
        public string[] AppId;
        public string[] AppType;
        private string _url;
        private string _response;

        public event EventHandler AppUpdate;

        public RokuTcp()
        {
        }

        private string sendApp(string id)
        {
            _url = BaseUrl + "/launch/" + id;
            return _url;
        }

        public void HttpApp(int i)
        {
            try
            {
                _response = HttpConnect.Instance.Request(sendApp(Apps[i].Id), null, RequestType.Post);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(string.Format("Error sending command: {0} ", ex.Message));
            }
        }

        public void HttpGetApps()
        {
            try
            {
                _response = HttpConnect.Instance.Request((BaseUrl + "/query/apps"), null, RequestType.Get);
                Apps.Clear();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(_response);
                string json = JsonConvert.SerializeXmlNode(xmlDoc);
                JObject appData = JObject.Parse(json);
                string id;
                string type;
                string ver;
                string name;
                ushort num;
                var appNode = appData["apps"].SelectToken("app");
                var appArray = (JArray)appNode;
                for (int i = 0; i < appArray.Count; i++)
                {
                    id = (string) appArray[i].SelectToken("@id");
                    type = (string)appArray[i].SelectToken("@type");
                    ver = (string)appArray[i].SelectToken("@version");
                    name = (string)appArray[i].SelectToken("#text");
                    Apps.Add(new RokuApp(name, ver, type, id));
                }
                Appnum = (ushort)Apps.Count;
                AppName = new string[Apps.Count];
                AppId = new string[Apps.Count];
                AppType = new string[Apps.Count];
                for (int index = 0; index < Apps.Count; ++index)
                {
                    AppName[index] = Apps[index].Name;
                    AppId[index] = Apps[index].Id;
                    AppType[index] = Apps[index].Type;
                }
                TriggerAppUpdate();
            }

            catch (Exception ex)
            {
                ErrorLog.Error(string.Format("Error sending command: {0} ", ex.Message));

            }
        }

        public void HttpCall(string cmd)
        {
            try
            {
                string url = BaseUrl + cmd;
                _response = HttpConnect.Instance.Request(url, null, RequestType.Post);  
            }
            catch (Exception ex)
            {
                ErrorLog.Error(string.Format("Error sending command: {0} ", ex.Message));
            }
        }

        public void TriggerAppUpdate()
        {
            AppUpdate(this, new EventArgs());
        }
    }

    public class RokuApp
    {
        public string Id;
        public string Name;
        public string Type;
        public string Version;

        public RokuApp(string name, string ver, string type, string id)
        {
            Name = name;
            Version = ver;
            Id = id;
            Type = type;
        }
    }
}