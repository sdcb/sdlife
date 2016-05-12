using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace sdlife.web.Migrations
{
    public partial class Accounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingTitle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false), 
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingTitle_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TitleId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<int>(nullable: false),                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounting_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounting_AccountingTitle_TitleId",
                        column: x => x.TitleId,
                        principalTable: "AccountingTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "AccountingComment",
                columns: table => new
                {
                    AccountingId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingComment", x => x.AccountingId);
                    table.ForeignKey(
                        name: "FK_AccountingComment_Accounting_AccountingId",
                        column: x => x.AccountingId,
                        principalTable: "Accounting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_AccountingTitle_Title",
                table: "AccountingTitle",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AccountingComment");
            migrationBuilder.DropTable("Accounting");
            migrationBuilder.DropTable("AccountingTitle");
        }
    }
}
