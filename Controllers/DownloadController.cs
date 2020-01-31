using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Sys.Models;

namespace Sys.Controllers
{
    public class DownloadController : Controller
    {
        private SysContext db = new SysContext();

        // GET: Download
        public ActionResult Index(int? page)
        {
            Member memberr =(Member)Session["member"];

            if (memberr == null)
            {
                return RedirectToAction("Login", "Member");
            }

            Member member = db.Members.Where(x => x.Account == memberr.Account && x.Password == memberr.Password).FirstOrDefault();

            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }

            if (!page.HasValue) //因為第一頁不回傳，第二頁回傳2，但系統判斷是從0開始。
            {
                page = 0;
            }
            else
            {
                page--;  //page = page - 1
            }
            var downloads = db.Downloads.OrderByDescending(d => d.publishDate);
            return View(downloads.ToList().ToPagedList((int)page, DefaultPageSize));
        }


        private const int DefaultPageSize = 10; //每分頁10個

        public ActionResult Details( int? Id)
        {
            Member memberr = (Member)Session["member"];

            if (memberr == null)
            {
                return RedirectToAction("Login", "Member");
            }

            Member member = db.Members.Where(x => x.Account == memberr.Account && x.Password == memberr.Password).FirstOrDefault();

            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }

            Download downloads = db.Downloads.Find(Id);

            downloads.viewers = downloads.viewers + 1;
            db.Entry(downloads).State = EntityState.Modified;
            db.SaveChanges();

            return View(downloads);
        }
    }
}