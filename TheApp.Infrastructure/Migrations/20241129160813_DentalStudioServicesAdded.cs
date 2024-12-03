using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DentalStudioServicesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DentalStudioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sevices_DentalStudios_DentalStudioId",
                        column: x => x.DentalStudioId,
                        principalTable: "DentalStudios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sevices_DentalStudioId",
                table: "Sevices",
                column: "DentalStudioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sevices");
        }
    }
}
