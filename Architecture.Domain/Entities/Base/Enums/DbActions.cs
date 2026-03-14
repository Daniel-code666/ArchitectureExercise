using System.Text.Json.Serialization;

namespace Architecture.Domain.Entities.Base.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DbActions
    {
        Created = 1,
        NotCreated = 2,
        NotFound = 3,
        NotUpdated = 4,
        Updated = 5,
    }
}
