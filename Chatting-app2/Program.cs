using Chatting_app2;
using Chatting_app2.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Data.SqlClient;
using Microsoft.Graph;
internal class Program
{
    private static void Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);
        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        var defaultConnectionString = config.GetConnectionString("defaultConnection");

        builder.Services.AddDbContext<MessageContext>(options =>
                options.UseSqlServer(defaultConnectionString));


        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        List<MessageDTO> messageList = new List<MessageDTO>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapPost("Message", (MessageDTO messageDto, MessageContext db) =>
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

            return "nothing";
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
    }

    private static Action<SqlServerDbContextOptionsBuilder>? GetConnectionString(string v)
    {
        throw new NotImplementedException();
    }


}