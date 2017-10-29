using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BAC_TrackerService.Startup))]

namespace BAC_TrackerService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}