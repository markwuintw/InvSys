using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcPaging;
using Newtonsoft.Json;
using Sys.Filters;
using Sys.Models;

namespace Sys.Areas.admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class ContactUSController : Controller
    {
        private SysContext db = new SysContext();

        // GET: admin/ContactUS
        //public ActionResult Index()
        //{
        //    return View(db.ContactUS.ToList());
        //}

        private const int DefaultPageSize = 10; //每分頁10個

        public ActionResult Index(int? page)
        {
            if (!page.HasValue) //因為第一頁不回傳，第二頁回傳2，但系統判斷是從0開始。
            {
                page = 0;
            }
            else
            {
                page--;  //page = page - 1
            }
            var ContactUS = db.ContactUS;
            return View(ContactUS.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(int? page, GenderType? SearchGender, string SearchItem, string SearchName, DateTime? SDate, DateTime? EDate)
        {
            if (!page.HasValue) //因為第一頁不回傳，第二頁回傳2，但系統判斷是從0開始。
            {
                page = 0;
            }
            else
            {
                page--;  //page = page - 1
            }

            //

            //ViewBag.CategoryId = new SelectList(db.News, "Id", "Subject");

            var ContactUS = db.ContactUS.OrderByDescending(n => n.InitDate).AsQueryable(); // 意涵，我只是一個SQL語法，還不用去執行，代表還能作修改

            if (SearchGender.HasValue)
            {
                ContactUS = ContactUS.Where(x => x.Gender == SearchGender);
            }
            if (!string.IsNullOrEmpty(SearchItem))
            {
                ContactUS = ContactUS.Where(x => x.Item.Contains(SearchItem));
            }
            if (!string.IsNullOrEmpty(SearchName))
            {
                ContactUS = ContactUS.Where(x => x.Name.Contains(SearchName));
            }
            if (SDate.HasValue && EDate.HasValue)
            {
                DateTime FEDate = ((DateTime)EDate).AddDays(1); //LINQ無法作時間加總，故需要提前拉出來做
                ContactUS = ContactUS.Where(x => x.publishDate >= SDate && x.publishDate < FEDate);
            }

            return View(ContactUS.ToPagedList((int)page, DefaultPageSize));//ToList()代表將上方語法去查資料庫得所有資料，即作實體化.ToPagedList得該分頁資料

            //

            //var ContactUS = db.ContactUS;
            //return View(ContactUS.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        // GET: admin/ContactUS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS contactUS = db.ContactUS.Find(id);
            if (contactUS == null)
            {
                return HttpNotFound();
            }
            return View(contactUS);
        }

        // GET: admin/ContactUS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/ContactUS/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Item,Content,Name,Gender,Email,Tel,publishUser,publishDate,UpdateUser,UpdateDate,InitDate")] ContactUS contactUS)
        {
            if (ModelState.IsValid)
            {
                contactUS.InitDate = DateTime.UtcNow.AddHours(8);
                contactUS.publishDate = DateTime.UtcNow.AddHours(8);
                contactUS.UpdateDate = DateTime.UtcNow.AddHours(8);

                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                contactUS.UpdateUser = member.Name;

                db.ContactUS.Add(contactUS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUS);
        }

        // GET: admin/ContactUS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS contactUS = db.ContactUS.Find(id);
            if (contactUS == null)
            {
                return HttpNotFound();
            }
            return View(contactUS);
        }

        // POST: admin/ContactUS/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Item,Content,Name,Gender,Email,Tel,publishUser,publishDate,UpdateUser,UpdateDate,InitDate")] ContactUS contactUS)
        {
            if (ModelState.IsValid)
            {
                contactUS.UpdateDate = DateTime.UtcNow.AddHours(8);

                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                contactUS.UpdateUser = member.Name;

                db.Entry(contactUS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUS);
        }

        // GET: admin/ContactUS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUS contactUS = db.ContactUS.Find(id);
            if (contactUS == null)
            {
                return HttpNotFound();
            }
            return View(contactUS);
        }

        // POST: admin/ContactUS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUS contactUS = db.ContactUS.Find(id);
            db.ContactUS.Remove(contactUS);
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
