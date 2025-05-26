using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs;

public class ReactionGetDTO
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public int? ActionTypeId { get; set; }
    public int? BranchId { get; set; }
    public string? CustomerName { get; set; }
    public string? ActionTypeTitle { get; set; }
    public string? VisitDate { get; set; }
    public string? VisitTime { get; set; }
    public string BranchName { get; set; }
    public bool IsLegal { get; set; }
    public bool TimeIsImportant { get; set; }
    public bool GoingToBranch { get; set; }
    public bool IsCompleted { get; set; }
}
