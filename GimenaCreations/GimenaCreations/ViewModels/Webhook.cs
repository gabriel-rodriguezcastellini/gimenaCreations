using System.Text.Json.Serialization;

namespace GimenaCreations.ViewModels;

public class Webhook
{
    public string Action { get; set; } = null!;

    [JsonPropertyName("api_version")]
    public string ApiVersion { get; set; } = null!;

    public Data Data { get; set; } = null!;

    [JsonPropertyName("date_created")]
    public string DateCreated { get; set; } = null!;

    public long Id { get; set; }

    [JsonPropertyName("live_mode")]
    public bool LiveMode { get; set; }

    public string Type { get; set; } = null!;

    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = null!;
}

public class Data
{
    public string Id { get; set; } = null!;
}