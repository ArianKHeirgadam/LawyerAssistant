using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

public class DemandsModel : ModifyDateTimeWithUserModel
{
    protected DemandsModel()
    {

    }

    public DemandsModel(string name, int fileId)
    {
        Name = name;
        FileId = fileId;
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

    public int FileId { get; set; }
    public FilesTypesModel FilesType { get; set; }
}
