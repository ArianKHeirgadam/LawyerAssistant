using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs.BaseDefinitions;

public class GetDemandsDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public GenericDTO? FileType { get; set; }
}
