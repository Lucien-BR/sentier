using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperData;
using SuperAuthStructure;
using System.IO;
using System.Diagnostics;

namespace Sentier2._0.Controllers
{
    [ApiController]
    public class ServerController : ControllerBase
    {
        // lock / sync sur le singleton
        private static Server Ser = new Server();
        private static ContentDeliveryNetwork CDN = new ContentDeliveryNetwork();
        private FB fb = FB.getFirebaseInstance(); // return singleton
        private static userDB udb = new userDB();
        private static consoleUtility cu = new consoleUtility();
        StackTrace stackTrace = new StackTrace();

        [HttpGet]
        [Route("getData")]
        public IEnumerable<Data> getData()
        {
            cu.logTime("/getData");
            return Enumerable.Range(0, Ser.getData().Count()).Select(index => Ser.getData()[index]);
        }

        [HttpGet]
        [Route("getSHA1")]// DEPRECATED
        public string getSHA1()
        {
            cu.logTime("/getSHA1");
            return udb.getNewLogId();
        }

        [HttpPost] // DEPRECATED
        [Route("tryPHP")]
        public string tryPHP(string USERNAME, string PASSWORD) {
            cu.logTime("/tryPHP");
            string logid = udb.getNewLogId();
            //Console.WriteLine(logid);
            return udb.validateUserCreds(logid,USERNAME,PASSWORD);// retournerais 1 si bon, mais on s'attend a 0 car non encrypter
        }

        [HttpPost]
        [Route("sendUID")]// TODO: Rename 
        public Data sendUID(Data d)
        {
            cu.logTime("/sendUID");
            //string myUID = "Hello there, General Kenobi";
            Data D = new Data();
            D.logid = udb.getNewLogId();
            D.token = fb.CreateCustomToken(d.data);
            return D;
        }

        [HttpGet]
        [Route("getToken")] // DEPRECATED
        public Data getToken() {
            cu.logTime("/getToken");
            string myUID = "Hello there, General Kenobi";
            return new Data(fb.CreateCustomToken(myUID));
            //return Enumerable.Range(0, fb.data.Count()).Select(index => fb.data[index]);
        }

        [HttpPost]
        [Route("authUser")]
        public string authUser(AuthStructure AS) {
            cu.logTime("/authUser");
            string result = udb.validateUserCredsHash(AS.logID, AS.userID, AS.hashPassword);
            // 1 == connue et valide, 2 == comme 1 mais admin en plus // 0 == bonne syntaxe mais id + pw ne marchent pas
            string token; // ajouter la cause d'erreur
            string state;
            if (result == "1" | result == "2")
            {
                token = fb.CreateCustomToken(AS.deviceID);
                state = "[SERVER:] Authentification succeded";
                Console.WriteLine(state);
                return token;
            }
            else {
                state = "[SERVER:] Fail to authenticate user: " + "Unknown password/identifier pair";
                Console.WriteLine(state);
                return state;
            }
            // TODO: ameliorer mes conditions et gestion d'erreurs
        }

        // TODO: Bouger cette fonction avec son utility fonction dans un autre fichier
        [HttpGet] // DOESNT WORK ON DEPLOY
        [Route("getContent")]
        public async Task<IActionResult> Download([FromQuery] string guid)
        {
            cu.logTime("/getContent");
            guid = "sequoia.jpg";
            // TODO: Validation si le fichier existe ou n'est pas corompu

            MyFile file = await CDN.GetFile(Path.GetFullPath(guid));
            return File(file.bytes, file.Ext, file.Name);
        }
        private static string GetMimeTypes(string ext) // ctrl+c/ctrl+v
        {
            switch (ext)
            {
                case ".txt": return "text/plain";
                case ".csv": return "text/csv";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                default: return "application/octet-stream";
            }
        }

    }
}
