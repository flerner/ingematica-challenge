using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Ejercicio_Web.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string senderEmail, string subject, string message)
        {
            try
            {
                Console.WriteLine(senderEmail);
               
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587; 
                string smtpUsername = "felix.lerner001@gmail.com";
                string smtpPassword = "cbfnqzwqbszslywm";
                string recipientEmail = "felix.lerner001@gmail.com"; 

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true; 

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(recipientEmail); 
                    mailMessage.To.Add(recipientEmail); 
                    mailMessage.Subject = subject;
                    mailMessage.Body = $"From: {senderEmail}\n\n{message}";
                    Console.WriteLine(mailMessage);
                    smtpClient.Send(mailMessage);
                }

               
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email");
                return Page();
            }
        
        }

    }
}
