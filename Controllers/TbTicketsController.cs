using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VodaDestek.Models;
using PagedList;

namespace VodaDestek.Controllers
{
    public class TbTicketsController : Controller
    {
        private VodaDestekEntities3 db = new VodaDestekEntities3();




       


        // GET: TbTickets
        public ActionResult Index(string searchString , string sortOrder, int sayfa = 1)
        {
            var ticket = from s in db.TbTicket
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ticket = ticket.Where(s => s.StTicketName.Contains(searchString) || s.StSurname.Contains(searchString));
                //ticket = ticket.Where(s =>  s.StSurname.Contains(searchSurname));
            }

            ////////
            switch (sortOrder)
            {
                case "name_desc":
                    ticket = ticket.OrderByDescending(s => s.StTicketName);
                    break;
                case "Date":
                    ticket = ticket.OrderBy(s => s.StSurname);
                    break;
                case "date_desc":
                    ticket = ticket.OrderByDescending(s => s.DtDate);
                    break;
                default:
                    ticket = ticket.OrderBy(s => s.StTicketName);
                    break;

            }


            ActionResult ar = View(ticket.ToPagedList(sayfa, 5));
            return ar;
        }

        // GET: TbTickets/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbTicket tbTicket = db.TbTicket.Find(id);
            if (tbTicket == null)
            {
                return HttpNotFound();
            }
            return View(tbTicket);
        }

        // GET: TbTickets/Create

        public ActionResult Create()
        {
            return View();


        }

        // POST: TbTickets/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InUserID,StTicketName,StSurname,StEmail,StProje,StSubject,StTitle,StMessage,StFile,DtDate")] TbTicket tbTicket)
        {
          
            //var Kullanici = db.TbTicket.Where(a => a.StEmail == tbTicket.StEmail).FirstOrDefault();
            //if(Kullanici==null)
            if (ModelState.IsValid)
            {
                db.TbTicket.Add(tbTicket);
                db.SaveChanges();
                //return RedirectToAction("Index");

                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress("inceahmet4@gmail.com");
                ePosta.To.Add("inceahmet4@gmail.com");
                ePosta.Subject = tbTicket.StSubject;
                ePosta.Body = tbTicket.StMessage;
                ePosta.IsBodyHtml = true;


                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("inceahmet4@gmail.com", "ahmet147930");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(ePosta);

                //if (tbTicket != null)
                //{
                //    string dosyaYolu = Path.GetFileName(tbTicket.StFile);
                //    var yuklemeYeri = Path.Combine(Server.MapPath("~/File"), dosyaYolu);
                //    tbTicket.SaveAs(yuklemeYeri);
                //}

                return RedirectToAction("Index");
            }




            //else
            //{
            //    ViewBag.Error = "This Ticket Already Has Been Added. Please Change Your Email Address";
            //}
            return View(tbTicket);
        }



        // GET: TbTickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbTicket tbTicket = db.TbTicket.Find(id);
            if (tbTicket == null)
            {
                return HttpNotFound();
            }
            return View(tbTicket);
        }

        // POST: TbTickets/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InUserID,StTicketName,StSurname,StEmail,StProje,StSubject,StTitle,StMessage,DtDate")] TbTicket tbTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTicket);
        }

        // GET: TbTickets/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbTicket tbTicket = db.TbTicket.Find(id);
            if (tbTicket == null)
            {
                return HttpNotFound();
            }
            return View(tbTicket);
        }

        // POST: TbTickets/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbTicket tbTicket = db.TbTicket.Find(id);
            db.TbTicket.Remove(tbTicket);
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
