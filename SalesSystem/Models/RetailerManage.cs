using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SalesSystem.Models
{
    public class Commodity
    {
        public string commodityId { get; set; }
        public string commodityName { get; set; }
        public decimal primeCost { get; set; }
        public decimal sellingPrice { get; set; }
        public string unit { get; set; }

        public Commodity(string id, string name, decimal cost, decimal price, string unit)
        {
            this.commodityId = id;
            this.commodityName = name;
            this.primeCost = cost;
            this.sellingPrice = price;
            this.unit = unit;
        }
    }

    public class RetailerManage : SqlDb
    {
        public String SelectCommodity(string retailerId)
        {
            List<Commodity> table = new List<Commodity>();
            string sql = "select Commodity.commodity_id,commodity_name,prime_cost,selling_price,unit from Manage,Commodity where Manage.commodity_id = Commodity.commodity_id and retailer_id = '"+retailerId+"'"; 
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                table.Add(new Commodity(dr.GetString(0), dr.GetString(1), dr.GetDecimal(2), dr.GetDecimal(3), dr.GetString(4)));
            }
            dr.Close();
            string jsondata = JsonConvert.SerializeObject(table); //序列化
            conn.Close();
            return jsondata;
        }

        public void InertCommodity(string commodityName, string commodityPic, float primeCost, float sellingPrice, string unit)
        {
            Random ran = new Random();
            int key = ran.Next(0, 100);
            //commodityPic String类型，img
            String sql = "insert into Commodity values('" + key + "','" + commodityName + "','" + commodityPic + "','" + primeCost + "','" + sellingPrice + "','" + unit + "')";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();
        }
    }
}