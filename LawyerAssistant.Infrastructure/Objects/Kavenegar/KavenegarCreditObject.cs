using System.Text.Json.Serialization;

namespace LawyerAssistant.Infrastructure.Objects.Kavenegar;

internal class KavenegarCreditObject
{
    [JsonPropertyName("return")]
    public KavenegarCreditReturnStatus Return { get; set; }
    public KavenegarCreditEntries entries { get; set; }
}

public class KavenegarCreditEntries
{
    public int remaincredit { get; set; }
    public string expiredate { get; set; }
    public string type { get; set; }
}

public class KavenegarCreditReturnStatus
{
    public int status { get; set; }
    public string message { get; set; }
}
