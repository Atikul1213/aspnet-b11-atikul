using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBalanceTransferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingAccountTypeId = table.Column<int>(type: "int", nullable: false),
                    ReceiveAccountTypeId = table.Column<int>(type: "int", nullable: false),
                    SendingAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiveAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceTransfers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceTransfers");
        }
    }
}
