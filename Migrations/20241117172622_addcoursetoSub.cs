using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class addcoursetoSub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Expire",
                table: "SubscriptionsInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubInfoId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResgisterDate",
                table: "Subscriptions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 17, 17, 26, 22, 239, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CourseId",
                table: "Subscriptions",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Courses_CourseId",
                table: "Subscriptions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Courses_CourseId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CourseId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ResgisterDate",
                table: "Subscriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expire",
                table: "SubscriptionsInfo",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Subscriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubInfoId",
                table: "Subscriptions",
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
                column: "CreationDate",
                value: new DateTime(2024, 11, 17, 15, 38, 4, 916, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
