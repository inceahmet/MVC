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
    public class TbAdminsController : Controller
    {
        private VodaDestekEntities4 db = new VodaDestekEntities4();

        public ActionResult Logout()
        {
            Session["StUserName"] = null;
            //RedirectToAction("Login","TbAdmins");
            //return View();
        
        return RedirectToAction("Login","TbAdmins");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TbAdmin model)
        {
            var user = db.TbAdmin.FirstOrDefault(x => x.StUserName == model.StUserName && x.StPassword == model.StPassword);
            if (user != null)
            {
                Session["StUserName"] = user;
                return RedirectToAction("Index", "TbTickets");
            }

            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }
               // ViewBag.Error("Please Check Your User Name Or Password");
            
                return View();
        }

        // GET: TbAdmins
        public ActionResult Index()
        {
            return View(db.TbAdmin.ToList());
        }

        // GET: TbAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbAdmin tbAdmin = db.TbAdmin.Find(id);
            if (tbAdmin == null)
            {
                return HttpNotFound();
            }
            return View(tbAdmin);
        }

        // GET: TbAdmins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbAdmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InAdminId,StUserName,StPassword,StEmail,DtDAte")] TbAdmin tbAdmin)
        {
            var Kullanici = db.TbAdmin.Where(a => a.StUserName == tbAdmin.StUserName).FirstOrDefault();
            if (Kullanici == null)
            // if (ModelState.IsValid)
            {
                db.TbAdmin.Add(tbAdmin);
                db.SaveChanges();
                return RedirectToAction("Login");

            }
            else
            {
                ViewBag.Error = "This User Already Has Been Added";
              
            }


            return View(tbAdmin);
        }

       

        //public ActionResult Create(TbAdmin UyeBilgi)
        //{
        //    //Kullacı araması yapılıyor
        //    var Kullanici = db.TbAdmin.Where(a => a.StEmail == UyeBilgi.StEmail).FirstOrDefault();
        //    //eğer yoksa kaydı ekle
        //    if (Kullanici == null)
        //    {

        //        db.TbAdmin.Add(UyeBilgi);
        //        db.SaveChanges();
        //        return View(Kullanici);
        //    }
        //    //kullanıcı daha önceden varsa hata mesajı gönder..
        //    else
        //    {
        //        ViewBag.HataMesaji = "Bu Kullanıcı Adı Daha Önceden Alınmış";
        //        return View();
        //    }
        //}

        //private new ActionResult View(object hata)
        //{
        //    throw new NotImplementedException("Hata");
        //}


        // GET: TbAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbAdmin tbAdmin = db.TbAdmin.Find(id);
            if (tbAdmin == null)
            {
                return HttpNotFound();
            }
            return View(tbAdmin);
        }

        // POST: TbAdmins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InAdminId,StUserName,StPassword,StEmail,DtDAte")] TbAdmin tbAdmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAdmin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbAdmin);
        }

        // GET: TbAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbAdmin tbAdmin = db.TbAdmin.Find(id);
            if (tbAdmin == null)
            {
                return HttpNotFound();
            }
            return View(tbAdmin);
        }

        // POST: TbAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbAdmin tbAdmin = db.TbAdmin.Find(id);
            db.TbAdmin.Remove(tbAdmin);
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
