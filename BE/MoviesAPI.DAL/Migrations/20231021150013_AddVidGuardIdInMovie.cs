using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddVidGuardIdInMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VidGuardId",
                table: "Movies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VidGuardId",
                table: "Movies");
        }
    }
}
