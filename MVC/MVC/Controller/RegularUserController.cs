using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Controller
{
    class RegularUserController
    {
        private RegularUser regUserView;
        private List<Book> bookList;
        private List<Selling> sellingList;

        public RegularUserController(RegularUser regUserView)
        {
            this.regUserView = regUserView;
            regUserView.SetRegUserController(this);
            bookList = new List<Book>();
            sellingList = new List<Selling>();
        }

        internal string FilterTitle(string title)
        {
            BookService bs = new BookService("book.xml", "user.xml", "selling.xml");
            return bs.FilterTitles(title);
        }

        internal string FilterAuthor(string author)
        {
            BookService bs = new BookService("book.xml", "user.xml", "selling.xml");
            return bs.FilterAuthors(author);
        }

        internal string FilterGenre(string genre)
        {
            BookService bs = new BookService("book.xml", "user.xml", "selling.xml");
            return bs.FilterGenres(genre);
        }

        internal string ShowAll()
        {
            string display = "";
            BookDataGateway bdg = new BookDataGateway("book.xml");

            foreach (Book book in bdg.FindAll())
            {
                display += book.ToString() + "\n";
            }

            return display;
        }

        internal bool AddBook(User user, string id, string qty)
        {
            bool found = false;
            BookDataGateway bdg = new BookDataGateway("book.xml");
            int bookID;
            int quantity;

            try
            {
                bookID = int.Parse(id);
                quantity = int.Parse(qty);

                foreach (Book b in bookList)
                {
                    if(b.Id == bookID)
                    {
                        found = true;
                        quantity += b.Quantity;

                        if(!bdg.Find(bookID).SellBook(quantity))
                        {
                            return false;
                        }
                        else
                        {
                            sellingList.Add(new Selling(0, user.Id, bookID, quantity - b.Quantity, 
                                (quantity - b.Quantity) * b.Price));
                            b.Quantity = quantity;
                        }                       
                    }
                }

                if(!found)
                {
                    if (bdg.Find(bookID) == null || !bdg.Find(bookID).SellBook(quantity))
                    {
                        return false;
                    }
                    else
                    {
                        Book b = bdg.Find(bookID);
                        b.Quantity = quantity;
                        sellingList.Add(new Selling(0, user.Id, bookID, quantity,
                            quantity * b.Price));
                        bookList.Add(b);
                    }
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        internal string ShowProgress()
        {
            string display = "";
            BookDataGateway bdg = new BookDataGateway("book.xml");

            foreach (Book book in bookList)
            {
                display += book.Title + " - " + book.Author + " -> " +
                    " qty: " + book.Quantity + " total price: " + book.Quantity * book.Price + "\n";
            }

            return display;
        }

        internal void FinishOrder()
        {
            BookService bs = new BookService("book.xml", "user.xml", "selling.xml");
            bs.SellBooks(bookList, sellingList);
        }

        internal string ViewTransactions()
        {
            BookService bs = new BookService("book.xml", "user.xml", "selling.xml");
            return bs.ToString();
        }
    }
}
