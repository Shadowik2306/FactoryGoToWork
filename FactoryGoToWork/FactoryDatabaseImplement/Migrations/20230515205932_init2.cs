using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Reinforceds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Reinforceds",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
