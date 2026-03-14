using System.Text.Json.Serialization;

namespace Architecture.Domain.Entities.Invoices.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InvoiceStatus
    {
        Created = 1,
        Sended = 2
    }
}
