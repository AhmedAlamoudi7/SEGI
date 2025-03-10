﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEGI.WEB.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Short_Description_Item_NEw_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Short_Description",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Short_Description",
                table: "News");
        }
    }
}
