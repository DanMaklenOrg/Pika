using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    public partial class ProgressDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Progress = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => new { x.UserId, x.ObjectiveId, x.TargetId });
                    table.ForeignKey(
                        name: "FK_Progress_Entries_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progress_Objectives_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progress_ObjectiveTargets_ObjectiveId_TargetId",
                        columns: x => new { x.ObjectiveId, x.TargetId },
                        principalTable: "ObjectiveTargets",
                        principalColumns: new[] { "ObjectiveId", "TargetId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progress_ObjectiveId_TargetId",
                table: "Progress",
                columns: new[] { "ObjectiveId", "TargetId" });

            migrationBuilder.CreateIndex(
                name: "IX_Progress_TargetId",
                table: "Progress",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_UserId",
                table: "Progress",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progress");
        }
    }
}
