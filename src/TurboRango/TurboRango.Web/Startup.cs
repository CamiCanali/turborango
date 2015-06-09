using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TurboRango.Web.Startup))]
namespace TurboRango.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
