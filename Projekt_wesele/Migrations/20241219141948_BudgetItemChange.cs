using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_wesele.Migrations
{
    /// <inheritdoc />
    public partial class BudgetItemChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "BudgetItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "IsEssential",
                table: "BudgetItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "BudgetItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEssential",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "BudgetItems");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "BudgetItems",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
