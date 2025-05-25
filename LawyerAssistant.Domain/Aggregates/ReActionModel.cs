using Domain.Aggregates.Identities;
using Domain.Base.Enums;
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
        bool timeIsImportant,
        bool goingToBranch,
        int? branchId,
        int? complexeId,
        DateTime? date,
        int customerId,
        int fileId)
    {
        ActionTypeId = actionTypeId;
        Time = date;
        TimeIsImportant = timeIsImportant;
        GoingToBranch = goingToBranch;
        BranchId = branchId;
        ComplexeId = complexeId;
        CustomerId = customerId;
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
        bool timeIsImportant,
        bool goingToBranch,
        int? branchId,
        int? complexeId,
        DateTime? date,
        int customerId,
        int fileId)
    {
        ActionTypeId = actionTypeId;
        Time = date;
        ActionTypeId = actionTypeId;
        TimeIsImportant = timeIsImportant;
        GoingToBranch = goingToBranch;
        BranchId = branchId;
        ComplexeId = complexeId;
        CustomerId = customerId;
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
    public DateTime? Time { get; set; }
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
    /// شناسه مشتری.
    /// </summary>
    public int CustomerId { get; set; }
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
    /// مدل مشتری.
    /// </summary>
    public CustomersModel Customer { get; set; }

    /// <summary>
    /// مدل شعبه مربوطه.
    /// </summary>
    public BranchesModel? Branch { get; set; }
    #endregion
}
