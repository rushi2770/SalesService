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
                #region commented Code
                // Models.EmployeeModel employee;
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Employee, Models.EmployeeModel>();
                //});

                //IMapper mapper = config.CreateMapper();
                ////var employeeFromDB = new Employee();

                //var employeeModel = mapper.Map<Models.EmployeeModel, Employee>(employeeFromDB);
                //employee = new Models.EmployeeModel() {EmployeeID = employeeFromDB.EmployeeID,
                // FirstName = employeeFromDB.FirstName, LastName = employeeFromDB.LastName, MiddleInitial =  employeeFromDB.MiddleInitial  }; 
                #endregion

                var sourceEmployee = db.Employees.FirstOrDefault(item => item.EmployeeID == id);
                var destionationEmployeeModel = Mapper.Map<Models.EmployeeModel>(sourceEmployee);
                return Ok(destionationEmployeeModel);
            }

        }

        [HttpGet, Route("getbyname/{id}", Name = "getemployeebyname")]
        public IHttpActionResult GetEmployeeByName(string id)
        {
            Models.EmployeeModel employee;
            using (SalesDBEntities db = new SalesDBEntities())
            {
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
