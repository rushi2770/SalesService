using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
namespace SalesService.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        public IHttpActionResult GetEmployeeByID(int id)
        {
            using (SalesDBEntities db = new SalesDBEntities())
            {
                Models.EmployeeModel employee;
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Models.EmployeeModel, Employee>();
                //});
                //IMapper mapper = config.CreateMapper();
                ////var employeeFromDB = new Employee();
                var employeeFromDB = db.Employees.FirstOrDefault(item => item.EmployeeID == id);
                //var employeeModel = mapper.Map<Models.EmployeeModel, Employee>(employeeFromDB);
                employee = new Models.EmployeeModel() {EmployeeID = employeeFromDB.EmployeeID,
                    FirstName = employeeFromDB.FirstName, LastName = employeeFromDB.LastName, MiddleInitial =  employeeFromDB.MiddleInitial  };
                 
                return Ok(employee);
            }

        }

        [HttpGet, Route("getbyname/{id}", Name = "getemployeebyname")]
        public IHttpActionResult GetEmployeeByID(string id)
        {
            Models.EmployeeModel employee;
            using (SalesDBEntities db = new SalesDBEntities())
            {


               // var employeeFromDB = db.Employees.FirstOrDefault(i => i.LastName == id);
                var employeeFromDB = db.Employees.FirstOrDefault(item => item.FirstName.Contains(id) || item.LastName.Contains(id));
                employee = new Models.EmployeeModel()
                {
                    EmployeeID = employeeFromDB.EmployeeID,
                    FirstName = employeeFromDB.FirstName,
                    LastName = employeeFromDB.LastName,
                    MiddleInitial = employeeFromDB.MiddleInitial
                };

                
            }
            return Ok(employee);
        }
    }
}
