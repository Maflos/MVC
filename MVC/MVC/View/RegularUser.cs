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
    public partial class RegularUser : Form
    {
        private RegularUserController regularUserController;
        private User currentUser;

        public RegularUser(User user)
        {
            this.currentUser = user;
            InitializeComponent();
            textBox1.Text += user.Name;
        }

        internal void SetRegUserController(RegularUserController regularUserController)
        {
            this.regularUserController = regularUserController;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (regularUserController.FilterTitle(textBox2.Text)  == "")
            {
                MessageBox.Show("Nothing found!");
            }
            else
            {
                richTextBox1.ResetText();
                richTextBox1.Text += regularUserController.FilterTitle(textBox2.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (regularUserController.FilterAuthor(textBox3.Text) == "")
            {
                MessageBox.Show("Nothing found!");
            }
            else
            {
                richTextBox1.ResetText();
                richTextBox1.Text += regularUserController.FilterAuthor(textBox3.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (regularUserController.FilterGenre(textBox4.Text) == "")
            {
                MessageBox.Show("Nothing found!");
            }
            else
            {
                richTextBox1.ResetText();
                richTextBox1.Text += regularUserController.FilterGenre(textBox4.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(regularUserController.ShowAll() == "")
            {
                MessageBox.Show("Nothing found!");
            }
            else
            {
                richTextBox1.ResetText();
                richTextBox1.Text += regularUserController.ShowAll();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!regularUserController.AddBook(currentUser, textBox5.Text, textBox6.Text))
            {
                MessageBox.Show("Wrong input!");
            }
            else
            {
                richTextBox2.ResetText();
                richTextBox2.Text += regularUserController.ShowProgress();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            regularUserController.FinishOrder();
            MessageBox.Show("Books sold!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (regularUserController.ViewTransactions() == "")
            {
                MessageBox.Show("Nothing found!");
            }
            else
            {
                richTextBox2.ResetText();
                richTextBox2.Text += regularUserController.ViewTransactions();
            }
        }
    }
}
