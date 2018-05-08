using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SalesService.Controllers
{
    public class SalesCustomerJoinController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            using (SalesDBEntities db = new SalesDBEntities())
            {
                var result = db.Sales.Where(c => c.CustomerID == id).ToList().Join(db.Customers.AsNoTracking().ToList(),
                s => s.CustomerID,
                cust => cust.CustomerID,
            (sales, customer) => new
            {
                PersonId = sales.SalesPersonID,
                CustomerID = sales.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ProductID = sales.ProductID,
                Quantity = sales.Quantity
            });
                List<Models.customerSalesModel> modelList = new List<Models.customerSalesModel>();
                    
                foreach(var item in result)
                {
                    Models.customerSalesModel model = new Models.customerSalesModel();
                    model.FirstName = item.FirstName;
                    model.LastName = item.LastName;
                    model.CustomerID = item.CustomerID;
                    model.SalesPersonID = item.PersonId;
                    model.ProductID = item.ProductID;
                    model.Quantity = item.Quantity;
                    modelList.Add(model);
                }
                return Ok(modelList);
            }

        }
    }
}
