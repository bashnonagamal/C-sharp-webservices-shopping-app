using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class Cart : Form
    {
        float totalPrice = 0;
        public Cart()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            UserMain UserMainForm = new UserMain();
            UserMainForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get Payment Method
            string selectedPaymentMethod = comboBox1.SelectedItem.ToString();

            // Check if the cart is empty
            if (totalPrice == 0 || selectedPaymentMethod == null)
            {
                MessageBox.Show("Please Add products to cart to proceed");
                return;
            }
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();

            // Check if the quantity does not exit
            foreach (List<int> pro in Program.cart)
            {
                string[] itm = sc.getItemById(pro[0]);
                if (Convert.ToInt32(itm[2]) < pro[1])
                {
                    MessageBox.Show("Quantity is not in the stock");
                    return;
                }

            }

            // Create Order
            int[][] arrays = Program.cart.Select(a => a.ToArray()).ToArray();
            
            if (sc.order(Program.loggedInUser, arrays, totalPrice, selectedPaymentMethod) == 1)
            {
                MessageBox.Show("Ordered Successfully");
                Program.cart.Clear();
                dataGridView1.Rows.Clear();
                Cart_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Try Again!");
            }
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            int total = 0;
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            foreach(List<int> product in Program.cart)
            {
                string[] actual_product = sc.getItemById(product[0]);
                int sub_total = Convert.ToInt32(actual_product[4]) * Convert.ToInt32(product[1]);
                this.dataGridView1.Rows.Add(product[0],actual_product[1], product[1], actual_product[4] , sub_total);
                total = total + sub_total;
            }
            label2.Text = total.ToString();
            totalPrice = total;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            {
                List<int> willBeDeleted = new List<int>();
                willBeDeleted.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
                willBeDeleted.Add(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value));

                for(int i = 0;i < Program.cart.Count; i++)
                {
                    if (Program.cart[i][0] == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value)){
                        Program.cart.RemoveAt(i);
                        dataGridView1.Rows.Clear();
                        Cart_Load(sender, e);
                        return;
                    }
                }
                
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int newQentity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            foreach (List<int> product in Program.cart)
            {
                if (product[0] == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value))
                {
                    product[1] = newQentity;
                    dataGridView1.Rows.Clear();
                    Cart_Load(sender, e);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
