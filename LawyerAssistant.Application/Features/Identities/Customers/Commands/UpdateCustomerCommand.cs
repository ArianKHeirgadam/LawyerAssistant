using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Identities.Customers.Commands;

public class UpdateCustomerCommand : IRequest<SysResult<GetCustomerDetailsDTO>>
{
    

 /// <summary>
    /// شناسه مشتری
    /// </summary>
    [Display(Name = "شناسه مشتری")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int Id { get; set; }

    /// <summary>
    /// شماره موبایل مشتری
    /// </summary>
    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = ValidationCommonMessages.InvalidMobile)]
    public string MobileNumber { get; set; }

    /// <summary>
    /// نام مشتری
    /// </summary>
    [Display(Name = "نام")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(50, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string FirstName { get; set; }

    /// <summary>
    /// نام خانوادگی مشتری
    /// </summary>
    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(50, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string LastName { get; set; }

    /// <summary>
    /// کد ملی مشتری
    /// </summary>
    [Display(Name = "کد ملی")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [RegularExpression(@"^\d{10}$", ErrorMessage = ValidationCommonMessages.InvalidNationalCode)]
    public string NationalCode { get; set; }

    /// <summary>
    /// تاریخ تولد مشتری
    /// </summary>
    [Display(Name = "تاریخ تولد")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public string BirthDate { get; set; }

    /// <summary>
    /// آدرس مشتری
    /// </summary>
    [Display(Name = "آدرس")]
    [MaxLength(500, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string Address { get; set; }

    /// <summary>
    /// شناسه شهر (اختیاری)
    /// </summary>
    [Display(Name = "شهر")]
    public int? CityId { get; set; }

    /// <summary>
    /// شناسه استان (اختیاری)
    /// </summary>
    [Display(Name = "استان")]
    public int? ProvinceId { get; set; }
}

