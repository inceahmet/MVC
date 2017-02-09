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
    public class TbUsersRightsController : Controller
    {
        private VodaDestekEntities3 db = new VodaDestekEntities3();

        // GET: TbUsersRights
        public ActionResult Index()
        {
            var tbUsersRight = db.TbUsersRight.Include(t => t.TbRight).Include(t => t.TbUsers);
            return View(tbUsersRight.ToList());
        }

        // GET: TbUsersRights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUsersRight tbUsersRight = db.TbUsersRight.Find(id);
            if (tbUsersRight == null)
            {
                return HttpNotFound();
            }
            return View(tbUsersRight);
        }

        // GET: TbUsersRights/Create
        public ActionResult Create()
        {
            ViewBag.InRightID = new SelectList(db.TbRight, "InRightID", "StRightName");
            ViewBag.InUserID = new SelectList(db.TbUsers, "InUserID", "StUserName");
            return View();
        }


        // POST: TbUsersRights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InUserRightId,InUserID,InRightID")] TbUsersRight tbUsersRight)
        {
            var Kullanici = db.TbUsersRight.Where(a => a.InUserID == tbUsersRight.InUserID).FirstOrDefault();
            if (Kullanici == null)
                //if (ModelState.IsValid)
            {
                db.TbUsersRight.Add(tbUsersRight);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Error = "This User Already Has Been Added";
            }

            ViewBag.InRightID = new SelectList(db.TbRight, "InRightID", "StRightName", tbUsersRight.InRightID);
            ViewBag.InUserID = new SelectList(db.TbUsers, "InUserID", "StUserName", tbUsersRight.InUserID);
            return View(tbUsersRight);
        }

   
// GET: TbUsersRights/Edit/5
public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUsersRight tbUsersRight = db.TbUsersRight.Find(id);
            if (tbUsersRight == null)
            {
                return HttpNotFound();
            }
            ViewBag.InRightID = new SelectList(db.TbRight, "InRightID", "StRightName", tbUsersRight.InRightID);
            ViewBag.InUserID = new SelectList(db.TbUsers, "InUserID", "StUserName", tbUsersRight.InUserID);
            return View(tbUsersRight);
        }

        // POST: TbUsersRights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InUserRightId,InUserID,InRightID")] TbUsersRight tbUsersRight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsersRight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InRightID = new SelectList(db.TbRight, "InRightID", "StRightName", tbUsersRight.InRightID);
            ViewBag.InUserID = new SelectList(db.TbUsers, "InUserID", "StUserName", tbUsersRight.InUserID);
            return View(tbUsersRight);
        }

        // GET: TbUsersRights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUsersRight tbUsersRight = db.TbUsersRight.Find(id);
            if (tbUsersRight == null)
            {
                return HttpNotFound();
            }
            return View(tbUsersRight);
        }

        // POST: TbUsersRights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbUsersRight tbUsersRight = db.TbUsersRight.Find(id);
            db.TbUsersRight.Remove(tbUsersRight);
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
