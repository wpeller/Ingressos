using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Applications.Dtos
{
    public class GetApplicationForEditOutput
    {
		public CreateOrEditApplicationDto Application { get; set; }


    }
}