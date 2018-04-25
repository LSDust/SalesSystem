using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SalesSystem.Models
{
    public class SqlDb
    {
        public SqlConnection conn = new SqlConnection("Data Source=LENOVO-PC;Initial Catalog=SalesSystemDb;Integrated Security=True");

        public bool OpenDb()
        {
            bool result = true;
            try
            {
                conn.Open();
            }
            catch (SqlException ex)
            {
                result = false;
            }
            return result;
        }
        public void Closedb()
        {
            conn.Close();
        }

        //注册
        public void InertPurchaser(string tel,string pass)
        {
            Random ran = new Random();
            int key = ran.Next(0, 100);
            String sql = "insert into Purchaser(purchaser_id,purchaser_password,phone_number) values('" + key + "','" + pass + "','" + tel + "')";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();
        }

        //查找管理员记录，返回主键
        public string SelectAdmin(string adminName,string adminpass)
        {
            string id = string.Empty;
            string sql = "select admin_id from Admin where admin_name = '"+adminName+"'and admin_password = '"+adminpass+"'";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                id = dr.GetString(0);
            }
            conn.Close();
            return id;
        }

        //查找销售商记录,返回主键
        public string SelectRetailer(string retailerTel, string retailerPass)
        {
            string id = string.Empty;
            string sql = "select retailer_id from Retailer where phone_number = '" + retailerTel + "'and retailer_password = '" + retailerPass + "'";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                id = dr.GetString(0);
            }
            conn.Close();
            return id;
        }

        //查找业主记录,返回主键
        public string SelectPurchaser(string purchaserTel, string purchaserPass)
        {
            string id = string.Empty;
            string sql = "select purchaser_id from Purchaser where phone_number = '" + purchaserTel + "'and purchaser_password = '" + purchaserPass + "'";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                id = dr.GetString(0);
            }
            conn.Close();
            return id;
        }
    }
}