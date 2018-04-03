using AutoMapper;
using Microsoft.Owin;
using Owin;
using SimpleBlog.Models;

[assembly: OwinStartupAttribute(typeof(SimpleBlog.Startup))]
namespace SimpleBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
        }
    }
}
