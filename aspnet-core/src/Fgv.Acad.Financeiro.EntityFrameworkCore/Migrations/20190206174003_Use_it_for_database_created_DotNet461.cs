using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fgv.Acad.Financeiro.Migrations
{
    public partial class Use_it_for_database_created_DotNet461 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>("IsDeleted", "UserOrganizationUnits", defaultValue: false);

            //migrationBuilder.CreateTable(
            //    name: "RoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        TenantId = table.Column<int>(nullable: true),
            //        RoleId = table.Column<int>(nullable: false),
            //        ClaimType = table.Column<string>(maxLength: 256, nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RoleClaims_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleClaims_RoleId",
            //    table: "RoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleClaims_TenantId_ClaimType",
            //    table: "RoleClaims",
            //    columns: new[] { "TenantId", "ClaimType" });

            //migrationBuilder.CreateTable(
            //    name: "EntityChangeSets",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        BrowserInfo = table.Column<string>(maxLength: 512, nullable: true),
            //        ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
            //        ClientName = table.Column<string>(maxLength: 128, nullable: true),
            //        CreationTime = table.Column<DateTime>(nullable: false),
            //        ExtensionData = table.Column<string>(nullable: true),
            //        ImpersonatorTenantId = table.Column<int>(nullable: true),
            //        ImpersonatorUserId = table.Column<long>(nullable: true),
            //        Reason = table.Column<string>(maxLength: 256, nullable: true),
            //        TenantId = table.Column<int>(nullable: true),
            //        UserId = table.Column<long>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EntityChangeSets", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EntityChanges",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ChangeTime = table.Column<DateTime>(nullable: false),
            //        ChangeType = table.Column<byte>(nullable: false),
            //        EntityChangeSetId = table.Column<long>(nullable: false),
            //        EntityId = table.Column<string>(maxLength: 48, nullable: true),
            //        EntityTypeFullName = table.Column<string>(maxLength: 192, nullable: true),
            //        TenantId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EntityChanges", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EntityChanges_EntityChangeSets_EntityChangeSetId",
            //            column: x => x.EntityChangeSetId,
            //            principalTable: "EntityChangeSets",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EntityChangeSets_TenantId_CreationTime",
            //    table: "EntityChangeSets",
            //    columns: new[] { "TenantId", "CreationTime" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EntityChangeSets_TenantId_Reason",
            //    table: "EntityChangeSets",
            //    columns: new[] { "TenantId", "Reason" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EntityChangeSets_TenantId_UserId",
            //    table: "EntityChangeSets",
            //    columns: new[] { "TenantId", "UserId" });

            //migrationBuilder.AddColumn<int>("Discriminator", "Editions", nullable: true);
            //migrationBuilder.AddColumn<int>("ExpiringEditionId", "Editions", nullable: true);
            //migrationBuilder.AddColumn<decimal>("MonthlyPrice", "Editions", nullable: true);
            //migrationBuilder.AddColumn<int>("TrialDayCount", "Editions", nullable: true);
            //migrationBuilder.AddColumn<decimal>("AnnualPrice", "Editions", nullable: true);
            //migrationBuilder.AddColumn<int>("WaitingDayAfterExpire", "Editions", nullable: true);

            //migrationBuilder.AddColumn<bool>("IsDisabled", "Languages", defaultValue: false);

            //migrationBuilder.AddColumn<string>("ConcurrencyStamp", "Roles", nullable: true);
            //migrationBuilder.AddColumn<string>("NormalizedName", "Roles", nullable: true);

            //migrationBuilder.AddColumn<string>("ConcurrencyStamp", "Users", nullable: true);
            //migrationBuilder.AddColumn<string>("GoogleAuthenticatorKey", "Users", nullable: true);
            //migrationBuilder.AddColumn<string>("NormalizedEmailAddress", "Users", nullable: true);
            //migrationBuilder.AddColumn<string>("NormalizedUserName", "Users", nullable: true);
            //migrationBuilder.AddColumn<string>("SignInToken", "Users", nullable: true);
            //migrationBuilder.AddColumn<DateTime?>("SignInTokenExpireTimeUtc", "Users", nullable: true);

            //migrationBuilder.AddColumn<bool>("IsInTrialPeriod", "Tenants", defaultValue: false);
            //migrationBuilder.AddColumn<DateTime?>("SubscriptionEndDateUtc", "Tenants", nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "UserTokens",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        TenantId = table.Column<int>(nullable: true),
            //        UserId = table.Column<long>(nullable: false),
            //        LoginProvider = table.Column<string>(maxLength: 128, nullable: true),
            //        Name = table.Column<string>(maxLength: 128, nullable: true),
            //        Value = table.Column<string>(maxLength: 512, nullable: true),
            //        ExpireDate = table.Column<DateTime>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserTokens", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserTokens_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserTokens_UserId",
            //    table: "UserTokens",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserTokens_TenantId_UserId",
            //    table: "UserTokens",
            //    columns: new[] { "TenantId", "UserId" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn("IsDeleted", "UserOrganizationUnits");

            //migrationBuilder.DropIndex(
            //    name: "IX_RoleClaims_RoleId",
            //    table: "RoleClaims");

            //migrationBuilder.DropIndex(
            //    name: "IX_RoleClaims_TenantId_ClaimType",
            //    table: "RoleClaims");

            //migrationBuilder.DropTable(name: "RoleClaims");

            //migrationBuilder.DropTable(
            //    name: "EntityChanges");

            //migrationBuilder.DropTable(
            //    name: "EntityChangeSets");

            //migrationBuilder.DropColumn("Discriminator", "Editions");
            //migrationBuilder.DropColumn("ExpiringEditionId", "Editions");
            //migrationBuilder.DropColumn("MonthlyPrice", "Editions");
            //migrationBuilder.DropColumn("TrialDayCount", "Editions");
            //migrationBuilder.DropColumn("AnnualPrice", "Editions");
            //migrationBuilder.DropColumn("WaitingDayAfterExpire", "Editions");

            //migrationBuilder.DropColumn("IsDisabled", "Languages");

            //migrationBuilder.DropColumn("ConcurrencyStamp", "Roles");
            //migrationBuilder.DropColumn("NormalizedName", "Roles");

            //migrationBuilder.DropColumn("ConcurrencyStamp", "Users");
            //migrationBuilder.DropColumn("GoogleAuthenticatorKey", "Users");
            //migrationBuilder.DropColumn("NormalizedEmailAddress", "Users");
            //migrationBuilder.DropColumn("NormalizedUserName", "Users");
            //migrationBuilder.DropColumn("SignInToken", "Users");
            //migrationBuilder.DropColumn("SignInTokenExpireTimeUtc", "Users");

            //migrationBuilder.DropColumn("IsInTrialPeriod", "Tenants");
            //migrationBuilder.DropColumn("SubscriptionEndDateUtc", "Tenants");

            //migrationBuilder.DropTable(
            //    name: "UserTokens");
        }
    }
}
