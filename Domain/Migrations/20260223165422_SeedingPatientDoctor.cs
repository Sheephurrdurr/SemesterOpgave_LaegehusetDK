using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class SeedingPatientDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpr",
                table: "Patients",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5a06284c-7c2a-48ec-97ef-0900402aed7c"), "Mads Hyttemads" },
                    { new Guid("7b00057b-a70d-4ee9-a964-e080c48d06c2"), "Hans Gylling" },
                    { new Guid("c2eb3589-0cd0-4800-b0b3-f22e7ec69762"), "Grete Gylling" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Cpr", "Name" },
                values: new object[,]
                {
                    { new Guid("c9911795-4faa-4ed3-9199-06bf4961996a"), "0101003022", "Torben Hansen" },
                    { new Guid("e0e56a31-8b2f-4f00-b640-eaa0b52f1f60"), "0202020072", "Mette Mette" },
                    { new Guid("f135eeba-dbe6-46a5-9a78-fdf97b4c2ed5"), "1100390020", "Thue Madsen" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("5a06284c-7c2a-48ec-97ef-0900402aed7c"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("7b00057b-a70d-4ee9-a964-e080c48d06c2"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("c2eb3589-0cd0-4800-b0b3-f22e7ec69762"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("c9911795-4faa-4ed3-9199-06bf4961996a"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("e0e56a31-8b2f-4f00-b640-eaa0b52f1f60"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("f135eeba-dbe6-46a5-9a78-fdf97b4c2ed5"));

            migrationBuilder.AlterColumn<int>(
                name: "Cpr",
                table: "Patients",
                type: "INTEGER",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 10);
        }
    }
}
