namespace LawyerAssistant.Domain.Base;

public class ModifyDateTimeWithUserModel : IdentifierModel
{
    //=============================================================
    /// <summary>
    /// تاریخ تغییر
    /// </summary>
    public DateTime? ModDateTime { get; set; }
    //=============================================================
    /// <summary>
    /// تاریخ درج
    /// </summary>
    public DateTime RegDateTime { get; set; }
}
