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
        Demands = new HashSet<DemandsModel>();
    }

    public void Edit(string name)
    {
        Name = name;
    }
    //===========================================================
    /// <summary>
    ///  نام 
    /// </summary>
    public string Name { get; set; }
    public ICollection<DemandsModel> Demands { get; set; }
}
