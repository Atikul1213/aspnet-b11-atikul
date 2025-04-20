using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.Web.Data.Migrations
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
                    { new Guid("5457d1c5-bc6e-4ef7-bc4f-b86a0b012de1"), "4/19/2025 1:02:03 AM", "Author", "AUTHOR" },
                    { new Guid("8acf9875-73d7-4a67-921a-e28b3af34e71"), "4/19/2025 1:02:01 AM", "Admin", "ADMIN" },
                    { new Guid("cd98dc1c-4f83-493c-a11d-1dd9d3528322"), "4/19/2025 1:02:02 AM", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5457d1c5-bc6e-4ef7-bc4f-b86a0b012de1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8acf9875-73d7-4a67-921a-e28b3af34e71"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cd98dc1c-4f83-493c-a11d-1dd9d3528322"));
        }
    }
}
