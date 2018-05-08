using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.Models
{
    public class customerSalesModel
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        public int SalesID { get; set; }
        public int SalesPersonID { get; set; }
        
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }
}