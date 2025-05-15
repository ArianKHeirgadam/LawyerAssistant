namespace LawyerAssistant.Application.Extentions;

public static class DecimalExtensions
{
    public static decimal Ceil(this decimal value, int decimalPlaces = 2)
    {
        decimal factor = (decimal)Math.Pow(10, decimalPlaces);
        return Math.Ceiling(value * factor) / factor;
    }

    /// <summary>
    /// عدد را به نزدیکترین عدد اعشاری بزرگتر یا مساوی با یک رقم اعشار گرد میکند
    /// مثال: 3.2 → 3.2 | 4.57 → 4.6 | -2.33 → -2.3
    /// </summary>
    /// <param name="number">عدد ورودی</param>
    /// <returns>عدد گرد شده به بالا با یک رقم اعشار</returns>
    public static double RoundTOUp(this double number)
    {
        return Math.Ceiling(number * 10) / 10;
    }

    /// <summary>
    /// عدد را به تعداد اعشار مشخص شده به سمت بالا گرد میکند
    /// مثال: RoundUp(2.341, 2) → 2.35 | RoundUp(1.111, 1) → 1.2
    /// </summary>
    /// <param name="number">عدد ورودی</param>
    /// <param name="decimals">تعداد اعشار مورد نظر</param>
    /// <returns>عدد گرد شده به بالا با اعشار مشخص</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double Round(this double number, int decimals)
    {
        if (decimals < 0)
            throw new ArgumentOutOfRangeException(nameof(decimals), "تعداد اعشار نمیتواند منفی باشد.");

        double factor = Math.Pow(10, decimals);
        return Math.Ceiling(number * factor) / factor;
    }
}
