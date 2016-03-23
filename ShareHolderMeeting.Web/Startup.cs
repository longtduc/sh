using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShareHolderMeeting.Web.Startup))]
namespace ShareHolderMeeting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
