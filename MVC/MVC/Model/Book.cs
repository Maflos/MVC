using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Book(int id, string title, string author, 
            string genre, int qty, double price)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Quantity = qty;
            Price = price;
        }

        public override string ToString()
        {
            return Id + ") " + Title + " - " + Author + " qty: " + Quantity + " price: " + Price;
        }

        public bool SellBook(int quantity)
        {
            bool canSell = false;

            if (quantity <= Quantity)
            {
                canSell = true;
            }

           return canSell;
        }
    }
}
