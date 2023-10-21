using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedVotedMovieDbSEt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VotedMovies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MovieId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Upvote = table.Column<int>(type: "int", nullable: false),
                    Downvote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotedMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotedMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotedMovies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VotedMovies_MovieId",
                table: "VotedMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_VotedMovies_UserId",
                table: "VotedMovies",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotedMovies");
        }
    }
}
