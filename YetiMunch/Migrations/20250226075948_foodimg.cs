using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YetiMunch.Migrations
{
    /// <inheritdoc />
    public partial class foodimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodImgUrl",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodImgUrl",
                table: "Foods");
        }
    }
}
