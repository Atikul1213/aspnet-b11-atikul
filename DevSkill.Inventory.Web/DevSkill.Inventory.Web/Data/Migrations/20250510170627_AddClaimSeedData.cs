using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3953c591-39c4-43a4-82f6-54e4c94aa376"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Registered", "REGISTERED" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "create_product", "allowed", new Guid("16fed63d-5437-43a5-4c8b-08dd8f741869") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3953c591-39c4-43a4-82f6-54e4c94aa376"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Registred", "REGISTRED" });
        }
    }
}
