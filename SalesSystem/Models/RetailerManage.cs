using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

    public class Purchaser
    {
        public string purchaserId { get; set; }
        public string purchaserName { get; set; }
        public string purchaserPassword { get; set; }
        public string phoneNumber { get; set; }
        public string purchaserPic { get; set; }

        public Purchaser(string id,string name)
        {
            this.purchaserId = id;
            this.purchaserName = name;
        }
    }

    public class RetailerManage : SqlDb
    {
        private string GetRandomString(int length)
        {
            const string key = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            if (length < 1)
                return string.Empty;

            Random rnd = new Random();
            byte[] buffer = new byte[8];

            ulong bit = 31;
            ulong result = 0;
            int index = 0;
            StringBuilder sb = new StringBuilder((length / 5 + 1) * 5);

            while (sb.Length < length)
            {
                rnd.NextBytes(buffer);

                buffer[5] = buffer[6] = buffer[7] = 0x00;
                result = BitConverter.ToUInt64(buffer, 0);

                while (result > 0 && sb.Length < length)
                {
                    index = (int)(bit & result);
                    sb.Append(key[index]);
                    result = result >> 5;
                }
            }
            return sb.ToString();
        }

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

        public bool InertCommodity(string id, string commodityName, string commodityPic, float primeCost, float sellingPrice, string unit)
        {
            //Random ran = new Random();
            //int key = ran.Next(0, 100);
            //commodityPic String类型，img
            bool result = true;
            try
            {
                String sql = "insert into Commodity values('" + id + "','" + commodityName + "','" + commodityPic + "','" + primeCost + "','" + sellingPrice + "','" + unit + "')";
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

        public void UpdateCommodity(string commodityId,string commodityName, decimal primeCost, decimal sellingPrice, string unit)
        {
            String sql = "update Commodity set commodity_name='" + commodityName + "',prime_cost='" + primeCost + "',selling_price='" + sellingPrice + "',unit='" + unit + "' where commodity_id = '" + commodityId + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();
        }

        public bool InserManage(string commodityId,string retailerId,float purchaseQuantity)
        {
            bool result = true;
            try
            {
                String sql = "insert into Manage values('" + commodityId + "','" + retailerId + "','" + purchaseQuantity + "')";
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
        public bool DeleteCommodity(string commodityId)
        {
            bool result = true;
            try
            {
                String sql = "delete from Commodity where commodity_id = '" + commodityId + "'";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                result = false;
            }
            return result;
        }
        public bool DeleteManage(string commodityId)
        {
            bool result = true;
            try
            {
                String sql = "delete from Manage where commodity_id = '"+commodityId+"'";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                result = false;
            }
            return result;
        }
        public bool UpdateManage(string retailerId, string commodityName, float purchaseQuantity)
        {
            bool result = true;
            try
            {
                string sql1 = "select Commodity.commodity_id from Commodity,Manage where commodity_name = '"+commodityName+"' and Commodity.commodity_id = Manage.commodity_id";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql1, conn);
                SqlDataReader dr = comm1.ExecuteReader();
                dr.Read();
                string commodityId = dr.GetString(0);
                conn.Close();

                String sql2 = "update Manage set purchase_quantity += "+purchaseQuantity+" where commodity_id = '"+commodityId+"' and retailer_id = '"+retailerId+"'";
                SqlCommand comm2 = new SqlCommand(sql2, conn);
                conn.Open();
                comm2.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                result = false;
            }
            return result;
        }

        public String SelectPurchaser()
        {
            try
            {
                List<Purchaser> table = new List<Purchaser>();
                string sql = "select purchaser_id,purchaser_name from Purchaser";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Purchaser(dr.GetString(0), dr.GetString(1)));
                }
                dr.Close();
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                conn.Close();
                return jsondata;
            }
            catch(SqlException ex)
            {
                string result = string.Empty;
                return result;
            }
        }

        public bool InsertBill(string retailerId, string commodityName, string purchaserId, float billQuantity)
        {
            bool result = true;
            try
            {
                string sql1 = "select Commodity.commodity_id from Commodity,Manage where commodity_name = '" + commodityName + "' and Commodity.commodity_id = Manage.commodity_id";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql1, conn);
                SqlDataReader dr = comm1.ExecuteReader();
                dr.Read();
                string commodityId = dr.GetString(0);
                conn.Close();

                string billId = GetRandomString(5);
                DateTime date = DateTime.Now;;
                string sql = "insert into Bill(bill_id,retailer_id,commodity_id,purchaser_id,bill_quantity,payment_date) values ('" + billId + "','" + retailerId + "','" + commodityId + "','" + purchaserId + "'," + billQuantity + ",GETDATE())";
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
    }
}