using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class SeedingPatientDoctorAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("23a85b5a-b1ba-4c8b-9058-912d37d76ab7"), "Hans Gylling" },
                    { new Guid("881f9667-d4f1-4d49-aedd-67ad6da01c71"), "Grete Gylling" },
                    { new Guid("fd0ddfda-79b0-4d4f-9f46-720a5223586c"), "Mads Hyttemads" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Cpr", "Name" },
                values: new object[,]
                {
                    { new Guid("a0d535b1-1488-42fb-93cf-769c557ef393"), "1100390020", "Thue Madsen" },
                    { new Guid("a3b0f1c3-6455-4c7d-8041-77df9a5a1f79"), "0202020072", "Mette Mette" },
                    { new Guid("d1a6a056-3302-4adb-8d32-57ad2d922ddc"), "0101003022", "Torben Hansen" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("23a85b5a-b1ba-4c8b-9058-912d37d76ab7"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("881f9667-d4f1-4d49-aedd-67ad6da01c71"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("fd0ddfda-79b0-4d4f-9f46-720a5223586c"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("a0d535b1-1488-42fb-93cf-769c557ef393"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("a3b0f1c3-6455-4c7d-8041-77df9a5a1f79"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("d1a6a056-3302-4adb-8d32-57ad2d922ddc"));

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
    }
}
