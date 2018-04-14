using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.Models
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
    }
}