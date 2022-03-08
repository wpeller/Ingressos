using Abp.Notifications;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}