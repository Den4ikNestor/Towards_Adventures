using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Adventures.Startup))]
namespace Web_Adventures
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
