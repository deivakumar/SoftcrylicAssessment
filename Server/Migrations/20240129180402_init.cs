using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAssessment.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenantName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisits",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_PatientVisits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientProgressNotes",
                columns: table => new
                {
                    ProgressNoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitId = table.Column<int>(type: "INTEGER", nullable: false),
                    SectionName = table.Column<string>(type: "TEXT", nullable: false),
                    SectionText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProgressNotes", x => x.ProgressNoteId);
                    table.ForeignKey(
                        name: "FK_PatientProgressNotes_PatientVisits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "PatientVisits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "TenantName" },
                values: new object[] { 1, "Alpha" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "TenantName" },
                values: new object[] { 2, "Beta" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "TenantName" },
                values: new object[] { 3, "Gamma" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "City", "CreatedAt", "FirstName", "IsDeleted", "LastName", "State", "TenantId", "UpdatedAt" },
                values: new object[] { 1, "Che", new DateTime(2024, 1, 29, 23, 34, 2, 759, DateTimeKind.Local).AddTicks(8988), "Potter", false, "Harry", "St", 1, null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "City", "CreatedAt", "FirstName", "IsDeleted", "LastName", "State", "TenantId", "UpdatedAt" },
                values: new object[] { 2, "Beng", new DateTime(2024, 1, 29, 23, 34, 2, 759, DateTimeKind.Local).AddTicks(9004), "Hermoine", false, "Gi", "Ka", 1, null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "City", "CreatedAt", "FirstName", "IsDeleted", "LastName", "State", "TenantId", "UpdatedAt" },
                values: new object[] { 3, "Mi", new DateTime(2024, 1, 29, 23, 34, 2, 759, DateTimeKind.Local).AddTicks(9008), "Voltmore", false, "Vi", "Mp", 1, null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "City", "CreatedAt", "FirstName", "IsDeleted", "LastName", "State", "TenantId", "UpdatedAt" },
                values: new object[] { 4, "Tru", new DateTime(2024, 1, 29, 23, 34, 2, 759, DateTimeKind.Local).AddTicks(9010), "Ginger", false, "Mi", "Kl", 1, null });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "City", "CreatedAt", "FirstName", "IsDeleted", "LastName", "State", "TenantId", "UpdatedAt" },
                values: new object[] { 5, "Tru", new DateTime(2024, 1, 29, 23, 34, 2, 759, DateTimeKind.Local).AddTicks(9013), "Benji", false, "Fr", "Ap", 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_PatientProgressNotes_VisitId",
                table: "PatientProgressNotes",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TenantId",
                table: "Patients",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisits_PatientId",
                table: "PatientVisits",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientProgressNotes");

            migrationBuilder.DropTable(
                name: "PatientVisits");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
