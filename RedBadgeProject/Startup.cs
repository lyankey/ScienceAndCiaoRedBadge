using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedBadgeProject.Startup))]
namespace RedBadgeProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
