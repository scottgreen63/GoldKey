using System.Web;
using System.Web.Mvc;
using GoldKeyLib.Entities;
using System.Web.Security;
using GoldKeyWeb.Controllers;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel;
//using Microsoft.AspNet.Identity;

namespace Security.Controllers
{
    public class AccountController : ApplicationController
    {
        //Users _users = Users.Listing.List();
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(User model, string returnUrl = "")
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    //model.User.AuthenticateUser(model.User.UserName, model.User.UserPassword);
        //    //    // var user = _userContext.Where(u => u.UserName == model.User.UserName && u.UserPassword == model.User.UserPassword).FirstOrDefault();
        //    //    User _user = _users.Where(u => u.UserName == model.UserName).FirstOrDefault();
        //    //    if(_user != null)
        //    //    {
        //    //        UserHistoryEntry historyentry = new UserHistoryEntry();
        //    //            historyentry.UserId = _user.UserId;
        //    //            historyentry.UserAction = "Login Attempted";
        //    //            historyentry.AddUserHistoryEntry(0);

        //    //            _user.UserHistory.List(0, _user.UserId);
        //    //    }

        //    //    if (_user.AuthenticateUser(model.UserName, model.UserPassword))
        //    //    {
                    
        //    //        UserGroup usergroup = _user.UserGroup;

        //    //       // var serializeModel = new GoldKeyLib.Entities.User();
        //    //        //serializeModel.UserId = _user.UserId;
        //    //        //serializeModel.UserFirstName = _user.UserFirstName;
        //    //        //serializeModel.UserLastName = _user.UserLastName;
        //    //        //serializeModel.UserGroup = _user.UserGroup;

        //    //       string userData = JsonConvert.SerializeObject(_user);
        //    //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
        //    //                 1,
        //    //                 _user.UserName,
        //    //                 DateTime.Now,
        //    //                 DateTime.Now.AddMinutes(15),
        //    //                 false,
        //    //                 userData);

        //    //        string encTicket = FormsAuthentication.Encrypt(authTicket);
        //    //        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //    //        Response.Cookies.Add(faCookie);

                   

        //    //        if (usergroup.UserGroupCode.Contains("SYSGRP"))
        //    //        {
                        
        //    //            return RedirectToAction("Index", "Admin",_user);
        //    //        }
        //    //        else if (usergroup.UserGroupCode.Contains("HIKER"))
        //    //        {
                        
        //    //            return RedirectToAction("Index", "User",_user);
        //    //        }
        //    //        else
        //    //        {
        //    //            ViewBag.User = _user;
        //    //            return RedirectToAction("Index", "Home");
        //    //        }
        //    //    }

        //    //    ModelState.AddModelError("", "Incorrect username and/or password");
        //    //}

        //    return View(model);
        //}

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Users.List(0);
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            User _user = new User();
            _user = Users.Find(user.UserName.ToUpper());
            bool goodtogo = true; //_user.AuthenticateUser(user.UserName, user.UserPassword);

            if (_user != null && goodtogo)
            {
                
                var ident = new ClaimsIdentity(new[] 
                { 
                  // adding following 2 claim just for supporting default antiforgery provider
                  new Claim(ClaimTypes.NameIdentifier, _user.UserName),
                  new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                  new Claim(ClaimTypes.Name,_user.UserName),
                  new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                  new Claim(ClaimTypes.Role, _user.UserGroup.UserGroupCode)
                  
                },DefaultAuthenticationTypes.ApplicationCookie);

              HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false }, ident);
                GoldKeyWeb.Models.UserViewModel userviewmodel = new GoldKeyWeb.Models.UserViewModel();
                List<User> users = new List<User>();
                users.Add(user);
                userviewmodel.Menu = user.UserGroup.UserGroupMenu;
                userviewmodel.User = users;

                return RedirectToAction("Index", "Home", userviewmodel); // auth succeed 
            }

            else
            {
                ModelState.AddModelError("", "invalid username or password");

                return RedirectToAction("Login", "Account", null); }

        }
    }
}