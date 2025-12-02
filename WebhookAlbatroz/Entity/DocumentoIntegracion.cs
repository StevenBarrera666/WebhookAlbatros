using System.ComponentModel.DataAnnotations;

namespace WebhookAlbatroz.Entity
{
    public class DocumentoIntegracion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string TrackId { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string NitEmisor { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TipoDocumento { get; set; } = string.Empty; // XML, PDF, etc.

        [Required]
        public string ContenidoXML { get; set; } = string.Empty;

        [Required]
        public DateTime FechaDescarga { get; set; }

        [MaxLength(500)]
        public string? UrlOrigen { get; set; }

        public long TamanoBytes { get; set; }

        // Información extraída del XML
        [MaxLength(100)]
        public string? CufeExtraido { get; set; }

        [MaxLength(20)]
        public string? NumeroFactura { get; set; }
    }
}
