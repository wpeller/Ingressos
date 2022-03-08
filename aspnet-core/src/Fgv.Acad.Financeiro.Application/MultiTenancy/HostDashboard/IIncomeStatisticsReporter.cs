using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.MultiTenancy.HostDashboard.Dto;

namespace Fgv.Acad.Financeiro.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}