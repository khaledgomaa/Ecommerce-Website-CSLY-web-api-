using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSLY.Startup))]
namespace CSLY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
