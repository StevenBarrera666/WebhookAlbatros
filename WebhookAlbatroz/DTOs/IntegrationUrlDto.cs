using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebhookAlbatroz.DTOs
{
    public class IntegrationUrlDto
    {
        [JsonPropertyName("url")]
        [Required]
        [StringLength(500)]
        public string Url { get; set; }
        
        [JsonPropertyName("urlIntegracion")]
        [StringLength(500)]
        public string UrlIntegracion { get; set; }
    }
}