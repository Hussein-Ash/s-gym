using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class addMessagessss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 19, 19, 42, 28, 711, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Deleted", "FullName", "Img", "ModifiedDate", "Password", "PhoneNumber", "RefreshToken", "Role", "SubId", "UserName" },
                values: new object[] { new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"), new DateTime(2024, 11, 19, 19, 42, 28, 711, DateTimeKind.Utc).AddTicks(7873), false, null, null, null, "12345678", "07816565518", null, 1, null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-43a069619c0e"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 19, 19, 38, 5, 246, DateTimeKind.Utc).AddTicks(4494));
        }
    }
}
