using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CSLY.Dtos
{
    public class CategoryDto
    {

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "You should fill out the name")]
        public string CategoryName { get; set; }
    }
}