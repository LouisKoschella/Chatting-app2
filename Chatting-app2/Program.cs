using Microsoft.AspNetCore.Mvc;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

List<string> messageList = new List<string>();
DateTime currentDateTime = DateTime.Now;
Console.WriteLine(", Current date and time: " + currentDateTime);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/Messag3", () =>
{
    return messageList ;
})
.WithName("GetMessage")
.WithOpenApi();

app.MapPost("send-Message",() =>
{
    string message = "Hi, how r u ? "; 
    messageList.Add(message); 
    return "message has been successfully send";
    
});

app.MapGet("MessageHistory", () =>
{
    return messageList;
});

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}