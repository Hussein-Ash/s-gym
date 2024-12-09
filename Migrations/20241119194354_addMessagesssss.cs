using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class addMessagesssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"),
                columns: new[] { "CreationDate", "FullName" },
                values: new object[] { new DateTime(2024, 11, 19, 19, 43, 53, 985, DateTimeKind.Utc).AddTicks(9831), "Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                columns: new[] { "CreationDate", "FullName" },
                values: new object[] { new DateTime(2024, 11, 19, 19, 43, 53, 985, DateTimeKind.Utc).AddTicks(9801), "Super" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"),
                columns: new[] { "CreationDate", "FullName" },
                values: new object[] { new DateTime(2024, 11, 19, 19, 42, 28, 711, DateTimeKind.Utc).AddTicks(7873), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                columns: new[] { "CreationDate", "FullName" },
                values: new object[] { new DateTime(2024, 11, 19, 19, 42, 28, 711, DateTimeKind.Utc).AddTicks(7583), null });
        }
    }
}
