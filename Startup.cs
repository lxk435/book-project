using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookProject.Startup))]
namespace BookProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
