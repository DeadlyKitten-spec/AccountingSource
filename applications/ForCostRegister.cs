﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class ForCostRegister
    {
        public string date = "";
        public string cp = "";
        public string objectt = "";
        public string id = "";
        public string price = "";
        public string priceSalary = "";

        public ForCostRegister(string date, string cp, string objectt, string id, string price, string priceSalary)
        {
            this.date = date;
            this.cp = cp;
            this.objectt = objectt;
            this.id = id;
            this.price = price;
            this.priceSalary = priceSalary;
        }
    }
}
