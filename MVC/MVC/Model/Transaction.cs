using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Seller { get; set; }
        public List<Selling> SellingList { get; set; }
        
        public Transaction(int id, int seller, List<Selling> sellingList)
        {
            Id = id;
            Seller = seller;
            SellingList = sellingList;
        }

        public double CalculateTotalPrice()
        {
            double price = 0.0;

            foreach (Selling s in SellingList)
            {
                price += s.TotalPrice;
            }

            return price;
        }
    }
}
