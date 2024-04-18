namespace Chatting_app2.DataModels
{
    public class MessageHistoryDTO
    {
        public Guid HistoryId  { get; set; }
        public string Username { get; set; }
        public string MessageText { get; set; }

        internal object CreateMessageHistoryDTOContext()
        {
            throw new NotImplementedException();
        }
    }
}
