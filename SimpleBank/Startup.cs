using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleBank.Startup))]
namespace SimpleBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
