using System.Text.Json.Serialization;

namespace GimenaCreations.Models.MercadoPago;

public class GetPaymentResponse
{
    public string Status { get; set; }

    [JsonPropertyName("external_reference")]
    public string ExternalReference { get; set; }
}