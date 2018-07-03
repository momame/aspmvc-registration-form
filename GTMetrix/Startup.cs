using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GTMetrix.Startup))]
namespace GTMetrix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
