using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Sys.Models;


namespace Sys.Filters
{
    public class PermissionFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SysContext db = new SysContext();

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.menu = "";
                return;
            }
            //todo:組選單字串

            string strUserData = ((FormsIdentity) (HttpContext.Current.User.Identity)).Ticket.UserData;

            Member member = JsonConvert.DeserializeObject<Member>(strUserData);

            StringBuilder stringBuilder = new StringBuilder();

            filterContext.Controller.ViewBag.menu += @"<li><a class=""menuitem"" >國際縱火調查人員協會</a><ul class=""submenu"">";

            if (member.Permission.Contains("帳號管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/Login/Index"" >帳號管理</a></li>";
                //filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/Login/Role"" >角色管理</a></li>";
            }

            if (member.Permission.Contains("網站內容管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/WebArticles/Index"" > 網站內容管理 </a></li>";
            }

            if (member.Permission.Contains("最新消息管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/News/Index"" > 最新消息管理 </a></li>";
            }

            if (member.Permission.Contains("相關連結管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/AboutLinks/Index"" > 相關連結管理 </a></li>";

            }

            if (member.Permission.Contains("首頁圖片管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/Banners/Index"" > 首頁圖片管理 </a></li>";

            }

            if (member.Permission.Contains("聯絡我們管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/ContactUS/Index"" > 聯絡我們管理 </a></li>";

            }

            if (member.Permission.Contains("討論區管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/Downloads/Index"" > 討論區管理 </a></li>";

            }

            if (member.Permission.Contains("知識庫管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/Knowledges/Index"" > 知識庫管理 </a></li>";

            }

            if (member.Permission.Contains("專家介紹管理"))
            {
                filterContext.Controller.ViewBag.menu += @"<li><a href = ""/admin/Experts/Index"" > 專家介紹管理 </a></li>";

            }


            filterContext.Controller.ViewBag.menu += @"</ul></li>";

        }
    }
}