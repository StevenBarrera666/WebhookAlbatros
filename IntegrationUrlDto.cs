using System.ComponentModel.DataAnnotations;

namespace WebhookAlbatroz.Entity
{
    public class WebhookSuscripcion
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Url { get; set; }
        
        [StringLength(500)]
        public string? UrlIntegracion { get; set; } // Nueva propiedad
        
        [StringLength(255)]
        public string? SecretKey { get; set; }
        
        public bool Activo { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime? FechaUltimoEvento { get; set; }
        
        [StringLength(100)]
        public string? UltimoNitEmisor { get; set; }
        
        [StringLength(100)]
        public string? UltimoTrackId { get; set; }
        
        public int TimeoutSegundos { get; set; } = 30;
        
        // Navegación
        public virtual ICollection<EventoSuscripcion> EventosSuscritos { get; set; } = new List<EventoSuscripcion>();
    }
}