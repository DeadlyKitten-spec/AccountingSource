using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class ForCostRegister
    {
        public string date = "";
        public string objectt = "";
        public string id = "";
        public string price = "";

        public ForCostRegister(string date, string objectt, string id, string price)
        {
            this.date = date;
            this.objectt = objectt;
            this.id = id;
            this.price = price;
        }
    }
}
