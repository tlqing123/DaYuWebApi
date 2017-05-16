using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MyBuy.WebApi.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("default api",
                 "api/{Controller}/{item}",
                 new
                 {
                     item = RouteParameter.Optional//可选项
                 });
        }
    }
}