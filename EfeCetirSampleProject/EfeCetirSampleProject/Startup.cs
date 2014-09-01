using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EfeCetirSampleProject.Startup))]
namespace EfeCetirSampleProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
