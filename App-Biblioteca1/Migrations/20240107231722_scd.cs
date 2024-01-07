using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Biblioteca1.Migrations
{
    /// <inheritdoc />
    public partial class scd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Inventories_InventoryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropColumn(
                name: "IdLoans",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "guidsBooks",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "guidBook",
                table: "Loans",
                newName: "guidUser");

            migrationBuilder.RenameColumn(
                name: "ActualReturnDate",
                table: "Loans",
                newName: "CurrentReturnDate");

            migrationBuilder.RenameColumn(
                name: "InventoryId",
                table: "Books",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_InventoryId",
                table: "Books",
                newName: "IX_Books_StoreId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdLoan",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "guidBooks",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BookStores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStored = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityTotal = table.Column<int>(type: "int", nullable: false),
                    IdBook = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStores", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStores_StoreId",
                table: "Books",
                column: "StoreId",
                principalTable: "BookStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStores_StoreId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookStores");

            migrationBuilder.DropColumn(
                name: "IdLoan",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "guidBooks",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "guidUser",
                table: "Loans",
                newName: "guidBook");

            migrationBuilder.RenameColumn(
                name: "CurrentReturnDate",
                table: "Loans",
                newName: "ActualReturnDate");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Books",
                newName: "InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_StoreId",
                table: "Books",
                newName: "IX_Books_InventoryId");

            migrationBuilder.AddColumn<string>(
                name: "IdLoans",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "guidsBooks",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    QuantityTotal = table.Column<int>(type: "int", nullable: false),
                    dateInventory = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Inventories_InventoryId",
                table: "Books",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
