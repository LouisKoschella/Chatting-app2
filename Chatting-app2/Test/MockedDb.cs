using Microsoft.EntityFrameworkCore;

namespace Chatting_app2.Test
{
    public class MockedDb : IDbContextFactory<MessageContext>
    {

        public MessageContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<MessageContext>()
                .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new MessageContext(options);
        }
    }
}
