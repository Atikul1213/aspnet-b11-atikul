using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { -1, "create_user", "allowed", new Guid("8db2dfb1-3150-4d72-ad44-a4d3a28db1d1") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
