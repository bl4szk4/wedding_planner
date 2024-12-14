using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_wesele.Migrations
{
    /// <inheritdoc />
    public partial class ExtraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPartner",
                table: "Guests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsKid",
                table: "Guests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPartner",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "IsKid",
                table: "Guests");
        }
    }
}
