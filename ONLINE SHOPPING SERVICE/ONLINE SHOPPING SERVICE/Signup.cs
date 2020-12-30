using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Login LoginFrom = new Login();
            LoginFrom.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string Name = NameTxt.Text.ToString();
            string Email = EmailTxt.Text.ToString();
            string Password = PasswordTxt.Text.ToString();
            string PhoneNum = PhoneNumTxt.Text.ToString();
            string Address = AddressTxt.Text.ToString();


            ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
            if (sc.Register(Name, Email, Password, PhoneNum, Address) == 1)
            {
                MessageBox.Show("Account Added Successfully");
                UserMain UserMainForm = new UserMain();
                UserMainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Try Again!");
            }

        }
    }
}
