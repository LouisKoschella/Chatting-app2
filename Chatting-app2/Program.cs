using Chatting_app2;
using Chatting_app2.DataModels;
using Chatting_app2.Entities;
using Chatting_app2.MessageHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var defaultConnectionString = config.GetConnectionString("defaultConnection");

builder.Services.AddDbContext<MessageContext>(options =>
    options.UseSqlServer(defaultConnectionString));


builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatsocket");
});

app.MapPost("api/send", (MessageDTO messageDto, MessageContext db, [FromServices]IHubContext<ChatHub> hubContext) =>
{
    var message = new Message
    {
        Id = new Guid(),
        Username = messageDto.Username,
        MessageText = messageDto.MessageText,
        MessageTime = messageDto.MessageTime
    };

    db.Add(message);
    db.SaveChanges();
    
    hubContext.Clients.All.SendAsync("ReceiveOne", messageDto.Username, messageDto.MessageText, messageDto.MessageTime);

    return message;
});

app.MapGet("api/messageHistory", (MessageContext db) =>
{
    return db.Message.Select(x => new MessageDTO()
    {
        MessageText = x.MessageText,
        MessageTime = x.MessageTime,
        Username = x.Username
    }).ToList();
});

app.MapGet("api/messageHistory/{username}", (string username, MessageContext db) =>
{
    return db.Message.Select(x => new MessageDTO()
    {
        MessageText = x.MessageText,
        MessageTime = x.MessageTime,
        Username = x.Username
    }).Where(x=> x.Username == username).ToList();
});

app.Run();