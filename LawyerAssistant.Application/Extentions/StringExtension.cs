using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using Application.Exceptions;
using Microsoft.Extensions.Options;
using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Extentions;

public static class stringExtension
{
    //=====================================================================================
    public static string HashSet(this string Value)
    {
        byte[] Buffer = Encoding.UTF8.GetBytes(Value);
        MD5 md = MD5.Create();
        return Convert.ToBase64String(md.ComputeHash(Buffer));
    }
    //=====================================================================================
    public static string ToBase64String(string Value)
    {
        byte[] Buffer = Encoding.UTF8.GetBytes(Value);
        return Convert.ToBase64String(Buffer);
    }
    //=====================================================================================
    public static DateTime ToDate(this string date, IOptions<AppConfig> appConfigOptions)
    {
        var dt = DateTime.UtcNow;
        var splittedDate = date.Split("/");
        int Year = int.Parse(splittedDate[0]);
        int Month = int.Parse(splittedDate[1]);
        int Day = int.Parse(splittedDate[2]);
        switch (appConfigOptions.Value.calendarType)
        {
            case 1:
                var persianDate = new PersianCalendar();
                dt = persianDate.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
                break;
            case 2:
                dt = new DateTime(Year, Month, Day, 0, 0, 0);
                break;
        }
        return dt;
    }
    //=====================================================================================
    public static TimeSpan ToTimeSpan(this string data)
    {
        string[] time = data.Split(":");
        int hour = Convert.ToInt32(time[0]);
        int minutes = Convert.ToInt32(time[1]);
        var timeSpan = new TimeSpan(hour, minutes, 0);
        return timeSpan;
    }
    //=====================================================================================
    public static string ToMaskedPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length < 10)
        {
            return "**********"; 
        }

        return phoneNumber.Substring(0, 4) + "***" + phoneNumber.Substring(phoneNumber.Length - 4);
    }
    //=====================================================================================
    public static int ToIntegerIdentifier(this string value)
    {
        try
        {
            return Convert.ToInt32(value);
        }
        catch (Exception)
        {
            throw new CustomException(SystemCommonMessage.IdentifierIsNotValid);
        }
    }
    //=====================================================================================
    public static string EncriptWithDESAlgoritm(this string data, bool isPaddingRemove = false)
    {
        byte[] buffer_key = Encoding.UTF8.GetBytes("QWERTYui");
        byte[] buffer_IV = Encoding.UTF8.GetBytes("LKJHGesq");
        byte[] buffer_Message = Encoding.UTF8.GetBytes(data);
        MemoryStream memoryStream = new MemoryStream();
        var DES = new DESCryptoServiceProvider();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, DES.CreateEncryptor(buffer_key, buffer_IV), CryptoStreamMode.Write);
        cryptoStream.Write(buffer_Message, 0, buffer_Message.Length);
        cryptoStream.FlushFinalBlock();
        cryptoStream.Dispose();
        var result =  Convert.ToBase64String(memoryStream.ToArray()).Replace("/", "-").Replace("+", "_");
       
        if (isPaddingRemove && result.EndsWith("=="))
            result = result.Replace("==", "2L");
        if (isPaddingRemove && result.EndsWith("="))
            result = result.Replace("=", "1L");
        return result;

    }
    //=====================================================================================
    public static string DecriptWithDESAlgoritm(this string data, bool isPaddingRemoved = false)
    {
        try
        {
            if (isPaddingRemoved && data.EndsWith("2L"))
                data = data.Substring(0, data.Length - 2) + "==";
            if (isPaddingRemoved && data.EndsWith("1L"))
                data = data.Substring(0, data.Length - 2) + "=";

            byte[] buffer_Message = Convert.FromBase64String(data.Replace("-", "/").Replace("_", "+"));
            byte[] buffer_key = Encoding.UTF8.GetBytes("QWERTYui");
            byte[] buffer_IV = Encoding.UTF8.GetBytes("LKJHGesq");
            var memoryStream = new MemoryStream();
            var DES = new DESCryptoServiceProvider();
            var cryptoStream = new CryptoStream(memoryStream, DES.CreateDecryptor(buffer_key, buffer_IV), CryptoStreamMode.Write);
            cryptoStream.Write(buffer_Message, 0, buffer_Message.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Dispose();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        catch (Exception)
        {
            throw new CustomException("شناسه رمز شده صحیح نمی باشد");
        }

    }
    //=====================================================================================
    public static dynamic ToFilterExpression(this string data)
    {
        dynamic result = JsonConvert.DeserializeObject(data);
        return result;
    }

    public static T ToFilterExpression<T>(this string data)
    {
        dynamic result = JsonConvert.DeserializeObject<T>(data);
        return result;
    }
    //=====================================================================================
    public static string CharacterAnalysis(this string data)
    {
        if (string.IsNullOrEmpty(data))
            return data;
        var str = data.Trim();
        str = str.Replace("۰", "0");
        str = str.Replace("۱", "1");
        str = str.Replace("۲", "2");
        str = str.Replace("۳", "3");
        str = str.Replace("۴", "4");
        str = str.Replace("۵", "5");
        str = str.Replace("۶", "6");
        str = str.Replace("۷", "7");
        str = str.Replace("۸", "8");
        str = str.Replace("۹", "9");
        //str = str.Replace("ک", "ك");
        //str = str.Replace("ي", "ی");
        //str = str.Replace("ى", "ی");
        return str;
    }

    public static string SpaceIgnore(this string data)
    {
        var result = data.Replace(" ", "").Replace("‌", "");
        return result;
    }
    //=====================================================================================
    public static string Truncate(this string str, int maxLength, string suffix = "...")
    {
        if (str == null || str.Length <= maxLength)
            return str;
        int strLength = maxLength - suffix.Length;
        return str.Substring(0, strLength) + suffix;
    }
    //=====================================================================================
    public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
    {
        return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
    } 
    //=====================================================================================
}
