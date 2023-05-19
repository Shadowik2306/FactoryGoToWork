using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class h : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "PlanLathes");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "PlanComponents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "PlanLathes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "PlanComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
