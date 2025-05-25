using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

public class FilesTypesModel : ModifyDateTimeWithUserModel, IEntity
{
    protected FilesTypesModel()
    {

    }

    public FilesTypesModel(string name)
    {
        Name = name;
        RegDateTime = DateTime.UtcNow;
        Demands = new HashSet<DemandsModel>();
        Reactions = new HashSet<ReActionModel>();
    }

    public void Edit(string name)
    {
        Name = name;
        ModDateTime = DateTime.UtcNow;
    }
    //===========================================================
    /// <summary>
    ///  نام 
    /// </summary>
    public string Name { get; set; }
    public ICollection<DemandsModel> Demands { get; set; }
    public ICollection<ReActionModel> Reactions { get; set; }
    public ICollection<FilesModel> Files { get; set; }
}
