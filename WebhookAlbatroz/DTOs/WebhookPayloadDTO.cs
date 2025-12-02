namespace WebhookAlbatroz.DTOs
{
    public class WebhookPayloadDTO
    {
        public string Evento { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public WebhookDataDTO Data { get; set; } = new();
    }
}