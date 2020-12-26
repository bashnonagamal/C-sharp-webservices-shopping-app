using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace ArchProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShoppingWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ShoppingWebService.svc or ShoppingWebService.svc.cs at the Solution Explorer and start debugging.
    public class ShoppingWebService : IShoppingWebService
    {
        public int AddItem(string productName, int stockQuentity, string description, float price, int categoryID)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");//Change Data Source
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Product (Product_Name,Stock_Quantity,Description,price,Category_ID) values (@A,@B,@C,@D,@E)", conn);
            SqlParameter pn = new SqlParameter("@A", productName);
            SqlParameter SQ = new SqlParameter("@B", stockQuentity);
            SqlParameter Desc = new SqlParameter("@C", description);
            SqlParameter Price = new SqlParameter("@D", price);
            SqlParameter catId = new SqlParameter("@E", categoryID);
            cmd.Parameters.Add(pn);
            cmd.Parameters.Add(SQ);
            cmd.Parameters.Add(Desc);
            cmd.Parameters.Add(Price);
            cmd.Parameters.Add(catId);
            cmd.ExecuteNonQuery();
            conn.Close();
            return 1;
        }
        public void getAllItems()
        {

        }
        public void editItem()
        {

        }
        public int deleteItem(int ProductID)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Product where product_id=@ProductID", con);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            con.Close();
            return result;

        }
        public int Register(string userName, string userEmail, string userPassword, string userPhoneNumber, string userAddress)
        {
            //Check whether user info already exists
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");//Change Data Source
            conn.Open();
            SqlCommand cmd = new SqlCommand("select User_name from Users where User_name=@A", conn);
            SqlParameter userP = new SqlParameter("@A", userName);
            cmd.Parameters.Add(userP);
            SqlDataReader reader = cmd.ExecuteReader();
            //if it already exists return 0;
            if (reader.Read())
            {
                reader.Close();
                conn.Close();
                return 0;
            }
            //if it doesn't then add user info to database
            else
            {
                reader.Close();
                cmd = new SqlCommand("insert into Users (User_name,Email,User_password,Phone_number,Address) values (@A,@B,@C,@D,@E)", conn);
                SqlParameter userP1 = new SqlParameter("@A", userName);
                SqlParameter emailP = new SqlParameter("@B", userEmail);
                SqlParameter passwordP = new SqlParameter("@C", userPassword);
                SqlParameter phoneP = new SqlParameter("@D", userPhoneNumber);
                SqlParameter addressP = new SqlParameter("@E", userAddress);
                cmd.Parameters.Add(userP1);
                cmd.Parameters.Add(emailP);
                cmd.Parameters.Add(passwordP);
                cmd.Parameters.Add(phoneP);
                cmd.Parameters.Add(addressP);
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }
        }
        public void login()
        {
            //List<string> usr = new List<string>();
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");
            //con.Open();
            //SqlCommand cmd = new SqlCommand("select * user where User_name=@UserName and User_password=@Password", con);
            //cmd.Parameters.AddWithValue("@UserName", userName);
            //cmd.Parameters.AddWithValue("@Password", pass);

            //SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.Read() == true)
            //{

            //    usr.Add(dr[0].ToString());
            //    usr.Add(dr[2].ToString());
            //}
            //con.Close();
        }
        public int addCategory(string categoryName)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");//Change Data Source
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Category (Category_name) values (@A)", conn);
            SqlParameter pn = new SqlParameter("@A", categoryName);
            cmd.Parameters.Add(pn);
            cmd.ExecuteNonQuery();
            conn.Close();
            return 1;
        }
        public List<List<string>> getCategories()
        {
            
            List<List<string>> Categories = new List<List<string>>();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");//Change Data Source
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Category", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                List<string> Category = new List<string>();
                Category.Add(Convert.ToString(reader["Category_ID"]));
                Category.Add(Convert.ToString(reader["Category_name"]));
                Categories.Add(Category);
            }
            return Categories;
   
        }
        public void getItemById()
        {

        }

        public int order()
        {
            return 0;
        }

    }
}
