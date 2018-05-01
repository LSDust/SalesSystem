using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //注册
        [HttpPost]
        public bool Signup(string tel, string pass)
        {
            SqlDb db = new SqlDb();
            bool result = db.InertPurchaser(tel, pass);
            db.Closedb();
            return result;
        }

        //登陆验证
        [HttpPost]
        public String Signin(string tel, string pass)
        {
            SqlDb db = new SqlDb();
            if (db.SelectAdmin(tel, pass) != string.Empty)
            {
                Session["id"] = db.SelectAdmin(tel, pass);
                Session["tel"] = tel;
                return "Admin";
            }
            else if (db.SelectRetailer(tel, pass) != string.Empty)
            {
                Session["id"] = db.SelectRetailer(tel, pass);
                Session["tel"] = tel;
                return "Retailer";
            }
            else if (db.SelectPurchaser(tel, pass) != string.Empty)
            {
                Session["id"] = db.SelectPurchaser(tel, pass);
                Session["tel"] = tel;
                return "Purchaser";
            }
            else
            {
                return "登陆失败";
            }
        }
    }
}