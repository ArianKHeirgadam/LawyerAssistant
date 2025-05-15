using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.Objects;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.EmailServices
{
    public class EmailService : IEmailService, IScoped
    {
        IOptions<AppConfig> options;
        public EmailService(IOptions<AppConfig> _options)
        {
            options = _options;
        }
        //********************************************************************************************************************
        public async Task<SysResult> Send(EmailSettingObject _setting)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(options.Value.emailAddress),
                    Subject = _setting.Subject,
                    IsBodyHtml = _setting.IsBodyHtml,
                    Body = _setting.Message
                };
                mail.To.Add(_setting.Receiver);


                var smtp = new SmtpClient
                {
                    Host = options.Value.emailHost,
                    EnableSsl = true,
                    Port = options.Value.emailPort,
                    UseDefaultCredentials = false
                };

                smtp.UseDefaultCredentials = true;
                var credential = new NetworkCredential(options.Value.emailUsername, options.Value.emailPassword);
                smtp.Credentials = credential;
                await smtp.SendMailAsync(mail);
                mail.Dispose();
                smtp.Dispose();
                return new SysResult()
                {
                    IsSuccess = true,
                    Message = "ایمیل با موفقیت ارسال گردید",
                    Value = true
                };
            }
            catch (Exception e)
            {
                return new SysResult()
                {
                    IsSuccess = false,
                    Message = "ارسال ایمیل با خطا مواجه شد",
                };
            }
        }

    }
}
