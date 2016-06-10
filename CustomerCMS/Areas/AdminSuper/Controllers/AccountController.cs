using CustomerCMS.Areas.AdminSuper.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerCMS.Areas.AdminSuper.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db=new AccountContext();
        // GET: AdminSuper/Account
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "登录前…";

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string account = fc["account"];
            string password = fc["password"];

            var user = db.SysUsers.Where(a => a.Email == account & a.Password == password);

            if (user.Count() > 0)
            {
                ViewBag.LoginState = "欢迎登录:" + account;
            }
            else
            {
                ViewBag.LoginState =  account+"__用户不存在！";
            }
            return View();
            //以下代码为跳转实例，不用而已
            //return RedirectToAction("Login", "Account", new { area = "AdminSuper" });
        }

        [HttpPost]
        public ActionResult Register()
        {
            return View();
        }
    }
}