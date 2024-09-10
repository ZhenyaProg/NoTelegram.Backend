using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoTelegram.DataAccess.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class createChats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatName = table.Column<string>(type: "text", nullable: false),
                    ChatAccess = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    SecurityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Authenticated = table.Column<bool>(type: "boolean", nullable: false),
                    AuthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.SecurityId);
                });

            migrationBuilder.CreateTable(
                name: "ChatsEntityUsersEntity",
                columns: table => new
                {
                    ChatsChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InterlocutorsSecurityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatsEntityUsersEntity", x => new { x.ChatsChatId, x.InterlocutorsSecurityId });
                    table.ForeignKey(
                        name: "FK_ChatsEntityUsersEntity_Chats_ChatsChatId",
                        column: x => x.ChatsChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatsEntityUsersEntity_Users_InterlocutorsSecurityId",
                        column: x => x.InterlocutorsSecurityId,
                        principalTable: "Users",
                        principalColumn: "SecurityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatsEntityUsersEntity_InterlocutorsSecurityId",
                table: "ChatsEntityUsersEntity",
                column: "InterlocutorsSecurityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatsEntityUsersEntity");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
