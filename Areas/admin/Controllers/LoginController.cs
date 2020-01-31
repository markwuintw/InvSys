using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MvcPaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // 欲使用 JObject
using Sys.Filters;
using Sys.Models;

namespace Sys.Areas.admin.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class LoginController : Controller
    {
        private SysContext db = new SysContext();

        // GET: Login
        //public ActionResult Index()
        //{
        //    return View(db.Members.ToList());
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
            var Member = db.Members;
            return View(Member.ToList().ToPagedList((int)page, DefaultPageSize));
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            return View(member);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            List<Permissions> permission = db.Permissions.ToList();
            @ViewBag.pArray = permission;
            //string a = permission.ToString();
            //@ViewBag.permission = db.Members.Where(x => x.Account == Account);

            //List<Permissions> permission = db.Permissions.ToList();
            //StringBuilder stringBuilder = new StringBuilder();
            //getList(permission, stringBuilder);
            //@ViewBag.chkPermission = stringBuilder.ToString();

            return View();
        }

        // POST: Login/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                "Id,Account,Password,Name,Gender,Email,PhoneNumber,Image,ManagerType,JobTitle,Permission")]
            Member member, HttpPostedFileBase Image, string[] check)
        {
            if (ModelState.IsValid)
            {

                if (check != null)
                {
                    member.Permission += string.Join(",", check);
                }
                else
                {
                    member.Permission += "";
                }

                if (Image != null)
                {
                    if (Image.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(member);
                    }

                    //取得副檔名
                    string extension = Image.FileName.Split('.')[Image.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);
                    Image.SaveAs(savedName);
                    member.Image = fileName;

                }

                string userPwd = member.Password;

                string strSalt = Guid.NewGuid().ToString();

                //SHA256加密
                byte[] pwdAndSalt = Encoding.UTF8.GetBytes(userPwd + strSalt);
                byte[] hashBytes = new SHA256Managed().ComputeHash(pwdAndSalt);
                string hashStr = Convert.ToBase64String(hashBytes);

                member.Salt = strSalt;

                member.Password = hashStr;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(member);
        }


        [HttpPost]
        public ActionResult CheckAccount(string Account)
        {
            var member = db.Members.Where(x => x.Account == Account).FirstOrDefault();
            if (member != null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);

            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            List<Permissions> permission = db.Permissions.ToList();
            @ViewBag.pArray = permission;

            var chkPermission = db.Members.Where(x => x.Id == id).FirstOrDefault();

            @ViewBag.permission = chkPermission.Permission;

            //string a =db.Members.Where(x => x.Id == id).ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            List<Permissions> permission0 = db.Permissions.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            getList(permission0, stringBuilder);
            @ViewBag.chkPermission = stringBuilder.ToString();

            return View(member);
        }

        // POST: Login/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "Id,Account,Password,Name,Gender,Email,PhoneNumber,Image,ManagerType,JobTitle,Permission,Salt")]
            Member member, HttpPostedFileBase UpImage, string[] check, string Password1)
        {
            if (ModelState.IsValid)
            {
                List<Permissions> permission = db.Permissions.ToList();
                @ViewBag.pArray = permission;

                if (Password1 != "")
                {
                    string userPwd = Password1;

                    string strSalt = Guid.NewGuid().ToString();

                    //SHA256加密
                    byte[] pwdAndSalt = Encoding.UTF8.GetBytes(userPwd + strSalt);
                    byte[] hashBytes = new SHA256Managed().ComputeHash(pwdAndSalt);
                    string hashStr = Convert.ToBase64String(hashBytes);

                    member.Salt = strSalt;

                    member.Password = hashStr;

                }

                if (check != null)
                {
                    member.Permission = string.Join(",", check);
                }
                else
                {
                    member.Permission = "";
                }

                if (UpImage != null)
                {
                    if (UpImage.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(member);
                    }

                    //取得副檔名
                    string extension = UpImage.FileName.Split('.')[UpImage.FileName.Split('.').Length - 1];
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("~/UpFiles"), fileName);
                    UpImage.SaveAs(savedName);
                    member.Image = fileName;
                }


                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            return View(member);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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

        // 練習 API

        public ActionResult GetJson()
        {
            string guid = Guid.NewGuid().ToString(); //GUID 用法
            var about = db.Members.ToList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(about);
            return Content(json, "application/json");
        }

        // 練習 JObject ，用在組自訂格式的不規則物件

        public ActionResult GetJson1()
        {
            string guid = Guid.NewGuid().ToString(); //GUID 用法
            //var about = db.Members.ToList().ToString();

            List<string> Accounts = new List<string>(); //這邊List只能是基本型別，如 int,string...
            List<string> Emails = new List<string>();

            JObject json = new JObject();

            foreach (Models.Member Members in db.Members)
            {
                Accounts.Add(Members.Account);
                Emails.Add(Members.Email);
            }

            json.Add(new JProperty("Accounts", Accounts));
            json.Add(new JProperty("Emails", Emails));
            json.Add(new JProperty("guid", guid));

            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(json, Formatting.Indented);
            return Content(jsonContent, "application/json");
        }

        //雙層
        //JObject jsonNew = new JObject();
        //jsonNew.Add("第二層屬性","ohya");

        ////加到第一層的物件裡
        //json.Add(new JProperty("第二層", jsonNew));

        public ActionResult GetJson2()
        {
            string guid = Guid.NewGuid().ToString(); //GUID 用法

            //................................................................

            List<string> Accounts = new List<string>(); //這邊List只能是基本型別，如 int,string...
            List<string> Emails = new List<string>();

            JObject json = new JObject();

            foreach (Models.Member Members in db.Members)
            {
                Accounts.Add(Members.Account);
                Emails.Add(Members.Email);
            }

            json.Add(new JProperty("Accounts", Accounts));
            json.Add(new JProperty("Emails", Emails));
            json.Add(new JProperty("guid", guid));

            //................................................................

            List<string> Name = new List<string>(); //這邊List只能是基本型別，如 int,string...
            List<string> PhoneNumber = new List<string>();

            JObject jsonInner = new JObject();

            foreach (Models.Member Members in db.Members)
            {
                Name.Add(Members.Name);
                PhoneNumber.Add(Members.PhoneNumber);
            }

            jsonInner.Add(new JProperty("Name", Name));
            jsonInner.Add(new JProperty("PhoneNumber", PhoneNumber));

            //................................................................

            json.Add(new JProperty("jsonInner", jsonInner));


            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(json, Formatting.Indented);
            return Content(jsonContent, "application/json");
        }

        public ActionResult GetJson3()
        {
            string guid = Guid.NewGuid().ToString(); //GUID 用法
            var about = db.Members.ToList();
            var Newabout = about.Select(x => new {abb = x.Account, www = x.Permission});
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Newabout);
            return Content(json, "application/json");
        }
        // 如果只想顯示特定欄位時， 在Model欲不顯示的欄位上加 [JsonIgnore]標籤 或 使用Select命令( var newAbout = about.Select(x=> new{ subject=x.subject,url=x.url}) ;


        public ActionResult GetJson16()
        {
            int n = 0;

            dynamic json0 = new JObject();

            json0.Add(new JProperty("id", n));

            json0.Add(new JProperty("text", "縱火"));

            //..............................................

            dynamic json1 = new JObject();

            json1.Add(new JProperty("id", n));

            json1.Add(new JProperty("text", "縱火"));

            dynamic json2 = new JObject();

            json2.Add(new JProperty("id", n));

            json2.Add(new JProperty("text", "縱火"));

            dynamic array = new JObject();

            array.hhh = new JArray() as dynamic;

            array.hhh.Add(json1);

            array.hhh.Add(json2);


            json0.Add(new JProperty("children", array.hhh));


            //......................................................

            foreach (Member Member in db.Members)
            {
                string[] Permissions = Member.Permission.Split(',');
            }


           
            var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(json0, Formatting.Indented);
            return Content(jsonContent, "application/json");
        }

        public void getList(List<Permissions> permission, StringBuilder stringBuilder)
        {
            foreach (var item in permission)
            {
                if (stringBuilder.ToString().IndexOf("{id:" + item.Id) == -1)
                {
                    stringBuilder.Append("{id:" + item.Id + ",text:'" + item.Permission + "'");
                    if (permission.Where(x => x.PId == item.Id).Count() > 0)
                    {
                        stringBuilder.Append(",children:[");
                        var subP = permission.Where(x => x.PId == item.Id).ToList();
                        getList(subP, stringBuilder);
                        stringBuilder.Append("]");
                    }
                    stringBuilder.Append("},");
                }
            }
        }
    }
}
