using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace triggeredapi.Migrations
{
    /// <inheritdoc />
    public partial class novels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Novel_User_UserId",
                table: "Novel");

            migrationBuilder.DropIndex(
                name: "IX_Novel_UserId",
                table: "Novel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Novel");

            migrationBuilder.CreateTable(
                name: "NovelUser",
                columns: table => new
                {
                    NovelsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelUser", x => new { x.NovelsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_NovelUser_Novel_NovelsId",
                        column: x => x.NovelsId,
                        principalTable: "Novel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NovelUser_UsersId",
                table: "NovelUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NovelUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Novel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Novel_UserId",
                table: "Novel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Novel_User_UserId",
                table: "Novel",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
