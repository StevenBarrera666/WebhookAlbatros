using Microsoft.EntityFrameworkCore;
using WebhookAlbatroz.Entity; 

namespace WebhookAlbatroz.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WebhookSuscripcion> WebhookSuscripciones { get; set; }
        public DbSet<WebhookEventoSuscrito> WebhookEventosSuscritos { get; set; }
        public DbSet<EventoRegistro> EventosRegistrados { get; set; }

        public DbSet<DocumentoIntegracion> DocumentoIntegracion { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            // Configurar conversión de enum a string
            modelBuilder.Entity<EventoRegistro>()
                .Property(e => e.TipoEvento)
                .HasConversion<string>();

            modelBuilder.Entity<WebhookEventoSuscrito>()
                .Property(e => e.TipoEvento)
                .HasConversion<string>();

            // Configurar relaciones
            modelBuilder.Entity<WebhookEventoSuscrito>()
                .HasOne(e => e.WebhookSuscripcion)
                .WithMany(s => s.EventosSuscritos)
                .HasForeignKey(e => e.WebhookSuscripcionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar valores por defecto
            modelBuilder.Entity<WebhookSuscripcion>()
                .Property(s => s.Activo)
                .HasDefaultValue(true);

            modelBuilder.Entity<WebhookSuscripcion>()
                .Property(s => s.FechaCreacion)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<DocumentoIntegracion>()
                .Property(d => d.FechaDescarga)
                .HasDefaultValueSql("NOW()");

            // **CONFIGURACIONES ADICIONALES RECOMENDADAS**

            // Configurar índices para mejorar rendimiento
            modelBuilder.Entity<EventoRegistro>()
                .HasIndex(e => e.TrackId)
                .HasDatabaseName("IX_EventoRegistro_TrackId");

            modelBuilder.Entity<EventoRegistro>()
                .HasIndex(e => e.NitEmisor)
                .HasDatabaseName("IX_EventoRegistro_NitEmisor");

            modelBuilder.Entity<EventoRegistro>()
                .HasIndex(e => e.FechaHora)
                .HasDatabaseName("IX_EventoRegistro_FechaHora");

            modelBuilder.Entity<DocumentoIntegracion>()
                .HasIndex(d => d.TrackId)
                .HasDatabaseName("IX_DocumentoIntegracion_TrackId");

            modelBuilder.Entity<DocumentoIntegracion>()
                .HasIndex(d => d.NitEmisor)
                .HasDatabaseName("IX_DocumentoIntegracion_NitEmisor");

            // Configurar constraints únicos
            modelBuilder.Entity<DocumentoIntegracion>()
                .HasIndex(d => d.TrackId)
                .IsUnique()
                .HasDatabaseName("UX_DocumentoIntegracion_TrackId");

            // Configurar valores por defecto adicionales
            modelBuilder.Entity<EventoRegistro>()
                .Property(e => e.FechaHora)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<WebhookSuscripcion>()
                .Property(s => s.TimeoutSegundos)
                .HasDefaultValue(30);

            modelBuilder.Entity<WebhookSuscripcion>()
                .Property(s => s.ReintentosMax)
                .HasDefaultValue(3);

            // Configurar precisión para campos de texto largos
            modelBuilder.Entity<EventoRegistro>()
                .Property(e => e.Glosario)
                .HasColumnType("TEXT");

            modelBuilder.Entity<DocumentoIntegracion>()
                .Property(d => d.ContenidoXML)
                .HasColumnType("TEXT");

            // Configurar nombres de tablas explícitos (opcional)
            modelBuilder.Entity<WebhookSuscripcion>()
                .ToTable("WebhookSuscripciones");

            modelBuilder.Entity<WebhookEventoSuscrito>()
                .ToTable("WebhookEventosSuscritos");

            modelBuilder.Entity<EventoRegistro>()
                .ToTable("EventosRegistrados");

            modelBuilder.Entity<DocumentoIntegracion>()
                .ToTable("DocumentosIntegracion");

            base.OnModelCreating(modelBuilder);
        }
    }
}