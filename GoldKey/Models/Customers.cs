using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldKey.Models
{
    public class Customers
    {
    // Example data source
        private static List<Customer> customers = new List<Customer>()
    {
        new Customer
        {
            CustomerID = 1,
            LastName = "Fisher",
            FirstName = "Bob",
            Address1 = "1507 SW Maple Drive",
            Address2 = "",
            City = "Portland",
            State = "OR",
            ZipCode = "97201",
            ContactEmail = "bobfisher@bobfisher.com"
        },
        new Customer
        {
            CustomerID = 2,
            LastName = "Anderson",
            FirstName = "Glenda",
            Address1 = "201 NW Glisan",
            Address2 = "Apt 107",
            City = "Portland",
            State = "OR",
            ZipCode = "97201",
            ContactEmail = "glendaa@glendaa.com"
        },
        new Customer
        {
            CustomerID = 3,
            LastName = "Daniels",
            FirstName = "Jim",
            Address1 = "1854 Oak Street",
            Address2 = "",
            City = "Memphis",
            State = "TN",
            ZipCode = "65734",
            ContactEmail = "jd@jd.com"
        },
        new Customer
        {
            CustomerID = 4,
            LastName = "Kat",
            FirstName = "Bob",
            Address1 = "1786 156th Ave",
            Address2 = "",
            City = "Tigard",
            State = "OR",
            ZipCode = "97111",
            ContactEmail = "bobkat@bobkat.com"
        }
    };

        // C part of CRUD
        public static void CreateCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        // R part of CRUD
        public static List<Customer> GetAll()
        {
            return customers;
        }

        public static Customer GetCustomer(int id)
        {
            return customers.Find(x => x.CustomerID == id); // Find one user and return him
        }

        // U part of CRUD
        public static void UpdateCustomer(int id, Customer customer)
        {
            customers.Remove(customers.Find(x => x.CustomerID == id)); // Remove the previous User
            customers.Add(customer);
        }

        // D part of CRUD 
        public static void DeleteCustomer(int id)
        {
            customers.Remove(customers.Find(x => x.CustomerID == id)); // Find and remove the user
        }
    }
}