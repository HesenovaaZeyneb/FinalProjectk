using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evergreen_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatetablepatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Patients",
                newName: "Weight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Patients",
                newName: "Width");
        }
    }
}
