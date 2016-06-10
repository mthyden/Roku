using System;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;
using Crestron.SimplSharp.CrestronXml;
using System.Collections.Generic;

namespace Roku
{
    public class RokuClient
    {
        public String baseurl = "http://192.168.1.56";
        private String url;
        public List<RokuApp> Apps = new List<RokuApp>();
        public ushort appnum;
        public String app1name;
        public String app2name;
        public String app3name;
        public String app4name;
        public String app5name;
        public String app6name;
        public String app7name;
        public String app8name;
        public String app9name;
        public String app10name;
        public String app11name;
        public String app12name;
        public String app13name;
        public String app14name;
        public String app15name;
        public String app16name;
        public String app17name;
        public String app18name;
        public String app19name;
        public String app20name;
        public String app1id;
        public String app2id;
        public String app3id;
        public String app4id;
        public String app5id;
        public String app6id;
        public String app7id;
        public String app8id;
        public String app9id;
        public String app10id;
        public String app11id;
        public String app12id;
        public String app13id;
        public String app14id;
        public String app15id;
        public String app16id;
        public String app17id;
        public String app18id;
        public String app19id;
        public String app20id;
        public String app1type;
        public String app2type;
        public String app3type;
        public String app4type;
        public String app5type;
        public String app6type;
        public String app7type;
        public String app8type;
        public String app9type;
        public String app10type;
        public String app11type;
        public String app12type;
        public String app13type;
        public String app14type;
        public String app15type;
        public String app16type;
        public String app17type;
        public String app18type;
        public String app19type;
        public String app20type;

        public RokuClient()
        {
        }

        private string SendCmd(int i)
        {
            switch(i)
            {
                case 1:
                    {
                        url = (baseurl + "/keypress/up");
                        break;
                    }
                case 2:
                    {
                        url = (baseurl + "/keypress/down");
                        break;
                    }
                case 3:
                    {
                        url = (baseurl + "/keypress/left");
                        break;
                    }
                case 4:
                    {
                        url = (baseurl + "/keypress/right");
                        break;
                    }
                case 5:
                    {
                        url = (baseurl + "/keypress/select");
                        break;
                    }
                case 6:
                    {
                        url = (baseurl + "/query/apps");
                        break;
                    }
                case 7:
                    {
                        url = (baseurl + "/keypress/home");
                        break;
                    }
                case 8:
                    {
                        url = (baseurl + "/keypress/play");
                        break;
                    }
                case 9:
                    {
                        url = (baseurl + "/keypress/rew");
                        break;
                    }
                case 10:
                    {
                        url = (baseurl + "/keypress/fwd");
                        break;
                    }
                case 11:
                    {
                        url = (baseurl + "/keypress/info");
                        break;
                    }
                case 12:
                    {
                        url = (baseurl + "/keypress/back");
                        break;
                    }
                case 13:
                    {
                        url = (baseurl + "/keypress/search");
                        break;
                    }
                case 14:
                    {
                        url = (baseurl + "/keypress/backspace");
                        break;
                    }
                case 15:
                    {
                        url = (baseurl + "/keypress/enter");
                        break;
                    }
                case 16:
                    {
                        url = (baseurl + "/keypress/instantreplay");
                        break;
                    }
            }
            //CrestronConsole.PrintLine("cmd is {0}", url);
            return url;

        }

        private string sendApp(string id)
        {
            url = (baseurl + "/launch/" + id);
            return url;
        }

        public void HttpApp(int i)
        {
            try
            {
                HttpClient myHttpClient = new HttpClient();
                HttpClientRequest myHttpRequest = new HttpClientRequest();
                myHttpClient.TimeoutEnabled = true;
                myHttpClient.Timeout = 5;
                myHttpClient.KeepAlive = false;
                int listVal = (i - 21);
                myHttpRequest.Url.Parse(sendApp(Apps[listVal].Id)); //Need access to list here
                myHttpRequest.RequestType = RequestType.Post;
                HttpClientResponse myHttpResponse = myHttpClient.Dispatch(myHttpRequest);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(String.Format("Error sending command: {0} ", ex.Message));
            }
        }

        public void HttpCall(int i)
        {
            try
            {             
                HttpClient myHttpClient = new HttpClient();
                HttpClientRequest myHttpRequest = new HttpClientRequest();
                myHttpClient.TimeoutEnabled = true;
                myHttpClient.Timeout = 5;
                myHttpClient.KeepAlive = false;
                myHttpRequest.Url.Parse(SendCmd(i));
                if (i == 6)
                {
                    myHttpRequest.RequestType = RequestType.Get;
                }
                else
                {
                    myHttpRequest.RequestType = RequestType.Post;
                }
                HttpClientResponse myHttpResponse = myHttpClient.Dispatch(myHttpRequest);
                if (i == 6)
                {
                    string tempName;
                    string tempId;
                    string tempVersion;
                    string tempType;
                    Apps.Clear();
                    XmlReader myReader = new XmlReader(myHttpResponse.ContentString);
                    while (!myReader.EOF)
                    {
                        myReader.Read();
                        if (myReader.NodeType == XmlNodeType.Element)
                        {
                            if (myReader.Name.ToUpper() == "APP" && myReader.HasAttributes)
                            {
                                myReader.MoveToAttribute("id");
                                tempId = myReader.Value;
                                myReader.MoveToAttribute("type");
                                tempType = myReader.Value;
                                myReader.MoveToAttribute("version");
                                tempVersion = myReader.Value;
                                tempName = myReader.ReadElementString();
                                Apps.Add(new RokuApp(tempName, tempVersion, tempType, tempId));
                                appnum = (ushort)Apps.Count;
                                CrestronConsole.PrintLine("{0}, {1}, {2}. {3}",tempName, tempVersion, tempType, tempId);
                            }
                        }
                    }
                    app1name = Apps[0].Name;
                    app1id = Apps[0].Id;
                    app1type = Apps[0].Type;
                    app2name = Apps[1].Name;
                    app2id = Apps[1].Id;
                    app2type = Apps[1].Type;
                    app3name = Apps[2].Name;
                    app3id = Apps[2].Id;
                    app3type = Apps[2].Type;
                    app4name = Apps[3].Name;
                    app4id = Apps[3].Id;
                    app4type = Apps[3].Type;
                    app5name = Apps[4].Name;
                    app5id = Apps[4].Id;
                    app5type = Apps[4].Type;
                    app6name = Apps[5].Name;
                    app6id = Apps[5].Id;
                    app6type = Apps[5].Type;
                    app7name = Apps[6].Name;
                    app7id = Apps[6].Id;
                    app7type = Apps[6].Type;
                    app8name = Apps[7].Name;
                    app8id = Apps[7].Id;
                    app8type = Apps[7].Type;
                    app9name = Apps[8].Name;
                    app9id = Apps[8].Id;
                    app9type = Apps[8].Type;
                    app10name = Apps[9].Name;
                    app10id = Apps[9].Id;
                    app10type = Apps[9].Type;
                    app11name = Apps[10].Name;
                    app11id = Apps[10].Id;
                    app11type = Apps[10].Type;
                    app12name = Apps[11].Name;
                    app12id = Apps[11].Id;
                    app12type = Apps[11].Type;
                    app13name = Apps[12].Name;
                    app13id = Apps[12].Id;
                    app13type = Apps[12].Type;
                    app14name = Apps[13].Name;
                    app14id = Apps[13].Id;
                    app14type = Apps[13].Type;
                    app15name = Apps[14].Name;
                    app15id = Apps[14].Id;
                    app15type = Apps[14].Type;
                    app16name = Apps[15].Name;
                    app16id = Apps[15].Id;
                    app16type = Apps[15].Type;
                    app17name = Apps[16].Name;
                    app17id = Apps[16].Id;
                    app17type = Apps[16].Type;
                    app18name = Apps[17].Name;
                    app18id = Apps[17].Id;
                    app18type = Apps[17].Type;
                    app19name = Apps[18].Name;
                    app19id = Apps[18].Id;
                    app19type = Apps[18].Type;
                    app20name = Apps[19].Name;
                    app20id = Apps[19].Id;
                    app20type = Apps[19].Type;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(String.Format("Error sending command: {0} ", ex.Message));
            }
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
            this.Name = name;
            this.Version = ver;
            this.Id = id;
            this.Type = type;
        }
    }
}
