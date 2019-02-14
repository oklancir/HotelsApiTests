using AutoMapper;
using Hotels.App_Start;
using NLog;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hotels
{
    public class MvcApplication : HttpApplication
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public class AutoMapperConfiguration
        {
            public void Configure()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                Mapper.Configuration.AssertConfigurationIsValid();
            }
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var code = (ex is HttpException httpException) ? httpException.GetHttpCode() : 500;

            if (code != 404)
            {
                Logger.Error(ex);
            }

            Response.Clear();
            Server.ClearError();

            Response.Redirect($"~/Error/Http{code}");
        }
    }
}
