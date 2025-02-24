using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YetiMunch.Migrations
{
    /// <inheritdoc />
    public partial class hellno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelID",
                table: "Hotels",
                newName: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "Hotels",
                newName: "HotelID");
        }
    }
}
