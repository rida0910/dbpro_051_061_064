using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreelancerMarketplace.Startup))]
namespace FreelancerMarketplace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
