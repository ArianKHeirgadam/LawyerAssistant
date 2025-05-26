using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates;

public class ReActionModel : ModifyDateTimeWithUserModel, IEntity
{
    protected ReActionModel()
    {

    }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// سازنده کلاس برای مقداردهی اولیه.
    /// </summary>
    public ReActionModel(
        int actionTypeId,
        int? rememberTime,
        TimeSpan? visitTime,
        bool timeIsImportant,
        bool goingToBranch,
        bool isRemember,
        int? branchId,
        int? complexeId,
        DateTime visitDate,
        int fileId)
    {
        RememberTime = rememberTime;
        ActionTypeId = actionTypeId;
        VisitDate = visitDate;
        VisitTime = visitTime;
        TimeIsImportant = timeIsImportant;
        GoingToBranch = goingToBranch;
        BranchId = branchId;
        ComplexeId = complexeId;
        FileId = fileId;
        RegDateTime = DateTime.UtcNow;
        IsCompleted = false;
    }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// بروزرسانی مقادیر اصلی (بدون تغییر ناوبری‌ها).
    /// </summary>
    public void Edit(
        int actionTypeId,
        int? rememberTime,
        TimeSpan? visitTime,
        bool timeIsImportant,
        bool goingToBranch,
        bool isRemember,
        int? branchId,
        int? complexeId,
        DateTime visitDate,
        int fileId)
    {
        RememberTime = rememberTime;
        ActionTypeId = actionTypeId;
        VisitDate = visitDate;
        VisitTime = visitTime;
        IsRemember = isRemember;
        ActionTypeId = actionTypeId;
        TimeIsImportant = timeIsImportant;
        GoingToBranch = goingToBranch;
        BranchId = branchId;
        ComplexeId = complexeId;
        FileId = fileId;
        ModDateTime = DateTime.UtcNow;
    }
    public void IsComplete(bool isCompleete)
    {
        IsCompleted = isCompleete;
    }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// تغییر تصمیم مراجعه به شعبه.
    /// </summary>
    public void ChangeBranchDecision(bool goingToBranch, int? branchId)
    {
        GoingToBranch = goingToBranch;
        BranchId = branchId;
        ModDateTime = DateTime.UtcNow;
    }


    #region Props
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// زمان یادآوری
    /// </summary>

    public int? RememberTime { get; set; }
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// زمان یادآوری
    /// </summary>
    public TimeSpan? VisitTime { get; set; }
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// تاریخ
    /// </summary>
    public DateTime VisitDate { get; set; }
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// مشخص می‌کند که آیا زمان اهمیت دارد یا خیر.
    /// </summary>
    public bool TimeIsImportant { get; set; }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// مشخص می‌کند که آیا مشتری قصد مراجعه به شعبه را دارد یا خیر.
    /// </summary>
    public bool GoingToBranch { get; set; }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// شناسه شعبه (در صورت مراجعه به شعبه).
    /// </summary>
    public int? BranchId { get; set; }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// شناسه مجتمع (در صورت وجود).
    /// </summary>
    public int? ComplexeId { get; set; }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// انجام شده یا نشده.
    /// </summary>
    public bool IsCompleted { get; set; }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// شناسه نوع پرونده.
    /// </summary>
    public int FileId { get; set; }
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// نوع قدام
    /// </summary>
    public int ActionTypeId { get; set; }
    public ActionTypesModel ActionType { get; set; }
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    /// مدل نوع فایل مربوطه.
    /// </summary>
    public FilesModel Files { get; set; }

    /// <summary>
    /// مدل مجتمع مربوطه.
    /// </summary>
    public ComplexesModel? Complexe { get; set; }

    /// <summary>
    /// مدل شعبه مربوطه.
    /// </summary>
    public BranchesModel? Branch { get; set; }
    public ICollection<SMSCenterModel> SMSCenters { get; set; }
    public bool IsSent { get; set; } = false;
    public bool IsRemember { get; set; }
    #endregion
}
