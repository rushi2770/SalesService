using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SalesService.Controllers
{
    public class ProductController : ApiController
    {
        //GET: api/product
        public IHttpActionResult GetAllProducts()
        {
            using(SalesDBEntities db = new SalesDBEntities())
            {
                var productsFromDB = db.Products.ToList();
                List<Models.ProductModel> product = new List<Models.ProductModel>();
                
                foreach(var item in productsFromDB)
                {
                    Models.ProductModel model = new Models.ProductModel();
                    model.ProductID = item.ProductID;
                    model.Name = item.Name;
                    model.Price = item.Price;
                    product.Add(model);
                }
                return Ok(product);
            } 
        }

        //GET: api/product/1
        public IHttpActionResult GetProduct(int id)
        {
            using (SalesDBEntities db = new SalesDBEntities())
            {
                var productFromDB = db.Products.FirstOrDefault(x => x.ProductID == id);
                if(productFromDB == null)
                {
                    //return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with product Id = " + id + " not found");
                    return NotFound();
                }else
                {
                    //Used AutoMapper to copy data from the database table object to bussiness view model object
                    return Ok(AutoMapper.Mapper.Map<Models.ProductModel>(productFromDB));
                }
            }         
        }

        //POST: api/product
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                using(SalesDBEntities db = new SalesDBEntities())
                {
                    var newProductAdded = AutoMapper.Mapper.Map<Product>(product);
                    db.Products.Add(newProductAdded);
                    db.SaveChanges();
                    return CreatedAtRoute("DefaultApi", new { id = newProductAdded.ProductID }, newProductAdded);
                }
            }else
            {
                return BadRequest(ModelState);
            }
        }

        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductID)
            {
                return BadRequest();
            }
            using (SalesDBEntities db = new SalesDBEntities())
            {
                var productFromDb = db.Products.FirstOrDefault(x => x.ProductID == id);
                if (productFromDb != null)
                {
                    //productFromDb = AutoMapper.Mapper.Map<ProductModel, Product>(product);
                    productFromDb.Price = product.Price;
                    productFromDb.Name = product.Name;
                    //db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteProduct(int id)
        {
            using (SalesDBEntities db = new SalesDBEntities())
            {
                var productToDelete = db.Products.FirstOrDefault(x => x.ProductID == id);
                if (productToDelete != null)
                {
                    db.Products.Remove(productToDelete);
                    db.SaveChanges();
                    return Ok(productToDelete);
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}
