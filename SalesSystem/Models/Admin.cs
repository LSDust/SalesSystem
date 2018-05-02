using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SalesSystem.Models
{
    public class Retailer
    {
        public string retailerId { get; set; }
        public string retailerName { get; set; }
        public string retailerPic { get; set; }
        public string phoneNumber { get; set; }
        public string storeLocation { get; set; }
        public int sales { get; set; }

        public Retailer(string id,string name,string tel,string location)
        {
            this.retailerId = id;
            this.retailerName = name;
            this.phoneNumber = tel;
            this.storeLocation = location;
        }
        public Retailer(string name, string img, string add)
        {
            this.retailerName = name;
            this.retailerPic = img;
            this.storeLocation = add;
        }
    }

    public class Admin:SqlDb
    {
        public string SelectPurchaser()
        {
            try
            {
                List<Purchaser> table = new List<Purchaser>();
                string sql = "select purchaser_id,purchaser_name,phone_number from Purchaser";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Purchaser(dr.GetString(0), dr.GetString(1),dr.GetString(2)));
                }
                dr.Close();
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                conn.Close();
                return jsondata;
            }
            catch (SqlException ex)
            {
                string result = string.Empty;
                return result;
            }
        }
        public bool DeletePurchaser(string purchaserId)
        {
            bool result = true;
            try
            {
                String sql = "delete from Purchaser where purchaser_id = '" + purchaserId + "'";
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
        public String SelectRetailer()
        {
            try
            {
                List<Retailer> table = new List<Retailer>();
                string sql = "select retailer_id,retailer_name,phone_number,store_location from Retailer";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Retailer(dr.GetString(0), dr.GetString(1), dr.GetString(2),dr.GetString(3)));
                }
                dr.Close();
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                conn.Close();
                return jsondata;
            }
            catch (SqlException ex)
            {
                string result = string.Empty;
                return result;
            }
        }
        public bool InsertRetailer(string id,string name,string tel,string address)
        {
            bool result = true;
            try
            {
                String sql = "insert into Retailer (retailer_id,retailer_name,phone_number,store_location) values('" + id + "','" + name + "','" + tel + "','" + address + "')";
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
        public bool DeleteRetailer(string id)
        {
            bool result = true;
            try
            {
                String sql = "delete from Retailer where retailer_id = '" + id + "'";
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

        public String SelectAllBill()
        {
            try
            {
                List<Bill> table = new List<Bill>();
                string sql = "select bill_id,Retailer.retailer_name,commodity_name,purchaser_name,bill_quantity,selling_price,payment_date from Bill,Commodity,Retailer,Purchaser where Commodity.commodity_id = Bill.commodity_id and Retailer.retailer_id = Bill.retailer_id and Purchaser.purchaser_id = Bill.purchaser_id";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Bill(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetDouble(4),dr.GetDecimal(5),dr.GetDateTime(6)));
                }
                dr.Close();
                string jsondata = JsonConvert.SerializeObject(table); //序列化
                conn.Close();
                return jsondata;
            }
            catch (SqlException ex)
            {
                string result = string.Empty;
                return result;
            }
        }

        public bool SelectAdmin2(string id)
        {
            bool result = true;
            try
            {
                string sql = "select * from Admin where admin_id = '"+id+"'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.HasRows)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                dr.Close();
                conn.Close();
                
            }
            catch (SqlException ex)
            {
                return false;
            }
            return result;
        }
    }
}