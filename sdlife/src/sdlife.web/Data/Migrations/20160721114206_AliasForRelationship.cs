using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sdlife.web.Data.Migrations
{
    public partial class AliasForRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "AccountingUserAuthorization",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "AccountingUserAuthorization");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName");
        }
    }
}
