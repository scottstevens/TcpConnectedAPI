using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TCPLightingWebServer
{

    public static class MyExtensions
    {
        public static string ToStringOrEmpty(this XElement element)
        {
            return element == null ? "" : element.Value;
        }
    }

}
