using AngularServer1.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace AngularServer1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public TestController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task TestAction()
        {
            //await _emailSender.SendEmailAsync("chani540067@gmail.com", "subject",
            //             $"Enter email body here");
            // פרטי התחברות לשרת SMTP
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587; // ניתן לשנות את הפורט לפי ההגדרות של הספק
            string smtpUsername = "p0556706@gmail.com";
            string smtpPassword = "Pp0556706588!";

            // כתובת הדואר האלקטרוני של הזוכה
            string winnerEmail = "chani54006@gmail.com";

            // יצירת הודעת מייל
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(winnerEmail);
            mailMessage.Subject = "Congratulations! You're the Winner!";
            mailMessage.Body = "Dear Winner, \n\nCongratulations on winning the lottery!";

            // הגדרת ה- SmtpClient
            SmtpClient smtpClient = new SmtpClient(smtpServer);
            smtpClient.Port = smtpPort;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true; // ניתן לשנות לפי הגדרות הספק

            try
            {
                // שליחת המייל
                smtpClient.Send(mailMessage);
                Console.WriteLine("Mail sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending mail: " + ex.Message);
            }
        }
    }
}
