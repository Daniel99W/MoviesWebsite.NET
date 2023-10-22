

using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Application.Commands.Users;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.DAL;
using MoviesAPI.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRepositoryUser, UserRepository>();
builder.Services.AddScoped<IRepositoryMovie, MovieRepository>();
builder.Services.AddScoped<IRepositoryCategory, CategoryRepository>();
builder.Services.AddScoped<IRepositoryMovieCategory, MovieCategoryRepository>();
builder.Services.AddScoped<IRepositoryVotedMovies, VotedMoviesRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly));
builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConn"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConn")));
});
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("moviepiratedweb-firebase-adminsdk-epkfz-9991c3a39a.json")
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/health");

app.UseAuthorization();

app.MapControllers();

app.Run();
