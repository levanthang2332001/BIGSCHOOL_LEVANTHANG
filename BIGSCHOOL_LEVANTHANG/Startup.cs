using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BIGSCHOOL_LEVANTHANG.Startup))]
namespace BIGSCHOOL_LEVANTHANG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
