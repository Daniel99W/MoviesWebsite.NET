using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNewCategoriesForNormalWebsite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Funny');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Cats');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Dogs');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Action');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'Horror');");
            migrationBuilder.Sql("insert into movieswebsite.categories(Id, Name) values(UUID(),'SF');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
