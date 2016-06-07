using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldKeyLib.Entities;

namespace GoldKeyWeb.Controllers
{
    [HandleError]
    public class VehiclesController : Controller
    {
        [HttpPost]
        public JsonResult VehicleList(string CustomerId)
        {
            try
            {
                List<Vehicle> vehicles = Vehicles.ListCustomerVehicles(0, CustomerId).ToList();
                return Json(new { Result = "OK", Records = vehicles });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: Vehicles
        public ActionResult Index()
        {
            
            return View(Vehicles.ListVehicles(0));
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(string vehicleid)
        {
            Vehicle vehicle = new Vehicle();
                vehicle.GetVehicle(0,vehicleid);

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            Vehicle _vehicle = new Vehicle();
            _vehicle.VehicleID = "0";

            return View(_vehicle);
        }

        // POST: Vehicles/Create
        [HttpPost]
        public ActionResult Create(Vehicle vehicle)
        {
            try
            {
                Vehicle _vehicle = new Vehicle();
                _vehicle = vehicle;
                _vehicle.VehicleID = "0";
                _vehicle.AddVehicle(0);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(string vehicleid)
        {
            Vehicle _vehicle = new Vehicle();
            _vehicle = _vehicle.GetVehicle(0,vehicleid);

            return View(_vehicle);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        public ActionResult Edit(Vehicle vehicle)
        {
            try
            {
                vehicle.ModifyVehicle(0);
                Vehicles.ListVehicles(0);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(string vehicleid)
        {
            Vehicle _vehicle = new Vehicle();
            _vehicle = _vehicle.GetVehicle(0, vehicleid);
            return View(_vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        public ActionResult Delete(string vehicleid, Vehicle vehicle)
        {
            Vehicle _vehicle = new Vehicle();
            try
            {
                _vehicle = _vehicle.GetVehicle(0, vehicleid); 
                _vehicle.DeleteVehicle(0);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(_vehicle.Error);
            }
        }

        public ActionResult VehicleDetail(string vehicleid = null)
        {
            Vehicle vehicle = new Vehicle();
            vehicle = vehicle.GetVehicle(0,vehicleid);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return PartialView("_VehicleDetail", vehicle);
        }
    }
}
