using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;

namespace WebApiRole
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            app
                .UseCors(CorsOptions.AllowAll)
                .UseWelcomePage("/test")
                .UseWebApi(config);
        }
    }
}