using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialWebsiteMVC5.Startup))]
namespace SocialWebsiteMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
