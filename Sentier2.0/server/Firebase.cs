using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperData;
namespace Sentier2._0
{
    public sealed class FB
    {
        private static FB instance = null;


        private GoogleCredential Credential { get; set; }
        private FirebaseApp fbInstance { get; set; }
        private FirebaseAuth fbAuth { get; set; }
        
        
        private FB() {
            try
            {
                fbInstance = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("adminKey.json")
                }) ;

                fbAuth = FirebaseAuth.GetAuth(fbInstance);

            } catch {

            }
        }

        public static FB getFirebaseInstance() {
            if (instance == null) {
                instance = new FB();
            }
            return instance;
        }



        public string CreateCustomToken(string uid) {
            //var temp = FirebaseAuth.GetAuth(Instance);
            return fbAuth.CreateCustomTokenAsync(uid).GetAwaiter().GetResult();
            //return await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
        }

    }
}
