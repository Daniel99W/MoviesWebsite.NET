using FirebaseAdmin;
using Google;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Application.Commands.Users;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.DAL;
using MoviesAPI.DAL.Repositories;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddScoped<IRepositoryUser, UserRepository>();
builder.Services.AddScoped<IRepositoryMovie, MovieRepository>();
builder.Services.AddScoped<IRepositoryCategory, CategoryRepository>();
builder.Services.AddScoped<IRepositoryMovieCategory, MovieCategoryRepository>();
builder.Services.AddScoped<IRepositoryVotedMovies, VotedMoviesRepository>();
builder.Services.AddScoped<IRepositoryFavoriteMovie, FavoriteMovieRepository>();
builder.Services.AddScoped<IRepositoryMovieTag, MovieTagRepository>();
builder.Services.AddScoped<IRepositoryTag, TagRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly));
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  ));

var mysqlConnString = builder.Configuration["ConnectionStrings:MySqlConn"];

Console.WriteLine(mysqlConnString);

builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options.UseMySql(mysqlConnString,
    ServerVersion.AutoDetect(mysqlConnString));
});
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("moviepiratedweb-firebase-adminsdk-epkfz-9991c3a39a.json")
});

var audience = builder.Configuration["Authentication:Audience"];
var validIssuer = builder.Configuration["Authentication:ValidIssuer"];


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
    {
        jwtOptions.Authority = validIssuer;
        jwtOptions.Audience = audience;
        jwtOptions.TokenValidationParameters.ValidIssuer = validIssuer;
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseHealthChecks("/health");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MoviesDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
