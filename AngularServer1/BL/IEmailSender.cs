namespace AngularServer1.BL
{
   
        public interface IEmailSender
        {
            Task SendEmailAsync(string email, string subject, string message);
        }
    
}
