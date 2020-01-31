using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Sys.Filters;
using Sys.Models;

namespace Sys.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        private SysContext db = new SysContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account, string Password)
        {
            Member memberr = db.Members.Where(a => a.Account == Account).FirstOrDefault();

            string strSalt = memberr.Salt;

            //SHA256加密
            byte[] pwdAndSalt = Encoding.UTF8.GetBytes(Password + strSalt);
            byte[] hashBytes = new SHA256Managed().ComputeHash(pwdAndSalt);
            string hashStr = Convert.ToBase64String(hashBytes); // 轉成文字


            Member member = db.Members.Where(x => x.Account == Account && x.Password == hashStr).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "登入失敗";
                return View();
            }
            string userData = JsonConvert.SerializeObject(member);
            SetAuthenTicket(userData, member.Account);

            //User.Identity.Name 可呼叫上行
            //return RedirectToAction("Index");
            return RedirectToAction("Index","Home",  new { Area = "admin"});

        }

        void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //將Cookie寫入回應
            Response.Cookies.Add(authenticationcookie);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        [PermissionFilters]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}