using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSLY.Models;

namespace CSLY.ViewModel
{
    public class ProductCategoryImageViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<Category> Category { get; set; }

    }
}