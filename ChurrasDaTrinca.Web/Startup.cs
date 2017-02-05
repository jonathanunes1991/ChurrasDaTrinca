using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChurrasDaTrinca.Web.Startup))]
namespace ChurrasDaTrinca.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
