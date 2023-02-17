using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    public partial class RemoveEntityChildRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entity_ParentId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_ParentId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Entity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Entity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entity_ParentId",
                table: "Entity",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entity_ParentId",
                table: "Entity",
                column: "ParentId",
                principalTable: "Entity",
                principalColumn: "Id");
        }
    }
}
