using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPMVC.Startup))]
namespace ASPMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
