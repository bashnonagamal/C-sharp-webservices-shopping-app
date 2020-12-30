using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class AddItem : Form
    {
        
        public AddItem()
        {

            InitializeComponent();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            AdminMain AdminMainForm = new AdminMain();
            AdminMainForm.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
       
            string Name = NameTxt.Text.ToString();
            int StockCount = int.Parse(StockCountTxt.Text.ToString());
            string Description = DescriptionTxt.Text.ToString();
            float Price = float.Parse(PriceTxt.Text.ToString());
            int categoryID = CategoryIDTxt.SelectedIndex + 1;






            /*int categoryIDIndex = CategoryIDTxt.SelectedIndex;
            string Description = DescriptionTxt.Text.ToString();
            string priceSTR = PriceTxt.Text.ToString();

            if (Name == "" || Description == "" || stockCountSTR == "" || priceSTR == "" || categoryIDIndex == -1)
            {
                MessageBox.Show("Add All Fields!");
                return;
            }
            else
            {
                float Price = float.Parse(priceSTR);
                int StockCount = int.Parse(stockCountSTR);
                string category = CategoryIDTxt.SelectedItem.ToString();
            }*/



            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            
            if (sc.AddItem(Name, StockCount, Description, Price, categoryID) == 1)
            {
                MessageBox.Show("Item Added Successfully");
            }
            else
            {
                MessageBox.Show("Try Again!");
            }

        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            string[][] categories = sc.getCategories();
            for (int i = 0; i < categories.Length ; i++)
            {
                CategoryIDTxt.Items.Add(categories[i][1]);
            }
            

        }

        private void NameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void StockCountTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void DescriptionTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
