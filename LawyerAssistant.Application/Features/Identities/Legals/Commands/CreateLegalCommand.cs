using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Identities.Legals.Commands;

public class CreateLegalCommand : IRequest<SysResult>
{
    /// <summary>
    /// نام شرکت حقوقی
    /// </summary>
    [Display(Name = "نام شرکت")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(200, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string CompanyName { get; set; }

    /// <summary>
    /// کد ملی حقوقی
    /// </summary>
    [Display(Name = "کد ملی حقوقی")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = ValidationCommonMessages.InvalidNationalCode)]
    public string LegalNationalCode { get; set; }

    /// <summary>
    /// آدرس شرکت
    /// </summary>
    [Display(Name = "آدرس")]
    [MaxLength(500, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string Address { get; set; }

    /// <summary>
    /// لیست مشتریانی که متعلق به این شرکت حقوقی خواهند شد
    /// </summary>
    public List<int> CustomerIds { get; set; } = new();
}
