using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class UserMain : Form
    {
        public UserMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cart CartForm = new Cart();
            CartForm.Show();
            this.Hide();
        }

        private void UserMain_Load(object sender, EventArgs e)
        {
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            string[][] items = sc.getAllItems();
            for (int i = 0; i < items.Length; i++)
            {
                this.dataGridView1.Rows.Add(items[i][0], items[i][1], items[i][2], items[i][3], items[i][4], items[i][6]);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex].Name == "add_to_cart")
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null || dataGridView1.Rows[e.RowIndex].Cells[6].Value == null)
                {
                    MessageBox.Show("Please Choose a Quantity");
                    return;
                }
                // Get Selected product ID from the grid
                List<int> product = new List<int>();
                int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                int quanitiy = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                product.Add(productID);
                product.Add(quanitiy);

                // Check if the product is already in the cart
                foreach(List<int> prod in Program.cart)
                {
                    if (prod[0] == productID)
                    {
                        MessageBox.Show("Product is already in the cart");
                        return;
                    }
                }
                // Put in the global variable
                Program.cart.Add(product);
                MessageBox.Show("Product is added your cart with quantity " + quanitiy);


            }
        }
    }
}
