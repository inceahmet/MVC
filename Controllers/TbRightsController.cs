using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VodaDestek.Models;

namespace VodaDestek.Controllers
{
    public class TbRightsController : Controller
    {
        private VodaDestekEntities3 db = new VodaDestekEntities3();

        // GET: TbRights
        public ActionResult Index()
        {
            return View(db.TbRight.ToList());
        }

        // GET: TbRights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbRight tbRight = db.TbRight.Find(id);
            if (tbRight == null)
            {
                return HttpNotFound();
            }
            return View(tbRight);
        }

        // GET: TbRights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbRights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InRightID,StRightName")] TbRight tbRight)
        {
            if (ModelState.IsValid)
            {
                db.TbRight.Add(tbRight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbRight);
        }

        // GET: TbRights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbRight tbRight = db.TbRight.Find(id);
            if (tbRight == null)
            {
                return HttpNotFound();
            }
            return View(tbRight);
        }

        // POST: TbRights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InRightID,StRightName")] TbRight tbRight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbRight);
        }

        // GET: TbRights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbRight tbRight = db.TbRight.Find(id);
            if (tbRight == null)
            {
                return HttpNotFound();
            }
            return View(tbRight);
        }

        // POST: TbRights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbRight tbRight = db.TbRight.Find(id);
            db.TbRight.Remove(tbRight);
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
