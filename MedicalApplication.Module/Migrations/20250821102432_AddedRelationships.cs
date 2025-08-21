using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalApplication.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Denumire",
                table: "Specializari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataOra",
                table: "Programari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "MedicId",
                table: "Programari",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PacientId",
                table: "Programari",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SpecializareId",
                table: "Programari",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Programari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNP",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Medici",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SpecializareId",
                table: "Medici",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programari_MedicId",
                table: "Programari",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Programari_PacientId",
                table: "Programari",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_Programari_SpecializareId",
                table: "Programari",
                column: "SpecializareId");

            migrationBuilder.CreateIndex(
                name: "IX_Medici_SpecializareId",
                table: "Medici",
                column: "SpecializareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medici_Specializari_SpecializareId",
                table: "Medici",
                column: "SpecializareId",
                principalTable: "Specializari",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Programari_Medici_MedicId",
                table: "Programari",
                column: "MedicId",
                principalTable: "Medici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programari_Pacienti_PacientId",
                table: "Programari",
                column: "PacientId",
                principalTable: "Pacienti",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programari_Specializari_SpecializareId",
                table: "Programari",
                column: "SpecializareId",
                principalTable: "Specializari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medici_Specializari_SpecializareId",
                table: "Medici");

            migrationBuilder.DropForeignKey(
                name: "FK_Programari_Medici_MedicId",
                table: "Programari");

            migrationBuilder.DropForeignKey(
                name: "FK_Programari_Pacienti_PacientId",
                table: "Programari");

            migrationBuilder.DropForeignKey(
                name: "FK_Programari_Specializari_SpecializareId",
                table: "Programari");

            migrationBuilder.DropIndex(
                name: "IX_Programari_MedicId",
                table: "Programari");

            migrationBuilder.DropIndex(
                name: "IX_Programari_PacientId",
                table: "Programari");

            migrationBuilder.DropIndex(
                name: "IX_Programari_SpecializareId",
                table: "Programari");

            migrationBuilder.DropIndex(
                name: "IX_Medici_SpecializareId",
                table: "Medici");

            migrationBuilder.DropColumn(
                name: "DataOra",
                table: "Programari");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Programari");

            migrationBuilder.DropColumn(
                name: "PacientId",
                table: "Programari");

            migrationBuilder.DropColumn(
                name: "SpecializareId",
                table: "Programari");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Programari");

            migrationBuilder.DropColumn(
                name: "CNP",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "SpecializareId",
                table: "Medici");

            migrationBuilder.AlterColumn<string>(
                name: "Denumire",
                table: "Specializari",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Medici",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
