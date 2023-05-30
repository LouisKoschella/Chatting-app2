using Chatting_app2;
using Microsoft.AspNetCore.Mvc;
using System.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

List<Message> messageList = new List<Message>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("Message",(Message message) =>
{ 
    messageList.Add(message); 
    return "message has been successfully send";
    
});

app.MapGet("MessageHistory", () =>
{
    return messageList;
});

app.MapGet("MessageHistory/{username}", (string username) =>
{
    var fileteredList = messageList.Where(x => x.Username == username);
    return fileteredList;
});

app.Run();

