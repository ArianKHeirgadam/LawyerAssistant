using LawyerAssistant.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs.Identities;

public class GetCustomerDetailsDTO
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
    public GenericDTO City { get; set; }
    public GenericDTO Province { get; set; }

    [Display(Name = "تاریخ ایجاد")]
    public string CreateDate { get; set; }
    [Display(Name = "آدرس")]
    public string Address { get; set; }
}
