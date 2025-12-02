using System.ComponentModel.DataAnnotations;
using WebhookAlbatroz.Entity;

namespace WebhookAlbatroz.DTOs
{
    public class NotificarEventoRequest
    {
        [Required]
        public TipoEvento TipoEvento { get; set; }

        [Required]
        public WebhookDataDTO Datos { get; set; } = new();
    }
}