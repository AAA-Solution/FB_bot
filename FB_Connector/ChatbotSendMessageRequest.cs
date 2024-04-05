namespace FB_Connector
{
    public class ChatbotSendMessageRequest
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string DeviceId { get; set; }
        public long? UserId { get; set; }
        public string Content { get; set; }
        public string MsgType { get; set; }
        public string PrevCommand { get; set; }
        public string Command { get; set; }
    }
}
