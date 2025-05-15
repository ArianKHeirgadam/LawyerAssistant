namespace LawyerAssistant.Application.Extentions;

public static class DayOfWeekExtension
{
    public static int ToPersianDayOfWeekNumber(this DayOfWeek dayOfWeek)
    {
        int result = 0;
        switch (dayOfWeek)
        {
            case DayOfWeek.Saturday:
                result = 1;
                break;
            case DayOfWeek.Sunday:
                result = 2;
                break;
            case DayOfWeek.Monday:
                result = 3;
                break;
            case DayOfWeek.Tuesday:
                result = 4;
                break;
            case DayOfWeek.Wednesday:
                result = 5;
                break;
            case DayOfWeek.Thursday:
                result = 6;
                break;
            case DayOfWeek.Friday:
                result = 7;
                break;

        }
        return result;
    }
    public static int ToDayOfWeekNumber(this DayOfWeek dayOfWeek)
    {
        int result = 0;
        switch (dayOfWeek)
        {
            case DayOfWeek.Saturday:
                result =  1;
                break;
            case DayOfWeek.Sunday:
                result = 2;
                break;
            case DayOfWeek.Monday:
                result = 3;
                break;
            case DayOfWeek.Tuesday:
                result = 4;
                break;
            case DayOfWeek.Wednesday:
                result = 5;
                break;
            case DayOfWeek.Thursday:
                result = 6;
                break;
            case DayOfWeek.Friday:
                result = 7;
                break;

        }
        return result; 
    }
    //================================================================================================
    public static string ToPersianDayName(this DayOfWeek dayOfWeek)
    {
        string result =string.Empty;
        switch (dayOfWeek)
        {
            case DayOfWeek.Saturday:
                result = "شنبه";
                break;
            case DayOfWeek.Sunday:
                result = "یکشنبه";
                break;
            case DayOfWeek.Monday:
                result = "دوشنبه";
                break;
            case DayOfWeek.Tuesday:
                result = "سه شنبه";
                break;
            case DayOfWeek.Wednesday:
                result = "چهارشنبه";
                break;
            case DayOfWeek.Thursday:
                result = "پنجشنبه";
                break;
            case DayOfWeek.Friday:
                result = "جمعه";
                break;
        }
        return result;
    }
    //================================================================================================
    public static string ToGregorianDayName(this DayOfWeek dayOfWeek)
    {
        string result = string.Empty;
        switch (dayOfWeek)
        {
            case DayOfWeek.Saturday:
                result = "Saturday";
                break;
            case DayOfWeek.Sunday:
                result = "Sunday";
                break;
            case DayOfWeek.Monday:
                result = "Monday";
                break;
            case DayOfWeek.Tuesday:
                result = "Tuesday";
                break;
            case DayOfWeek.Wednesday:
                result = "Wednesday";
                break;
            case DayOfWeek.Thursday:
                result = "Thursday";
                break;
            case DayOfWeek.Friday:
                result = "Friday";
                break;
        }
        return result;
    }

}
