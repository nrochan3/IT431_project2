using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdmissionsWebsite.Startup))]
namespace AdmissionsWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
