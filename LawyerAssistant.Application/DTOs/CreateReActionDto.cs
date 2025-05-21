using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs;

public class CreateReActionDto : IValidatableObject
{
    /// <summary>
    /// آیا زمان اهمیت دارد؟
    /// </summary>
    [Required(ErrorMessage = "وارد کردن مقدار «آیا زمان اهمیت دارد؟» الزامی است.")]
    [Display(Name = "آیا زمان اهمیت دارد؟")]
    public bool TimeIsImportant { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "وارد کردن مقدار نوع اقدام الزامی است.")]
    [Display(Name = "نوع اقدام")]
    public int ActionTypeId { get; set; }
    /// <summary>
    /// آیا مشتری قصد مراجعه به شعبه را دارد؟
    /// </summary>
    [Required(ErrorMessage = "وارد کردن مقدار «آیا مراجعه به شعبه دارید؟» الزامی است.")]
    [Display(Name = "آیا مراجعه به شعبه دارید؟")]
    public bool GoingToBranch { get; set; }

    /// <summary>
    /// زمان مراجعه (در صورت نیاز)
    /// </summary>
    [Display(Name = "زمان مراجعه")]
    public DateTime? Time { get; set; }

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
    /// شناسه مشتری
    /// </summary>
    [Required(ErrorMessage = "وارد کردن شناسه مشتری الزامی است.")]
    [Display(Name = "شناسه مشتری")]
    public int CustomerId { get; set; }

    /// <summary>
    /// شناسه نوع پرونده
    /// </summary>
    [Required(ErrorMessage = "وارد کردن شناسه نوع پرونده الزامی است.")]
    [Display(Name = "شناسه نوع پرونده")]
    public int FileTypeId { get; set; }

    /// <summary>
    /// اعتبارسنجی اختصاصی برای منطق شرطی
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
                    "در صورت مراجعه به شعبه، شناسه شعبه الزامی است.",
                    new[] { nameof(BranchId) }
                );
            }

            if (ComplexeId == null)
            {
                yield return new ValidationResult(
                    "در صورت مراجعه به شعبه، شناسه مجتمع الزامی است.",
                    new[] { nameof(ComplexeId) }
                );
            }
        }
    }
}