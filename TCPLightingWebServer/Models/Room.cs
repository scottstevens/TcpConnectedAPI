using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPLightingWebServer.Models
{
    public class Room
    {
        public string RoomID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Known { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string ColorID { get; set; }
        public string Image { get; set; }
        public string Power { get; set; }
        public string PowerAvg { get; set; }
        public string Energy { get; set; }
        public List<Device> Devices { get; set; }

    }
}
