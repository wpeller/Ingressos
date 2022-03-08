using System;
using System.Linq;
using Abp.AspNetZeroCore;
using Castle.MicroKernel;

namespace Fgv.Acad.Financeiro
{
    public class CastleSelectorHandler : IHandlerSelector
    {
        public bool HasOpinionAbout(string key, Type service)
        {
            return service == typeof(AbpAspNetZeroCoreModule);
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
                return handlers.FirstOrDefault(x => x.ComponentModel.Implementation == typeof(FgvFinanceiroCustomCoreModule));
        }

    }
}