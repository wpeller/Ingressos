using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Abp.Domain.Entities;
using Abp.Localization;

namespace Fgv.Acad.Financeiro.Navigations
{
    [Table("Navigations")]
    public class Navigation : Entity<Guid>
    {
        private string _localizableDisplayName;

        [MaxLength(120)]
        public virtual string Name { get; set; }

        [MaxLength(120)]
        public virtual string LocalizableDisplayName
        {
            get => _localizableDisplayName;
            set
            {
                _localizableDisplayName = value;
                DisplayNameEnUs = LocalizationHelper.GetString(FinanceiroConsts.LocalizationSourceName, _localizableDisplayName, new CultureInfo("en-US"));
                DisplayNamePtBr = LocalizationHelper.GetString(FinanceiroConsts.LocalizationSourceName, _localizableDisplayName, new CultureInfo("pt-BR"));
            }
        }

        [MaxLength(200)]
        public virtual string DisplayNamePtBr { get; set; }
        [MaxLength(200)]
        public virtual string DisplayNameEnUs { get; set; }
        public virtual string UrlPath { get; set; }
        [MaxLength(120)]
        public virtual string Module { get; set; }
        [MaxLength(70)]
        public virtual string Icon { get; set; }
        [MaxLength(300)]
        public virtual string TemplateUrl { get; set; }
        [MaxLength(120)]
        public virtual string RequiredPermissionName { get; set; }
        [MaxLength(300)]
        public virtual string UrlToNavigate { get; set; }
        public virtual bool Visible { get; set; }
        public virtual int Order { get; set; }
        [ForeignKey("ParentNavigationId")]
        public virtual List<Navigation> ChildrenNavigations { get; set; }
        public virtual Navigation ParentNavigation { get; set; }
        [ForeignKey("ParentNavigation")]
        public virtual Guid? ParentNavigationId { get; set; }

        public Navigation()
        {
           
        }

        public Navigation(string name, string displayName, string urlPath, string icon, string module, string templateUrl = null, string requiredPermissionName = null, bool visible = true)
        {
            Name = name;
            LocalizableDisplayName = displayName;
            UrlPath = urlPath;
            Icon = icon;
            TemplateUrl = templateUrl;
            RequiredPermissionName = requiredPermissionName;
            Visible = visible;
            Module = module;
        }

        public Navigation CreateChild(string name, string localizableDisplayName, string urlPath, string icon, string templateUrl, string requiredPermissionName, bool visible)
        {
            var authorization = new Navigation(name, localizableDisplayName, urlPath, icon, this.Module, templateUrl,
                requiredPermissionName, visible);

            if (ChildrenNavigations == null)
                ChildrenNavigations = new List<Navigation>();

            ChildrenNavigations.Add(authorization);

            return authorization;
        }

        public Navigation CreateAsRoute(string name, string urlPath, string module)
        {
            return new Navigation(name, name, urlPath, null, module, null, null, false);
        }

        public string GetFullUrlPath()
        {
            if (ParentNavigation == null)
                return UrlPath;

            return $"{GetParentUrlPath(ParentNavigation)}{UrlPath}".ToLower();
        }

        private string GetParentUrlPath(Navigation parentNavigation)
        {
            var retorno = parentNavigation.UrlPath;
            if (parentNavigation.ParentNavigation == null)
                return retorno;
            retorno = GetParentUrlPath(parentNavigation.ParentNavigation);
            retorno = $"{retorno}{parentNavigation.UrlPath}";

            return retorno;
        }
    }
}
