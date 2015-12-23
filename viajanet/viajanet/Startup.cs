using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(viajanet.Startup))]
namespace viajanet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
