using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    public partial class NullableRootEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Entries_RootEntryId",
                table: "Domains");

            migrationBuilder.AlterColumn<Guid>(
                name: "RootEntryId",
                table: "Domains",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Entries_RootEntryId",
                table: "Domains",
                column: "RootEntryId",
                principalTable: "Entries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Entries_RootEntryId",
                table: "Domains");

            migrationBuilder.AlterColumn<Guid>(
                name: "RootEntryId",
                table: "Domains",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Entries_RootEntryId",
                table: "Domains",
                column: "RootEntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
