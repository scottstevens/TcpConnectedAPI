using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using TCPLightingWebServer.Domain;
using TCPLightingWebServer.Models;
using TCPLightingWebServer;

namespace TCPConnectedAPI.Code
{
    public abstract class BaseLightController : ApiController
    {
        private string _baseLoginCommand = "<gip><version>1</version><email>{0}</email><password>{1}</password></gip>";
        private string _getStateString = "<gwrcmds><gwrcmd><gcmd>RoomGetCarousel</gcmd><gdata><gip><version>1</version><token>{0}</token><fields>name,control,power,product,class,realtype,status</fields></gip></gdata></gwrcmd></gwrcmds>";
        private string _lightingURL { get; set; }
        private string _requestString = "cmd={0}&data={1}&fmt=xml";
        private string _roomSendCommand = "<gip><version>1</version><token>{0}</token><rid>{1}</rid><value>{2}</value></gip>";
        private string _roomSendLevelCommand = "<gip><version>1</version><token>{0}</token><rid>{1}</rid><value>{2}</value><type>level</type></gip>";
        private string _deviceSendCommand = "<gip><version>1</version><token>{0}</token><did>{1}</did><value>{2}</value></gip>";
        private string _deviceSendLevelCommand = "<gip><version>1</version><token>{0}</token><did>{1}</did><value>{2}</value><type>level</type></gip>";

        protected LightingDomainDataContext Context { get; private set; }

        public BaseLightController()
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true; // YOLO
            _lightingURL = "https://192.168.11.62" + ":443/gwr/gop.php";
            Context = new LightingDomainDataContext();
        }

        private string SendCommand(string gipString, string command)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string formattedCommand = String.Format(_requestString, command, Uri.EscapeUriString(gipString));
            var payload = encoding.GetBytes(formattedCommand);
            var request = WebRequest.Create(_lightingURL);
            request.Method = "POST";
            request.ContentType = "text/html";
            request.ContentLength = payload.Length;
            using (var sw = request.GetRequestStream())
            {
                sw.Write(payload, 0, payload.Length);
            }

            var res = request.GetResponse();
            string data = String.Empty;
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                data = sr.ReadToEnd();

            }
            return data;
        }

        protected List<Room> GetRoomInfo(string token)
        {
            var command = string.Format(_getStateString, token);
            var data = SendCommand(command, "GWRBatch");
            var xDoc = XDocument.Parse(data);
            var roomsXml = xDoc.Descendants("room").ToList();
            var rooms = new List<Room>();
            foreach (var roomXml in roomsXml)
            {
                var room = new Room()
                {
                    RoomID = roomXml.Element("rid").Value,
                    Name = roomXml.Element("name").Value,
                    Known = roomXml.Element("known").Value,
                    Type = roomXml.Element("type").Value,
                    Color = roomXml.Element("color").Value,
                    ColorID = roomXml.Element("colorid").Value,
                    Image = roomXml.Element("img").Value,
                    Power = roomXml.Element("power").Value,
                    PowerAvg = roomXml.Element("poweravg").Value,
                    Energy = roomXml.Element("energy").Value
                };
                room.Devices = new List<Device>();

                foreach (var dev in roomXml.Elements("device"))
                {
                    var device = new Device()
                    {
                        DeviceID = dev.Element("did").ToStringOrEmpty(),
                        Known = dev.Element("known").ToStringOrEmpty(),
                        Lock = dev.Element("lock").ToStringOrEmpty(),
                        State = dev.Element("state").ToStringOrEmpty(),
                        Level = dev.Element("level").ToStringOrEmpty(),
                        Node = dev.Element("node").ToStringOrEmpty(),
                        Port = dev.Element("port").ToStringOrEmpty(),
                        NodeType = dev.Element("nodetype").ToStringOrEmpty(),
                        Name = dev.Element("name").ToStringOrEmpty(),
                        Description = dev.Element("desc").ToStringOrEmpty(),
                        ColorID = dev.Element("colorid").ToStringOrEmpty(),
                        Type = dev.Element("type").ToStringOrEmpty(),
                        RangeMin = dev.Element("rangemin").ToStringOrEmpty(),
                        RangeMax = dev.Element("rangemax").ToStringOrEmpty(),
                        Power = dev.Element("power").ToStringOrEmpty(),
                        PowerAvg = dev.Element("poweravg").ToStringOrEmpty(),
                        Energy = dev.Element("energy").ToStringOrEmpty(),
                        Score = dev.Element("score").ToStringOrEmpty(),
                        ProductID = dev.Element("productid").ToStringOrEmpty(),
                        productBrand = dev.Element("prodbrand").ToStringOrEmpty(),
                        ProductModel = dev.Element("prodmodel").ToStringOrEmpty(),
                        ProductTYpe = dev.Element("prodtype").ToStringOrEmpty(),
                        ProdTypeID = dev.Element("prodtypeid").ToStringOrEmpty(),
                        ClassID = dev.Element("classid").ToStringOrEmpty(),
                        SubClassID = dev.Element("subclassid").ToStringOrEmpty()
                    };
                    room.Devices.Add(device);
                }
                rooms.Add(room);

            }
            return rooms;

        }


        protected string GenerateToken()
        {
            var guidCredential = Guid.NewGuid().ToString();
            var loginCommand = String.Format(_baseLoginCommand, guidCredential, guidCredential);
            var data = SendCommand(loginCommand, "GWRLogin");
            var xDoc = XDocument.Parse(data);
            var rc = xDoc.Root.Element("rc");
            if (rc != null && rc.Value.ToString() == "404")
            {
                return "";
            }
            var token = xDoc.Root.Element("token");
            if (token == null)
                return "";
            else
                return token.Value.ToString();
        }


    }
}