using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SalesSystem.Models
{
    public class RetailerManage : SqlDb
    {

        public void SelectCommodity(string retailerId)
        {
            string sql = "select * from Retailer where retailer_id = '" + retailerId + "'";     //有误
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                //断点
            }
            conn.Close();
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