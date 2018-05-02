using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

            public CommodityInfo(string cname,string cpic,decimal price,string unit)
            {
                this.commodityName = cname;
                this.commodityPic = cpic;
                this.commodityPrice = price;
                this.commodityUnit = unit;
            }
        }

        public String SelectCommodityInfo(string retailerId)
        {
            try
            {
                List<CommodityInfo> table = new List<CommodityInfo>();
                string sql = "select commodity_name,commodity_pic,selling_price,unit from Commodity,Manage where Manage.retailer_id = '"+retailerId+"' and Commodity.commodity_id = Manage.commodity_id";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    string a = dr.GetString(0);
                    string b = dr.GetString(1);
                    decimal c = dr.GetDecimal(2);
                    string d = dr.GetString(3);
                    table.Add(new CommodityInfo(dr.GetString(0), dr.GetString(1),dr.GetDecimal(2),dr.GetString(3)));
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
                string sql = "select retailer_name,retailer_pic,store_location from Retailer where retailer_id != '000'";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Retailer(dr.GetString(0), dr.GetString(1), dr.GetString(2)));
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

        //public string UploadImg(byte[] fileBytes, int id)
        //{
        //    try
        //    {
        //        string filePath = Server.MapPath(".") + "\\EmpImage\\" + id + ".jpg"; //图片要保存的路径及文件名
        //        using (MemoryStream memoryStream = new MemoryStream(fileBytes))//1.定义并实例化一个内存流，以存放提交上来的字节数组。
        //        {
        //            using (FileStream fileUpload = new FileStream(filePath, FileMode.Create))//2.定义实际文件对象，保存上载的文件。
        //            {
        //                memoryStream.WriteTo(fileUpload); ///3.把内存流里的数据写入物理文件  
        //            }
        //        }
        //        //GetSqlExcuteNonQuery是我写好的一个执行command的ExcuteNonQuery()方法
        //        GetSqlExcuteNonQuery(string.Format("insert into EmpImg values('{0}','{1}')", id, filePath)); //将该图片保存的文件路径写入数据库
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

    }
}