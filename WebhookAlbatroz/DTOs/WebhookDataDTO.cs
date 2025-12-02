using WebhookAlbatroz.Entity;

namespace WebhookAlbatroz.DTOs
{
    public class WebhookDataDTO
    {

        public TipoEvento TipoEvento { get; set; }

        public string? TrackId { get; set; }
        public string? NitEmisor { get; set; }

        public int? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Cufe { get; set; }
        public string? UrlIntegracion { get; set; }

        public string? UrlUbl { get; set; }
        public string? UrlPDF { get; set; }
        public string? UrlRespuestaDian { get; set; }
        public string? usuario { get; set; }

        public int? CodigoMotivo { get; set; }
        public string? Motivo { get; set; }
        public string? Glosario { get; set; }

        public string? Cude { get; set; }
    }
}