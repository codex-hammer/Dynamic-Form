using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TemplateGenerator.Startup))]
namespace TemplateGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
