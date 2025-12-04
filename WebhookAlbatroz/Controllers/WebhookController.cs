using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebhookAlbatroz.DTOs;
using WebhookAlbatroz.Services;
using WebhookAlbatroz.Entity;

namespace WebhookAlbatroz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly SaleInfoRQService _saleInfoRQ;
        private readonly WebhookService _webhookService;
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(WebhookService webhookService, SaleInfoRQService saleInfoRQ, ILogger<WebhookController> logger)
        {
            _saleInfoRQ = saleInfoRQ;
            _webhookService = webhookService;
            _logger = logger;
        }

        [HttpPost("notificar")]
        public async Task<IActionResult> NotificarEvento([FromBody] NotificarEventoRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido recibido: ", ModelState);
                return BadRequest(new
                {
                    mensaje = "Datos inválidos",
                    errores = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() 
                    )
                });
            }



            try
            {
                _logger.LogInformation("Notificando evento {TipoEvento} para NIT {NitEmisor}",
                    request.TipoEvento, request.Datos?.NitEmisor ?? "No especificado");

                await _webhookService.NotificarEvento(request.TipoEvento, request.Datos);

                return Ok(new
                {
                    mensaje = "Evento notificado exitosamente",
                    tipoEvento = request.TipoEvento.ToString(),
                    timestamp = DateTime.UtcNow,
                    datos = new
                    {
                        nitEmisor = request.Datos?.NitEmisor,
                        trackId = request.Datos?.TrackId
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error notificando evento {TipoEvento}. Inner Exception: {InnerException}",
            request.TipoEvento, ex.InnerException?.Message ?? "No inner exception");

                return StatusCode(500, new
                {
                    error = "Error interno del servidor",
                    detalle = ex.Message,
                    innerException = ex.InnerException?.Message, // Agregar inner exception
                    timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpPost("notificarDocumento")]
        public async Task<IActionResult> NotificarDocumento([FromBody] SaleInfoRQDTO request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido recibido: ", ModelState);
                return BadRequest(new
                {
                    mensaje = "Datos inválidos",
                    errores = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            try
            {
                await _saleInfoRQ.RqService(request);

                return Ok(new
                {
                    mensaje = "Evento notificado exitosamente",
                    timestamp = DateTime.UtcNow,
                    datos = new
                    {
                        locField = request.locField
              
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error notificando documento. Inner Exception: {InnerException}",
                    ex.InnerException?.Message ?? "No inner exception");

                return StatusCode(500, new
                {
                    error = "Error interno del servidor",
                    detalle = ex.Message,
                    innerException = ex.InnerException?.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpPut("evento/track/{trackId}")]
        public async Task<IActionResult> ActualizarEventoPorTrackId(string trackId, [FromBody] WebhookDataDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    mensaje = "Datos inválidos",
                    errores = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            try
            {
                // Actualizar el TrackId en datos si no coincide
                if (string.IsNullOrEmpty(datos.TrackId))
                    datos.TrackId = trackId;

                var actualizado = await _webhookService.ActualizarEventoPorTrackId(datos);

                if (actualizado)
                {
                    return Ok(new
                    {
                        mensaje = "EventoRegistro actualizado exitosamente",
                        trackId = trackId,
                        timestamp = DateTime.UtcNow
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        mensaje = "No se encontró EventoRegistro para el TrackId proporcionado",
                        trackId = trackId
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando EventoRegistro para TrackId: {TrackId}", trackId);
                return StatusCode(500, new
                {
                    error = "Error interno del servidor",
                    detalle = ex.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }



        [HttpGet("eventos")]
        public IActionResult GetEventos()
        {
            var eventos = Enum.GetValues<TipoEvento>()
                .Select(e => new
                {
                    timestamp = DateTime.UtcNow,
                    nombre = e.ToString(),
                    valor = (int)e,
                    descripcion = GetEventoDescripcion(e)
                })
                .ToList();

            return Ok(new
            {
                mensaje = "Tipos de eventos disponibles",
                eventos = eventos
            });
        }

        private static string GetEventoDescripcion(TipoEvento evento)
        {
            return evento switch
            {
                TipoEvento.Procesado => "Documento procesado exitosamente",
                TipoEvento.ValidadoDian => "Documento validado por la DIAN",
                TipoEvento.RechazadoDian => "Documento rechazado por la DIAN",
                TipoEvento.Enviado => "Documento enviado",
                TipoEvento.Devuelto => "Documento devuelto",
                TipoEvento.Acuse => "Acuse de recibo",
                TipoEvento.Aceptado => "Documento aceptado",
                TipoEvento.Aceptado_Tacitamente => "Documento aceptado tácitamente",
                TipoEvento.Rechazado => "Documento rechazado",
                _ => "Evento desconocido"
            };
        }
    }
}