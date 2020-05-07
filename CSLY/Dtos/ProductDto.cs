using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSLY.Dtos
{
    public class ProductDto
    {
        [Required(ErrorMessage = "You should fill out the name")]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }


    }
}