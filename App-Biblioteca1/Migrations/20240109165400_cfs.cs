using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Biblioteca1.Migrations
{
    /// <inheritdoc />
    public partial class cfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLoan",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdBook",
                table: "StateBooks");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "StateBooks");

            migrationBuilder.DropColumn(
                name: "guidBooks",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "guidUser",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookStore");

            migrationBuilder.DropColumn(
                name: "StateBookId",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdLoan",
                table: "Users",
                type: "uniqueidentifier",
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

            migrationBuilder.AddColumn<Guid>(
                name: "guidBooks",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "guidUser",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "BookStore",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StateBookId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
