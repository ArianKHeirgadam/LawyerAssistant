namespace LawyerAssistant.Application.Objects;

public static class ValidationCommonMessages
{
    // عمومی
    public const string Required = "{0} الزامی می‌باشد.";
    public const string Invalid = "{0} نامعتبر است.";

    // طول رشته
    public const string MaxLength = "طول {0} نمی‌تواند بیشتر از {1} کاراکتر باشد.";
    public const string MinLength = "طول {0} باید حداقل {1} کاراکتر باشد.";
    public const string StringLength = "طول {0} باید بین {2} تا {1} کاراکتر باشد.";

    // الگو (Regex)
    public const string InvalidFormat = "فرمت {0} معتبر نمی‌باشد.";
    public const string InvalidMobile = "{0} معتبر نمی‌باشد. (مثال: 09123456789)";
    public const string InvalidNationalCode = "{0} معتبر نمی‌باشد. باید ۱۰ رقم باشد.";
    public const string InvalidPhoneNumber = "شماره تماس وارد شده برای {0} معتبر نیست.";
    public const string InvalidPostalCode = "کد پستی {0} باید ۱۰ رقمی و معتبر باشد.";

    // عددی
    public const string Range = "{0} باید بین {1} و {2} باشد.";
    public const string GreaterThan = "{0} باید بزرگ‌تر از {1} باشد.";
    public const string LessThan = "{0} باید کوچک‌تر از {1} باشد.";

    // تاریخ
    public const string InvalidDate = "{0} تاریخ معتبر نمی‌باشد.";
    public const string DateMustBePast = "{0} باید در گذشته باشد.";
    public const string DateMustBeFuture = "{0} باید در آینده باشد.";

    // ایمیل و آدرس
    public const string InvalidEmail = "ایمیل وارد شده برای {0} معتبر نمی‌باشد.";
    public const string InvalidUrl = "آدرس اینترنتی وارد شده برای {0} معتبر نیست.";

    // تطبیق مقادیر
    public const string Compare = "{0} با {1} مطابقت ندارد.";
    public const string NotEqual = "{0} نباید برابر با {1} باشد.";

    // Unique (برای سطح اپلیکیشن)
    public const string Duplicate = "{0} تکراری است و قبلاً ثبت شده است.";

    // File / Upload
    public const string InvalidFileFormat = "فرمت فایل {0} معتبر نمی‌باشد.";
    public const string MaxFileSize = "حجم فایل {0} نمی‌تواند بیشتر از {1} مگابایت باشد.";

    // ثابت‌ها برای فیلدهای خاص (در صورت نیاز برای وضوح بیشتر)
    public const string IdentifierRequired = "شناسه الزامی می‌باشد.";
}
