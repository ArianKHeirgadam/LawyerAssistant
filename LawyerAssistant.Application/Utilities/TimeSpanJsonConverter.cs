using System.Text.Json;
using System.Text.Json.Serialization;

namespace LawyerAssistant.Application.Utilities;

public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    private const string Format = @"hh\:mm";

    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}
