using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sdlife.web.Data.Migrations
{
    public partial class AddIsInCome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTitle_User_CreateUserId",
                table: "AccountingTitle");

            migrationBuilder.DropIndex(
                name: "IX_AccountingTitle_CreateUserId",
                table: "AccountingTitle");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "AccountingTitle");

            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "AccountingTitle",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "AccountingTitle");

            migrationBuilder.AddColumn<int>(
                name: "CreateUserId",
                table: "AccountingTitle",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTitle_CreateUserId",
                table: "AccountingTitle",
                column: "CreateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTitle_User_CreateUserId",
                table: "AccountingTitle",
                column: "CreateUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
