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
    public class PRODUCTsController : Controller
    {
        private WEBDIENTHOAIEntities2 db = new WEBDIENTHOAIEntities2();

        // GET: Admin/PRODUCTs
        public ActionResult Index()
        {
            var pRODUCTs = db.PRODUCTs.Include(p => p.BRAND).Include(p => p.Operation);
            return View(pRODUCTs.ToList());
        }

        // GET: Admin/PRODUCTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.BRANDs, "ID", "NAME");
            ViewBag.OSID = new SelectList(db.Operations, "ID", "NAME");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Stock,Descriptions,SIM,MemoryStorage,Ram,Picture,BrandID,OSID")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTs.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BrandID);
            ViewBag.OSID = new SelectList(db.Operations, "ID", "NAME", pRODUCT.OSID);
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BrandID);
            ViewBag.OSID = new SelectList(db.Operations, "ID", "NAME", pRODUCT.OSID);
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Stock,Descriptions,SIM,MemoryStorage,Ram,Picture,BrandID,OSID")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BrandID);
            ViewBag.OSID = new SelectList(db.Operations, "ID", "NAME", pRODUCT.OSID);
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
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
