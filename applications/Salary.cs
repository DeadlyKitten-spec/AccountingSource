using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class Salary
    {
        public string id;
        public string car;
        public string objectt;
        public int countTrip;
        public double price;
        public double sum;
        public double tax;
        public double wotax;
        public double cash;

        public Salary(string id, string car, string objectt, int countTrip, double price, double sum, double tax, double wotax, double cash)
        {
            this.id = id;
            this.car = car;
            this.objectt = objectt;
            this.countTrip = countTrip;
            this.price = price;
            this.sum = sum;
            this.tax = tax;
            this.wotax = wotax;
            this.cash = cash;
        }
    }
}
