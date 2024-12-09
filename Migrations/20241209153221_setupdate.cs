using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class setupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sets",
                table: "Sets",
                newName: "Name");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sets",
                newName: "Sets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 19, 19, 43, 53, 985, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 19, 19, 43, 53, 985, DateTimeKind.Utc).AddTicks(9801));
        }
    }
}
