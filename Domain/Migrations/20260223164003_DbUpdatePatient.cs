using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdatePatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cpr",
                table: "Patients",
                type: "INTEGER",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ConsultationTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ConsultationTypes",
                keyColumn: "Id",
                keyValue: new Guid("185a3999-762c-4d98-a601-5492b16d3e04"),
                column: "Name",
                value: "Counseling");

            migrationBuilder.UpdateData(
                table: "ConsultationTypes",
                keyColumn: "Id",
                keyValue: new Guid("4adf9313-e990-4bfc-b186-a886179da195"),
                column: "Name",
                value: "Regular");

            migrationBuilder.UpdateData(
                table: "ConsultationTypes",
                keyColumn: "Id",
                keyValue: new Guid("9e6f6390-2a8d-4144-9f3a-01eca22f5fea"),
                column: "Name",
                value: "Vaccination");

            migrationBuilder.UpdateData(
                table: "ConsultationTypes",
                keyColumn: "Id",
                keyValue: new Guid("ce3e1283-08af-4cc4-8b2f-e29b63a7de12"),
                column: "Name",
                value: "Perscription Renewal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpr",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ConsultationTypes");
        }
    }
}
