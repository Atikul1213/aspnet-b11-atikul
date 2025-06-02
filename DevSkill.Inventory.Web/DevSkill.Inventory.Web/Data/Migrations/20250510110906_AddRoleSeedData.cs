using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3953c591-39c4-43a4-82f6-54e4c94aa376"), "5/19/2025 1:01:02 AM", "Registred", "REGISTRED" },
                    { new Guid("63aa3e36-f509-43b8-a322-4ff046f14600"), "5/19/2025 1:01:04 AM", "Guest", "GUEST" },
                    { new Guid("d5d68b30-a048-450d-ba24-a9c035d02ea3"), "5/19/2025 1:01:03 AM", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("f2ab8092-2370-4696-a2fb-13caa07e4fe4"), "5/19/2025 1:01:01 AM", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3953c591-39c4-43a4-82f6-54e4c94aa376"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63aa3e36-f509-43b8-a322-4ff046f14600"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d5d68b30-a048-450d-ba24-a9c035d02ea3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f2ab8092-2370-4696-a2fb-13caa07e4fe4"));
        }
    }
}
