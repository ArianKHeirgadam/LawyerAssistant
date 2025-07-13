using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs;

public class ReactionGetDTO
{
    public int Id { get; set; }
    public GenericDTO Customer { get; set; }
    public GenericDTO? ActionType { get; set; }
    public GenericDTO? Branch { get; set; }
    public string? VisitDate { get; set; }
    public string? VisitTime { get; set; }
    public bool IsLegal { get; set; }
    public bool TimeIsImportant { get; set; }
    public bool GoingToBranch { get; set; }
    public bool IsCompleted { get; set; }
}
