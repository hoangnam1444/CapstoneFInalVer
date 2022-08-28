using Contracts.HandleServices;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Net.Mail;

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

        public void SendEmail(string name, string toEmail, string code)
        {
            var client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "mtossender@gmail.com",
                    Password = "kfuycufzpgblqdcs"
                }
            };
            MailAddress FromEmail = new MailAddress("mtossender@gmail.com", "Mto-Manager");
            MailAddress ToEmail = new MailAddress(toEmail, "Mtp-New account");
            MailMessage Message = new MailMessage
            {
                From = FromEmail,
                Subject = "MTO active account code",
                Body = "Hi " + name + "\n Your active code is: " + code + " \n Regard \n MTO Manager"
            };
            Message.To.Add(ToEmail);
            client.Send(Message);
        }
    }
}
