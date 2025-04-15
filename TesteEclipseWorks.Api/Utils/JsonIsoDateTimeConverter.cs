using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TesteEclipseWorks.Api.Utils;

[ExcludeFromCodeCoverage]
public class JsonIsoDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString() ?? string.Empty;
        return string.IsNullOrWhiteSpace(dateString) ? DateTime.MinValue : DateTime.Parse(dateString);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        string format = "yyyy-MM-ddTHH:mm:ss.fffffff";
        writer.WriteStringValue(value.ToString(format));
    }
}
