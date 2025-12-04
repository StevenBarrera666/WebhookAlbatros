using WebhookAlbatroz.Context;
using System.Xml;
using WebhookAlbatroz.DTOs;
using WebhookAlbatroz.Entity;

using WebhookAlbatroz.Context;

namespace WebhookAlbatroz.Services
{
    public class SaleInfoRQService
    {

        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SaleInfoRQService> _logger;

        public SaleInfoRQService(AppDbContext context, HttpClient httpClient, ILogger<SaleInfoRQService> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task RqService(SaleInfoRQDTO salerq)
        {
            try
            {
                _logger.LogInformation("inicio guardado datos ", salerq);

                var documento = new SaleInfoRQ
                {
                    locField = salerq.locField,
                    dateBookField = salerq.dateBookField,
                    eventoField = salerq.eventoField,
                    channelField = salerq.channelField,
                    portalField = salerq.portalField,
                    TechnologyField = salerq.TechnologyField,
                    includeFlightsField = salerq.includeFlightsField,
                    stringincludeCarsField = salerq.stringincludeCarsField,
                    includeHotelsField = salerq.includeHotelsField,
                    includeInsuranceField = salerq.includeInsuranceField,
                    includePackageField = salerq.includePackageField,
                    payload = salerq.payload
                };

                _context.SaleInfoRQs.Add(documento);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error en RqService: {Message}", ex.Message);
            }


        }
    }
}
