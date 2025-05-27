using LawyerAssistant.Application.Objects;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs;

public class UpdateReactionDTO : IValidatableObject
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "آیا زمان اهمیت دارد؟")]
    public bool TimeIsImportant { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "نوع اقدام")]
    public int ActionTypeId { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "آیا مراجعه به شعبه دارید؟")]
    public bool GoingToBranch { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "تاریخ مراجعه")]
    [DataType(DataType.Date)]
    public DateTime VisitDate { get; set; }

    [Display(Name = "ساعت مراجعه")]
    [SwaggerSchema(Format = "HH:mm", Description = "Format: HH:mm")] 
    public TimeSpan? VisitTime { get; set; }

    [Display(Name = "شناسه شعبه")]
    public int? BranchId { get; set; }

    [Display(Name = "شناسه مجتمع")]
    public int? ComplexeId { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "یادآوری")]
    public bool IsRemember { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "شناسه پرونده")]
    public int FileId { get; set; }

    [Display(Name = "زمان یادآوری")]
    public int? RememberTime { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TimeIsImportant && VisitTime == null)
        {
            yield return new ValidationResult(
                "در صورت اهمیت داشتن زمان، وارد کردن زمان الزامی است.",
                new[] { nameof(VisitTime) }
            );
        }

        if (GoingToBranch)
        {
            if (BranchId == null)
            {
                yield return new ValidationResult(
                    "در صورت مراجعه به شعبه، وارد کردن شناسه شعبه الزامی است.",
                    new[] { nameof(BranchId) }
                );
            }

            if (ComplexeId == null)
            {
                yield return new ValidationResult(
                    "در صورت مراجعه به شعبه، وارد کردن شناسه مجتمع الزامی است.",
                    new[] { nameof(ComplexeId) }
                );
            }
        }

        if (IsRemember)
        {
            if (RememberTime == null)
            {
                yield return new ValidationResult(
                    "در صورت فعال بودن یادآوری، وارد کردن زمان یادآوری الزامی است.",
                    new[] { nameof(RememberTime) }
                );
            }

            // فقط ادامه بده اگر تاریخ معتبره
            if (VisitDate != default && RememberTime != null)
            {
                var baseDateTime = VisitDate.Date + (TimeIsImportant
                    ? VisitTime ?? TimeSpan.Zero
                    : VisitTime ?? TimeSpan.Zero);

                var reminderTime = baseDateTime - TimeSpan.FromMinutes(RememberTime.Value);

                if (reminderTime <= DateTime.Now)
                {
                    yield return new ValidationResult(
                        "زمان یادآوری نمی‌تواند قبل از زمان فعلی باشد.",
                        new[] { nameof(RememberTime) }
                    );
                }
            }
        }
    }
}
