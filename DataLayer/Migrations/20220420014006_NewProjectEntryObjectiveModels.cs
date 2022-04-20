using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    public partial class NewProjectEntryObjectiveModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Entries_RequiredEntriesId",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Objectives_ParentObjectivesId",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Entries_EntryId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Objectives");

            migrationBuilder.RenameColumn(
                name: "EntryId",
                table: "Objectives",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_EntryId",
                table: "Objectives",
                newName: "IX_Objectives_ProjectId");

            migrationBuilder.RenameColumn(
                name: "RequiredEntriesId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "ObjectivesId");

            migrationBuilder.RenameColumn(
                name: "ParentObjectivesId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "EntriesId");

            migrationBuilder.RenameIndex(
                name: "IX_EntryDbModelObjectiveDbModel_RequiredEntriesId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "IX_EntryDbModelObjectiveDbModel_ObjectivesId");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Entries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DomainId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ParentId",
                table: "Entries",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DomainId",
                table: "Projects",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_ParentId",
                table: "Entries",
                column: "ParentId",
                principalTable: "Entries",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Projects_ProjectId",
                table: "Objectives",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_ParentId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Entries_EntriesId",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Objectives_ObjectivesId",
                table: "EntryDbModelObjectiveDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Projects_ProjectId",
                table: "Objectives");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ParentId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Objectives",
                newName: "EntryId");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_ProjectId",
                table: "Objectives",
                newName: "IX_Objectives_EntryId");

            migrationBuilder.RenameColumn(
                name: "ObjectivesId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "RequiredEntriesId");

            migrationBuilder.RenameColumn(
                name: "EntriesId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "ParentObjectivesId");

            migrationBuilder.RenameIndex(
                name: "IX_EntryDbModelObjectiveDbModel_ObjectivesId",
                table: "EntryDbModelObjectiveDbModel",
                newName: "IX_EntryDbModelObjectiveDbModel_RequiredEntriesId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Objectives",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Entries_RequiredEntriesId",
                table: "EntryDbModelObjectiveDbModel",
                column: "RequiredEntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryDbModelObjectiveDbModel_Objectives_ParentObjectivesId",
                table: "EntryDbModelObjectiveDbModel",
                column: "ParentObjectivesId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Entries_EntryId",
                table: "Objectives",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
