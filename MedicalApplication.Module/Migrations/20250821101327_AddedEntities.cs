using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalApplication.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacients",
                table: "Pacients");

            migrationBuilder.RenameTable(
                name: "Pacients",
                newName: "Pacienti");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacienti",
                table: "Pacienti",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Medici",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programari",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializari",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializari", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medici");

            migrationBuilder.DropTable(
                name: "Programari");

            migrationBuilder.DropTable(
                name: "Specializari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacienti",
                table: "Pacienti");

            migrationBuilder.RenameTable(
                name: "Pacienti",
                newName: "Pacients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacients",
                table: "Pacients",
                column: "id");
        }
    }
}
