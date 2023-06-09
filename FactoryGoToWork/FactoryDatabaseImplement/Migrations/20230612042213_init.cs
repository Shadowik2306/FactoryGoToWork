﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    EngenierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engeniers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engeniers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LatheBusies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percent = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatheBusies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reinforceds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReinforcedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngenierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reinforceds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reinforceds_Engeniers_EngenierId",
                        column: x => x.EngenierId,
                        principalTable: "Engeniers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lathes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatheName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    BusyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lathes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lathes_LatheBusies_BusyId",
                        column: x => x.BusyId,
                        principalTable: "LatheBusies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lathes_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentPlans_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanReinforceds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    ReinforceId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ReinforcedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanReinforceds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanReinforceds_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanReinforceds_Reinforceds_ReinforcedId",
                        column: x => x.ReinforcedId,
                        principalTable: "Reinforceds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LatheComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatheId = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatheComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LatheComponents_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LatheComponents_Lathes_LatheId",
                        column: x => x.LatheId,
                        principalTable: "Lathes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LatheReinforcedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatheId = table.Column<int>(type: "int", nullable: false),
                    ReinforcedId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatheReinforcedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LatheReinforcedes_Lathes_LatheId",
                        column: x => x.LatheId,
                        principalTable: "Lathes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LatheReinforcedes_Reinforceds_ReinforcedId",
                        column: x => x.ReinforcedId,
                        principalTable: "Reinforceds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPlans_ComponentId",
                table: "ComponentPlans",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPlans_PlanId",
                table: "ComponentPlans",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LatheComponents_ComponentId",
                table: "LatheComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_LatheComponents_LatheId",
                table: "LatheComponents",
                column: "LatheId");

            migrationBuilder.CreateIndex(
                name: "IX_LatheReinforcedes_LatheId",
                table: "LatheReinforcedes",
                column: "LatheId");

            migrationBuilder.CreateIndex(
                name: "IX_LatheReinforcedes_ReinforcedId",
                table: "LatheReinforcedes",
                column: "ReinforcedId");

            migrationBuilder.CreateIndex(
                name: "IX_Lathes_BusyId",
                table: "Lathes",
                column: "BusyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lathes_MasterId",
                table: "Lathes",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanReinforceds_PlanId",
                table: "PlanReinforceds",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanReinforceds_ReinforcedId",
                table: "PlanReinforceds",
                column: "ReinforcedId");

            migrationBuilder.CreateIndex(
                name: "IX_Reinforceds_EngenierId",
                table: "Reinforceds",
                column: "EngenierId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_PlanId",
                table: "Stages",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentPlans");

            migrationBuilder.DropTable(
                name: "LatheComponents");

            migrationBuilder.DropTable(
                name: "LatheReinforcedes");

            migrationBuilder.DropTable(
                name: "PlanReinforceds");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Lathes");

            migrationBuilder.DropTable(
                name: "Reinforceds");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "LatheBusies");

            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.DropTable(
                name: "Engeniers");
        }
    }
}
