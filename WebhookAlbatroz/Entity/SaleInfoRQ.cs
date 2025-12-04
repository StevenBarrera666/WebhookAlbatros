using System.ComponentModel.DataAnnotations;

namespace WebhookAlbatroz.Entity
{
    public class SaleInfoRQ
    {
        [Key]
        public int Id { get; set; }

        public string locField { get; set; } = string.Empty;
        public string dateBookField { get; set; } = string.Empty;
        public string eventoField { get; set; } = string.Empty;
        public string channelField { get; set; } = string.Empty;
        public string portalField { get; set; } = string.Empty;
        public string TechnologyField {get; set; } = string.Empty;
        public string includeFlightsField { get; set; } = string.Empty;
        public string stringincludeCarsField  { get; set; } = string.Empty;
        public string includeHotelsField{ get; set; } = string.Empty;
        public string includeInsuranceField {get; set; } = string.Empty;
        public string includePackageField {get; set; } = string.Empty;
        public string payload {get; set; } = string.Empty;
    }
}
