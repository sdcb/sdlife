using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sdlife.web.Data.Migrations
{
    public partial class AccountingUserAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingUserAuthorization",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    AuthorizedUserId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingUserAuthorization", x => new { x.UserId, x.AuthorizedUserId });
                    table.ForeignKey(
                        name: "FK_AccountingUserAuthorization_User_AuthorizedUserId",
                        column: x => x.AuthorizedUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountingUserAuthorization_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingUserAuthorization_AuthorizedUserId",
                table: "AccountingUserAuthorization",
                column: "AuthorizedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingUserAuthorization_UserId",
                table: "AccountingUserAuthorization",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingUserAuthorization");
        }
    }
}
