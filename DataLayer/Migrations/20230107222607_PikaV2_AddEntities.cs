#nullable disable
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pika.DataLayer.Migrations
{
    public partial class PikaV2_AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Domains_Entries_RootEntryId", table: "Domains");
            migrationBuilder.DropForeignKey(name: "FK_Entries_Domains_DomainId", table: "Entries");
            migrationBuilder.DropForeignKey(name: "FK_Projects_Domains_DomainId", table: "Projects");
            migrationBuilder.DropPrimaryKey(name: "PK_Domains", table: "Domains");
            migrationBuilder.RenameTable(name: "Domains", newName: "Domain");
            migrationBuilder.RenameIndex(name: "IX_Domains_RootEntryId", table: "Domain", newName: "IX_Domain_RootEntryId");
            migrationBuilder.AlterColumn<string>(name: "Name",
                table: "Domain",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
            migrationBuilder.AddPrimaryKey(name: "PK_Domain", table: "Domain", column: "Id");
            migrationBuilder.CreateTable(name: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DomainId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(name: "FK_Entity_Domain_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(name: "FK_Entity_Entity_ParentId", column: x => x.ParentId, principalTable: "Entity", principalColumn: "Id");
                });
            migrationBuilder.CreateIndex(name: "IX_Entity_DomainId", table: "Entity", column: "DomainId");
            migrationBuilder.CreateIndex(name: "IX_Entity_ParentId", table: "Entity", column: "ParentId");
            migrationBuilder.AddForeignKey(name: "FK_Domain_Entries_RootEntryId",
                table: "Domain",
                column: "RootEntryId",
                principalTable: "Entries",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Entries_Domain_DomainId",
                table: "Entries",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Projects_Domain_DomainId",
                table: "Projects",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.Sql(@"
                INSERT INTO ""Entity"" (""Id"", ""Name"", ""DomainId"", ""ParentId"")
                SELECT ""Id"", ""Title"", ""DomainId"", ""ParentId"" FROM ""Entries""");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Domain_Entries_RootEntryId", table: "Domain");
            migrationBuilder.DropForeignKey(name: "FK_Entries_Domain_DomainId", table: "Entries");
            migrationBuilder.DropForeignKey(name: "FK_Projects_Domain_DomainId", table: "Projects");
            migrationBuilder.DropTable(name: "Entity");
            migrationBuilder.DropPrimaryKey(name: "PK_Domain", table: "Domain");
            migrationBuilder.RenameTable(name: "Domain", newName: "Domains");
            migrationBuilder.RenameIndex(name: "IX_Domain_RootEntryId", table: "Domains", newName: "IX_Domains_RootEntryId");
            migrationBuilder.AlterColumn<string>(name: "Name",
                table: "Domains",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldUnicode: false,
                oldMaxLength: 100);
            migrationBuilder.AddPrimaryKey(name: "PK_Domains", table: "Domains", column: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Domains_Entries_RootEntryId",
                table: "Domains",
                column: "RootEntryId",
                principalTable: "Entries",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Entries_Domains_DomainId",
                table: "Entries",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(name: "FK_Projects_Domains_DomainId",
                table: "Projects",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
