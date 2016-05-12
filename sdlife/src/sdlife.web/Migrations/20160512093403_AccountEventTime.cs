using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace sdlife.web.Migrations
{
    public partial class AccountEventTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AccountingComment_Accounting_AccountingId", table: "AccountingComment");
            migrationBuilder.DropPrimaryKey(name: "PK_Accounting", table: "Accounting");
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Accounting",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");
            migrationBuilder.AddColumn<DateTime>(
                name: "EventTime",
                table: "Accounting",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounting",
                table: "Accounting",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);
            migrationBuilder.CreateIndex(
                name: "IX_Accounting_EventTime",
                table: "Accounting",
                column: "EventTime",
                unique: true)
                .Annotation("SqlServer:Clustered", true);
            migrationBuilder.AddForeignKey(
                name: "FK_AccountingComment_Accounting_AccountingId",
                table: "AccountingComment",
                column: "AccountingId",
                principalTable: "Accounting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AccountingComment_Accounting_AccountingId", table: "AccountingComment");
            migrationBuilder.DropPrimaryKey(name: "PK_Accounting", table: "Accounting");
            migrationBuilder.DropIndex(name: "IX_Accounting_EventTime", table: "Accounting");
            migrationBuilder.DropColumn(name: "EventTime", table: "Accounting");
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Accounting",
                nullable: false);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounting",
                table: "Accounting",
                column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_AccountingComment_Accounting_AccountingId",
                table: "AccountingComment",
                column: "AccountingId",
                principalTable: "Accounting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
