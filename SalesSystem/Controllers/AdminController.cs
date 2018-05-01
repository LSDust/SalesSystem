using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public string InitPurchaser()
        {
            Admin db = new Admin();
            string jsondata = db.SelectPurchaser();
            db.Closedb();
            return jsondata;
        }
        public bool RemovalPurchaser(string id)
        {
            Admin db = new Admin();
            bool flag = db.DeletePurchaser(id);
            db.Closedb();
            return flag;
        }

        //销售商信息模块
        public ActionResult RetailerInfo()
        {
            return View();
        }
        public string InitRetailer()
        {
            Admin db = new Admin();
            string jsondata = db.SelectRetailer();
            db.Closedb();
            return jsondata;
        }
        public bool AddRetailer(string id, string name, string tel, string address)
        {
            Admin db = new Admin();
            return db.InsertRetailer(id, name, tel, address);
        }
        public bool RemovalRetailer(string id)
        {
            Admin db = new Admin();
            bool flag = db.DeleteRetailer(id);
            db.Closedb();
            return flag;
        }

        //订单统计
        public ActionResult AllBill()
        {
            return View();
        }
        public string InitAllBill()
        {
            Admin db = new Admin();
            string jsondata = db.SelectAllBill();
            db.Closedb();
            return jsondata;
        }
    }
}