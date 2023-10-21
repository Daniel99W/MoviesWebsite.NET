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
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'MILF');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Asia');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'BBC');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'blowjob');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'sex');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'romanian');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'interacial');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
