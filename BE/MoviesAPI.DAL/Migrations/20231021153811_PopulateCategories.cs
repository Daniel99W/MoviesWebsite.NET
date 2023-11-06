using Microsoft.EntityFrameworkCore.Migrations;
using System.Xml.Linq;

#nullable disable

namespace MoviesAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Action');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Horror');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Animation');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Adventure');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Comedy');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Drama');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Family');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Musical');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'SF');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'War');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Sport');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
