using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Biblioteca1.Migrations
{
    /// <inheritdoc />
    public partial class CDFS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_StoreId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_StoreId",
                table: "Books",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_StoreId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_StoreId",
                table: "Books",
                column: "StoreId",
                unique: true);
        }
    }
}
