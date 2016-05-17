using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sdlife.web.Data.Migrations
{
    public partial class DeleteAccountingUselessColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTitle_User_CreateUserId",
                table: "AccountingTitle");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "AccountingTitle");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Accounting");

            migrationBuilder.AlterColumn<int>(
                name: "CreateUserId",
                table: "AccountingTitle",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTitle_User_CreateUserId",
                table: "AccountingTitle",
                column: "CreateUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingTitle_User_CreateUserId",
                table: "AccountingTitle");

            migrationBuilder.AlterColumn<int>(
                name: "CreateUserId",
                table: "AccountingTitle",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "AccountingTitle",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Accounting",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingTitle_User_CreateUserId",
                table: "AccountingTitle",
                column: "CreateUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
