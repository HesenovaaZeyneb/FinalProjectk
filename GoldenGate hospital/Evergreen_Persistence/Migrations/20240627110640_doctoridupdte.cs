using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evergreen_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class doctoridupdte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctoId",
                table: "Appointments",
                newName: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointments",
                newName: "DoctoId");
        }
    }
}
