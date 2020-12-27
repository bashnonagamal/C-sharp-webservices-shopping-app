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
        public List<List<string>> getAllItems()
        {
            List<List<string>> Products = new List<List<string>>();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True;MultipleActiveResultSets=true");//Change Data Source
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Product", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                List<string> Product = new List<string>();
                Product.Add(Convert.ToString(reader["Product_ID"]));
                Product.Add(Convert.ToString(reader["Product_Name"]));
                Product.Add(Convert.ToString(reader["Stock_Quantity"]));
                Product.Add(Convert.ToString(reader["Description"]));
                Product.Add(Convert.ToString(reader["price"]));
                string cat_id = Convert.ToString(reader["Category_ID"]);
                Product.Add(cat_id);
                

                // Get Category Name
                SqlCommand cmd2 = new SqlCommand("select * from Category where Category_ID=@cat", conn);
                cmd2.Parameters.AddWithValue("@cat", cat_id);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while(reader2.Read())
                {
                    Product.Add(Convert.ToString(reader2["Category_name"]));
                }

                Products.Add(Product);
            }
            return Products;
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
        public int login(string userName, string password)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where User_name=@A and User_password=@B", conn);
            SqlParameter user = new SqlParameter("@A", userName);
            SqlParameter pass = new SqlParameter("@B", password);
            cmd.Parameters.Add(user);
            cmd.Parameters.Add(pass);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int userID = Convert.ToInt32(reader["User_ID"]);
                conn.Close();
                return userID;
            }
            conn.Close();
            return 0;

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
        public List<string> getItemById(int itemId)
        {
            List<string> Product = new List<string>();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True;MultipleActiveResultSets=true");//Change Data Source
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Product where Product_ID=@A", conn);
            SqlParameter item_id = new SqlParameter("@A", itemId);
            cmd.Parameters.Add(item_id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Product.Add(Convert.ToString(reader["Product_ID"]));
                Product.Add(Convert.ToString(reader["Product_Name"]));
                Product.Add(Convert.ToString(reader["Stock_Quantity"]));
                Product.Add(Convert.ToString(reader["Description"]));
                Product.Add(Convert.ToString(reader["price"]));
                string cat_id = Convert.ToString(reader["Category_ID"]);
                Product.Add(cat_id);


                // Get Category Name
                SqlCommand cmd2 = new SqlCommand("select * from Category where Category_ID=@cat", conn);
                cmd2.Parameters.AddWithValue("@cat", cat_id);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    Product.Add(Convert.ToString(reader2["Category_name"]));
                }

                return Product;
            }
            return Product;
        }
        public int order(int userID, List<List<int>> cart, double totalPrice)
        {
            //Create cart for user
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-Q2BTOU1;Initial Catalog=onlineStoreDatabase;Integrated Security=True"); //Change Data Source
            conn.Open();
            int cartID=0;
            SqlCommand comm = new SqlCommand("insert into Cart (User_id, Total_price) output INSERTED.Cart_id values (@A,@B)", conn);
            SqlParameter userP = new SqlParameter("@A", userID);
            SqlParameter priceP = new SqlParameter("@B", totalPrice);

            comm.Parameters.Add(userP);
            comm.Parameters.Add(priceP);

            cartID=(int)comm.ExecuteScalar();

            //inserting products into product cart
            for(int i = 0; i < cart.Count; i++)
            {
                comm = new SqlCommand("insert into Product_Cart (Cart_ID, Product_ID, Quantity) values (@A,@B,@C)",conn);
                SqlParameter cartP = new SqlParameter("@A", cartID);
                SqlParameter productP = new SqlParameter("@B", cart[i][0]);
                SqlParameter quantityP = new SqlParameter("@c", cart[i][1]);
                comm.Parameters.Add(cartP);
                comm.Parameters.Add(productP);
                comm.Parameters.Add(quantityP);
                comm.ExecuteNonQuery();
            }
            conn.Close();
            
            return 1;
        }

    }
}
