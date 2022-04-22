using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    public partial class AddDomainIdInEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DomainId",
                table: "Entries",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_DomainId",
                table: "Entries",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Domains_DomainId",
                table: "Entries",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Domains_DomainId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_DomainId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "Entries");
        }
    }
}
