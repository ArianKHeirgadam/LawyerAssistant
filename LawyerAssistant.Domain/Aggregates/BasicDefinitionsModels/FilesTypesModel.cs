using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

public class FilesTypesModel : ModifyDateTimeWithUserModel
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
