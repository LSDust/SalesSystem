using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesSystem.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            return View();
        }
        public String InitPorductList(string id)
        {
            Client db = new Client();
            string jsondata = db.SelectCommodityInfo(id);
            db.Closedb();
            return jsondata;
        }
        public String InitRetailer()
        {
            Client db = new Client();
            string jsondata = db.SelectRetailerInfo();
            db.Closedb();
            return jsondata;
        }
        public ActionResult Personal()
        {
            return View();
        }
        public bool UpdatePurchaserInfo(string name,string tel)
        {
            Client db = new Client();
            bool flag = db.UpdatePurchaser(Session["id"].ToString(), name, tel);
            db.Closedb();
            return flag;
        }
        public void AddBill2(string id, string cname, float quantity)
        {
            RetailerManage db = new RetailerManage();
            db.InsertManageBill2(id, cname, Session["id"].ToString(), quantity);
            db.Closedb();
            db.UpdateManage2(id, cname, quantity);
            db.Closedb();
        }
        public String ThisRetailer(string id)
        {
            Client db = new Client();
            string jsondata = db.SelectThisRetailer(id);
            db.Closedb();
            return jsondata;
        }
        public ActionResult Bill()
        {
            return View();
        }
        public String InitBill2()
        {
            Client db = new Client();
            string jsondata = db.SelectBill(Session["id"].ToString());
            db.Closedb();
            return jsondata;
        }
    }
}