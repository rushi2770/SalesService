using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.Utility.Profiles
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            //Mapping the Employee(source i.e., data from database) to
            //EmployeeModel(destination i.e., object which is used to transfer the data to end user)
            CreateMap<Employee, Models.EmployeeModel>();
            CreateMap<Customer, Models.Customer>();
            CreateMap<Product, Models.ProductModel>();
            CreateMap<Models.ProductModel, Product>();
                //.ForMember(m=> m.ProductID, e=> e.Ignore());

            //CreateMap<IEnumerable<Product>, IEnumerable<Models.ProductModel>>();
        }
    }
}