using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class addMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_ReseverId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ReseverId",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ReseverId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubId",
                table: "Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderUsername = table.Column<string>(type: "text", nullable: false),
                    RecipientUsername = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    MessageSent = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SenderDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Imgs = table.Column<List<string>>(type: "text[]", nullable: true),
                    VoiceMsgs = table.Column<List<string>>(type: "text[]", nullable: true),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                columns: new[] { "CreationDate", "SubId" },
                values: new object[] { new DateTime(2024, 11, 18, 12, 17, 17, 94, DateTimeKind.Utc).AddTicks(3933), null });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "ReseverId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_ReseverId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                columns: new[] { "CreationDate", "SubId" },
                values: new object[] { new DateTime(2024, 11, 17, 20, 47, 9, 346, DateTimeKind.Utc).AddTicks(6556), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_ReseverId",
                table: "Notifications",
                column: "ReseverId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
