using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class addingDaySeqss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Sections_SectionId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Sections_SectionId",
                table: "Subscriptions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 17, 15, 38, 4, 916, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Sections_SectionId",
                table: "Courses",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Sections_SectionId",
                table: "Subscriptions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Sections_SectionId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Sections_SectionId",
                table: "Subscriptions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 14, 21, 12, 59, 669, DateTimeKind.Utc).AddTicks(8537));

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Sections_SectionId",
                table: "Courses",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Sections_SectionId",
                table: "Subscriptions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }
    }
}
