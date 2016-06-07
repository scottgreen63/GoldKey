using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldKeyLib.Entities;
using System.Web.Mvc.Filters;

namespace GoldKeyWeb.Controllers
{
    public abstract class ApplicationController : Controller
    {


        //protected override void OnAuthentication(AuthenticationContext filterContext)
        //{
        //    base.OnAuthentication(filterContext);

        //    if (!filterContext.Principal.Identity.IsAuthenticated)
        //    {
        //        // You may find that modifying the 
        //        // filterContext.HttpContext.User 
        //        // here works as desired. 
        //        // In my case I just set it to null
        //        filterContext.HttpContext.User = null;
        //    }
        //}




    }
}