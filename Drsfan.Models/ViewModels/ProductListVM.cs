using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.Models.ViewModels
{
    public class ProductListVM
    {
        
        public IEnumerable<Product> Products { get; set; }
       
        public IEnumerable<Category> Categories { get; set; }

    }
}
