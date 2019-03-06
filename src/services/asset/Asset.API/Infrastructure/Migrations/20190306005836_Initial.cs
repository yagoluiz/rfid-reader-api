using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asset.API.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetLocal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLocal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetLocalId = table.Column<int>(nullable: false),
                    AssetTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IdentifierNumber = table.Column<string>(maxLength: 50, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Epc = table.Column<string>(maxLength: 24, nullable: false),
                    Situation = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetItem_AssetLocal_AssetLocalId",
                        column: x => x.AssetLocalId,
                        principalTable: "AssetLocal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetItem_AssetType_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetItem_AssetLocalId",
                table: "AssetItem",
                column: "AssetLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetItem_AssetTypeId",
                table: "AssetItem",
                column: "AssetTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetItem");

            migrationBuilder.DropTable(
                name: "AssetLocal");

            migrationBuilder.DropTable(
                name: "AssetType");
        }
    }
}
