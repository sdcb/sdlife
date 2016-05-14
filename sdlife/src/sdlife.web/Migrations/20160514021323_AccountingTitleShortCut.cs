using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace sdlife.web.Migrations
{
    public partial class AccountingTitleShortCut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortCut",
                table: "AccountingTitle",
                nullable: false,
                defaultValue: "");
            migrationBuilder.CreateIndex(
                name: "IX_AccountingTitle_ShortCut",
                table: "AccountingTitle",
                column: "ShortCut");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_AccountingTitle_ShortCut", table: "AccountingTitle");
            migrationBuilder.DropColumn(name: "ShortCut", table: "AccountingTitle");
        }
    }
}
