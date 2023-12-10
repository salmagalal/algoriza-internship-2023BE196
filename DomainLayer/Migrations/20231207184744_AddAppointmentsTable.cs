using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class AddAppointmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorPrice = table.Column<double>(type: "float", nullable: false),
                    DoctorTimeDoctorId = table.Column<int>(type: "int", nullable: false),
                    DoctorTimeTimeId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_DoctorTimes_DoctorTimeDoctorId_DoctorTimeTimeId",
                        columns: x => new { x.DoctorTimeDoctorId, x.DoctorTimeTimeId },
                        principalTable: "DoctorTimes",
                        principalColumns: new[] { "DoctorId", "TimeId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorTimeDoctorId_DoctorTimeTimeId",
                table: "Appointments",
                columns: new[] { "DoctorTimeDoctorId", "DoctorTimeTimeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
