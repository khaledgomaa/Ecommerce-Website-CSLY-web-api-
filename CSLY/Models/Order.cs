using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSLY.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [ForeignKey("AccountInfo")]
        public int ClientId { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }
}