using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs.BaseDefinitions;

public class GetComplexDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public GenericDTO? City { get; set; }
    public GenericDTO? Province { get; set; }
}