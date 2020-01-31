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
    public class DownloadsController : Controller
    {
        private SysContext db = new SysContext();

        // GET: admin/Downloads
        //public ActionResult Index()
        //{
        //    return View(db.Downloads.ToList());
        //}

        private const int DefaultPageSize = 10;  //@*每分頁10個*@

        public ActionResult Index(int? page)
        {
            if (!page.HasValue) //因為第一頁不回傳，第二頁回傳2，但系統判斷是從0開始。
            {
                page = 0;
            }
            else
            {
                page--;  //@*page = page - 1 *@
            }
            var Download = db.Downloads;
            return View(Download.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(int? page, string SearchItem, DateTime? SDate, DateTime? EDate)
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

            var Download = db.Downloads.OrderByDescending(n => n.InitDate).AsQueryable(); // 意涵，我只是一個SQL語法，還不用去執行，代表還能作修改

            if (!string.IsNullOrEmpty(SearchItem))
            {
                Download = Download.Where(x => x.Item.Contains(SearchItem));
            }
            if (SDate.HasValue && EDate.HasValue)
            {
                DateTime FEDate = ((DateTime)EDate).AddDays(1); //LINQ無法作時間加總，故需要提前拉出來做
                Download = Download.Where(x => x.publishDate >= SDate && x.publishDate < FEDate);
            }

            return View(Download.ToPagedList((int)page, DefaultPageSize));//ToList()代表將上方語法去查資料庫得所有資料，即作實體化.ToPagedList得該分頁資料

            //

            //var ContactUS = db.ContactUS;
            //return View(ContactUS.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        // GET: admin/Downloads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return HttpNotFound();
            }
            return View(download);
        }

        // GET: admin/Downloads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Downloads/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,Content,viewers,publishUser,publishDate,InitDate")] Download download)
        {
            ModelState.Remove("publishUser");
            if (ModelState.IsValid)
            {
                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                //HttpCookie cookie = Request.Cookies[".ASPXAUTH"];
                //Member member = JsonConvert.DeserializeObject<Member>(cookie.Value);
                download.publishUser = member.Name;

                download.InitDate = DateTime.UtcNow.AddHours(8);
                download.publishDate = DateTime.UtcNow.AddHours(8);

                download.viewers = 0;

                db.Downloads.Add(download);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(download);
        }

        // GET: admin/Downloads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return HttpNotFound();
            }
            return View(download);
        }

        // POST: admin/Downloads/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,Content,viewers,publishUser,publishDate,InitDate")] Download download)
        {
            if (ModelState.IsValid)
            {
                db.Entry(download).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(download);
        }

        // GET: admin/Downloads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Download download = db.Downloads.Find(id);
            if (download == null)
            {
                return HttpNotFound();
            }
            return View(download);
        }

        // POST: admin/Downloads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Download download = db.Downloads.Find(id);
            db.Downloads.Remove(download);
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
