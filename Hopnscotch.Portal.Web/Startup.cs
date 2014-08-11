using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Hopnscotch.Portal.Web.Startup))]

namespace Hopnscotch.Portal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
