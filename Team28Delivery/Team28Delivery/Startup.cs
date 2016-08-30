using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team28Delivery.Startup))]
namespace Team28Delivery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
