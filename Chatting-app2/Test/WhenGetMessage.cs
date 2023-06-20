using Chatting_app2.Entities;
using com.sun.xml.@internal.ws.api.message;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Chatting_app2.Test
{
    public class WhenGetMessage
    {

        [Fact]
        public async Task ShouldReturnCorrectResponse()
        {
            // Arrange
        

            // Act
            await using var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(async _ =>
                    {
                        var dbContext = new MockedDb().CreateDbContext();
                        var messages = new List<Entities.Message>()
                        {
                            new Entities.Message
                            {
                                Id = Guid.NewGuid(),
                                MessageText = "test",
                                MessageTime = DateTime.Now
                            },

                            new Entities.Message
                            {
                                Id = Guid.NewGuid(),
                                MessageText = "testv2",
                                MessageTime = DateTime.Now,
                            }
                    };

                        dbContext.Set<Messages>().AddRange((IEnumerable<Messages>)messages);
                        await dbContext.SaveChangesAsync();

                        return dbContext;
                    });

                    services.BuildServiceProvider();
                });
            });
            

            using var client = application.CreateClient();
            var result = await client.GetAsync("/MessageHistory");
            var content = await result.Content.ReadFromJsonAsync<List<MessageDTO>>();

            //Assert
            Assert.NotNull(content);
            Assert.Equal("test", content?.First().MessageText);
        }
    }
}

