using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalApplication.Module.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllDay = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RecurrenceInfoXml = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RecurrencePatternID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReminderInfoXml = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RemindIn = table.Column<TimeSpan>(type: "time", nullable: true),
                    AlarmTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPostponed = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OptimisticLockField = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Events_Events_RecurrencePatternID",
                        column: x => x.RecurrencePatternID,
                        principalTable: "Events",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pacients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumarTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReportDataV2",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInplaceReport = table.Column<bool>(type: "bit", nullable: false),
                    PredefinedReportTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParametersObjectTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OptimisticLockField = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDataV2", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color_Int = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "EventResource",
                columns: table => new
                {
                    EventsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourcesKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventResource", x => new { x.EventsID, x.ResourcesKey });
                    table.ForeignKey(
                        name: "FK_EventResource_Events_EventsID",
                        column: x => x.EventsID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventResource_Resource_ResourcesKey",
                        column: x => x.ResourcesKey,
                        principalTable: "Resource",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventResource_ResourcesKey",
                table: "EventResource",
                column: "ResourcesKey");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RecurrencePatternID",
                table: "Events",
                column: "RecurrencePatternID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventResource");

            migrationBuilder.DropTable(
                name: "Pacients");

            migrationBuilder.DropTable(
                name: "ReportDataV2");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
