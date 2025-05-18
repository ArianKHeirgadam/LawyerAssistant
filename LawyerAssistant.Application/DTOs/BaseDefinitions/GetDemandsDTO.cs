namespace LawyerAssistant.Application.DTOs.BaseDefinitions;

public class GetDemandsDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int FileTypeId { get; set; }
    public string FileTypeTitle { get; set; }
}
