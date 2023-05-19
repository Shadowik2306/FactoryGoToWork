using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnginierId",
                table: "Components");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnginierId",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
