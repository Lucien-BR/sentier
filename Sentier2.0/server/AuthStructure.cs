using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperAuthStructure
{
    public class AuthStructure
    {
        public AuthStructure() { }

        public string userID { get; set;  }
        public string hashPassword { get; set; }
        public string logID { get; set; }
        public string deviceID { get; set; } // TODO: JE ME SOUVIENS PLUS A QUOI CA SERT, MAIS JE SAIS QUE C'EST VITALE


    }
}
