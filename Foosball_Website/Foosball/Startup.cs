using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Foosball.Startup))]
namespace Foosball
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
