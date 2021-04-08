using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PROJECT_MVC.Startup))]
namespace PROJECT_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
