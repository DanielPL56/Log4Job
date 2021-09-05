using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Log4Job.Startup))]
namespace Log4Job
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
