using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Selling
    {
        public int SellingID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public Selling(int sellingID, int userID, int bookID, 
            int quantity, double totalPrice)
        {
            SellingID = sellingID;
            UserID = userID;
            BookID = bookID;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        public override string ToString()
        {
            return " qty: " + Quantity + " totalPrice " + TotalPrice;
        }
    }
}
