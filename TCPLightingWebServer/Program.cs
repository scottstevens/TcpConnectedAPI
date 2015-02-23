using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TCPLightingWebServer
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string baseURL = @"http://localhost:80";
            string APIURL = @"http://localhost:8080";
            var root = Assembly.GetExecutingAssembly().Location;
            if(root.IndexOf("bin") > 0)
            { 
                root = root.Substring(0, root.LastIndexOf("bin")) + "\\app\\";
            }
            else
            {
                root = root.Substring(0, root.LastIndexOf("\\")) + "\\app\\";
            }

            var options = new FileServerOptions();
            var fileSystem = new PhysicalFileSystem(root);
            options.FileSystem = fileSystem;
            options.EnableDirectoryBrowsing = true;
            
            
           
            WebApp.Start<Startup>(APIURL);
            WebApp.Start(baseURL,builder=>builder.UseFileServer(options));
            Console.WriteLine("local connected lightserver back online, take that TCP!");
            Console.ReadLine();
        }
        
    }

}
