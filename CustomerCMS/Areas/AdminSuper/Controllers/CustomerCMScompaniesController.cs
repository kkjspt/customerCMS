using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerCMS.Areas.AdminSuper.DAL;
using CustomerCMS.Areas.AdminSuper.comm;
using CustomerCMS.Areas.AdminSuper.Models;

namespace CustomerCMS.Areas.AdminSuper.Controllers
{
    public class CustomerCMScompaniesController : Controller
    {
        private AccountContext db = new AccountContext();

        // GET: AdminSuper/CustomerCMScompanies
        public ActionResult Index(string searchkey, string index)
        {

            

            if (string.IsNullOrEmpty(index))
                index = "1";
            if (string.IsNullOrEmpty(searchkey))
                searchkey = string.Empty;

            List<CustomerCMScompany> totalList = db.CustomerCMScompanies.ToList().Where(p => p.cc_name.ToLower().Contains(searchkey.ToLower())).ToList();
            BasePageModel page = new BasePageModel() { SearchKeyWord = searchkey, CurrentIndex = Int32.Parse(index), TotalCount = totalList.Count };

            List<CustomerCMScompany> pageList = totalList.Skip((page.CurrentIndex - 1) * page.PageSize).Take(page.PageSize).ToList();
            ViewData["pagemodel"] = page;
            return View(pageList);

            //return View(db.CustomerCMScompanies.ToList());
            //return Json(db.CustomerCMScompanies,JsonRequestBehavior.AllowGet);
        }

        // GET: AdminSuper/CustomerCMScompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerCMScompany customerCMScompany = db.CustomerCMScompanies.Find(id);
            if (customerCMScompany == null)
            {
                return HttpNotFound();
            }
            return View(customerCMScompany);
        }

        // GET: AdminSuper/CustomerCMScompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminSuper/CustomerCMScompanies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,cc_account,cc_password,cc_name,cc_numer,cc_img,cc_fr,cc_emailsetting,cc_revoerytime,cc_revoerytime_everday,cc_revoerytime_oneday,cc_regtime,cc_endtime,cc_flag")] CustomerCMScompany customerCMScompany)
        {
            if (ModelState.IsValid)
            {
                db.CustomerCMScompanies.Add(customerCMScompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerCMScompany);
        }

        // GET: AdminSuper/CustomerCMScompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerCMScompany customerCMScompany = db.CustomerCMScompanies.Find(id);
            if (customerCMScompany == null)
            {
                return HttpNotFound();
            }
            return View(customerCMScompany);
        }

        // POST: AdminSuper/CustomerCMScompanies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,cc_name,cc_numer,cc_img,cc_fr,cc_emailsetting,cc_revoerytime,cc_revoerytime_everday")] CustomerCMScompany customerCMScompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerCMScompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerCMScompany);
        }

        // GET: AdminSuper/CustomerCMScompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerCMScompany customerCMScompany = db.CustomerCMScompanies.Find(id);
            if (customerCMScompany == null)
            {
                return HttpNotFound();
            }
            return View(customerCMScompany);
        }

        // POST: AdminSuper/CustomerCMScompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerCMScompany customerCMScompany = db.CustomerCMScompanies.Find(id);
            db.CustomerCMScompanies.Remove(customerCMScompany);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
