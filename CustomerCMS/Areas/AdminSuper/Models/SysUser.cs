using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerCMS.Areas.AdminSuper.Models
{
    public class SysUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<SysUserRole> SysUserToles { get; set; }
    }
}