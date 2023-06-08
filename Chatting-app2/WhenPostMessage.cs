using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Chatting_app2.Test;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Graph.Models.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;


namespace Chatting_app2
{

    public class WhenPostMessage
    {
        [Fact]
        public async Task ShouldReturnCorrectResponse()
        {
            
            // Arrange
            await using var context = new MockedDb().CreateDbContext();

            // Act
            await using var application = new WebApplicationFactory<Program>();

            using var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/Message", new MessageDTO { MessageText = "test"});
            var content = await result.Content.ReadFromJsonAsync<Message>();
            //Assert

            //Assert.AreEqual("test", content?. );

            Xunit.Assert.NotNull(content);
        }
    }
}
