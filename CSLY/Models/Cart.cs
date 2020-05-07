using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSLY.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int? ProductId { get; set; }

        public int? MemberId { get; set; }

        public int? CartStatusId { get; set; }

        public Product Product { get; set; }

        public Member Member { get; set; }

        public CartStatus CartStatus { get; set; }
    }
}