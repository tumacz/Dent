using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveServiceTagDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceTags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceTags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
