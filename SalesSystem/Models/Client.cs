using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SalesSystem.Models
{
    public class Client : SqlDb
    {
        public class CommodityInfo
        {
            public string commodityName { get; set; }
            public string commodityPic { get; set; }
            public decimal commodityPrice { get; set; }
            public string commodityUnit { get; set; }
            public int frequency { get; set; }
            public double quantity { get; set; }

            public CommodityInfo(string cname,string cpic,decimal price,string unit,double quantity)
            {
                this.commodityName = cname;
                this.commodityPic = cpic;
                this.commodityPrice = price;
                this.commodityUnit = unit;
                this.quantity = quantity;
            }
        }

        public class BillTwo
        {
            public string billId { get; set; }
            public string retailerName { get; set; }
            public string commodityName { get; set; }
            public string pic { get; set; }
            public double billQuantity { get; set; }
            public decimal price { get; set; }
            public string unit { get; set; }
            public string paymentDate { get; set; }
            public bool flag { get; set; }

            public BillTwo(string id,string rname,string cname,string pic,double quantity,decimal price,string unit,string date,bool flag){
                this.billId = id;
                this.retailerName = rname;
                this.commodityName = cname;
                this.pic = pic;
                this.billQuantity = quantity;
                this.price = price;
                this.unit = unit;
                this.paymentDate = date;
                this.flag = flag;
            }
        }

        public String SelectCommodityInfo(string retailerId)
        {
            try
            {
                List<CommodityInfo> table = new List<CommodityInfo>();
                string sql = "select commodity_name,commodity_pic,selling_price,unit,inventory_quantity from Commodity,Manage where Manage.retailer_id = '" + retailerId + "' and Commodity.commodity_id = Manage.commodity_id";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    string a = dr.GetString(0);
                    string b = dr.GetString(1);
                    decimal c = dr.GetDecimal(2);
                    string d = dr.GetString(3);
                    table.Add(new CommodityInfo(dr.GetString(0), dr.GetString(1),dr.GetDecimal(2),dr.GetString(3),dr.GetDouble(4)));
                }
                dr.Close();
                conn.Close();
                for (int i = 0; i < table.Count(); i++)
                {
                    string sql2 = "select COUNT(*) from Bill,Commodity where commodity_name = '"+table[i].commodityName+"' and retailer_id = '"+retailerId+"' and Commodity.commodity_id = Bill.commodity_id";
                    conn.Open();
                    SqlCommand comm2 = new SqlCommand(sql2, conn);
                    int a = Convert.ToInt32(comm2.ExecuteScalar());
                    table[i].frequency = Convert.ToInt32(comm2.ExecuteScalar());
                    conn.Close();
                }
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                return jsondata;
            }
            catch (SqlException ex)
            {
                return string.Empty;
            }
        }

        public string SelectRetailerInfo()
        {
            try
            {
                List<Retailer> table = new List<Retailer>();
                string sql = "select retailer_id,retailer_name,retailer_pic,phone_number, store_location from Retailer where retailer_id != '000'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Retailer(dr.GetString(0), dr.GetString(1), dr.GetString(2),dr.GetString(3),dr.GetString(4)));
                }
                dr.Close();
                conn.Close();
                for (int i = 0; i < table.Count(); i++)
                {
                    string sql2 = "select COUNT(*) from Bill,Retailer where Bill.retailer_id = Retailer.retailer_id and retailer_name = '"+table[i].retailerName+"'";
                    conn.Open();
                    SqlCommand comm2 = new SqlCommand(sql2, conn);
                    int a = Convert.ToInt32(comm2.ExecuteScalar());
                    table[i].sales = Convert.ToInt32(comm2.ExecuteScalar());
                    conn.Close();
                }
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                return jsondata;
            }
            catch (SqlException ex)
            {
                return string.Empty;
            }
        }

        public bool UpdatePurchaser(string id, string name, string tel)
        {
            bool result = true;
            try
            {
                String sql = "update Purchaser set purchaser_name = '"+name+"',phone_number = '"+tel+"' where purchaser_id = '"+id+"'";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                result = false;
            }
            return result;
        }
        public string SelectThisRetailer(string id)
        {
            try
            {
                string sql = "select retailer_id,retailer_name,retailer_pic,phone_number,store_location from Retailer where retailer_id = '"+id+"'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                dr.Read();                
                Retailer retailer = new Retailer(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                dr.Close();
                string jsondata = JsonConvert.SerializeObject(retailer); //序列化
                conn.Close();
                return jsondata;                
            }
            catch(SqlException ex)
            {
                return string.Empty;
            }
        }

        public String SelectBill(string id)
        {
            try
            {
                List<BillTwo> table = new List<BillTwo>();
                string sql = "select bill_id,retailer_name,commodity_name,commodity_pic,bill_quantity,selling_price,unit,payment_date,flag from Bill,Commodity,Retailer where purchaser_id = '" + id + "' and Bill.commodity_id = Commodity.commodity_id and Retailer.retailer_id = Bill.retailer_id order by payment_date DESC";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new BillTwo(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetDouble(4), dr.GetDecimal(5), dr.GetString(6), dr.GetDateTime(7).GetDateTimeFormats('f')[0].ToString(), dr.GetBoolean(8)));
                }
                dr.Close();
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                conn.Close();
                return jsondata;
            }
            catch (SqlException ex)
            {
                return string.Empty;
            }
        }
    }
}