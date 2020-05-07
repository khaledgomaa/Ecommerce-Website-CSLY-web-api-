using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSLY.Models
{
    public class MemberRole
    {
        public int MemberRoleId { get; set; }

        public int MemberId { get; set; }

        public int RoleId { get; set; }

        public Member Member { get; set; }

        public Role Role { get; set; }
    }
}