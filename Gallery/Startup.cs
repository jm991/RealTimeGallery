using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gallery.Startup))]
namespace Gallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder MyApp)
        {
            MyApp.MapSignalR();
        }
    }
}