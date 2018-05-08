using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SalesService.Controllers
{
    [RoutePrefix("api/Customer")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        //[HttpGet]
        [HttpGet, Route("getbyname/{id}")]
        public System.Web.Mvc.ActionResult GetCustomerById(int id)
        {
            var inputId = id;
            SalesService.Models.Customer custmerById;
            using (var salesDBEntities = new SalesDBEntities())
            {
                var customerFromDatabase = salesDBEntities.Customers.Where(item => item.CustomerID == id).ToList().First();
                custmerById = new Models.Customer() { FirstName = customerFromDatabase.FirstName, CustomerID = customerFromDatabase.CustomerID, LastName = customerFromDatabase.LastName, MiddleInitial = customerFromDatabase.MiddleInitial };

            }
            var result = new System.Web.Mvc.JsonResult();
            result.Data = custmerById;
            return result;
        }

        public System.Web.Mvc.ActionResult GetCustomerByName(string name)
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
            var result = new System.Web.Mvc.JsonResult();
            result.Data = custmerByName;
            return result;
        }

        [Route("editById/{id}")]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            using(SalesDBEntities db = new SalesDBEntities())
            {
                var customerFromDB = db.Customers.FirstOrDefault(x => x.CustomerID == id);
                if(id == customer.CustomerID)
                {
                    if (customerFromDB != null)
                    {

                       customerFromDB.FirstName = customer.FirstName;
                       customerFromDB.LastName = customer.LastName;
                       customerFromDB.MiddleInitial = customer.MiddleInitial;
                        db.Customers.Add(customerFromDB);
                        db.SaveChanges();
                        return Ok(customerFromDB);
                    }else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
