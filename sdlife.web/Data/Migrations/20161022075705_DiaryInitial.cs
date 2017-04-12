using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sdlife.web.Data.Migrations
{
    public partial class DiaryInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feeling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordTime = table.Column<DateTime>(nullable: false, defaultValueSql: "SYSDATETIME()"),
                    UserId = table.Column<int>(nullable: false),
                    WeatherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryHeader", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_DiaryHeader_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaryHeader_Weather_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "Weather",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryContent",
                columns: table => new
                {
                    DiaryId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryContent", x => x.DiaryId);
                    table.ForeignKey(
                        name: "FK_DiaryContent_DiaryHeader_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "DiaryHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryFeeling",
                columns: table => new
                {
                    FeelingId = table.Column<int>(nullable: false),
                    DiaryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryFeeling", x => new { x.FeelingId, x.DiaryId });
                    table.ForeignKey(
                        name: "FK_DiaryFeeling_DiaryHeader_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "DiaryHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaryFeeling_Feeling_FeelingId",
                        column: x => x.FeelingId,
                        principalTable: "Feeling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryContent_DiaryId",
                table: "DiaryContent",
                column: "DiaryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaryFeeling_DiaryId",
                table: "DiaryFeeling",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryFeeling_FeelingId",
                table: "DiaryFeeling",
                column: "FeelingId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryHeader_RecordTime",
                table: "DiaryHeader",
                column: "RecordTime",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaryHeader_UserId",
                table: "DiaryHeader",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryHeader_WeatherId",
                table: "DiaryHeader",
                column: "WeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeling_Name",
                table: "Feeling",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weather_Name",
                table: "Weather",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaryContent");

            migrationBuilder.DropTable(
                name: "DiaryFeeling");

            migrationBuilder.DropTable(
                name: "DiaryHeader");

            migrationBuilder.DropTable(
                name: "Feeling");

            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}
