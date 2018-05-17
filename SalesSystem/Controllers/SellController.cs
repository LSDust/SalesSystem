using SalesSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SalesSystem.Controllers
{
    public class SellController : Controller
    {
        // GET: Sell
        public ActionResult Index()
        {
            return View();
        }
        //商品管理
        [HttpGet]
        public ActionResult CommodityInfo()
        {
            return View();
        }

        public String InitCommodity()
        {
            RetailerManage db = new RetailerManage();
            string jsondata = db.SelectCommodity(Session["id"].ToString());
            db.Closedb();
            return jsondata;
        }
        [HttpPost]
        public void AddCommodity(string id, string cname, float cost, float price, string unit)
        {
            string pic = Request.Form["pic"];
            RetailerManage db = new RetailerManage();
            db.InertCommodity(id, cname, pic, cost, price, unit);
            db.Closedb();
            db.InserManage(Session["id"].ToString(), id, 0);
            db.Closedb();
        }
        public void SaveCommodity(string id,string cname,decimal cost,decimal price,string unit)
        {
            RetailerManage db = new RetailerManage();
            db.UpdateCommodity(id, cname, cost, price, unit);
        }
        public bool RemovalCommodity(string id)
        {
            RetailerManage db = new RetailerManage();
            db.DeleteManage(id);
            db.Closedb();
            bool flag = db.DeleteCommodity(id);
            db.Closedb();
            return flag;
        }

        //入库管理
        public ActionResult Warehousing()
        {
            return View();
        }

        public void SaveManage(string cname, float quantity)
        {
            RetailerManage db = new RetailerManage();
            db.UpdateManage(Session["id"].ToString(), cname, quantity);
            db.Closedb();
            db.InsertManageBill(Session["id"].ToString(), cname, quantity);
            db.Closedb();
        }

        //销售单
        public ActionResult AddBill()
        {
            return View();
        }
        public string InitPurchaser()
        {
            RetailerManage db = new RetailerManage();
            string session = Session["id"].ToString();
            string jsondata = db.SelectPurchaser(session); ;
            db.Closedb();
            return jsondata;
        }
        public void SaveBill(string purchaserId, string cname, float quantity)
        {
            RetailerManage db = new RetailerManage();
            db.InsertBill(Session["id"].ToString(), cname, purchaserId, quantity);
            db.Closedb();
            db.UpdateManage(Session["id"].ToString(), cname, 0 - quantity);
            db.Closedb();
        }

        //查看账单模块
        public ActionResult Record()
        {
            return View();
        }
        public String InitBill()
        {
            RetailerManage db = new RetailerManage();
            string jsondata = db.SelectBill(Session["id"].ToString());
            db.Closedb();
            return jsondata;
        }
        public ActionResult Record2()
        {
            return View();
        }
        public String InitBill2()
        {
            RetailerManage db = new RetailerManage();
            string jsondata = db.SelectBill2(Session["id"].ToString());
            db.Closedb();
            return jsondata;
        }

        //查看库存
        public ActionResult Inventory()
        {
            return View();
        }
        public String InitInventory()
        {
            RetailerManage db = new RetailerManage();
            string jsondata = db.SelectManage(Session["id"].ToString());
            db.Closedb();
            return jsondata;
        }
        public ActionResult RetailerInfo()
        {
            return View();
        }
        public void UpdateRetailerInfo(string name,string tel,string add)
        {
            RetailerManage db = new RetailerManage();
            db.UpdateRetailer(Session["id"].ToString(), name, tel, add);
            db.Closedb();
        }
        public ActionResult GetForm()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string id = Request.Form["id"];
            HttpFileCollection FileCollect = request.Files;
            if (FileCollect.Count > 0)          //如果集合的数量大于0
            {
                HttpPostedFile FileSave = FileCollect[0];  //用key获取单个文件对象HttpPostedFile                
                string imgPath = "/Image/Retailer/Commodity/" + id + ".jpg";     //通过此对象获取文件名
                string AbsolutePath = Server.MapPath(imgPath);
                FileSave.SaveAs(AbsolutePath);              //将上传的东西保存
                RetailerManage db = new RetailerManage();
                db.UpdateCommodityImg(id, imgPath);
                db.Closedb();
            }
            return View("CommodityInfo");
        }

        public void UpdateBill(string bid)
        {
            RetailerManage db = new RetailerManage();
            db.UpdateBillFlag(bid);
            db.Closedb();
        }
    }
}