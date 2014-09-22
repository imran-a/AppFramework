using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppFramework.Startup))]
namespace AppFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
