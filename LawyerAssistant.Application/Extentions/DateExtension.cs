using Application.Extentions;
using LawyerAssistant.Application.Objects;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace LawyerAssistant.Application.Extentions;

public static class DateTimeExtension
{
    //===================================================================
    public static string ToDateShortFormatString(this DateTime date, IOptions<AppConfig> appConfigOptions)
    {
        string Year = string.Empty, Month = string.Empty, Day = string.Empty;
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                var persianCalendar = new PersianCalendar();
                Year = persianCalendar.GetYear(date).ToString();
                Month = persianCalendar.GetMonth(date).ToString("00");
                Day = persianCalendar.GetDayOfMonth(date).ToString("00");
                break;
            case 2:
                Year = date.Year.ToString();
                Month = date.Month.ToString("00");
                Day = date.Day.ToString("00");
                break;
        }
        return string.Format("{0:D4}/{1:D2}/{2:D2}", Year, Month, Day); ;
    }
    //======================================================================
    public static DateTime ToLocalDateTime(this DateTime utcTime, IOptions<AppConfig> appConfigOptions)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(appConfigOptions.Value.timeZone);
        if (timeZone.IsDaylightSavingTime(utcTime) && !appConfigOptions.Value.isApplyDaylightSaving)
        {
            utcTime = utcTime.AddHours(-1);
        }
        var convertedTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
        return convertedTime;
    }
    //===================================================================

    public static DateTime ToUtcDateTime(this DateTime dateTime, IOptions<AppConfig> appConfigOptions)
    {
        try
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(appConfigOptions.Value.timeZone);
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
            if (timeZone.IsDaylightSavingTime(dateTime) && !appConfigOptions.Value.isApplyDaylightSaving)
            {
                utcTime = utcTime.AddHours(1);
            }

            return utcTime;

        }
        catch (ArgumentException ex)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(appConfigOptions.Value.timeZone);
            DateTime adjustedUtcDateTime = dateTime.Add(timeZoneInfo.GetUtcOffset(dateTime));
            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(adjustedUtcDateTime, timeZoneInfo);
            return utcDateTime;
        }

    }
    //===================================================================
    public static string ToLocalDateLongFormatString(this DateTime date, IOptions<AppConfig> appConfigOptions)
    {
        date = date.ToLocalDateTime(appConfigOptions);
        int Year = 0, Month = 0, Day = 0;
        string MonthName = string.Empty, DayName = string.Empty;
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                var persianCalendar = new PersianCalendar();
                Year = persianCalendar.GetYear(date);
                Month = persianCalendar.GetMonth(date);
                Day = persianCalendar.GetDayOfMonth(date);
                DayName = date.DayOfWeek.ToPersianDayName();
                MonthName = Month.ToPersianMonthName();
                break;
            case 2:
                Year = date.Year;
                Month = date.Month;
                Day = date.Day;
                DayName = date.DayOfWeek.ToGregorianDayName();
                MonthName = Month.ToGregorianMonthName();
                break;
        }
        var result = string.Format("{0} {1} {2} {3}", DayName, Day, MonthName, Year);
        return result;
    }
    //===================================================================
    public static string ToLocalDateTimeDayName(this DateTime dateTime, IOptions<AppConfig> appConfigOptions)
    {
        dateTime = dateTime.ToLocalDateTime(appConfigOptions);
        string DayName = string.Empty;
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                DayName = dateTime.DayOfWeek.ToPersianDayName();
                break;
            case 2:
                DayName = dateTime.DayOfWeek.ToGregorianDayName();
                break;
        }
        var result = string.Format("{0}", DayName);
        return result;
    }
    //===================================================================
    public static string ToLocalDateTimeLongFormatString(this DateTime dateTime, IOptions<AppConfig> appConfigOptions)
    {
        dateTime = dateTime.ToLocalDateTime(appConfigOptions);
        int Year = 0, Month = 0, Day = 0;
        string MonthName = string.Empty, DayName = string.Empty;
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                var persianCalendar = new PersianCalendar();
                Year = persianCalendar.GetYear(dateTime);
                Month = persianCalendar.GetMonth(dateTime);
                Day = persianCalendar.GetDayOfMonth(dateTime);
                DayName = dateTime.DayOfWeek.ToPersianDayName();
                MonthName = Month.ToPersianMonthName();
                break;
            case 2:
                Year = dateTime.Year;
                Month = dateTime.Month;
                Day = dateTime.Month;
                DayName = dateTime.DayOfWeek.ToGregorianDayName();
                MonthName = Month.ToGregorianMonthName();
                break;
        }
        int Hour = dateTime.TimeOfDay.Hours;
        int Minute = dateTime.TimeOfDay.Minutes;
        var result = string.Format("{0} {1} {2} {3} - {4:D2}:{5:D2}", DayName, Day, MonthName, Year, Hour, Minute);
        return result;
    }
    //===================================================================
    public static string ToLocalDateTimeShortFormatString(this DateTime dateTime, IOptions<AppConfig> appConfigOptions)
    {
        dateTime = dateTime.ToLocalDateTime(appConfigOptions);
        int Year = 0, Month = 0, Day = 0;
        string MonthName = string.Empty, DayName = string.Empty;
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                var persianCalendar = new PersianCalendar();
                Year = persianCalendar.GetYear(dateTime);
                Month = persianCalendar.GetMonth(dateTime);
                Day = persianCalendar.GetDayOfMonth(dateTime);
                break;
            case 2:
                Year = dateTime.Year;
                Month = dateTime.Month;
                Day = dateTime.Month;
                break;
        }
        int Hour = dateTime.TimeOfDay.Hours;
        int Minute = dateTime.TimeOfDay.Minutes;
        var result = string.Format("{0:D4}/{1:D2}/{2:D2}-{3:D2}:{4:D2}", Year, Month, Day, Hour, Minute);
        return result;
    }
    public static string ToLocalDateShortFormatString(this DateTime dateTime, IOptions<AppConfig> appConfigOptions)
    {
        dateTime = dateTime.ToLocalDateTime(appConfigOptions);
        int Year = 0, Month = 0, Day = 0;
        string MonthName = string.Empty, DayName = string.Empty;
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                var persianCalendar = new PersianCalendar();
                Year = persianCalendar.GetYear(dateTime);
                Month = persianCalendar.GetMonth(dateTime);
                Day = persianCalendar.GetDayOfMonth(dateTime);
                break;
            case 2:
                Year = dateTime.Year;
                Month = dateTime.Month;
                Day = dateTime.Month;
                break;
        }
        int Hour = dateTime.TimeOfDay.Hours;
        int Minute = dateTime.TimeOfDay.Minutes;
        var result = string.Format("{0:D4}/{1:D2}/{2:D2}", Year, Month, Day);
        return result;
    }
    //===================================================================
    /// <summary>
    /// برگشت نتیجه به ثانیه می باشد
    /// </summary>
    public static int DifferenceOfCurrentTime(this DateTime date)
    {
        var value = Convert.ToInt32(Math.Floor(date.Subtract(DateTime.UtcNow).TotalSeconds));
        return value > 0 ? value : 0;
    }
    //===================================================================

    public static DateTime GetDayStartUTC(this DateTime DateUTC, IOptions<AppConfig> appConfigOptions)
    {
        var localDateTime = DateUTC.ToLocalDateTime(appConfigOptions);
        return new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day, 0, 0, 0).ToUtcDateTime(appConfigOptions);
    }

    //===================================================================
    public static DateTime GetDayEndUTC(this DateTime DateUTC, IOptions<AppConfig> appConfigOptions)
    {
        var localDateTime = DateUTC.ToLocalDateTime(appConfigOptions);
        return new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day, 23, 59, 59).ToUtcDateTime(appConfigOptions);
    }
    //===================================================================
    public static DateTime GetMonthStartUTC(this DateTime DateUTC, IOptions<AppConfig> appConfigOptions)
    {
        var localDateTime = DateUTC.ToLocalDateTime(appConfigOptions);
        var persianCalendar = new PersianCalendar();
        int Year = persianCalendar.GetYear(localDateTime);
        int Month = persianCalendar.GetMonth(localDateTime);
        persianCalendar = new PersianCalendar();
        return persianCalendar.ToDateTime(Year, Month, 1, 0, 0, 0, 0).ToUtcDateTime(appConfigOptions);

    }
    //===================================================================
    public static DateTime GetMonthEndUTC(this DateTime DateUTC, IOptions<AppConfig> appConfigOptions)
    {
        var localDateTime = DateUTC.ToLocalDateTime(appConfigOptions);
        var persianCalendar = new PersianCalendar();
        int Year = persianCalendar.GetYear(localDateTime);
        int Month = persianCalendar.GetMonth(localDateTime);
        persianCalendar = new PersianCalendar();
        return persianCalendar.ToDateTime(Year, Month, persianCalendar.GetDaysInMonth(Year, Month), 23, 59, 59, 0).ToUtcDateTime(appConfigOptions);
    }
    //===================================================================
    public static string TimeAgo(this DateTime dateUTC)
    {
        string result = string.Empty;
        var timeSpan = DateTime.UtcNow.Subtract(dateUTC);

        if (timeSpan <= TimeSpan.FromSeconds(60))
        {
            result = string.Format("{0} ثانیه قبل", timeSpan.Seconds);
        }
        else if (timeSpan <= TimeSpan.FromMinutes(60))
        {
            result = timeSpan.Minutes > 1 ?
                string.Format("{0} دقیقه پیش", timeSpan.Minutes) :
                "یک دقیقه پیش";
        }
        else if (timeSpan <= TimeSpan.FromHours(24))
        {
            result = timeSpan.Hours > 1 ?
                string.Format("{0} ساعت پیش", timeSpan.Hours) :
                "یک ساعت پیش";
        }
        else if (timeSpan <= TimeSpan.FromDays(30))
        {
            result = timeSpan.Days > 1 ?
                string.Format("{0} روز پیش", timeSpan.Days) :
                "دیروز";
        }
        else if (timeSpan <= TimeSpan.FromDays(365))
        {
            result = timeSpan.Days > 30 ?
                string.Format("{0} ماه پیش", timeSpan.Days / 30) :
                "یک ماه پیش";
        }
        else
        {
            result = timeSpan.Days > 365 ?
                string.Format("{0} سال پیش", timeSpan.Days / 365) :
                "یک سال پیش";
        }

        return result;
    }
    //===================================================================
    public static string CurrentDatePersianMonth(this DateTime UTCDateTime, IOptions<AppConfig> appConfigOptions)
    {
        var persianCalendar = new PersianCalendar();
        var dateTime = UTCDateTime.ToLocalDateTime(appConfigOptions);
        return persianCalendar.GetMonth(dateTime).ToPersianMonthName();
    }
    //===================================================================
    public static int CurrentDatePersianYaer(this DateTime UTCDateTime, IOptions<AppConfig> appConfigOptions)
    {
        var persianCalendar = new PersianCalendar();
        var dateTime = UTCDateTime.ToLocalDateTime(appConfigOptions);
        return persianCalendar.GetYear(dateTime);
    }
    //===================================================================
    public static string ToHijriDateTime(this DateTime dateTime)
    {
        dateTime = dateTime.ToLocalTime();

        var hc = new HijriCalendar();
        var year = hc.GetYear(dateTime);
        var month = hc.GetMonth(dateTime);
        var day = hc.GetDayOfMonth(dateTime);
        var hour = hc.GetHour(dateTime);
        var minute = hc.GetMinute(dateTime);
        var second = hc.GetSecond(dateTime);

        string monthName;

        switch (month)
        {
            case 1:
                monthName = "مُحَرَّم";
                break;

            case 2:
                monthName = "صَفَر";
                break;

            case 3:
                monthName = "رَبيع الأوّل";
                break;

            case 4:
                monthName = "رَبيع الثاني";
                break;

            case 5:
                monthName = "جُمادى الأولى";
                break;

            case 6:
                monthName = "جُمادى الآخرة";
                break;

            case 7:
                monthName = "رَجَب";
                break;

            case 8:
                monthName = "شَعْبان";
                break;

            case 9:
                monthName = "رَمَضان";
                break;

            case 10:
                monthName = "شَوّال";
                break;

            case 11:
                monthName = "ذی‌القعدة";
                break;

            case 12:
                monthName = "ذی‌الحجة";
                break;

            default:
                monthName = "خطا";
                break;
        }

        string dayName;

        switch (dateTime.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                dayName = "السبت";
                break;

            case DayOfWeek.Sunday:
                dayName = "الأحد";
                break;

            case DayOfWeek.Monday:
                dayName = "الإثنين";
                break;

            case DayOfWeek.Tuesday:
                dayName = "الثلاثاء";
                break;

            case DayOfWeek.Wednesday:
                dayName = "الأربعاء";
                break;

            case DayOfWeek.Thursday:
                dayName = "الخميس";
                break;

            case DayOfWeek.Friday:
                dayName = "الجمعة";
                break;

            default:
                dayName = "خطا";
                break;
        }

        return dayName + " - " + day + " " + monthName + " " + year;
    }
    //===================================================================
    public static long ToUnixDateTime(this DateTime date)
    {
        int unixTimestamp = (int)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        return unixTimestamp;
    }
    //===================================================================
    public static string ToLocalDateTimeLongFormatMongoString(this DateTime utcDateTime, IOptions<AppConfig> appConfigOptions)
    {
        // Convert MongoDB UTC date to local time
        var localDateTime = utcDateTime.ToLocalDateMongoTime(appConfigOptions);

        int year = 0, month = 0, day = 0;
        string monthName = string.Empty, dayName = string.Empty;

        switch (appConfigOptions.Value.calendarType)
        {
            case 1: // Persian calendar
                var persianCalendar = new PersianCalendar();
                year = persianCalendar.GetYear(localDateTime);
                month = persianCalendar.GetMonth(localDateTime);
                day = persianCalendar.GetDayOfMonth(localDateTime);
                dayName = localDateTime.DayOfWeek.ToPersianDayName();
                monthName = month.ToPersianMonthName();
                break;

            case 2: // Gregorian calendar
                year = localDateTime.Year;
                month = localDateTime.Month;
                day = localDateTime.Day;
                dayName = localDateTime.DayOfWeek.ToGregorianDayName();
                monthName = month.ToGregorianMonthName();
                break;
        }

        // Format time components
        int hour = localDateTime.Hour;
        int minute = localDateTime.Minute;

        return $"{dayName} {day} {monthName} {year} - {hour:D2}:{minute:D2}";
    }
    private static DateTime ToLocalDateMongoTime(this DateTime utcDateTime, IOptions<AppConfig> config)
    {
        // Implement your timezone conversion logic here
        // Example using Iran Standard Time (UTC+3:30)
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);

        // OR if you store offset in AppConfig:
        // return utcDateTime.AddMinutes(config.Value.TimeZoneOffsetMinutes);
    }
    // Helper extension methods
    //private static string ToPersianDayName(this DayOfWeek dayOfWeek)
    //{
    //    return dayOfWeek switch
    //    {
    //        DayOfWeek.Saturday => "شنبه",
    //        DayOfWeek.Sunday => "یکشنبه",
    //        DayOfWeek.Monday => "دوشنبه",
    //        DayOfWeek.Tuesday => "سه‌شنبه",
    //        DayOfWeek.Wednesday => "چهارشنبه",
    //        DayOfWeek.Thursday => "پنجشنبه",
    //        DayOfWeek.Friday => "جمعه",
    //        _ => throw new ArgumentOutOfRangeException()
    //    };
    //}

    //private static string ToPersianMonthName(this int month)
    //{
    //    return month switch
    //    {
    //        1 => "فروردین",
    //        2 => "اردیبهشت",
    //        3 => "خرداد",
    //        4 => "تیر",
    //        5 => "مرداد",
    //        6 => "شهریور",
    //        7 => "مهر",
    //        8 => "آبان",
    //        9 => "آذر",
    //        10 => "دی",
    //        11 => "بهمن",
    //        12 => "اسفند",
    //        _ => throw new ArgumentOutOfRangeException(nameof(month))
    //    };
    //}

    //private static string ToGregorianDayName(this DayOfWeek dayOfWeek)
    //{
    //    return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
    //}

    //private static string ToGregorianMonthName(this int month)
    //{
    //    return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
    //}
}
