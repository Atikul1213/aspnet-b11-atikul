using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Authors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Authors");
        }
    }
}
