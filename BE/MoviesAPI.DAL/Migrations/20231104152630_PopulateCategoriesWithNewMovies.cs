using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategoriesWithNewMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'MILF');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Sex');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'OnlyFans');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Asia');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'BBC');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Threesome');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Gangbang');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Creampie');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'POV');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Anal');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
