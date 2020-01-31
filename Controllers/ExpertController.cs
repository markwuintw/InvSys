using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sys.Models;

namespace Sys.Controllers
{
    public class ExpertController : Controller
    {
        private SysContext db = new SysContext();

        // GET: Expert
        public ActionResult Index()
        {
            var Experts = db.Experts.OrderBy(e=>e.Name).AsQueryable(); // IQueryable ，尚未執行

            ViewBag.Experts = Experts;

            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expert expert = db.Experts.Find(id);
            if (expert == null)
            {
                return HttpNotFound();
            }
            return View(expert);
        }
    }
}