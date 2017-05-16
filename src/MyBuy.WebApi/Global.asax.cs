using MyBuy.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;
using System.Web.SessionState;
using MyBuy;
using MyBuy.WebApi.App_Start;
using System.Web.Routing;

namespace MyBuy.WebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Cors跨域
            GlobalConfiguration.Configuration.EnableCors();
            //Log4Net配置
            LogHelper.SetConfig();
            //配置API路由

            /*GlobalConfiguration.Configuration.Routes.MapHttpRoute("default api",
                "api/{Controller}/{item}",
                new
                {
                    item = RouteParameter.Optional//可选项
                });*/
            RouteConfig.RegisterRoutes(RouteTable.Routes);

           // GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}