using FlightApp.Application.Configuration;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Infrastructure.Configuration;
using FlightApp.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
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


//hardcoded dictionary valuess
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


    dbContext.SaveChanges();
}
app.Run();

