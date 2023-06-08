namespace Chatting_app2
{
    public class MessageDTO
    {
       
        public string Username { get; set; }
        public string MessageText { get; set; }
        public  DateTime MessageTime { get; set; }

        internal object CreateMessageDTOContext()
        {
            throw new NotImplementedException();
        }
    }
}
