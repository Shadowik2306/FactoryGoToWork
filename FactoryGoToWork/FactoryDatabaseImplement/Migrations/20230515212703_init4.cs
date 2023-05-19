using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ReinforcedComponents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ReinforcedComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
