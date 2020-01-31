using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BotDetect.Web.Mvc;
using Newtonsoft.Json;
using Sys.Models;

namespace Sys.Controllers
{
    public class ContactController : Controller
    {
        private SysContext db = new SysContext();

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect Captcha Code!")]
        [HttpPost]
        public ActionResult Index([Bind(Include = "Id,Item,Content,Name,Gender,Email,Tel,PublishDate,UpdateUser,UpdateDate,InitDate")]ContactUS contactUS,bool captchaValid)
        {
            if (ModelState.IsValid)
            {

                contactUS.publishDate = DateTime.UtcNow.AddHours(8);
                contactUS.InitDate = DateTime.UtcNow.AddHours(8);

                db.ContactUS.Add(contactUS);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();
        }

        //// GET: Contact/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ContactViewModel contactViewModel = db.ContactViewModels.Find(id);
        //    if (contactViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contactViewModel);
        //}

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,PhoneNumber,Content")] ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.ContactViewModels.Add(contactViewModel);
                //db.SaveChanges();

                //todo:Send Email

                return RedirectToAction("Index", "Home");
            }

            return View(contactViewModel);
        }

        // GET: Contact/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ContactViewModel contactViewModel = db.ContactViewModels.Find(id);
        //    if (contactViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contactViewModel);
        //}

        //// POST: Contact/Edit/5
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Email,PhoneNumber,Content")] ContactViewModel contactViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(contactViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(contactViewModel);
        //}

        //// GET: Contact/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ContactViewModel contactViewModel = db.ContactViewModels.Find(id);
        //    if (contactViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contactViewModel);
        //}

        //// POST: Contact/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ContactViewModel contactViewModel = db.ContactViewModels.Find(id);
        //    db.ContactViewModels.Remove(contactViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
