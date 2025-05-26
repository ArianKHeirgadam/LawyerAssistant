using Domain.Aggregates.Identities;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;
using System.Xml.Linq;

namespace LawyerAssistant.Domain.Aggregates;

public class FilesModel : ModifyDateTimeWithUserModel, IEntity
{
    #region Constructors
    protected FilesModel() { }

    public FilesModel(
        string title,
        int demandId,
        int fileTypeId,
        int? customerId,
        int? legalId,
        bool? isLegal = null)
    {
        Title = title;
        IsLegal = isLegal ?? false;
        DemandId = demandId;
        FileTypeId = fileTypeId;
        LegalId = legalId;
        CustomerId = customerId;

        RegDateTime = DateTime.UtcNow;
    }
    #endregion

    #region Edit Method
    public void Edit(
        string title, 
        int demandId,
        int fileTypeId,
        int? customerId,
        int? legalId,
        bool? isLegal = null)
    {
        Title = title;
        IsLegal = isLegal ?? false;
        DemandId = demandId;
        FileTypeId = fileTypeId;
        LegalId = legalId;
        CustomerId = customerId;
        ModDateTime = DateTime.UtcNow;
    }
    #endregion

    #region Properties
    public string Title { get; set; }
    public int? CustomerId { get; set; }
    public int? LegalId { get; set; }
    public bool IsLegal { get; set; } = false;
    public int DemandId { get; set; }
    public int FileTypeId { get; set; }

    public DemandsModel Demand { get; set; }
    public CustomersModel Customer { get; set; }
    public LegalCustomersModel Legal { get; set; }
    public FilesTypesModel FilesTypes { get; set; }
    public ICollection<ReActionModel> Reactions { get; set; }
    #endregion

}
