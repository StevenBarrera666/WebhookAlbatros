using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebhookAlbatroz.Context;
using System.Xml;
using WebhookAlbatroz.DTOs;
using WebhookAlbatroz.Entity;

namespace WebhookAlbatroz.Services
{
    public class WebhookService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly ILogger<WebhookService> _logger;

        public WebhookService(AppDbContext context, HttpClient httpClient, ILogger<WebhookService> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _logger = logger;
        }


        private async Task<byte[]> DescargarDocumentoDesdeUrl(string url)
        {
            try
            {
                _logger.LogInformation("Descargando documento desde URL: {Url}", url);

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var documentoBytes = await response.Content.ReadAsByteArrayAsync();

                _logger.LogInformation("Documento descargado exitosamente. Tamaño: {Bytes} bytes", documentoBytes.Length);

                return documentoBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error descargando documento desde URL: {Url}", url);
                throw;
            }
        }


        public async Task NotificarEvento(TipoEvento tipoEvento, WebhookDataDTO datosEvento)
        {
            try
            {
                _logger.LogInformation("Iniciando notificación de evento ", tipoEvento);

                if (tipoEvento == TipoEvento.Procesado)
                {
                    var eventoProcesado = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,

                    };

                    _context.EventosRegistrados.Add(eventoProcesado);
                    await _context.SaveChangesAsync();

                }


                if (tipoEvento == TipoEvento.ValidadoDian)
                {
                    var eventoValidadoDian = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        Cufe = datosEvento.Cufe,
                        UrlIntegracion = datosEvento.UrlIntegracion,
                        UrlUbl = datosEvento.UrlUbl,
                        UrlPDF = datosEvento.UrlPDF,
                        UrlRespuestaDian = datosEvento.UrlRespuestaDian,
                        CodigoMotivo = datosEvento.CodigoMotivo,
                        Motivo = datosEvento.Motivo,
                        Glosario = datosEvento.Glosario,

                    };







                    _context.EventosRegistrados.Add(eventoValidadoDian);
                    await _context.SaveChangesAsync();

                    if (!string.IsNullOrEmpty(datosEvento.UrlIntegracion))
                    {
                        try
                        {
                            _logger.LogInformation("Iniciando descarga de XML para TrackId: {TrackId}", datosEvento.TrackId);

                            var documentoBytes = await DescargarDocumentoDesdeUrl(datosEvento.UrlIntegracion);
                            await ProcesarDocumentoXML(documentoBytes, datosEvento);

                            _logger.LogInformation("XML descargado y procesado exitosamente para TrackId: {TrackId}", datosEvento.TrackId);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error descargando/procesando XML para TrackId: {TrackId}", datosEvento.TrackId);
                            
                        }
                    }

                   
                    else
                    {
                        _logger.LogWarning("No se proporcionó UrlIntegracion para descargar XML. TrackId: {TrackId}", datosEvento.TrackId);
                    }

                }

                if(tipoEvento == TipoEvento.RechazadoDian)
                {
                    var eventoRechazadoDian = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        Cufe = datosEvento.Cufe,
                        UrlIntegracion = datosEvento.UrlIntegracion,
                        UrlUbl = datosEvento.UrlUbl,
                        UrlPDF = datosEvento.UrlPDF,
                        UrlRespuestaDian = datosEvento.UrlRespuestaDian,
                        CodigoMotivo = datosEvento.CodigoMotivo,
                        Motivo = datosEvento.Motivo,
                        Glosario = datosEvento.Glosario,

                    };


                    _context.EventosRegistrados.Add(eventoRechazadoDian);
                    await _context.SaveChangesAsync();

                }

                if(tipoEvento == TipoEvento.Enviado)
                {
                    var eventoEnviado = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        Usuario = datosEvento.usuario,
                        CodigoMotivo = datosEvento.CodigoMotivo,
                        //IdEnvio = datosEvento.IdEnvio,

                    };


                    _context.EventosRegistrados.Add(eventoEnviado);
                    await _context.SaveChangesAsync();


                }

                if (tipoEvento == TipoEvento.Devuelto)
                {
                    var eventoDevuelto = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        Cufe = datosEvento.Cufe,
                        //Email = datosEvento.usuario,
                        CodigoMotivo = datosEvento.CodigoMotivo,
                        Motivo = datosEvento.Motivo,
                        Glosario = datosEvento.Glosario,
                        //IdEnvio = datosEvento.IdEnvio,

                    };


                    _context.EventosRegistrados.Add(eventoDevuelto);
                    await _context.SaveChangesAsync();
                }

                if (tipoEvento==TipoEvento.Acuse)
                {
                    var eventoAcuse = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        CodigoMotivo = datosEvento.CodigoMotivo,

                        //Email = datosEvento.usuario,
                        //IdEnvio = datosEvento.IdEnvio,


                    };

                    _context.EventosRegistrados.Add(eventoAcuse);
                    await _context.SaveChangesAsync();

                }

                if (tipoEvento == TipoEvento.Aceptado)
                {
                    var eventoAceptado = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        Cufe = datosEvento.Cufe,
                        Glosario = datosEvento.Glosario,
                        Cude = datosEvento.Cude
                    };

                    _context.EventosRegistrados.Add(eventoAceptado);
                    await _context.SaveChangesAsync();
                }

                if (tipoEvento == TipoEvento.Aceptado_Tacitamente) {


                    var eventoAceptadoTacitamente = new EventoRegistro
                    {
                        FechaHora = DateTime.UtcNow,
                        TipoEvento = tipoEvento,
                        NitEmisor = datosEvento.NitEmisor,
                        TrackId = datosEvento.TrackId,
                        TipoDocumento = datosEvento.TipoDocumento,
                        NumeroDocumento = datosEvento.NumeroDocumento,
                        Cufe = datosEvento.Cufe,
                        UrlIntegracion = datosEvento.UrlIntegracion,
                        UrlUbl = datosEvento.UrlUbl,
                        UrlPDF = datosEvento.UrlPDF,
                        UrlRespuestaDian = datosEvento.UrlRespuestaDian,
                        //Email = datosEvento.usuario,
                        CodigoMotivo = datosEvento.CodigoMotivo,
                        Motivo = datosEvento.Motivo,
                        Glosario = datosEvento.Glosario,
                        Cude = datosEvento.Cude

                    };

                    _context.EventosRegistrados.Add(eventoAceptadoTacitamente);
                    await _context.SaveChangesAsync();
                }

                




            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error guardando evento en historial. Datos: {@Datos}");
                throw;
            }
        }



        private async Task ProcesarDocumentoXML(byte[] documentoBytes, WebhookDataDTO datos)
        {
            _logger.LogInformation("Procesando documento XML para {TrackId}", datos.TrackId);

            var xmlContent = System.Text.Encoding.UTF8.GetString(documentoBytes);

            try
            {
                // Verificar si ya existe el documento para evitar duplicados
                var documentoExistente = await _context.DocumentoIntegracion
                    .FirstOrDefaultAsync(d => d.TrackId == datos.TrackId);

                if (documentoExistente != null)
                {
                    _logger.LogWarning("Ya existe un documento XML para TrackId: {TrackId}", datos.TrackId);
                    return;
                }

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);

                // **CONFIGURAR NAMESPACE MANAGER para XML con prefijos**
                var nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsMgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                nsMgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");

                // Extraer información del XML usando el namespace manager
                string cufeExtraido = null;
                string numeroFactura = null;

                var cufeNode = xmlDoc.SelectSingleNode("//cbc:UUID", nsMgr);
                if (cufeNode != null)
                {
                    cufeExtraido = cufeNode.InnerText;
                    _logger.LogInformation("CUFE encontrado en XML: {CUFE}", cufeExtraido);
                }

                var numeroFacturaNode = xmlDoc.SelectSingleNode("//cbc:ID", nsMgr);
                if (numeroFacturaNode != null)
                {
                    numeroFactura = numeroFacturaNode.InnerText;
                    _logger.LogInformation("Número de factura encontrado en XML:", numeroFactura);
                }

                // Guardar XML en base de datos
                var documentoXml = new DocumentoIntegracion
                {
                    TrackId = datos.TrackId ?? "UNKNOWN",
                    NitEmisor = datos.NitEmisor ?? "UNKNOWN",
                    TipoDocumento = "XML",
                    ContenidoXML = xmlContent,
                    FechaDescarga = DateTime.UtcNow,
                    UrlOrigen = datos.UrlIntegracion,
                    TamanoBytes = documentoBytes.Length,
                    CufeExtraido = cufeExtraido,
                    NumeroFactura = numeroFactura
                };

                _context.DocumentoIntegracion.Add(documentoXml);
                await _context.SaveChangesAsync();

                _logger.LogInformation("XML guardado en base de datos para TrackId: {TrackId}, Tamaño: {Bytes} bytes",
                    datos.TrackId, documentoBytes.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parseando/guardando XML para {TrackId}", datos.TrackId);
                throw;
            }
        }




        public async Task ActualizarDatosSuscripcion(WebhookSuscripcion suscripcion, WebhookDataDTO datos)
        {
            try
            {
                suscripcion.FechaUltimoEvento = DateTime.UtcNow;
                suscripcion.UltimoNitEmisor = datos.NitEmisor;
                suscripcion.UltimoTrackId = datos.TrackId;

                await _context.SaveChangesAsync();
                _logger.LogDebug("Suscripción {SuscripcionId} actualizada", suscripcion.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando suscripción {SuscripcionId}", suscripcion.Id);
                throw;
            }
        }

        public async Task<bool> ActualizarSuscripcionPorTrackId(WebhookDataDTO datos)
        {
            try
            {
                if (string.IsNullOrEmpty(datos.TrackId))
                {
                    _logger.LogWarning("TrackId vacío o nulo, no se puede actualizar la suscripción");
                    return false;
                }

                var suscripcion = await _context.WebhookSuscripciones
                    .FirstOrDefaultAsync(s => s.UltimoTrackId == datos.TrackId);

                if (suscripcion == null)
                {
                    _logger.LogWarning("No se encontró suscripción para el TrackId: {TrackId}", datos.TrackId);
                    return false;
                }

                await ActualizarDatosSuscripcion(suscripcion, datos);
                _logger.LogInformation("Suscripción actualizada correctamente para TrackId: {TrackId}", datos.TrackId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando suscripción para TrackId: {TrackId}", datos.TrackId);
                throw;
            }
        }

        //private async Task EnviarWebhook(WebhookSuscripcion suscripcion, TipoEvento evento, WebhookDataDTO datos)
        //{
        //    var payload = new WebhookPayloadDTO
        //    {
        //        Evento = evento.ToString(),
        //        Timestamp = DateTime.UtcNow,
        //        Data = datos
        //    };

        //    var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    });

        //    using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        //    if (!string.IsNullOrEmpty(suscripcion.SecretKey))
        //    {
        //        content.Headers.Add("X-Webhook-Secret", suscripcion.SecretKey);
        //    }

        //    // Configurar timeout específico
        //    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(suscripcion.TimeoutSegundos));

        //    try
        //    {
        //        var response = await _httpClient.PostAsync(suscripcion.Url, content, cts.Token);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            _logger.LogInformation("Webhook enviado exitosamente a {Url} - Status: {StatusCode}", 
        //                suscripcion.Url, response.StatusCode);
        //        }
        //        else
        //        {
        //            _logger.LogWarning("Error enviando webhook a {Url} - Status: {StatusCode}", 
        //                suscripcion.Url, response.StatusCode);
        //        }
        //    }
        //    catch (TaskCanceledException)
        //    {
        //        _logger.LogWarning(" Timeout enviando webhook a {Url} después de {Timeout}s", 
        //            suscripcion.Url, suscripcion.TimeoutSegundos);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, " Excepción enviando webhook a {Url}", suscripcion.Url);
        //    }
        //}
    }
}