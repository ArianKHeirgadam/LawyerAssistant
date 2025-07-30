using LawyerAssistant.Application.Objects;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs;
public class FilesUpdateDto : IValidatableObject
{

    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "عنوان پرونده")]
    public string Title { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "شناسه خواسته")]
    public int DemandId { get; set; }

    [Display(Name = "شناسه مشتری")]
    public int? CustomerId { get; set; }

    [Display(Name = "شناسه حقوقی")]
    public int? LegalId { get; set; }

    [Display(Name = "مشتری حقوقی است؟")]
    public bool? IsLegal { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsLegal == true && !LegalId.HasValue)
        {
            yield return new ValidationResult("شناسه حقوقی الزامی است برای مشتریان حقوقی.", new[] { nameof(LegalId) });
        }

        if (IsLegal == false && !CustomerId.HasValue)
        {
            yield return new ValidationResult("شناسه مشتری الزامی است برای مشتریان حقیقی.", new[] { nameof(CustomerId) });
        }
    }
}
