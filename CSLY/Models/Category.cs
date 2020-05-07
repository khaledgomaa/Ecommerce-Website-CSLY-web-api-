using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace CSLY.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="You should fill out the name")]
        [StringLength(100,ErrorMessage ="Maximum number of characters is 100 and Minimum is 3",MinimumLength =3)]
        [Index("This Name is already exit",IsUnique = true)]
        public string CategoryName { get; set; }

        public bool? IsActive { get; set; }

    }
}