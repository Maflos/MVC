using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVC.Controller;

namespace MVC.View
{
    public partial class Admin : Form
    {
        private AdminController adminController;
        private User currentUser;

        public Admin(User user)
        {
            this.currentUser = user;
            InitializeComponent();
            textBox12.Text += user.Name;
        }

        internal void SetAdminController(AdminController adminController)
        {
            this.adminController = adminController;
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adminController.InsertUser(textBox2.Text, textBox3.Text, textBox4.Text,
                textBox5.Text))
            {
                MessageBox.Show("Inserted!");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }
        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adminController.DeleteUser(textBox1.Text))
            {
                MessageBox.Show("Deleted!");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adminController.UpdateUser(textBox1.Text, textBox2.Text, textBox3.Text,
                textBox4.Text, textBox5.Text))
            {
                MessageBox.Show("Updated!");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
            richTextBox1.Text += adminController.FindUser(textBox1.Text);
        }

        private void findAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
            richTextBox1.Text += adminController.FindAllUsers();
        }

        private void insertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (adminController.InsertBook(textBox7.Text, textBox8.Text, textBox9.Text,
               textBox10.Text, textBox11.Text))
            {
                MessageBox.Show("Inserted!");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (adminController.DeleteBook(textBox6.Text))
            {
                MessageBox.Show("Deleted!");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (adminController.UpdateBook(textBox6.Text, textBox7.Text, textBox8.Text,
                textBox9.Text, textBox10.Text, textBox11.Text))
            {
                MessageBox.Show("Updated!");
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
            richTextBox1.Text += adminController.FindBook(textBox6.Text);
        }

        private void findAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
            richTextBox1.Text += adminController.FindAllBooks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminController.GeneratePDF();
            MessageBox.Show("PDF created!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminController.GenerateCSV();
            MessageBox.Show("CSV created!");
        }
    }
}
