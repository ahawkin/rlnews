using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rlnews.Startup))]
namespace rlnews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
