using CoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
namespace life_is_feudal_your_own.utils
{
    public static class SendEmail
    {
        private static string _smtpAddress = "smtpout.secureserver.net";
        private static int _portNumber = 465;
        private static string _emailFrom = "caridia-blackoak@fosterbros-productions.com";
        private static string _password = "B@con123";
        private static string _emailTo = "jeremyfoster07@yahoo.com";
        public static void SendEmailMessage(string subject, string body)
        {
            try
            {
                using (var mail = new MailMessage())
                    using(var db = new LifeIsFeudalDb())
                {
                    _emailTo = db.Configurations.FirstOrDefault(x => x.Key == "Email").Value;
                    mail.From = new MailAddress(_emailFrom);
                    mail.To.Add(_emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential(_emailFrom, _password);
                        //smtp.Host = "localhost";
                        //smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                

            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}