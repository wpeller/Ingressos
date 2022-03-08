﻿using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.UiCustomization;
using Fgv.Acad.Financeiro.Web.UiCustomization.Metronic;

namespace Fgv.Acad.Financeiro.Web.UiCustomization
{
    public class UiThemeCustomizerFactory : IUiThemeCustomizerFactory
    {
        private readonly ISettingManager _settingManager;
        private readonly IServiceProvider _serviceProvider;

        public UiThemeCustomizerFactory(
            ISettingManager settingManager,
            IServiceProvider serviceProvider)
        {
            _settingManager = settingManager;
            _serviceProvider = serviceProvider;
        }

        public async Task<IUiCustomizer> GetCurrentUiCustomizer()
        {
            var theme = await _settingManager.GetSettingValueAsync(AppSettings.UiManagement.Theme);
            return GetUiCustomizerInternal(theme);
        }

        public IUiCustomizer GetUiCustomizer(string theme)
        {
            return GetUiCustomizerInternal(theme);
        }

        private IUiCustomizer GetUiCustomizerInternal(string theme)
        {
            if (theme.Equals(AppConsts.Theme8, StringComparison.InvariantCultureIgnoreCase))
            {
                return _serviceProvider.GetService<Theme8UiCustomizer>();
            }

            if (theme.Equals(AppConsts.Theme2, StringComparison.InvariantCultureIgnoreCase))
            {
                return _serviceProvider.GetService<Theme2UiCustomizer>();
            }

            if (theme.Equals(AppConsts.Theme11, StringComparison.InvariantCultureIgnoreCase))
            {
                return _serviceProvider.GetService<Theme11UiCustomizer>();
            }

            return _serviceProvider.GetService<ThemeDefaultUiCustomizer>();
        }
    }
}