using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Fgv.Acad.Financeiro.Editions.Dto;

namespace Fgv.Acad.Financeiro.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}