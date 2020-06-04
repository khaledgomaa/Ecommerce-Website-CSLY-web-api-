using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSLY.Dtos
{
    public class TransactionsDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public IEnumerable<ProductItem> ProductItems { get; set; }

    }

    public class ProductItem
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }
    }
}