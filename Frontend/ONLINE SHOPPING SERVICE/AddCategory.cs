using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            AdminMain adminMainForm = new AdminMain();
            adminMainForm.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string Name = CategoryTxt.Text.ToString();

            if (Name == "")
            {
                MessageBox.Show("Add New Category!");
                return;
            }



            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            if (sc.addCategory(Name) == 1)
            {
                MessageBox.Show("Category Added Successfully");
            }
            else
            {
                MessageBox.Show("Try Again!");
            }
        }
    }
}
