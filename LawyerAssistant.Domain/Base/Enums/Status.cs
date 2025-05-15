using System.ComponentModel.DataAnnotations;

namespace Domain.Base.Enums;

public enum Status
{
    [Display(Name = "فعال")]
    Active = 1,
    [Display(Name = "غیر فعال")]
    Deactive,
    [Display(Name = "حذف شده")]
    Deleted
}
