using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClainSeedData : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3953c591-39c4-43a4-82f6-54e4c94aa376"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Registred", "REGISTRED" });
        }
    }
}
