using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Biblioteca1.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdLoans",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdBook",
                table: "StateBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "StateBooks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "IdBook",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLoans",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdBook",
                table: "StateBooks");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "StateBooks");

            migrationBuilder.DropColumn(
                name: "IdBook",
                table: "Inventories");
        }
    }
}
