using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ONLINE_SHOPPING_SERVICE
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string Username = UsernameTxt.Text.ToString();
            string Password = PasswordTxt.Text.ToString();
            if (Username == "Admin")
            {
                if (Password == "123")
                {

                    MessageBox.Show("Login Successfully");
                    AdminMain AdminForm = new AdminMain();
                    AdminForm.Show();
                    this.Hide();

                    return;
                }
                else
                {
                    MessageBox.Show("Password is not correct!");
                }
            }
            else
            {
                ServiceReference1.ShoppingWebServiceClient sc = new ServiceReference1.ShoppingWebServiceClient();
                Program.loggedInUser = sc.login(Username, Password);
                if (Program.loggedInUser == 0)
                {
                    MessageBox.Show("Username or Password is not correct!");
                }
                else
                {
                    MessageBox.Show("Login Successfully");
                    UserMain UserMainForm = new UserMain();
                    UserMainForm.Show();
                    this.Hide();
                }
            }
        }

        private void SignupBtn_Click(object sender, EventArgs e)
        {
            Signup SignupFrom = new Signup();
            SignupFrom.Show();
            this.Hide();
        }
    }
}
