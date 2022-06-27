﻿using Contracts.HandleServices;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Repositories.HandleServices
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetAccountId()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type == "AccountId").Value);
        }

        public int GetAccountRole()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type == "Role").Value);
        }

        //public async Task SendEmail(string name, string toEmail, string code)
        //{
        //    var client = new SmtpClient()
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential()
        //        {
        //            UserName = "mtossender@gmail.com",
        //            Password = "capstone2022"
        //        }
        //    };
        //    MailAddress FromEmail = new MailAddress("mtossender@gmail.com", "Mto-Manager");
        //    MailAddress ToEmail = new MailAddress(toEmail, "Mtp-New account");
        //    MailMessage Message = new MailMessage
        //    {
        //        From = FromEmail,
        //        Subject = "MTO active account code",
        //        Body = string.Format($"Hi {0} \n Your active code is {1} \n Regard \n MTO Manager", name, code)
        //    };

        //    //Email.DefaultSender = sender;

        //    //var email = await Email
        //    //    .From("mtomanager@gmail.com")
        //    //    .To(toEmail)
        //    //    .Subject("MTO active account code")
        //    //    .Body(string.Format($"Hi {0} \n Your active code is {1} \n Regard \n MTO Manager", name, code))
        //    //    .SendAsync();
        //}
    }
}
