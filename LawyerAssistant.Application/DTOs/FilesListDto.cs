using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs;

public class FilesListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsLegal { get; set; }
    public UserGenericDTO Customer { get; set; }
    public UserGenericDTO Legal { get; set; }
    public GenericDTO Demand { get; set; }
    public GenericDTO FileType { get; set; }
}
