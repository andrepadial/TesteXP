using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc_App_Crud.Startup))]
namespace Mvc_App_Crud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
