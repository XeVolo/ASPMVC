using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SystemyBazDanychP1.Startup))]
namespace SystemyBazDanychP1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
