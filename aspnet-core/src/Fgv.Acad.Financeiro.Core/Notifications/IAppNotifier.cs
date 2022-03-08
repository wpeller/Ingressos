using System;
using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using Fgv.Acad.Financeiro.Authorization.Users;
using Fgv.Acad.Financeiro.MultiTenancy;

namespace Fgv.Acad.Financeiro.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task GdprDataPrepared(UserIdentifier user, Guid binaryObjectId);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
