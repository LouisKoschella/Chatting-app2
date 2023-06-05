namespace Chatting_app2.Entities
{
    public class Message
    {

        public Guid  Id { get; set; }
        public string Username { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }

    }
}
