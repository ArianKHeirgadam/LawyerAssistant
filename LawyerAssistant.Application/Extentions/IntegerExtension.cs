using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace Application.Extentions
{
    public static class IntegerExtension
    {
        //=====================================================================================
        public static string ToWeekDay(this int Value)
        {
            var result = string.Empty;
            switch (Value)
            {
                case 1:
                    result = "شنبه";
                    break;
                case 2:
                    result = "يکشنبه";
                    break;
                case 3:
                    result = "دوشنبه";
                    break;
                case 4:
                    result = "سه شنبه";
                    break;
                case 5:
                    result = "چهارشنبه";
                    break;
                case 6:
                    result = "پنجشنبه";
                    break;
                case 7:
                    result = "جمعه";
                    break;
            }
            return result;
        }
        //=====================================================================================
        public static int RoundUp(this int value)
        {
            value = value + (value * 10 / 100);
            return (int)Math.Ceiling((decimal)value / 100) * 100;
        }
        //=====================================================================================
        public static string ToMoneyStringFormat(this int data)
        {
            return String.Format("{0:N0}", data);
        }
        //=====================================================================================
        public static int ToInteger(this int? value)
        {
            int result = value ?? 0;
            return result;
        }
        //=====================================================================================
        public static string ToGregorianMonthName(this int mouth)
        {
            string monthName = string.Empty;
            switch (mouth)
            {
                case 1:
                    monthName = "January";
                    break;

                case 2:
                    monthName = "February";
                    break;

                case 3:
                    monthName = "March";
                    break;

                case 4:
                    monthName = "April";
                    break;

                case 5:
                    monthName = "May";
                    break;

                case 6:
                    monthName = "June";
                    break;

                case 7:
                    monthName = "July";
                    break;

                case 8:
                    monthName = "August";
                    break;

                case 9:
                    monthName = "September";
                    break;

                case 10:
                    monthName = "October";
                    break;

                case 11:
                    monthName = "November";
                    break;

                case 12:
                    monthName = "December";
                    break;
            }
            return monthName;
        }
        //=====================================================================================
        public static string ToPersianMonthName(this int month)
        {
            string result = string.Empty;
            switch (month)
            {
                case 1:
                    result = "فروردین";
                    break;
                case 2:
                    result = "اردیبهشت";
                    break;
                case 3:
                    result = "خرداد";
                    break;
                case 4:
                    result = "تیر";
                    break;
                case 5:
                    result = "مرداد";
                    break;
                case 6:
                    result = "شهریور";
                    break;
                case 7:
                    result = "مهر";
                    break;
                case 8:
                    result = "آبان";
                    break;
                case 9:
                    result = "آذر";
                    break;
                case 10:
                    result = "دی";
                    break;
                case 11:
                    result = "بهمن";
                    break;
                case 12:
                    result = "اسفند";
                    break;
                default:
                    throw new Exception("شماره روز ها صحیح وارد نشده است");

            }
            return result;
        }
        //=====================================================================================
        public static string TostringEducationDegree(this int Degree)
        {
            string result = string.Empty;
            switch (Degree)
            {
                case 1:
                    result = "زیر دیپلم";
                    break;
                case 2:
                    result = "دیپلم";
                    break;
                case 3:
                    result = "فوق دیپلم";
                    break;
                case 4:
                    result = "لیسانس";
                    break;
                case 5:
                    result = "فوق لیسانس";
                    break;
                case 6:
                    result = "دکترا";
                    break;
            }
            return result;
        }
        public static string TostringMillitaryStatus(this int Degree)
        {
            string result = string.Empty;
            switch (Degree)
            {
                case 1:
                    result = "انجام داده";
                    break;
                case 2:
                    result = "انجام نداده";
                    break;
                case 3:
                    result = "معافیت";
                    break;
                case 4:
                    result = "---";
                    break;
            }
            return result;
        }
    }
}
