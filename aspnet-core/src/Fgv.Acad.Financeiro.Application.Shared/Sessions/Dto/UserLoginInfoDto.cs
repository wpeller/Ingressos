using Abp.Application.Services.Dto;

namespace Fgv.Acad.Financeiro.Sessions.Dto
{
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string ProfilePictureId { get; set; }

        public string Siga2UserName { get; set; }
        public string Siga2Name { get; set; }
	}
}
