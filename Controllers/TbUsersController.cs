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
    public class TbUsersController : Controller
    {
        private VodaDestekEntities3 db = new VodaDestekEntities3();

        // GET: TbUsers
        public ActionResult Index()
        {
            return View(db.TbUsers.ToList());
        }

        // GET: TbUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUsers tbUsers = db.TbUsers.Find(id);
            if (tbUsers == null)
            {
                return HttpNotFound();
            }
            return View(tbUsers);
        }

        // GET: TbUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InUserID,StUserName,StSurname")] TbUsers tbUsers)
        {
            var Kullanici = db.TbUsers.Where(a => a.StUserName == tbUsers.StUserName).FirstOrDefault();
            //if (ModelState.IsValid)
                if (Kullanici == null)
                {
                db.TbUsers.Add(tbUsers);
                db.SaveChanges();
               
                return RedirectToAction("Index");
               
            }
            else
            {
                ViewBag.Error = "This User Already Has Been Added";
            }
            return View(tbUsers);
        }
       
        
        // GET: TbUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUsers tbUsers = db.TbUsers.Find(id);
            if (tbUsers == null)
            {
                return HttpNotFound();
            }
            return View(tbUsers);
        }

        // POST: TbUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InUserID,StUserName,StSurname")] TbUsers tbUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbUsers);
        }

        // GET: TbUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbUsers tbUsers = db.TbUsers.Find(id);
            if (tbUsers == null)
            {
                return HttpNotFound();
            }
            return View(tbUsers);
        }

        // POST: TbUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbUsers tbUsers = db.TbUsers.Find(id);
            db.TbUsers.Remove(tbUsers);
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
