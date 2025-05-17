using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs.Identities;

public class GetCustomersDTO
{
    public int Id { get; set; }

    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }

    [Display(Name = "شماره موبایل")]
    public string MobileNumber { get; set; }

    [Display(Name = "کد ملی")]
    public string NationalCode { get; set; }

    [Display(Name = "شهر")]
    public string CityName { get; set; }

    [Display(Name = "استان")]
    public string ProvinceName { get; set; }
    [Display(Name = "تاریخ ایجاد")]
    public string CreateDate { get; set; }
}
