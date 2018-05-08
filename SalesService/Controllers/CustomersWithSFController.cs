﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SalesService;
using System.Web.Http.Cors;
using AutoMapper;

namespace SalesService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomersWithSFController : ApiController
    {
        private SalesDBEntities db = new SalesDBEntities();

        // GET: api/CustomersWithSF
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/CustomersWithSF/5
        //[ResponseType(typeof(SalesService.Models.Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            //var sourceEmployee = db.Employees.FirstOrDefault(item => item.EmployeeID == id);
            var destionationCustomerModel = Mapper.Map<Models.Customer>(customer);
            return Ok(destionationCustomerModel);
           // return Ok(new SalesService.Models.Customer(customer));
        }

        // PUT: api/CustomersWithSF/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CustomersWithSF
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/CustomersWithSF/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var sales = db.Sales.Where(sale => sale.CustomerID == customer.CustomerID);
            db.Sales.RemoveRange(sales);
            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}