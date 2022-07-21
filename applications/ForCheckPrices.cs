using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class ForCheckPrices
    {
        public string id = "";
        public string date = "";
        public string cp = "";
        public string objectt = "";
        public string price = "";
        public string priceSalary = "";
        public string priceDict = "";
        public string priceSalaryDict = "";

        public ForCheckPrices(string id, string date, string cp, string objectt,  string price, string priceSalary, string priceDict, string priceSalaryDict)
        {
            this.id = id;
            this.date = date;
            this.cp = cp;
            this.objectt = objectt;
            this.price = price;
            this.priceSalary = priceSalary;
            this.priceDict = priceDict;
            this.priceSalaryDict = priceSalaryDict;
        }
    }
}
