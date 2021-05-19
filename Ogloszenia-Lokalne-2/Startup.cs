using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ogloszenia_Lokalne_2.Startup))]
namespace Ogloszenia_Lokalne_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
