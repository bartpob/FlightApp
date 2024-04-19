using FlightApp.Application.Configuration;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Infrastructure.Configuration;
using FlightApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//hardcoded dictionary valuess and identity user
var scope = app.Services.CreateScope();
using (var dbContext =scope.ServiceProvider.GetRequiredService<FlightAppDbContext>())
{
    dbContext.Airports.Add(Airport.Create("WAW", "Warsaw", "Poland"));
    dbContext.Airports.Add(Airport.Create("VIE", "Vienna", "Austria"));
    dbContext.Airports.Add(Airport.Create("VGO", "Vigo", "Spain"));
    dbContext.Airports.Add(Airport.Create("VNO", "Vilnius", "Lithuania"));
    dbContext.Airports.Add(Airport.Create("WAS", "Washington", "USA"));
    dbContext.Airports.Add(Airport.Create("WRO", "Wrocław", "Poland"));


    dbContext.AirplaneTypes.Add(AirplaneType.Create("DREAMLINER"));
    dbContext.AirplaneTypes.Add(AirplaneType.Create("AIRBUS"));
    dbContext.AirplaneTypes.Add(AirplaneType.Create("BOEING"));
    var hasher = new PasswordHasher<IdentityUser>();
    dbContext.Users.Add(new IdentityUser
    {
        Id = "9370d875-da5b-49b4-a351-965c5db1f486",
        Email = "admin@flightapp.com",
        NormalizedEmail = "ADMIN@FLIGHTAPP.COM",
        UserName = "admin@flightapp.com",
        NormalizedUserName = "ADMIN@FLIGHTAPP.COM",
        PasswordHash = hasher.HashPassword(null, "P@ssword1")
    });

    dbContext.Roles.Add(new IdentityRole
    {
        Name = "Administrator",
        NormalizedName = "ADMINISTRATOR",
        Id = "e809ff18-5df8-4da2-bb09-97369e28e432"
    });

    dbContext.UserRoles.Add(
        new IdentityUserRole<string>
        {
            RoleId = "e809ff18-5df8-4da2-bb09-97369e28e432",
            UserId = "9370d875-da5b-49b4-a351-965c5db1f486"
        });
    dbContext.SaveChanges();
}
app.Run();

