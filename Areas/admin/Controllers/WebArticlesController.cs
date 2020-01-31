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
    public class WebArticlesController : Controller
    {
        private SysContext db = new SysContext();


        private const int DefaultPageSize = 10;  //每分頁10個

        public ActionResult Index(int? page)
        {
            if (!page.HasValue) //因為第一頁不回傳，第二頁回傳2，但系統判斷是從0開始。
            {
                page = 0;
            }
            else
            {
                page--;  // page = page - 1 
            }
            var aboutLink = db.WebArticles;
            return View(aboutLink.ToList().ToPagedList((int)page, DefaultPageSize));
        }


        //// GET: admin/WebArticles
        //public ActionResult Index()
        //{
        //    return View(db.WebArticles.ToList());
        //}

        // GET: admin/WebArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebArticle webArticle = db.WebArticles.Find(id);
            if (webArticle == null)
            {
                return HttpNotFound();
            }
            return View(webArticle);
        }

        // GET: admin/WebArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/WebArticles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Article,viewers,publishUser,publishDate,UpdateUser,InitDate")] WebArticle webArticle)
        {
            if (ModelState.IsValid)
            {
                webArticle.viewers = 0;
                db.WebArticles.Add(webArticle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webArticle);
        }

        // GET: admin/WebArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebArticle webArticle = db.WebArticles.Find(id);
            if (webArticle == null)
            {
                return HttpNotFound();
            }

            webArticle.Content = HttpUtility.HtmlDecode(webArticle.Content);

            return View(webArticle);
        }

        // POST: admin/WebArticles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Article,viewers,publishUser,publishDate,UpdateUser,InitDate,Content")] WebArticle webArticle)
        {
            //ModelState.Remove("Id");
            //ModelState.Remove("Article");
            //ModelState.Remove("viewers");
            //ModelState.Remove("publishUser");
            //ModelState.Remove("publishDate");
            //ModelState.Remove("UpdateUser");
            //ModelState.Remove("InitDate");
            //ModelState.Remove("Content");

            if (ModelState.IsValid)
            {
                //HttpCookie cookie = Request.Cookies["authenticationcookie"];
                //todo 抓Cookies的使用者名稱

                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                //HttpCookie cookie = Request.Cookies[".ASPXAUTH"];
                //Member member = JsonConvert.DeserializeObject<Member>(cookie.Value);
                webArticle.UpdateUser = member.Name;

                //todo 瀏覽人數
                //webArticle.viewers = 1; 

                db.Entry(webArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webArticle);
        }

        // GET: admin/WebArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebArticle webArticle = db.WebArticles.Find(id);
            if (webArticle == null)
            {
                return HttpNotFound();
            }
            return View(webArticle);
        }

        // POST: admin/WebArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebArticle webArticle = db.WebArticles.Find(id);
            db.WebArticles.Remove(webArticle);
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
