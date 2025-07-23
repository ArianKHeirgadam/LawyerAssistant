using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Identities.Legals.Commands;

public class UpdateLegalCommand : IRequest<SysResult<GetLegalCustomerDetailsDTO>>
{
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int Id { get; set; }

    [Display(Name = "نام شرکت")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(200, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string CompanyName { get; set; }

    [Display(Name = "کد ملی حقوقی")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = ValidationCommonMessages.InvalidNationalCode)]
    public string LegalNationalCode { get; set; }

    [Display(Name = "آدرس")]
    [MaxLength(500, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string Address { get; set; }

    [Display(Name = "مشتریان")]
    public List<int> CustomerIds { get; set; }
}

