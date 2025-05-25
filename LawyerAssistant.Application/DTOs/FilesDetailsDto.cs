namespace LawyerAssistant.Application.DTOs;

public class FilesDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsLegal { get; set; }

    public int? CustomerId { get; set; }
    public string CustomerFullName { get; set; }

    public int? LegalId { get; set; }
    public string LegalCompanyName { get; set; }

    public int DemandId { get; set; }
    public string DemandTitle { get; set; }

    public int FileTypeId { get; set; }
    public string FileTypeTitle { get; set; }
}
