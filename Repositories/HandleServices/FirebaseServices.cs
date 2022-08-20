using Contracts.HandleServices;
using Entities.DTOs;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.HandleServices
{
    public class FirebaseServices : IFirebaseService
    {
        private readonly IConfiguration _configuration;

        public FirebaseServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FirebaseInfo> GetInforFromToken(string firebaseToken)
        {
            try
            {
                FirebaseToken decodeToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
                return new FirebaseInfo
                {
                    Email = decodeToken.Claims.GetValueOrDefault("email").ToString(),
                    Image = decodeToken.Claims.GetValueOrDefault("picture").ToString(),
                    UserName = decodeToken.Claims.GetValueOrDefault("name").ToString()
                };
            }
            catch (Exception ex)
            {
                return new FirebaseInfo { Email = "Get email from token error: " + ex.Message };
            }
        }

        public void InitFirebase()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                string path = _configuration["Firebase:CridentialPath"];
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path),
                    ServiceAccountId = _configuration["FirebaseApp:ServiceAccountId"]
                });
            }
        }
    }
}
