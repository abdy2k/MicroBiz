using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicroBiz.Startup))]
namespace MicroBiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
