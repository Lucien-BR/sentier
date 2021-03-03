using System;

namespace SuperData
{
    public class Data
    {
        public Data() { }

        public Data(Data d)
        {
            this.data = d.data;
        }

        public Data(string sumtinwong)
        {
            this.data = sumtinwong;
        }

        public string data { get; set; }
        public string logid { get; set; }
        public string token { get; set; }

    }
}
