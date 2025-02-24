using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YetiMunch.Migrations
{
    /// <inheritdoc />
    public partial class Foodandhotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Foods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HotelName",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_HotelId",
                table: "Foods",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Hotels_HotelId",
                table: "Foods",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Hotels_HotelId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_HotelId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "HotelName",
                table: "Foods");
        }
    }
}
