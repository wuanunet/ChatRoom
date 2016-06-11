using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Chat.Web.Startup))]
namespace Chat.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}