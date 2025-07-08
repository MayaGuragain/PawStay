using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PawStay.Startup))]
namespace PawStay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
