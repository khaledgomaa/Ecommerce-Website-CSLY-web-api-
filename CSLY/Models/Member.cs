using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSLY.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Required, StringLength(15, ErrorMessage = "Maximum number of characters is 100 and Minimum is 3", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}