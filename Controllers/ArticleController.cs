using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sys.Models;

namespace Sys.Controllers
{
    public class ArticleController : Controller
    {
        private SysContext db = new SysContext();

        // GET: Article
        public ActionResult About()
        {
            WebArticle WebArticles = db.WebArticles.Find(3);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Organization()
        {
            WebArticle WebArticles = db.WebArticles.Find(4);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult History()
        {
            WebArticle WebArticles = db.WebArticles.Find(5);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Member()
        {
            WebArticle WebArticles = db.WebArticles.Find(1);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        //public ActionResult Expert()
        //{
        //    return View();
        //}

        public ActionResult Declare()
        {
            WebArticle WebArticles = db.WebArticles.Find(5);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Job()
        {
            WebArticle WebArticles = db.WebArticles.Find(6);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Licenses()
        {
            WebArticle WebArticles = db.WebArticles.Find(7);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Refer()
        {
            WebArticle WebArticles = db.WebArticles.Find(8);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Survey()
        {
            WebArticle WebArticles = db.WebArticles.Find(9);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }

        public ActionResult Calendar()
        {
            WebArticle WebArticles = db.WebArticles.Find(10);

            WebArticles.viewers = WebArticles.viewers + 1;
            db.Entry(WebArticles).State = EntityState.Modified;
            db.SaveChanges();

            return View(WebArticles);
        }
    }
}