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
    public class BannersController : Controller
    {
        private SysContext db = new SysContext();

        // GET: admin/Banners
        //public ActionResult Index()
        //{
        //    return View(db.Banners.ToList());
        //}

        private const int DefaultPageSize = 10;  //每分頁10個

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
            var Banner = db.Banners;
            return View(Banner.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        // GET: admin/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: admin/Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Banners/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,Photo,LinkURL,OnSite,order,publishUser,publishDate,UpdateUser,UpdateDate,InitDate")] Banner banner, HttpPostedFileBase Photo)
        {
            //ModelState.Remove("InitDate");
            //ModelState.Remove("UpdateDate");
            //ModelState.Remove("UpdateUser");
            //ModelState.Remove("publishDate");
            ModelState.Remove("publishUser");
            //ModelState.Remove("order");
            //ModelState.Remove("Id");



            if (ModelState.IsValid)
            {
                // 上傳檔案
                if (Photo != null)
                {
                    if (Photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(banner);
                    }
                    //取得副檔名
                    string extension = Photo.FileName.Split('.')[Photo.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);//須先建立好存放資料夾
                    Photo.SaveAs(savedName);
                    banner.Photo = fileName;
                }

                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                banner.publishUser = member.Name;
                banner.UpdateUser = member.Name;
                banner.InitDate = DateTime.UtcNow.AddHours(8);
                banner.UpdateDate = DateTime.UtcNow.AddHours(8);
                banner.publishDate = DateTime.UtcNow.AddHours(8);
                banner.order = (db.Banners.Count())+1;


                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }


        [HttpPost]
        public ActionResult Sort(string sortData)
        {
            if (!string.IsNullOrEmpty(sortData))
            {
                string[] tempDatas = sortData.TrimEnd(',').Split(',');
                foreach (string tempData in tempDatas)
                {
                    string[] itemDatas = tempData.Split(':');
                    Banner banner = db.Banners.Find(Convert.ToInt16(itemDatas[0]));
                    banner.order = Convert.ToInt16(itemDatas[1]);
                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        // GET: admin/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: admin/Banners/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,Photo,LinkURL,OnSite,order,publishUser,publishDate,UpdateUser,UpdateDate,InitDate")] Banner banner, HttpPostedFileBase UpImage)
        {
            if (ModelState.IsValid)
            {
                if (UpImage != null) //上傳欄位不為空，即重新做新增的動作，並將aboutLinks.Image重新賦值(原是抓儲放在隱藏欄位的原值)，最後一併修改資料庫
                {
                    if (UpImage.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(banner);
                    }
                    //取得副檔名
                    string extension = UpImage.FileName.Split('.')[UpImage.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);//須先建立好存放資料夾
                    UpImage.SaveAs(savedName);
                    banner.Photo = fileName;
                }

                string str_userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
                Member member = JsonConvert.DeserializeObject<Member>(str_userData);

                banner.UpdateUser = member.Name;
                banner.UpdateDate = DateTime.UtcNow.AddHours(8);

                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: admin/Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
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
