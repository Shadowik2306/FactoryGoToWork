using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class initmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "Stages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ComponentId",
                table: "Stages",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_PlanId",
                table: "Stages",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Components_ComponentId",
                table: "Stages",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Plans_PlanId",
                table: "Stages",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Components_ComponentId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Plans_PlanId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_ComponentId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_PlanId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Stages");

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
    }
}
