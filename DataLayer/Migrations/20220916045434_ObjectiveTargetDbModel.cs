using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    public partial class ObjectiveTargetDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Entries_EntriesId",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Objectives_ObjectivesId",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryDbModelObjectiveDbModel",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.RenameTable(
                name: "EntryDbModelObjectiveDbModel",
                newName: "ObjectiveTargets");

            migrationBuilder.RenameColumn(
                name: "ObjectivesId",
                table: "ObjectiveTargets",
                newName: "TargetId");

            migrationBuilder.RenameColumn(
                name: "EntriesId",
                table: "ObjectiveTargets",
                newName: "ObjectiveId");

            migrationBuilder.RenameIndex(
                name: "IX_EntryDbModelObjectiveDbModel_ObjectivesId",
                table: "ObjectiveTargets",
                newName: "IX_ObjectiveTargets_TargetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveTargets",
                table: "ObjectiveTargets",
                columns: new[] { "ObjectiveId", "TargetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveTargets_Entries_TargetId",
                table: "ObjectiveTargets",
                column: "TargetId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveTargets_Objectives_ObjectiveId",
                table: "ObjectiveTargets",
                column: "ObjectiveId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveTargets_Entries_TargetId",
                table: "ObjectiveTargets");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveTargets_Objectives_ObjectiveId",
                table: "ObjectiveTargets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveTargets",
                table: "ObjectiveTargets");

            migrationBuilder.RenameTable(
                name: "ObjectiveTargets",
                newName: "EntryDbModelObjectiveDbModel");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "ObjectivesId");

            migrationBuilder.RenameColumn(
                name: "ObjectiveId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "EntriesId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjectiveTargets_TargetId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "IX_EntryDbModelObjectiveDbModel_ObjectivesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryDbModelObjectiveDbModel",
                table: "EntryDbModelObjectiveDbModel",
                columns: new[] { "EntriesId", "ObjectivesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Entries_EntriesId",
                table: "EntryDbModelObjectiveDbModel",
                column: "EntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Objectives_ObjectivesId",
                table: "EntryDbModelObjectiveDbModel",
                column: "ObjectivesId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
