using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoldKey.Models;

namespace GoldKey.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> customers = Customers.GetAll();

        // GET: Customers
        public ActionResult Index()
        {
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = Customers.GetCustomer(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,LastName,FirstName,Address1,Address2,City,State,ZipCode,ContactPhone,ContactEmail,CompanyName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customers.CreateCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = Customers.GetCustomer(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,LastName,FirstName,Address1,Address2,City,State,ZipCode,ContactPhone,ContactEmail,CompanyName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customers.UpdateCustomer(customer.CustomerID, customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = Customers.GetCustomer(Convert.ToInt32(id));

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = Customers.GetCustomer(Convert.ToInt32(id));
            Customers.DeleteCustomer(Convert.ToInt32(id));
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // Customers.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
