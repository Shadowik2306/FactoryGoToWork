using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class k : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "LatheReinforcedes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "LatheReinforcedes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
