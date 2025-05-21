using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs;

public class ReactionGetDTO
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int ActionTypeId { get; set; }
    public string ActionTypeTitle { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public int? BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsCompleted { get; set; }
}
