using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs.BaseDefinitions;
public class GetBranchDTO
{
    public int Id { get; set; }

    public string Title { get; set; }
    public GenericDTO? Complex { get; set; }
}