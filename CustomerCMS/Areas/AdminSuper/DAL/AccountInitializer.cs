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

            ///向公司表中插入数据
            var CustomerCMScompanies = new List<CustomerCMScompany>
            {
                new CustomerCMScompany {cc_account="hxj1",cc_password="123123",cc_name="泸州柒泉盛世天香酒业有限责任公司",cc_numer="510000000787923",cc_img="/img/yyzz-01.jpg",cc_fr="邢瑞琳",cc_emailsetting=0,cc_revoerytime=1,cc_revoerytime_everday="22:00",cc_revoerytime_oneday=DateTime.Parse("2016-04-18 22:00:00"),cc_regtime=DateTime.Now,cc_endtime=DateTime.Now.AddYears(1),cc_flag=2},
                new CustomerCMScompany {cc_account="hxj2",cc_password="123123",cc_name="点点客泸州分公司",cc_numer="510000000787923",cc_img="/img/yyzz-01.jpg",cc_fr="邢瑞琳",cc_emailsetting=0,cc_revoerytime=1,cc_revoerytime_everday="22:00",cc_revoerytime_oneday=DateTime.Parse("2016-04-18 22:00:00"),cc_regtime=DateTime.Now,cc_endtime=DateTime.Now.AddYears(1),cc_flag=2},
                new CustomerCMScompany {cc_account="hxj3",cc_password="123123",cc_name="四川赶酒会文化传媒有限责任公司",cc_numer="510000000787923",cc_img="/img/yyzz-01.jpg",cc_fr="邢瑞琳",cc_emailsetting=0,cc_revoerytime=1,cc_revoerytime_everday="22:00",cc_revoerytime_oneday=DateTime.Parse("2016-04-18 22:00:00"),cc_regtime=DateTime.Now,cc_endtime=DateTime.Now.AddYears(1),cc_flag=2}
            };

            CustomerCMScompanies.ForEach(s => context.CustomerCMScompanies.Add(s));


            context.SaveChanges();
            


            // base.Seed(context);
        }
    }
}