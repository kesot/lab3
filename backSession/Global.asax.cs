using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using lab2.Models;

namespace backSession
{
    public class WebApiApplication : System.Web.HttpApplication
    {
	    public static List<User> authorizedUsers = new List<User>();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
