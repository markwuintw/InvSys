using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Sys.Models;

namespace Sys.Controllers
{

    public class NewsController : Controller
    {
        private SysContext db = new SysContext();

        // GET: News
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
            var News = db.News.OrderByDescending(e => e.publishDate);
            return View(News.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        private const int DefaultPageSize = 10; //每分頁10個


        // GET: Home/Details/5
        public ActionResult Details( int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(Id);
            if (news == null)
            {
                return HttpNotFound();
            }

            news.viewers = news.viewers + 1;
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();

            return View(news);
        }
    }
}