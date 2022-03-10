using System.Linq;
using Fgv.Acad.Financeiro.Applications;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Fgv.Acad.Financeiro.Authorization.Distributed;
using Microsoft.EntityFrameworkCore;
using Fgv.Acad.Financeiro.Authorization.Roles;
using Fgv.Acad.Financeiro.Authorization.Users;
using Fgv.Acad.Financeiro.Editions;
using Fgv.Acad.Financeiro.MultiTenancy;
using Fgv.Acad.Financeiro.MultiTenancy.Accounting;
using Fgv.Acad.Financeiro.MultiTenancy.Payments;
using Fgv.Acad.Financeiro.Navigations;
using Fgv.Acad.Financeiro.Storage;
using Fgv.Acad.Financeiro.Eventos;

namespace Fgv.Acad.Financeiro.EntityFrameworkCore
{
    public class FinanceiroDbContext : AbpZeroDbContext<Tenant, Role, User, FinanceiroDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<Application> Applications { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<DistributedAuthorization> DistributedAuthorizations { get; set; }
        public virtual DbSet<Navigation> Navigations { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<TipoIngresso> TipoIngresso { get; set; }


        public FinanceiroDbContext(DbContextOptions<FinanceiroDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("");
            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder
              .Entity<TipoIngresso>()
              .HasOne(e => e.Evento)
              .WithMany(e => e.ListaTipoIngresso)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
              .Entity<Venda>()
              .HasOne(e => e.TipoIngresso)
              .WithMany(e => e.Vendas)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
             .Entity<Venda>()
             .HasOne(e => e.Cliente )
             .WithMany(e => e.Vendas)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>(entity => {
                entity.HasIndex(e => e.CPF).IsUnique();
            });

            modelBuilder.Entity<Cliente>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.ConfigurePersistedGrantEntity();

            //Adjust Conventions
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    //Remove PluralizingTableName
            //    if (entityType.ClrType != null)
            //        entityType.Relational().TableName = entityType.ClrType.Name;

            //    //Remove OneToManyCascadeDelete and ManyToManyCascadeDelete
            //    //entityType.GetForeignKeys().Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade).ToList().ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);

            //    //Remove Unicode
            //    entityType.GetProperties().Where(p => p.ClrType == typeof(string)).ToList().ForEach(p => p.IsUnicode(false));

            //    //Set MaxLength
            //    entityType.GetProperties().Where(p => p.ClrType == typeof(string) && !p.GetMaxLength().HasValue).ToList().ForEach(p => p.SetMaxLength(1000));
            //}
        }
    }
}
