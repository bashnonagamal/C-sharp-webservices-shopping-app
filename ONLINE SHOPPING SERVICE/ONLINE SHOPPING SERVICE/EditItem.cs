using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class EditItem : Form
    {
        string[] product;
        public EditItem(int productID)
        {
            // Get the product when the window is opened
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            product = sc.getItemById(productID);
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminMain adminMainFrom = new AdminMain();
            adminMainFrom.Show();
            this.Hide();
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            // Get all Categories and put them in 
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            string[][] categories = sc.getCategories();
            for (int i = 0; i < categories.Length; i++)
            {
                CategoryIDTxt.Items.Add(categories[i][1]);
            }

            // Fill the text with the data
            NameTxt.Text = product[1];
            StockCountTxt.Text = product[2];
            DescriptionTxt.Text = product[3];
            PriceTxt.Text = product[4];
            CategoryIDTxt.Text = product[6];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Name = NameTxt.Text.ToString();
            int StockCount = int.Parse(StockCountTxt.Text.ToString());
            string Description = DescriptionTxt.Text.ToString();
            float Price = float.Parse(PriceTxt.Text.ToString());
            int categoryID = CategoryIDTxt.SelectedIndex + 1;

            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();

            if (sc.editItem(Convert.ToInt32(product[0]) ,Name, StockCount, Description, Price, categoryID) == 1)
            {
                MessageBox.Show("Item Edited Successfully");
                AdminMain adminMainForm = new AdminMain();
                adminMainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Try Again!");
            }
        }
    }
}
