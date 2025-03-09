using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGI.WEB.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "FixedItems",
                newName: "Logo_White");

            migrationBuilder.AddColumn<string>(
                name: "Logo_Dark",
                table: "FixedItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo_Dark",
                table: "FixedItems");

            migrationBuilder.RenameColumn(
                name: "Logo_White",
                table: "FixedItems",
                newName: "Logo");
        }
    }
}
