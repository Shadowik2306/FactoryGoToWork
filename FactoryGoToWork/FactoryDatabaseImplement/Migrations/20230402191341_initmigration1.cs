using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class initmigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ComponentId",
                table: "Plans",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlanId",
                table: "Plans",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Components_ComponentId",
                table: "Plans",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Plans_PlanId",
                table: "Plans",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Components_ComponentId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Plans_PlanId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_ComponentId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_PlanId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Plans");
        }
    }
}
