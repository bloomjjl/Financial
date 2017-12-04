using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Financial.WebApplication.Startup))]
namespace Financial.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
