using CustomerCMS.Areas.AdminSuper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomerCMS.Areas.AdminSuper.DAL
{
    public class AccountInitializer:DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser {UserName="Tom",Email="hxjlo@126.com", Password="1" },
                new SysUser {UserName="Jerrey",Email="kk@126.com",Password="2" }
            };

            sysUsers.ForEach(s => context.SysUsers.Add(s));

            var sysRoles = new List<SysRole>
            {
                new SysRole {RoleName="Administrator",RoleDesc="超级管理员" },
                new SysRole {RoleName="General Users",RoleDesc="普通管理员" }
            };
            sysRoles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
            


            // base.Seed(context);
        }
    }
}