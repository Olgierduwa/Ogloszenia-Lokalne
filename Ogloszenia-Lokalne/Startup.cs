using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ogloszenia_Lokalne.Startup))]
namespace Ogloszenia_Lokalne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
