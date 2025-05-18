namespace LawyerAssistant.Application.DTOs.BaseDefinitions;
public class GetBranchDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int ComplexId { get; set; }

    public string ComplexTitle { get; set; }
}