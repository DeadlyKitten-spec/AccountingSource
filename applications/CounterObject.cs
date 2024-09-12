using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications
{
    class CounterObject
    {
        public string name = "-1";
        public string status = "-1";
        public string age = "";

        public CounterObject(string name, string status, string age)
        {
            this.name = name;
            this.status = status;
            this.age = age;
        }
    }
}
