using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SalesService.Controllers
{
    public class CustomerController : ApiController
    {
        //[HttpGet]
        public ActionResult GetCustomerById(int id)
        {
            var inputId = id;
            SalesService.Models.Customer custmerById; 
            using (var salesDBEntities = new SalesDBEntities())
            {
               var  customerFromDatabase = salesDBEntities.Customers.Where(item => item.CustomerID == id).ToList().First();
                custmerById = new Models.Customer() { FirstName = customerFromDatabase.FirstName, CustomerID = customerFromDatabase.CustomerID, LastName = customerFromDatabase.LastName, MiddleInitial = customerFromDatabase.MiddleInitial };

            }
            var result =  new JsonResult();
            result.Data = custmerById;
            return result;
        }

        public ActionResult GetCustomerByName(string name)
        {
            SalesService.Models.Customer custmerByName;
            using (var salesDBEntities = new SalesDBEntities())
            {
                var customerFromDatabase = salesDBEntities.Customers.FirstOrDefault(item => item.FirstName.Contains(name) || item.LastName.Contains(name));
                if (customerFromDatabase == null)
                {
                }
                custmerByName = new Models.Customer() { FirstName = customerFromDatabase.FirstName, CustomerID = customerFromDatabase.CustomerID, LastName = customerFromDatabase.LastName, MiddleInitial = customerFromDatabase.MiddleInitial };

            }
            var result = new JsonResult();
            result.Data = custmerByName;
            return result;
        }
    }
}
