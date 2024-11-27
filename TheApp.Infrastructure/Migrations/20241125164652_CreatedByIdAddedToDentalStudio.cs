using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByIdAddedToDentalStudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "DentalStudios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DentalStudios_CreatedById",
                table: "DentalStudios",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_DentalStudios_AspNetUsers_CreatedById",
                table: "DentalStudios",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DentalStudios_AspNetUsers_CreatedById",
                table: "DentalStudios");

            migrationBuilder.DropIndex(
                name: "IX_DentalStudios_CreatedById",
                table: "DentalStudios");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "DentalStudios");
        }
    }
}
