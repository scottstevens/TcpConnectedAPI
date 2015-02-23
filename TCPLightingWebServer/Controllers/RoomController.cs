using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using TCPConnectedAPI.Code;
using TCPConnectedAPI.Models;
using TCPLightingWebServer.Domain;
using TCPLightingWebServer.Models;



namespace TCPLightingWebServer.Controllers
{
    public class RoomController : BaseLightController
    {
        
        public List<Room> GetRoomList(string token)
        {
            return GetRoomInfo(token);
        }

    }
}
