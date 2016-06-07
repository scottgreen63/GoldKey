using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldKeyWeb.Controllers
{
    public class CustomerServicePackagesController : Controller
    {
        // GET: CustomerServicePackages
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerServicePackages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerServicePackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerServicePackages/Create
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

        // GET: CustomerServicePackages/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerServicePackages/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerServicePackages/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerServicePackages/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
