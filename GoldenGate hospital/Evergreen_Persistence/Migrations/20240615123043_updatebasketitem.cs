using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evergreen_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatebasketitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_UserId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_UserId1",
                table: "BasketItems",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserId1",
                table: "BasketItems",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserId1",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_UserId1",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BasketItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UsertId",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_UserId",
                table: "BasketItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserId",
                table: "BasketItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
