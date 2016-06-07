using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldKeyLib.Entities;
using GoldKeyWeb.CustomAttributes;
using System.Security.Claims;
using System.Globalization;
using GoldKeyWeb.Models;

namespace GoldKeyWeb.Controllers
{
    
    public class HomeController : ApplicationController 
    {
       
        
     
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            User _user = new User();
            _user.GetUser(Convert.ToInt32(identity.Claims.FirstOrDefault(x => x.Type.ToString(CultureInfo.InvariantCulture) == "NameIdentifier")));
            //UserGroupMenuItems _usermenuitems = user.UserGroup.UserGroupMenu;
            UserViewModel usermodel = new UserViewModel();
            List<User> users = new List<GoldKeyLib.Entities.User>();
                       
            users.Add(_user);
            

            usermodel.User = users;
            usermodel.Menu = _user.UserGroup.UserGroupMenu;
            
            return View(usermodel);
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "GoldKey Valet at PDX";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "GoldKey Valet Contact";

            return View();
        }
    }
}