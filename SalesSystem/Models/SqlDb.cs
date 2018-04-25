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

        //查找管理员记录
        public bool SelectAdmin(string adminName,string adminpass)
        {
            string sql = "select * from Admin where admin_name = '"+adminName+"'and admin_password = '"+adminpass+"'";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            bool result = dr.HasRows;
            conn.Close();
            return result;
        }

        //查找销售商记录
        public bool SelectRetailer(string retailerTel, string retailerPass)
        {
            string sql = "select * from Retailer where phone_number = '" + retailerTel + "'and retailer_password = '" + retailerPass + "'";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            bool result = dr.HasRows;
            conn.Close();
            return result;
        }

        //查找业主记录
        public bool SelectPurchaser(string purchaserTel, string purchaserPass)
        {
            string sql = "select * from Purchaser where phone_number = '" + purchaserTel + "'and purchaser_password = '" + purchaserPass + "'";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            dr.Read();
            bool result = dr.HasRows;
            conn.Close();
            return result;
        }
    }
}