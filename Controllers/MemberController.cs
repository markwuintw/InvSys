using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BotDetect.Web.Mvc;
using Newtonsoft.Json;
using Sys.Models;

namespace Sys.Controllers
{
    public class MemberController : Controller
    {
        private SysContext db = new SysContext();

        // GET: Member
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account,string password)
        {

            Member memberr = db.Members.Where(a => a.Account == account).FirstOrDefault();
            if (memberr == null)
            {
                ViewBag.Message = "登入失敗";
                return View();
            }
            string strSalt = memberr.Salt;

            //SHA256加密
            byte[] pwdAndSalt = Encoding.UTF8.GetBytes(password + strSalt);
            byte[] hashBytes = new SHA256Managed().ComputeHash(pwdAndSalt);
            string hashStr = Convert.ToBase64String(hashBytes); // 轉成文字


            Member member = db.Members.Where(x => x.Account == account && x.Password == hashStr).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "登入失敗";
                return View();
            }
            string userData = JsonConvert.SerializeObject(member);

            Session["member"] = member;


            //User.Identity.Name 可呼叫上行
            //return RedirectToAction("Index");
            return RedirectToAction("Index", "Download");
        }


        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Download");
        }


        public ActionResult Register()
        {
            return View();
        }

        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect Captcha Code!")]
        [HttpPost]
        public ActionResult Register(Member member, HttpPostedFileBase Image, bool captchaValid)
        {
            if (ModelState.IsValid)
            {
                var member0 = db.Members.Where(m => m.Account.Contains(member.Account)).Count();
                if (member0>0)
                {
                    ViewBag.AcMessage = "已存在相同帳號";
                    return View();
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
                return RedirectToAction("Login");
            }
            return View();
        }


        public ActionResult Edit()
        {
            return View();
        }
    }
}