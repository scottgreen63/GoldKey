using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldKeyLib.Entities;
namespace GoldKeyWeb.Controllers
{
    public class UserGroupsController : Controller
    {
        // GET: UserGroups
        public ActionResult Index()
        {
            UserGroups.List(0);
            return View(UserGroups.List(0));
        }

        // GET: UserGroups/Details/5
        public ActionResult Details(int usergroupid)
        {
            UserGroup _usergroup = UserGroups.Find(0,usergroupid);
            return View(_usergroup);
        }

        // GET: UserGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserGroups/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserGroups/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int usergroupid)
        {
            UserGroup _usergroup = UserGroups.Find(0,usergroupid);
            return View(_usergroup);
        }

        // POST: UserGroups/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int usergroupid, UserGroup incomingusergroup)
        {
            try
            {
                foreach (UserGroupPermission perm in incomingusergroup.UserGroupPermissions)
                {
                    perm.UserGroupId = incomingusergroup.UserGroupId;
                }
                //UserGroup _usergroup = incomingusergroup;
                incomingusergroup.ModifyUserGroup();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserGroups/Delete/5
        public ActionResult Delete(int usergroupid)
        {
            return View();
        }

        // POST: UserGroups/Delete/5
        [HttpPost]
        public ActionResult Delete(int usergroupid, UserGroup usergroup)
        {
            try
            {
                
                UserGroup _usergroup = usergroup;
                _usergroup.DeleteUserGroup();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
