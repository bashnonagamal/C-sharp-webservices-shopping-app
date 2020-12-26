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
        public void AddItem()
        {

        }
        public void getAllItems()
        {

        }
        public void editItem()
        {

        }
        public void deleteItem()
        {

        }
        public int Register(string userName, string userEmail, string userPassword, string userPhoneNumber, string userAddress)
        {
            //Check whether user info already exists
            SqlConnection conn = new SqlConnection("Data Source=ANDREW;Initial Catalog=onlineStoreDatabase;Integrated Security=True");//Change Data Source
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

        }
        public void getItemById()
        {

        }
    }
}
