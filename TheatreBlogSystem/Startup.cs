using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheatreBlogSystem.Startup))]
namespace TheatreBlogSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
