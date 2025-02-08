using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDentalServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DentalServiceId",
                table: "DentalStudioServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DentalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalServices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DentalStudioServices_DentalServiceId",
                table: "DentalStudioServices",
                column: "DentalServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DentalStudioServices_DentalServices_DentalServiceId",
                table: "DentalStudioServices",
                column: "DentalServiceId",
                principalTable: "DentalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DentalStudioServices_DentalServices_DentalServiceId",
                table: "DentalStudioServices");

            migrationBuilder.DropTable(
                name: "DentalServices");

            migrationBuilder.DropIndex(
                name: "IX_DentalStudioServices_DentalServiceId",
                table: "DentalStudioServices");

            migrationBuilder.DropColumn(
                name: "DentalServiceId",
                table: "DentalStudioServices");
        }
    }
}
