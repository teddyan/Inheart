using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pinzoe_Client.Startup))]
namespace Pinzoe_Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
