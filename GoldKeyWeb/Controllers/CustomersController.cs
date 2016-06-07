using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldKeyWeb.Models;
using PagedList;
using GoldKeyLib.Interfaces;
using GoldKeyLib.Entities;

namespace GoldKeyWeb.Controllers
{
    public class CustomersController : Controller
    {
        const int RecordsPerPage = 5;

        // GET: Customers
        //public ActionResult Index(CustomerSearchModel model)
        //{
        //    if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
        //       {
        //        var entities = Customers.Listing;
        //        var results = entities.List(0)
        //        //.Where(p => (p.LastName.StartsWith(model.LastName) || model.LastName == null)
        //        //&& (p.FirstName == model.FirstName || model.FirstName == null))

        //       .OrderBy(p => p.LastName);

        //        var pageIndex = model.Page ?? 1;
        //        model.SearchResults = results.ToPagedList(pageIndex, RecordsPerPage);

        //       }

        //           return View(model);


        //}

        public ActionResult Index()
        {
            Customers model = Customers.Listing.List(0);
            return View(model);
            
        }
        // GET: Customers/Details/5
        public ActionResult Details(string customerid)
        {
            Customer customer = new Customer();
            customer = customer.GetCustomer(0,customerid);
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            Customer _customer = new Customer();
            _customer.CustomerID = "0";

            return View(_customer);
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                Customer _customer = new Customer();
                _customer = customer;
                _customer.AddCustomer(0);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //POST : CreateCustomer
        [HttpPost]  
        public JsonResult CreateCustomer(Customer customer)
        {  
            try  
            {  
               Customer _customer = new Customer();
                _customer = customer;
                _customer.AddCustomer(0);
                return Json(new { Result = "OK", Records = _customer}, JsonRequestBehavior.AllowGet);  
            }  
           catch(Exception ex)  
           {  
               return Json(new { Result = "ERROR", Message = ex.Message });  
            }  
        }  

        // GET: Customers/Edit/5
        public ActionResult Edit(string customerid)
        {
            Customer _customer = new Customer();
            _customer = _customer.GetCustomer(0, customerid);

            return View(_customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                customer.ModifyCustomer(0);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Customers/Edit/5
        [HttpPost]  
        public JsonResult UpdateCustomer(Customer customer)
    {  
         
        try  
        {
                customer.ModifyCustomer(0);
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);  
        }  
        catch (Exception ex)  
        {  
            return Json(new { Result = "ERROR", Message = ex.Message });  
        }  
    }  


        // GET: Customers/Delete/5
        public ActionResult Delete(string customerid)
        {
            Customer _customer = new Customer();
            _customer = Customers.Listing.Find(customerid);
            return View(_customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(string customerid, Customer customer)
        {
            Customer _customer = new Customer();
            try
            {
                
                _customer = customer;
                _customer.DeleteCustomer(0);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(_customer.Error);
            }
        }

        [HttpPost]  
    public JsonResult DeleteCustomer(string customerid)
    {

            Customer _customer = new Customer();
            try
            {
                _customer = _customer.GetCustomer(0, customerid);
                _customer.DeleteCustomer(0);
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);  
        }  
        catch (Exception ex)  
       {  
            return Json(new { Result = "ERROR", Message = ex.Message });  
        }  
    }  

        public ActionResult CustomerDetail (string customerid = null)
        {
            Customer customer = new Customer();
            customer = Customers.Listing.Find(customerid);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CustomerDetail", customer);
        }


        public IEnumerable<CustomerActivityType> GetCustomerActivityTypes()
            {

                IEnumerable<CustomerActivityType> cntTable = CustomerActivityTypes.Listing.List(0);
                return cntTable.ToList();
            }

        }



}
