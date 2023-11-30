using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Areas.Admin.Controllers
{
    public class BILLsController : Controller
    {
        private WEBDIENTHOAIEntities2 db = new WEBDIENTHOAIEntities2();

        // GET: Admin/BILLs
        public ActionResult Index()
        {
            var bILLs = db.BILLs.Include(b => b.Account);
            return View(bILLs.ToList());
        }

        // GET: Admin/BILLs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL bILL = db.BILLs.Find(id);
            if (bILL == null)
            {
                return HttpNotFound();
            }
            return View(bILL);
        }
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL donhang = db.BILLs.Find(id);
            donhang.Status = 1;
            db.SaveChanges();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
        // GET: Admin/BILLs/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "NAME");
            return View();
        }

        // POST: Admin/BILLs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateBuy,Status,AccountID,Address,TotalPrice")] BILL bILL)
        {
            if (ModelState.IsValid)
            {
                db.BILLs.Add(bILL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "NAME", bILL.AccountID);
            return View(bILL);
        }

        // GET: Admin/BILLs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL bILL = db.BILLs.Find(id);
            if (bILL == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "NAME", bILL.AccountID);
            return View(bILL);
        }

        // POST: Admin/BILLs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateBuy,Status,AccountID,Address,TotalPrice")] BILL bILL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bILL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "ID", "NAME", bILL.AccountID);
            return View(bILL);
        }

        // GET: Admin/BILLs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL bILL = db.BILLs.Find(id);
            if (bILL == null)
            {
                return HttpNotFound();
            }
            return View(bILL);
        }

        // POST: Admin/BILLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BILL bILL = db.BILLs.Find(id);
            db.BILLs.Remove(bILL);
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
