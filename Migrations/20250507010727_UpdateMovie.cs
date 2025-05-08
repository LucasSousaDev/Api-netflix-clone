using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace movies_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "Actors",
                table: "Movies",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
