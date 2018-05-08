using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using System.Web.Http.Cors;

namespace SalesService.Controllers
{
    [RoutePrefix("api/employee")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        [HttpGet, Route("getAllEmployees", Name = "getAllEmployees")]
        
        public IHttpActionResult GetEmployees()
        {
            using(SalesDBEntities db = new SalesDBEntities())
            {
                var employeeList = db.Employees.ToList();
                List<Models.EmployeeModel> employeeModelList = new List<Models.EmployeeModel>();
                foreach(var item in employeeList)
                {
                    Models.EmployeeModel employeeModel = new Models.EmployeeModel()
                    {
                        EmployeeID = item.EmployeeID,
                        FirstName = item.FirstName,
                        MiddleInitial = item.MiddleInitial,
                        LastName = item.LastName
                    };
                    employeeModelList.Add(employeeModel);
                }
                return Ok(employeeModelList);
            }
        }
        [HttpGet, Route("getById/{id}", Name = "getemployeebyId")]
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

        [HttpGet, Route("getByName/{id}", Name = "getemployeebyname")]
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
