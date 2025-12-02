using System.ComponentModel.DataAnnotations;

namespace WebhookAlbatroz.Entity
{
    public class EventoRegistro
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaHora { get; set; }

        [Required]
        public TipoEvento TipoEvento { get; set; }

        [MaxLength(10)]
        public string NitEmisor { get; set; } = string.Empty;

        [MaxLength(32)]
        public string TrackId { get; set; } = string.Empty;

        public int? TipoDocumento { get; set; }

        [MaxLength(20)]
        public string NumeroDocumento { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Cufe { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? UrlIntegracion { get; set; }

        [MaxLength(150)]
        public string UrlUbl { get; set; } = string.Empty;

        [MaxLength(150)]
        public string UrlPDF { get; set; } = string.Empty;

        public string Usuario { get; set; } = string.Empty;

        [MaxLength(150)]
        public string UrlRespuestaDian { get; set; } = string.Empty;

        public int? CodigoMotivo { get; set; }

        [MaxLength(500)]
        public string Motivo { get; set; } = string.Empty;

        public string Glosario { get; set; } = string.Empty;

        public string Cude { get; set; } = string.Empty;

    }
}
