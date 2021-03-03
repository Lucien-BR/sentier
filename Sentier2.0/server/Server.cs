using System;
using System.Collections.Generic;
using SuperData;

namespace Sentier2._0
{
    public class Server
    {
        private static CallDB DB = new CallDB();

        public void addData(Data d)
        {
            DB.addData(d);
        }

        public void addData(string somtinwong)
        {
            addData(new Data(somtinwong));
        }

        public List<Data> getData()
        {
            return DB.Datass;
        }
    }
}
