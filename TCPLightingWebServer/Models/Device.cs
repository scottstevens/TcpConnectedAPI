using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPLightingWebServer.Models
{
    public class Device
    {
        public string DeviceID { get; set; }
        public string Known { get; set; }
        public string Lock { get; set; }
        public string State { get; set; }
        public string Level { get; set; }
        public string Node { get; set; }
        public string Port { get; set; }
        public string NodeType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ColorID { get; set; }
        public string Type { get; set; }
        public string RangeMin { get; set; }
        public string RangeMax { get; set; }
        public string Power { get; set; }
        public string PowerAvg { get; set; }
        public string Energy { get; set; }
        public string Score { get; set; }
        public string ProductID { get; set; }
        public string productBrand { get; set; }
        public string ProductModel { get; set; }
        public string ProductTYpe { get; set; }
        public string ProdTypeID { get; set; }
        public string ClassID { get; set; }
        public string SubClassID { get; set; }
        public string Class { get; set; }
        public string SubClass { get; set; }
    }
}
