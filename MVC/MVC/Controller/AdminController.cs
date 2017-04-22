using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MVC.Model;

namespace MVC.Controller
{
    public class AdminController
    {
        private Admin adminView;

        public AdminController(Admin adminView)
        {
            this.adminView = adminView;
            adminView.SetAdminController(this);
        }

        internal bool InsertUser(string name, string email, string password, string admin)
        {
            UserDataGateway udg = new UserDataGateway("user.xml");
            bool isAdmin;

            if (name == "" || email == "" || password == "")
            {
                return false;
            }

            try
            {
                isAdmin = bool.Parse(admin);

                User usr = new User(0, name, email, password, isAdmin);
                udg.Insert(usr);
            }
            catch
            {
                return false;
            }
           
            return true;
        }

        internal bool DeleteUser(string id)
        {
            UserDataGateway udg = new UserDataGateway("user.xml");
            int userID;

            try
            {
                userID = int.Parse(id);
                udg.Delete(userID);
            }
            catch
            {
                return false;
            }

            return true;
        }

        internal bool UpdateUser(string id, string name, string email, string password, string admin)
        {
            UserDataGateway udg = new UserDataGateway("user.xml");
            bool isAdmin;
            int userID;

            if (name == "" || email == "" || password == "")
            {
                return false;
            }

            try
            {
                userID = int.Parse(id);
                isAdmin = bool.Parse(admin);

                User usr = new User(userID, name, email, password, isAdmin);
                udg.Update(userID, usr);
            }
            catch
            {
                return false;
            }

            return true;
        }

        internal string FindUser(string id)
        {
            UserDataGateway udg = new UserDataGateway("user.xml");
            string display = "Not found!";
            int userID = 0;

            try
            {
                userID = int.Parse(id);
                display = udg.Find(userID).ToString();
            }
            catch
            {
                return "Not found!";
            }
        
            return display;
        }

        internal string FindAllUsers()
        {
            string display = "";
            UserDataGateway udg = new UserDataGateway("user.xml");

            foreach (User usr in udg.FindAll())
            {
                display += usr.ToString() + "\n";
            }

            return display;
        }

        internal bool InsertBook(string title, string author, string genre, string quntity, string price)
        {
            BookDataGateway bdg = new BookDataGateway("book.xml");
            int qty;
            double pr;

            if (title == "" || author == "" || genre == "")
            {
                return false;
            }

            try
            {
                qty = int.Parse(quntity);
                pr = double.Parse(price);

                bdg.Insert(new Book(0, title, author, genre, qty, pr));
            }
            catch
            {
                return false;
            }

            return true;
        }

        internal bool DeleteBook(string id)
        {
            BookDataGateway bdg = new BookDataGateway("book.xml");
            int bookID;

            try
            {
                bookID = int.Parse(id);
                bdg.Delete(bookID);
            }
            catch
            {
                return false;
            }

            return true;
        }
     
        internal bool UpdateBook(string id, string title, string author, string genre, string quntity, string price)
        {
            BookDataGateway bdg = new BookDataGateway("book.xml");
            int bookID;
            int qty;
            double pr;

            if (title == "" || author == "" || genre == "")
            {
                return false;
            }

            try
            {
                bookID = int.Parse(id);
                qty = int.Parse(quntity);
                pr = double.Parse(price);

                bdg.Update(bookID, new Book(bookID, title, author, genre, qty, pr));
            }
            catch
            {
                return false;
            }

            return true;
        }

        internal string FindBook(string id)
        {
            BookDataGateway bdg = new BookDataGateway("book.xml");
            string display = "Not found!";
            int bookID = 0;

            try
            {
                bookID = int.Parse(id);
                display = bdg.Find(bookID).ToString();
            }
            catch
            {
                return "Not found!";
            }

            return display;
        }

        internal string FindAllBooks()
        {
            string display = "";
            BookDataGateway bdg = new BookDataGateway("book.xml");

            foreach (Book book in bdg.FindAll())
            {
                display += book.ToString() + "\n";
            }

            return display;
        }

        internal void GeneratePDF()
        {
            BookDataGateway bdg = new BookDataGateway("book.xml");
            string reportString = "";
            ReportFactory repFactory = new ReportFactory();
            Report rep = repFactory.GetReport("pdf");

            foreach(Book b in bdg.FindAll())
            {
                if(b.Quantity == 0)
                {
                    reportString += b.ToString() + "\n";
                }
            }

            rep.GenerateReport(reportString);
        }

        internal void GenerateCSV()
        {
            BookDataGateway bdg = new BookDataGateway("book.xml");
            var csv = new StringBuilder();

            foreach (Book b in bdg.FindAll())
            {
                if (b.Quantity == 0)
                {
                    var first = b.Id.ToString();
                    var second = b.Title;
                    var third = b.Author;
                    var fourth = b.Genre;
                    var fifth = b.Quantity.ToString();
                    var sixth = b.Price.ToString();

                    var newLine = string.Format("{0},{1},{2},{3},{4},{5}", first, second,
                        third, fourth, fifth, sixth);
                    csv.AppendLine(newLine);
                }
            }
            File.WriteAllText("report.csv", csv.ToString());
        }
    }
}
