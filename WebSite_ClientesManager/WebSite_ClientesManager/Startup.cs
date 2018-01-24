using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSite_ClientesManager.Startup))]
namespace WebSite_ClientesManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
