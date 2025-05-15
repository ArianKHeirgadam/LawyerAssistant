namespace LawyerAssistant.Application.Extentions;

public static class TimeExtension
{
    public static string ToTimePersianString( this TimeSpan data)
    { 
        return $"{data.Hours} ساعت و {data.Minutes} دقیقه";
    }


    public static string ToTimeString(this TimeSpan data)
    {
        return string.Format("{0:D2}:{1:D2}", data.Hours, data.Minutes);
    }
}
