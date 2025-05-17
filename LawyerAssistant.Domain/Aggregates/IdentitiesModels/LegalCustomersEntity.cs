using Domain.Aggregates.Identities;
using LawyerAssistant.Domain.Base.Contracts;
using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.IdentitiesModels;

public class LegalCustomersEntity : ModifyDateTimeModel, IEntity
{
    #region Methods

  
    protected LegalCustomersEntity()
    {
    }

    /// <summary>
    /// سازنده‌ی کلاس برای مقداردهی اولیه‌ی ویژگی‌ها
    /// </summary>
    /// <param name="companyName">نام شرکت حقوقی</param>
    /// <param name="legalNationalCode">کد ملی حقوقی</param>
    /// <param name="address">آدرس شرکت</param>
    public LegalCustomersEntity(string companyName, string legalNationalCode, string address)
    {
        CompanyName = companyName;
        LegalNationalCode = legalNationalCode;
        Address = address;
        CompanyCustomers = new HashSet<CustomersEntity>();
    }

    /// <summary>
    /// ویرایش اطلاعات شرکت حقوقی
    /// </summary>
    /// <param name="companyName">نام جدید شرکت</param>
    /// <param name="legalNationalCode">کد ملی حقوقی جدید</param>
    /// <param name="address">آدرس جدید</param>
    public void Edit(string companyName, string legalNationalCode, string address)
    {
        CompanyName = companyName;
        LegalNationalCode = legalNationalCode;
        Address = address;
    }
    #endregion

    #region Props

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    /// <summary>
    /// نام شرکت حقوقی
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// کد ملی حقوقی شرکت
    /// </summary>
    public string LegalNationalCode { get; set; }

    /// <summary>
    /// آدرس شرکت حقوقی
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// لیست مشتریان حقیقی مرتبط با این شرکت حقوقی
    /// </summary>
    public ICollection<CustomersEntity> CompanyCustomers { get; set; }

    #endregion

}
