using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Model
{
    public class Customer
    {
        [Required(ErrorMessage = "Id Is Required")]
        public int id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string name { get; set; }
    }

    public class Root
    {
        public List<Customer> customers { get; set; }
    }

   


}
