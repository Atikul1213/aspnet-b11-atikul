using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddContactUsInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUsInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    OpenHours = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    MapEmbedUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    HeaderTitle = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    HeaderSubtitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HeaderDescription = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsInfo", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ContactUsInfo",
                columns: new[] { "Id", "Address", "Email", "HeaderDescription", "HeaderSubtitle", "HeaderTitle", "MapEmbedUrl", "OpenHours", "Phone" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123456789abc"), "House # 184 (8th Floor), uttam para, kachinia, Dinajpur, Bangladesh", "atikuldpi@gmail.com", "Any question or remark? Just write us a message.", "We're here to help you succeed.", "Contact Us", "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d627.157029892025!2d90.36982840964166!3d23.804157847728483!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3755c796bbbcadcf%3A0xc14e0a242f03896!2sDev%20Skill!5e1!3m2!1sen!2sbd!4v1755592523100!5m2!1sen!2sbd", "Sun-Thu: 9AM - 11PM", "+8801722248512" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUsInfo");
        }
    }
}
