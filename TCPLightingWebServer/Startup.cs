using Owin;
using Microsoft.Owin;
using System.Web.Http;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(TCPLightingWebServer.Startup))]
namespace TCPLightingWebServer
{
    public class Startup
    {
        HttpConfiguration config = new HttpConfiguration();
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
        }
    } 
}
