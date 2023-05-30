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


app.MapPost("Message",(string message) =>
{ 
    messageList.Add(message); 
    return "message has been successfully send";
    
});

app.MapGet("MessageHistory", () =>
{
    return messageList;
});

app.MapGet("MessageHistory/{messageText}", (string messagetext) =>
{
    var fileteredList = messageList.Where(x => x == messagetext);
    return fileteredList;
});

app.Run();

