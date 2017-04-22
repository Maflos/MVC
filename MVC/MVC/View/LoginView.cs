using MVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC.View
{
    public partial class LoginView : Form
    {
        private LoginController loginController;

        public LoginView()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        public void SetLoginController(LoginController loginController)
        {
            this.loginController = loginController;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!loginController.Login(textBox1.Text, textBox2.Text))
            {
                MessageBox.Show("Wrong input!");
            }
        }
    }
}
