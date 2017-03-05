using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MEF3.Startup))]
namespace MEF3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
