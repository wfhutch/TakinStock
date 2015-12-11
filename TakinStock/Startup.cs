using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TakinStock.Startup))]
namespace TakinStock
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
