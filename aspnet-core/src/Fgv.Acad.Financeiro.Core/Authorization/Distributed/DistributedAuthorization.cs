using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.MultiTenancy;

namespace Fgv.Acad.Financeiro.Authorization.Distributed
{
    [Table("Authorizations")]
    public class DistributedAuthorization : Entity<Guid>
    {
        [MaxLength(120)]
        public string Name { get; set; }
        [MaxLength(120)]
        public string LocalizableDisplayName { get; set; }
        [MaxLength(120)]
        public virtual string LocalizableDescriptionName { get; set; }
        public virtual MultiTenancySides? MultiTenancySide { get; set; }
        [MaxLength(120)]
        public virtual string Module { get; set; }
        [ForeignKey("ParentAuthorizationId")]
        public virtual List<DistributedAuthorization> ChildrenAuthorizations { get; set; }
        public virtual DistributedAuthorization ParentAuthorization { get; set; }
        [ForeignKey("ParentAuthorization")]
        public virtual Guid? ParentAuthorizationId { get; set; }


        public DistributedAuthorization CreateChild(string name, string localizableDisplayName = null, string localizableDescription = null, MultiTenancySides multiTenancySides = MultiTenancySides.Tenant | MultiTenancySides.Host)
        {
            var authorization =  new DistributedAuthorization
            {
                Name = name, LocalizableDescriptionName = localizableDescription,
                LocalizableDisplayName = localizableDisplayName, Module = this.Module,
                MultiTenancySide = multiTenancySides, ParentAuthorizationId = this.Id
            };

            if (ChildrenAuthorizations == null)
                ChildrenAuthorizations = new List<DistributedAuthorization>();

            ChildrenAuthorizations.Add(authorization);

            return authorization;
        }

        public string GetFullName()
        {
            if (ParentAuthorization == null)
                return Name;

            return $"{GetParentName(ParentAuthorization)}.{Name}";
        }

        private string GetParentName(DistributedAuthorization parentAuthorization)
        {
            var retorno = parentAuthorization.Name;
            if (parentAuthorization.ParentAuthorization == null)
                return retorno;
            retorno = GetParentName(parentAuthorization.ParentAuthorization);
            retorno = $"{retorno}.{parentAuthorization.Name}";

            return retorno;
        }

    }
}