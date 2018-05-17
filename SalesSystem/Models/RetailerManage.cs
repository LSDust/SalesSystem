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
        public Purchaser(string id, string name,string tel)
        {
            this.purchaserId = id;
            this.purchaserName = name;
            this.phoneNumber = tel;
        }
    }

    public class Bill
    {
        public string billId { get; set; }
        public string retailerName { get; set; }
        public string commodityName { get; set; }
        public string purchaserName { get; set; }
        public double billQuantity { get; set; }
        public decimal price { get; set; }
        public DateTime paymentDate { get; set; }
        public bool flag { get; set; }

        public Bill(string billId, string cname,string pname,double quantity,decimal price,DateTime date)
        {
            this.billId = billId;
            this.commodityName = cname;
            this.purchaserName = pname;
            this.billQuantity = quantity;
            this.price = price;
            this.paymentDate = date;
        }
        public Bill(string billId, string cname, double quantity, decimal price, DateTime date)
        {
            this.billId = billId;
            this.commodityName = cname;
            this.billQuantity = quantity;
            this.price = price;
            this.paymentDate = date;
        }
        public Bill(string billId, string rname, string cname, string pname, double quantity, decimal price, DateTime date)
        {
            this.billId = billId;
            this.retailerName = rname;
            this.commodityName = cname;
            this.purchaserName = pname;
            this.billQuantity = quantity;
            this.price = price;
            this.paymentDate = date;
        }
        public Bill(string billId, string cname, string pname, double quantity, decimal price, DateTime date, bool flag)
        {
            this.billId = billId;
            this.commodityName = cname;
            this.purchaserName = pname;
            this.billQuantity = quantity;
            this.price = price;
            this.paymentDate = date;
            this.flag = flag;
        }
    }

    public class Manage
    {
        public string retailerId { get; set; }
        public string commodityName { get; set; }
        public double inventoryQuantity { get; set; }

        public Manage(string cname,double quantity)
        {
            this.commodityName = cname;
            this.inventoryQuantity = quantity;
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
            try
            {
                List<Commodity> table = new List<Commodity>();
                string sql = "select Commodity.commodity_id,commodity_name,prime_cost,selling_price,unit from Manage,Commodity where Manage.commodity_id = Commodity.commodity_id and retailer_id = '" + retailerId + "'";
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
            catch (SqlException ex)
            {
                return string.Empty;
            }
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

        public void UpdateCommodityImg(string id, string path)
        {
            String sql = "update Commodity set commodity_pic = '" + path + "' where commodity_id = '" + id + "'";
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

                String sql2 = "update Manage set inventory_quantity += " + purchaseQuantity + " where commodity_id = '" + commodityId + "' and retailer_id = '" + retailerId + "'";
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

        public String SelectPurchaser(string retailerId)
        {
            try
            {
                List<Purchaser> table = new List<Purchaser>();
                string sql = "select purchaser_id,purchaser_name from Purchaser where purchaser_id != '" + retailerId + "'";
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
                string sql = "insert into Bill(bill_id,retailer_id,commodity_id,purchaser_id,bill_quantity,payment_date,flag) values ('" + billId + "','" + retailerId + "','" + commodityId + "','" + purchaserId + "'," + billQuantity + ",GETDATE(),1)";
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

        //查看账单模块
        public String SelectBill(string retailerId)
        {
            try
            {
                List<Bill> table = new List<Bill>();
                string sql = "select bill_id,commodity_name,purchaser_name,bill_quantity,selling_price,payment_date,flag from Commodity,Purchaser,Bill where Bill.retailer_id='" + retailerId + "'and Commodity.commodity_id = Bill.commodity_id and Purchaser.purchaser_id = Bill.purchaser_id order by payment_date DESC";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Bill(dr.GetString(0),dr.GetString(1), dr.GetString(2), dr.GetDouble(3), dr.GetDecimal(4), dr.GetDateTime(5),dr.GetBoolean(6)));
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
        public String SelectBill2(string id)
        {
            try
            {
                List<Bill> table = new List<Bill>();
                string sql = "select bill_id,commodity_name,bill_quantity,selling_price,payment_date from Commodity,Purchaser,Bill where Purchaser.purchaser_id='" + id + "'and Commodity.commodity_id = Bill.commodity_id and Purchaser.purchaser_id = Bill.purchaser_id";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Bill(dr.GetString(0), dr.GetString(1), dr.GetDouble(2), dr.GetDecimal(3), dr.GetDateTime(4)));
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

        //查看库存模块
        public String SelectManage(string retailerId)
        {
            try
            {
                List<Manage> table = new List<Manage>();
                string sql = "select commodity_name,inventory_quantity from Manage,Commodity,Retailer where Retailer.retailer_id = '"+retailerId+"' and Retailer.retailer_id = Manage.retailer_id and Manage.commodity_id = Commodity.commodity_id";
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    table.Add(new Manage(dr.GetString(0), dr.GetDouble(1)));
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
        public bool InsertManageBill(string purchaserId, string commodityName, float billQuantity)
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
                DateTime date = DateTime.Now; ;
                string sql = "insert into Bill(bill_id,retailer_id,commodity_id,purchaser_id,bill_quantity,payment_date,flag) values ('" + billId + "','000','" + commodityId + "','" + purchaserId + "'," + billQuantity + ",GETDATE(),1)";
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

        //销售商信息修改
        public bool UpdateRetailer(string id,string name,string tel,string add)
        {
            bool result = true;
            try
            {
                String sql = "update Retailer set retailer_name = '" + name + "',phone_number = '" + tel + "',store_location = '" + add + "'where retailer_id = '" + id + "'";
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

        public bool InsertManageBill2(string retailerId, string commodityName, string purchaserId, float billQuantity)
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
                DateTime date = DateTime.Now; ;
                string sql = "insert into Bill(bill_id,retailer_id,commodity_id,purchaser_id,bill_quantity,payment_date,flag) values ('" + billId + "','" + retailerId + "','" + commodityId + "','" + purchaserId + "'," + billQuantity + ",GETDATE(),0)";
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
        public bool UpdateManage2(string rid, string cname, double quantity)
        {
            bool result = true;
            try
            {
                string sql1 = "select Commodity.commodity_id from Commodity,Manage where commodity_name = '" + cname + "' and Commodity.commodity_id = Manage.commodity_id";
                conn.Open();
                SqlCommand comm1 = new SqlCommand(sql1, conn);
                SqlDataReader dr = comm1.ExecuteReader();
                dr.Read();
                string commodityId = dr.GetString(0);
                conn.Close();

                String sql2 = "update Manage set inventory_quantity -= " + quantity + " where commodity_id = '" + commodityId + "' and retailer_id = '" + rid + "'";
                SqlCommand comm2 = new SqlCommand(sql2, conn);
                conn.Open();
                comm2.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                result = false;
            }
            return result;
        }

        public bool UpdateBillFlag(string bid)
        {
            bool result = true;
            try
            {
                String sql2 = "update Bill set flag = 1 where bill_id = '"+bid+"'";
                SqlCommand comm2 = new SqlCommand(sql2, conn);
                conn.Open();
                comm2.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                result = false;
            }
            return result;
        }
    }
}