﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ArchProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShoppingWebService" in both code and config file together.
    [ServiceContract]
    public interface IShoppingWebService
    {
        [OperationContract]
        int AddItem(string productName, int stockQuentity, string description, float price, int categoryID);

        [OperationContract]
        List<List<string>> getAllItems();

        [OperationContract]
        int editItem(int userID, string productName, int stockQuentity, string description, float price, int categoryID);

        [OperationContract]
        int deleteItem(int productID);

        [OperationContract]
        int Register(string userName, string userEmail, string userPassword, string userPhoneNumber, string userAddress);

        [OperationContract]
        int login(string userName, string pass);

        [OperationContract]
        List<string> getItemById(int itemId);

        [OperationContract]
        int addCategory(string categoryName);

        [OperationContract]
        int order(int userID, List<List<int>> cart, double totalPrice, string payment_method);

        [OperationContract]
        List<List<string>> getCategories();



    }
}
