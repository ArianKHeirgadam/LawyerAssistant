namespace LawyerAssistant.Application.Extentions;

public static class GenerateCode
{
    public static string InvoiceNo(this int UserId)
    {
        var random = new Random();
        var RandomOneId = random.Next(0,9).ToString();
        var RandomTowId = random.Next(0, 9).ToString();
        DateTime FirstDate = new DateTime(1900, 01, 01);
        var TotalDays = DateTime.UtcNow.Subtract(FirstDate).TotalDays;
        var DateString = (int)(TotalDays - 42300);
        var TimeString = (int)((TotalDays - (int)TotalDays) * 100000);
        return string.Format("{0}{1}{2}{3}{4}", DateString, RandomOneId, TimeString, RandomTowId , UserId);
    }
}
