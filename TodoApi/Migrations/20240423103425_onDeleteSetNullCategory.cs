using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class onDeleteSetNullCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Categories_CategoryId",
                table: "ToDos");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Categories_CategoryId",
                table: "ToDos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Categories_CategoryId",
                table: "ToDos");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Categories_CategoryId",
                table: "ToDos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
