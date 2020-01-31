using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Sys.Filters;
using Sys.Models;

namespace Sys.Areas.admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class AboutLinksController : Controller
    {
        private SysContext db = new SysContext();

        // GET: admin/AboutLinks
        //public ActionResult Index()
        //{
        //    return View(db.AboutLinks.ToList());
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
            var aboutLink = db.AboutLinks;
            return View(aboutLink.ToList().ToPagedList((int)page, DefaultPageSize));
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
                    AboutLink aboutLink = db.AboutLinks.Find(Convert.ToInt16(itemDatas[0]));
                    aboutLink.order = Convert.ToInt16(itemDatas[1]);
                    //db.Entry(publish).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        // GET: admin/AboutLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutLink aboutLink = db.AboutLinks.Find(id);
            if (aboutLink == null)
            {
                return HttpNotFound();
            }
            return View(aboutLink);
        }

        // GET: admin/AboutLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/AboutLinks/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,LinkURL,Photo,OnSite,order,publishDate,InitDate")] AboutLink aboutLink, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                // 上傳檔案
                if (Photo != null)
                {
                    if (Photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(aboutLink);
                    }
                    //取得副檔名
                    string extension = Photo.FileName.Split('.')[Photo.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);//須先建立好存放資料夾
                    Photo.SaveAs(savedName);
                    aboutLink.Photo = fileName;
                    aboutLink.order = db.AboutLinks.Count() + 1;

                }

                db.AboutLinks.Add(aboutLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutLink);
        }

        // GET: admin/AboutLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutLink aboutLink = db.AboutLinks.Find(id);
            if (aboutLink == null)
            {
                return HttpNotFound();
            }
            return View(aboutLink);
        }

        // POST: admin/AboutLinks/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]//傳HTML
        public ActionResult Edit([Bind(Include = "Id,Item,Photo,LinkURL,OnSite,order,publishDate,InitDate")] AboutLink aboutLink, HttpPostedFileBase UpImage)
        {
            if (ModelState.IsValid)
            {
                if (UpImage != null) //上傳欄位不為空，即重新做新增的動作，並將aboutLinks.Image重新賦值(原是抓儲放在隱藏欄位的原值)，最後一併修改資料庫
                {
                    if (UpImage.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(aboutLink);
                    }
                    //取得副檔名
                    string extension = UpImage.FileName.Split('.')[UpImage.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);//須先建立好存放資料夾
                    UpImage.SaveAs(savedName);
                    aboutLink.Photo = fileName;
                }

                db.Entry(aboutLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutLink);
        }

        // GET: admin/AboutLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutLink aboutLink = db.AboutLinks.Find(id);
            if (aboutLink == null)
            {
                return HttpNotFound();
            }
            return View(aboutLink);
        }

        // POST: admin/AboutLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutLink aboutLink = db.AboutLinks.Find(id);
            db.AboutLinks.Remove(aboutLink);
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
