using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(db_music.Startup))]
namespace db_music
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
