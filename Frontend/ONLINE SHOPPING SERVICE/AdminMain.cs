using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItem AddItemForm = new AddItem();
            AddItemForm.Show();
            this.Hide();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            //This Code Must be Tested Correctly..
            //Test it while integrationnnnnnnnnnnnnnnnn
            /*if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {
                this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            }*/



            //MASH 3AREF
            //int ProductID = this.dataGridView1.SelectedRows[0].//mash 3aref
            //MASH 3AREF
            //We will test it while integration

            /*
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            if (sc.deleteItem(ProductID) == 1)
            {
                MessageBox.Show("Item Deleted Successfully");
            }
            else
            {
                MessageBox.Show("Try Again!");
            }*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCategory AddCategoryForm = new AddCategory();
            AddCategoryForm.Show();
            this.Hide();
        }

        private void AdminMain_Load(object sender, EventArgs e)
        {
            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            string[][] items = sc.getAllItems();
            for(int i = 0; i < items.Length; i++)
            {
                this.dataGridView1.Rows.Add(items[i][0], items[i][1], items[i][2], items[i][3], items[i][4], items[i][6]);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // If Delete Button is pressed
            if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            {
                int toBeDeleted = 0;
                toBeDeleted = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
                if (sc.deleteItem(toBeDeleted) == 1)
                {
                    dataGridView1.Rows.Clear();
                    AdminMain_Load(sender, e);
                    MessageBox.Show("Product Deleted Successfully!");
                    return;
                }
                

            }
            // If edit button is pressed
            if (dataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                EditItem editItemForm = new EditItem(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
                editItemForm.Show();
                this.Hide();


            }
        }
    }
}
