using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_wesele.Migrations
{
    /// <inheritdoc />
    public partial class GuestsSide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "Guests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Side",
                table: "Guests");
        }
    }
}
