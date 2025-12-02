using System.ComponentModel.DataAnnotations;

namespace WebhookAlbatroz.Entity
{
    public class WebhookSuscripcion
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200), Url]
        public string Url { get; set; } = string.Empty;

        [Required]
        public bool Activo { get; set; } = true;

        [Range(5, 300)]
        public int TimeoutSegundos { get; set; } = 30;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [MaxLength(10)]
        public string? UltimoNitEmisor { get; set; }

        [MaxLength(100)]
        public string? SecretKey { get; set; }

        [Range(1, 10)]
        public int ReintentosMax { get; set; } = 3;

        public DateTime? FechaUltimoEvento { get; set; }

        [MaxLength(32)]
        public string? UltimoTrackId { get; set; }

        public List<WebhookEventoSuscrito> EventosSuscritos { get; set; } = new List<WebhookEventoSuscrito>();
    }

    // ? ELIMINAR ESTA CLASE DUPLICADA:
    // public class EventoRegistro { }

    public class WebhookEventoSuscrito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WebhookSuscripcionId { get; set; }

        [Required]
        public TipoEvento TipoEvento { get; set; }

        public WebhookSuscripcion WebhookSuscripcion { get; set; }
    }

    public enum TipoEvento
    {
        Procesado,              // ? COMA
        ValidadoDian,           // ? COMA
        RechazadoDian,          // ? COMA
        Enviado,                // ? COMA
        Devuelto,               // ? COMA
        Acuse,                  // ? COMA
        Aceptado,               // ? COMA
        Aceptado_Tacitamente,   // ? COMA
        Rechazado               // ? SIN COMA al final
    }
}