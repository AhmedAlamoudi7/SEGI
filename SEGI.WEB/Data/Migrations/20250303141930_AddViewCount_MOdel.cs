using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGI.WEB.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddViewCount_MOdel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Careers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Careers");
        }
    }
}
