using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class ForStandRegister
    {
        public string car = "";
        public string date = "";
        public string objectt = "";
        public string id = "";
        public string timeLoad = "";
        public string timeUnload = "";

        public ForStandRegister(string car, string date, string objectt, string id, string timeLoad, string timeUnload)
        {
            this.car = car;
            this.date = date;
            this.objectt = objectt;
            this.id = id;
            this.timeLoad = timeLoad;
            this.timeUnload = timeUnload;
        }
    }
}
