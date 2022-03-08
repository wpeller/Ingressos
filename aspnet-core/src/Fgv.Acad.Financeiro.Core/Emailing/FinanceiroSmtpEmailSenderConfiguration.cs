using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Security;

namespace Fgv.Acad.Financeiro.Emailing
{
    public class FinanceiroSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
    {
        public FinanceiroSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {

        }

        public override string Password => SimpleStringCipher.Instance.Decrypt(GetNotEmptySettingValue(EmailSettingNames.Smtp.Password));
    }
}