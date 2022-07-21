using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class ForPrice
    {

        public string counterparty;
        public string objectt;
        public string price;
        public string pricecount;
        public string pricebuy;
        public string pricebuycount;
        public string age;

        public ForPrice(string counterparty, string objectt, string price, string pricecount, string pricebuy, string pricebuycount, string age)
        {
            this.counterparty = counterparty;
            this.objectt = objectt;
            this.price = price;
            this.pricecount = pricecount;
            this.pricebuy = pricebuy;
            this.pricebuycount = pricebuycount;
            this.age = age;
        }
    }
}
