using System;
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
        void getAllItems();

        [OperationContract]
        void editItem();

        [OperationContract]
        int deleteItem(int productID);

        [OperationContract]
        int Register(string userName, string userEmail, string userPassword, string userPhoneNumber, string userAddress);

        [OperationContract]
        void login();

        [OperationContract]
        void getItemById();

        [OperationContract]
        int addCategory(string categoryName);

        [OperationContract]
        int order();

        [OperationContract]
        List<List<string>> getCategories();



    }
}
