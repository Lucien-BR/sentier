using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using System.Security.Cryptography.HashAlgorithm;

namespace Sentier2._0
{
    public class userDB
    {
        private static readonly HttpClient client = new HttpClient();

        // get APPID, constante: https://www.lessentiersdelestrie.qc.ca/users/universglid.php?APPID=20210203

        // Obtient un clef SHA1
        public string getNewLogId() { // victoire, ca fonctionne!
            string uri = "https://www.lessentiersdelestrie.qc.ca/users/universglid.php?APPID=20210203"; // le app ID est constant pour notre app
            var response = client.GetStringAsync(uri).Result;
            //Console.WriteLine("[SERVER:] getNewLogId() : " + response);
            //response = response.Substring(0, response.Length - 5);
            //Console.WriteLine("[SERVER:] getNewLogId() - 5 : " + response);
            return response;
        }

        public string validateUserCreds(string logid, string userID, string userPassword) {

            string pass = gimmeHash(userID + gimmeHash(userPassword) + logid);

            string uri = "https://www.lessentiersdelestrie.qc.ca/users/universval.php?"+
                "&PASSWORD="+pass+
                "&USERNAME="+userID+
                "&LOGID="+logid;
            
            //SLAP MES VALEURS DANS PHP ICI
            //var data = new StringContent(Encoding.UTF8, "text/plain");

            //byte[] byteArray = Encoding.UTF8.GetBytes(php);

            // Construire la request
            //string response = client.PostAsync(uri, data).Result.Content.ReadAsStringAsync().Result;
            return client.GetStringAsync(uri).Result;

        }

        public string validateUserCredsHash(string hLogid, string hUserID, string hUserPassword)
        {
            string uri = "https://www.lessentiersdelestrie.qc.ca/users/universval.php?" +
                "&PASSWORD=" + hUserPassword +
                "&USERNAME=" + hUserID +
                "&LOGID=" + hLogid;

            //SLAP MES VALEURS DANS PHP ICI
            //var data = new StringContent(Encoding.UTF8, "text/plain");

            //byte[] byteArray = Encoding.UTF8.GetBytes(php);

            // Construire la request
            //string response = client.PostAsync(uri, data).Result.Content.ReadAsStringAsync().Result;
            return client.GetStringAsync(uri).Result;

        }


        static string gimmeHash(string str)
        {
            var sha1 = new System.Security.Cryptography.SHA1Managed();
            var plaintextBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = sha1.ComputeHash(plaintextBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }

            return sb.ToString();
        }

    }
}
