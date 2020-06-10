using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSLY.Models
{
    public class RegisterationModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmedPassword { get; set; }

        [Required]
        public int PhoneNumber { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}