using System.Text.Json.Serialization;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Services;
using F1Predict.Context;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Data;
using F1Predict.Domain.Models;
using F1Predict.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSingleton<F1PredictContext>(_ => DataSeeder.Seed());   // одно хранилище на всё приложение

builder.Services.AddScoped<IRepository<Constructor>, ConstructorRepository>();
builder.Services.AddScoped<IRepository<Driver>, DriverRepository>();
builder.Services.AddScoped<IRepository<DriverResult>, DriverResultRepository>();
builder.Services.AddScoped<IRepository<GrandPrix>, GrandPrixRepository>();
builder.Services.AddScoped<IRepository<Prediction>, PredictionRepository>();
builder.Services.AddScoped<IRepository<Session>, SessionRepository>();
builder.Services.AddScoped<IRepository<Title>, TitleRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<UserInChampionship>, UserInChampionshipRepository>();
builder.Services.AddScoped<IRepository<UsersChampionship>, UsersChampionshipRepository>();

builder.Services.AddScoped<IConstructorService, ConstructorService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IDriverResultService, DriverResultService>();
builder.Services.AddScoped<IGrandPrixService, GrandPrixService>();
builder.Services.AddScoped<IPredictionService, PredictionService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInChampionshipService, UserInChampionshipService>();
builder.Services.AddScoped<IUsersChampionshipService, UsersChampionshipService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();