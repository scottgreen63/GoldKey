using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoldKeyWeb.Startup))]
namespace GoldKeyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
        }
    }
}
