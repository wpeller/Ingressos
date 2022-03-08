﻿using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Fgv.Acad.Financeiro.Configuration;

namespace Fgv.Acad.Financeiro.Tests.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(FinanceiroTestModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
