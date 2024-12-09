using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaluationBackend.Migrations
{
    /// <inheritdoc />
    public partial class addingDaySeqs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Exercises_ExerciseId",
                table: "DayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Muscles_MuscleId",
                table: "DayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Sets_SetsId",
                table: "DayExercises");

            migrationBuilder.AlterColumn<Guid>(
                name: "SetsId",
                table: "DayExercises",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "MuscleId",
                table: "DayExercises",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExerciseId",
                table: "DayExercises",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                column: "CreationDate",
                value: new DateTime(2024, 11, 14, 21, 12, 59, 669, DateTimeKind.Utc).AddTicks(8537));

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Exercises_ExerciseId",
                table: "DayExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Muscles_MuscleId",
                table: "DayExercises",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Sets_SetsId",
                table: "DayExercises",
                column: "SetsId",
                principalTable: "Sets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Exercises_ExerciseId",
                table: "DayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Muscles_MuscleId",
                table: "DayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_DayExercises_Sets_SetsId",
                table: "DayExercises");

            migrationBuilder.AlterColumn<Guid>(
                name: "SetsId",
                table: "DayExercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MuscleId",
                table: "DayExercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ExerciseId",
                table: "DayExercises",
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
                value: new DateTime(2024, 11, 14, 20, 47, 46, 86, DateTimeKind.Utc).AddTicks(7752));

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Exercises_ExerciseId",
                table: "DayExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Muscles_MuscleId",
                table: "DayExercises",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayExercises_Sets_SetsId",
                table: "DayExercises",
                column: "SetsId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
