using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class BookService
    {
        private BookDataGateway bookGateway;
        private UserDataGateway userGateway;
        private SellingDataGateway sellingGateway;

        public BookService(string bookPath, string userPath, string sellingPath)
        {
            bookGateway = new BookDataGateway(bookPath);
            userGateway = new UserDataGateway(userPath);
            sellingGateway = new SellingDataGateway(sellingPath);
        }

        public string FilterTitles(string title)
        {
            string display = "";

            foreach(Book b in bookGateway.FindAll())
            {
                if (b.Title == title)
                {
                    display += b.ToString() + "\n";
                }
            }

            return display;
        }

        public string FilterAuthors(string author)
        {
            string display = "";

            foreach (Book b in bookGateway.FindAll())
            {
                if (b.Author == author)
                {
                    display += b.ToString() + "\n";
                }
            }

            return display;
        }

        public string FilterGenres(string genre)
        {
            string display = "";

            foreach (Book b in bookGateway.FindAll())
            {
                if (b.Genre == genre)
                {
                    display += b.ToString() + "\n";
                }
            }

            return display;
        }

        public List<Transaction> GetSellings ()
        {
            List<Transaction> result = new List<Transaction>();
            List<Selling> sellingList = sellingGateway.FindAll();
            int id = 0;
            int i = 0;

            if (sellingList.Count > 0)
            {
                id = sellingList[0].SellingID;
                List<Selling> initialList = new List<Selling>();
                result.Add(new Transaction(id, sellingList[0].UserID, initialList));

                foreach (Selling selling in sellingList)
                {
                    if (selling.SellingID == id)
                    {
                        result[i].SellingList.Add(selling);
                    }
                    else
                    {
                        i++;
                        id = selling.SellingID;
                        List<Selling> aList = new List<Selling>();
                        aList.Add(selling);
                        result.Add(new Transaction(id, selling.UserID, aList));
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            string display = "";

            foreach (Transaction t in GetSellings())
            {
                display += "Purchase ID: " + t.Id + " - Seller: " + userGateway.Find(t.Seller).Name + "\n";

                foreach (Selling s in t.SellingList)
                {
                    display += "Book title: " + bookGateway.Find(s.BookID).Title + " qty: " + s.Quantity +
                        " price: " + s.TotalPrice + "\n";
                }

                display += "Total order price: " + t.CalculateTotalPrice() + "\n\n";
            }

            return display;
        }

        public void SellBooks(List<Book> bookList, List<Selling> order)
        {
            int id = GetSellings()[GetSellings().Count - 1].Id + 1;

            foreach (Book b in bookList)
            {
                Book book = bookGateway.Find(b.Id);
                book.Quantity -= b.Quantity;
                bookGateway.Update(b.Id, book);
            }
               
            foreach(Selling s in order)
            {
                s.SellingID = id;
                sellingGateway.Insert(s);
            }
        }

        public void deleteOrder(int id)
        {
            Transaction trans = null;
      
            foreach(Transaction t in GetSellings())
            {
                if(id == t.Id)
                {
                    trans = t;
                }
            }

            for(int i = 0; i < trans.SellingList.Count; i++)
            {
                sellingGateway.Delete(id);
            }
        }

    }
}
