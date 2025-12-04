using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebhookAlbatroz.Migrations
{
    /// <inheritdoc />
    public partial class albatros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentosIntegracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    NitEmisor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ContenidoXML = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    FechaDescarga = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UrlOrigen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TamanoBytes = table.Column<long>(type: "bigint", nullable: false),
                    CufeExtraido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroFactura = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosIntegracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventosRegistrados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TipoEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NitEmisor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TrackId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TipoDocumento = table.Column<int>(type: "int", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cufe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlIntegracion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UrlUbl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UrlPDF = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlRespuestaDian = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CodigoMotivo = table.Column<int>(type: "int", nullable: true),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Glosario = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Cude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosRegistrados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleInfoRQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    locField = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    dateBookField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventoField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    channelField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    portalField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnologyField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    includeFlightsField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stringincludeCarsField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    includeHotelsField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    includeInsuranceField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    includePackageField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payload = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleInfoRQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSuscripciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TimeoutSegundos = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UltimoNitEmisor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReintentosMax = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    FechaUltimoEvento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimoTrackId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSuscripciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookEventosSuscritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebhookSuscripcionId = table.Column<int>(type: "int", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEventosSuscritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookEventosSuscritos_WebhookSuscripciones_WebhookSuscripcionId",
                        column: x => x.WebhookSuscripcionId,
                        principalTable: "WebhookSuscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoIntegracion_NitEmisor",
                table: "DocumentosIntegracion",
                column: "NitEmisor");

            migrationBuilder.CreateIndex(
                name: "UX_DocumentoIntegracion_TrackId",
                table: "DocumentosIntegracion",
                column: "TrackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoRegistro_FechaHora",
                table: "EventosRegistrados",
                column: "FechaHora");

            migrationBuilder.CreateIndex(
                name: "IX_EventoRegistro_NitEmisor",
                table: "EventosRegistrados",
                column: "NitEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_EventoRegistro_TrackId",
                table: "EventosRegistrados",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookEventosSuscritos_WebhookSuscripcionId",
                table: "WebhookEventosSuscritos",
                column: "WebhookSuscripcionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentosIntegracion");

            migrationBuilder.DropTable(
                name: "EventosRegistrados");

            migrationBuilder.DropTable(
                name: "SaleInfoRQ");

            migrationBuilder.DropTable(
                name: "WebhookEventosSuscritos");

            migrationBuilder.DropTable(
                name: "WebhookSuscripciones");
        }
    }
}
