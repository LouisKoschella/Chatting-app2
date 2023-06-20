using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Chatting_app2
{
  

    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Entities.Message> Message { get; set; }
    }
}
