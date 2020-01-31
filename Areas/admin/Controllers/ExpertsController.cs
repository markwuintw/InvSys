
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class ExpertsController : Controller
    {
        private SysContext db = new SysContext();

        // GET: admin/Experts
        //public ActionResult Index()
        //{
        //    return View(db.Experts.ToList());
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
            var Expert = db.Experts;
            return View(Expert.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(int? page, string SearchName, DateTime? SDate, DateTime? EDate)
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

            var Expert = db.Experts.OrderByDescending(n => n.InitDate).AsQueryable(); // 意涵，我只是一個SQL語法，還不用去執行，代表還能作修改

            if (!string.IsNullOrEmpty(SearchName))
            {
                Expert = Expert.Where(x => x.Name.Contains(SearchName));
            }
            if (SDate.HasValue && EDate.HasValue)
            {
                DateTime FEDate = ((DateTime)EDate).AddDays(1); //LINQ無法作時間加總，故需要提前拉出來做
                Expert = Expert.Where(x => x.publishDate >= SDate && x.publishDate < FEDate);
            }

            return View(Expert.ToPagedList((int)page, DefaultPageSize));//ToList()代表將上方語法去查資料庫得所有資料，即作實體化.ToPagedList得該分頁資料

            //

            //var ContactUS = db.ContactUS;
            //return View(ContactUS.ToList().ToPagedList((int)page, DefaultPageSize));
        }


        // GET: admin/Experts/Details/5
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

        // GET: admin/Experts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Experts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Job,Photo,Education,BasicIntroduction,RelativeIntroduction,publishUser,publishDate,InitDate")] Expert expert, HttpPostedFileBase Photo)//Image另外寫是因為還需現做判斷
        {
            ModelState.Remove("publishUser");
            if (ModelState.IsValid)
            {
                // 上傳檔案
                if (Photo != null)
                {
                    if (Photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(expert);
                    }
                    //取得副檔名
                    string extension = Photo.FileName.Split('.')[Photo.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);//須先建立好存放資料夾
                    Photo.SaveAs(savedName);
                    expert.Photo = fileName;
                }

                expert.InitDate = DateTime.UtcNow.AddHours(8);
                expert.publishDate = DateTime.UtcNow.AddHours(8);

                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                //HttpCookie cookie = Request.Cookies[".ASPXAUTH"];
                //Member member = JsonConvert.DeserializeObject<Member>(cookie.Value);
                expert.publishUser = member.Name;

                db.Experts.Add(expert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expert);
        }

        // GET: admin/Experts/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: admin/Experts/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Job,Photo,Education,BasicIntroduction,RelativeIntroduction,publishUser,publishDate,InitDate")] Expert expert, HttpPostedFileBase UpImage)//Image另外寫是因為還需現做判斷
        {
            if (ModelState.IsValid)
            {
                if (UpImage != null) //上傳欄位不為空，即重新做新增的動作，並將aboutLinks.Image重新賦值(原是抓儲放在隱藏欄位的原值)，最後一併修改資料庫
                {
                    if (UpImage.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(expert);
                    }
                    //取得副檔名
                    string extension = UpImage.FileName.Split('.')[UpImage.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);//須先建立好存放資料夾
                    UpImage.SaveAs(savedName);
                    expert.Photo = fileName;
                }

                db.Entry(expert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expert);
        }

        // GET: admin/Experts/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: admin/Experts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expert expert = db.Experts.Find(id);
            db.Experts.Remove(expert);
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
