using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactManager.Startup))]
namespace ContactManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
