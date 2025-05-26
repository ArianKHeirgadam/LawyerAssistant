using LawyerAssistant.Application.Objects;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs;

public class CreateReActionDto : IValidatableObject
{
    /// <summary>
    /// آیا زمان اهمیت دارد؟
    /// </summary>
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "آیا زمان اهمیت دارد؟")]
    public bool TimeIsImportant { get; set; }

    /// <summary>
    /// شناسه نوع اقدام
    /// </summary>
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "نوع اقدام")]
    public int ActionTypeId { get; set; }

    /// <summary>
    /// آیا مراجعه به شعبه دارید؟
    /// </summary>
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "آیا مراجعه به شعبه دارید؟")]
    public bool GoingToBranch { get; set; }

    /// <summary>
    /// تاریخ مراجعه (اجباری در همه حالات)
    /// </summary>
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "تاریخ مراجعه")]
    [DataType(DataType.Date)]
    public DateTime VisitDate { get; set; }

    /// <summary>
    /// ساعت مراجعه (اجباری فقط در صورت اهمیت زمان)
    /// </summary>
    [Display(Name = "ساعت مراجعه")]
    [DataType(DataType.Time)]
    public TimeSpan? VisitTime { get; set; }
    /// <summary>
    /// شناسه شعبه (در صورت نیاز)
    /// </summary>
    [Display(Name = "شناسه شعبه")]
    public int? BranchId { get; set; }

    /// <summary>
    /// شناسه مجتمع (در صورت نیاز)
    /// </summary>
    [Display(Name = "شناسه مجتمع")]
    public int? ComplexeId { get; set; }

    /// <summary>
    /// یادآوری؟
    /// </summary>
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "یادآوری")]


    public bool IsRemember { get; set; }

    /// <summary>
    /// شناسه نوع پرونده
    /// </summary>
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "شناسه پرونده")]
    public int FileId { get; set; }

    /// <summary>
    /// زمان یادآوری
    /// </summary>
    [Display(Name = "زمان یادآوری")]
    public int? RememberTime { get; set; }

    /// <summary>
    /// اعتبارسنجی شرطی
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TimeIsImportant && Time == null)
        {
            yield return new ValidationResult(
                "در صورت اهمیت داشتن زمان، وارد کردن زمان الزامی است.",
                new[] { nameof(Time) }
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
    }
}