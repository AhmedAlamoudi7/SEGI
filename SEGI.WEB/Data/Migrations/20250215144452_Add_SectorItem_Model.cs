using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGI.WEB.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_SectorItem_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "SectorItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "SectorItems");
        }
    }
}
