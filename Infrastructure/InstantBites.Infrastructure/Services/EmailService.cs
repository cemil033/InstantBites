using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc.Routing;
using InstantBites.Domain.Services;
using Microsoft.Extensions.Options;

namespace InstantBites.Infrastructure.Services
{
    public class EmailService:IEmailService
    {        
        private readonly IHttpContextAccessor _request;      
        private readonly SendGridOption _sendGridOption;
        public EmailService(IHttpContextAccessor request, IOptions<SendGridOption> sendGridOption)
        {
            _request = request;
            _sendGridOption = sendGridOption.Value;
        }

        public async Task<HttpStatusCode> Send(string email,bool confmail,IUrlHelper url)
        {            
            var apiKey = _sendGridOption.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_sendGridOption.EmailFrom, _sendGridOption.NameFrom);
            var to = new EmailAddress($"{email}", "");
            var subject = ""; 
            var plainTextContent="";
            var htmlContent="";
            if (confmail)
            {
                subject= "Mail Confirmation";
                plainTextContent = $"Confirm Accunt :  {url.Action("Confirm", "Account", new { email = email }, _request.HttpContext.Request.Scheme)}";
                htmlContent = $"<strong>Confirm Accunt :  {url.Action("Confirm", "Account", new { email = email }, _request.HttpContext.Request.Scheme)}</strong>";
            }
            else
            {
                subject = "Reset Password";
                plainTextContent = $"Reset Password :  {url.Action("ResetPassword", "Account", new { email = email }, _request.HttpContext.Request.Scheme)}";
                htmlContent = $"<strong>Reset Password :  {url.Action("ResetPassword", "Account", new { email = email }, _request.HttpContext.Request.Scheme)}</strong>";
            }
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> SendMessage(string email,string message, IUrlHelper url)
        {
            var apiKey = _sendGridOption.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_sendGridOption.EmailFrom, _sendGridOption.NameFrom);
            var to = new EmailAddress($"{email}", "");
            var subject = "Subscription";
            var plainTextContent = $"{message}";
            var htmlContent = $"<strong>{message}</strong>";            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.StatusCode;
        }
    }
}
