using SuperData;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sentier2._0
{
    public class CallDB
    {
        public CallDB()
        {
            Datass = new List<Data>() { };
            Datass.Add(new Data("JE SUIS UN POGO"));
        }

        public List<Data> Datass { get; set; }

        public void addData(Data d)
        {
            this.Datass.Add(d);
        }

        public void addData(string somtinwong)
        {
            addData(new Data(somtinwong));
        }
    }
}
