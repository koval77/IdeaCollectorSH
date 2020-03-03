using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdeaCollectorSH.Startup))]
namespace IdeaCollectorSH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
