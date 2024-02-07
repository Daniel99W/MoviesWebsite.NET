using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTagMovietable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Movies_MovieId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MovieId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "MovieTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MovieId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TagId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieTags_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTags_MovieId",
                table: "MovieTags",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTags_TagId",
                table: "MovieTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieTags");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Tags",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MovieId",
                table: "Tags",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Movies_MovieId",
                table: "Tags",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
