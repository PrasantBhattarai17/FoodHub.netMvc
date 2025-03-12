using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YetiMunch.Migrations
{
    /// <inheritdoc />
    public partial class afterbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropColumn(
                name: "HotelName",
                table: "Foods");

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

            migrationBuilder.AddColumn<string>(
                name: "HotelName",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_Food_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_HotelId",
                table: "Food",
                column: "HotelId");
        }
    }
}
