using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class deletecourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Courses_CourseId",
                table: "Days");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 12, 13, 13, 51, 20, 764, DateTimeKind.Utc).AddTicks(9966));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 12, 13, 13, 51, 20, 764, DateTimeKind.Utc).AddTicks(9943));

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Courses_CourseId",
                table: "Days",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Courses_CourseId",
                table: "Days");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 12, 9, 15, 32, 20, 931, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 12, 9, 15, 32, 20, 931, DateTimeKind.Utc).AddTicks(1714));

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Courses_CourseId",
                table: "Days",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
