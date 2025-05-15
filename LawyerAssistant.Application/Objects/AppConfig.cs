namespace LawyerAssistant.Application.Objects;

public class AppConfig
{
    public bool isApplyDaylightSaving { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string connectionString { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string appFilePath { get; set; }
    /// <summary>
    ///  Country Finder Api Url
    /// </summary>
    public string CountryFinder { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string smsApiKey { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string smsSenderLine { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string zarinPal_MerchantId { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string payPing_MerchantId { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string panelUrl { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string webServiceUrl { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string applicationUrl { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string jwtTokenKey { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string cdnUrl { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string frontEndUrl { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string timeZone { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public int calendarType { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string companyName { get; set;  }
    //=========================================================== 
    /// <summary>
    /// 
    /// </summary>
    public string currencyType { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string LogoPath { get; set; }
    //===========================================================
    /// <summary>
    /// 
    /// </summary>
    public string  emailAddress { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public string  emailHost { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public int emailPort { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public string  emailUsername { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public string  emailPassword { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public int SmsRetryCount { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public int UserBlockTime { get; set; }
    //=======================================================
    /// <summary>
    /// 
    /// </summary>
    public string TranslateUrl { get; set; }
}
