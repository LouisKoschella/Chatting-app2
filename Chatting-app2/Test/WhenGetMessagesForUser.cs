﻿using Chatting_app2.DataModels;
using Chatting_app2.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Chatting_app2.Test
{
    public class WhenGetMessagesForUser
    {
        [Fact]
        public async Task ShouldReturnCorrectResponse()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_ =>
                    {
                        var dbContext = new MockedDb().CreateDbContext();

                        // Add test data
                        var messages = new List<Message>()
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                MessageText = "test",
                                MessageTime = DateTime.Now,
                                Username = "username"
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                MessageText = "testv2",
                                MessageTime = DateTime.Now,
                                Username = "usernamev2"
                            }
                        };

                        dbContext.Set<Message>().AddRange(messages);
                        dbContext.SaveChanges();

                        return dbContext;
                    });
                });
            });


            // Act
            var client = factory.CreateClient();
            var result = await client.GetAsync("/api/messageHistory/username");
            var content = await result.Content.ReadFromJsonAsync<List<MessageDTO>>();

            //Assert
            Assert.NotNull(content);
            Assert.Equal(1, content?.Count);
            Assert.Equal("test", content?.First().MessageText);
        }
    }
}