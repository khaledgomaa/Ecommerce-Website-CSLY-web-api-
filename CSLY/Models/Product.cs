using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CSLY.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSLY.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "You should fill out the name")]
        [StringLength(100, ErrorMessage = "Maximum number of characters is 100 and Minimum is 3", MinimumLength = 3)]
        public string ProductName { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Category Category { get; set; }

    }
}