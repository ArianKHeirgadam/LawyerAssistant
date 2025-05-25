using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

public class DemandsModel : ModifyDateTimeWithUserModel, IEntity
{
    protected DemandsModel()
    {

    }

    public DemandsModel(string name, int filetypeId)
    {
        Name = name;
        FileTypeId = filetypeId;
    }

    public void Edit(string name, int filetypeId)
    {
        Name = name;
        FileTypeId = filetypeId;
    }
    //===========================================================
    /// <summary>
    ///  نام 
    /// </summary>
    public string Name { get; set; }

    public int FileTypeId { get; set; }
    public FilesTypesModel FilesType { get; set; }
    public ICollection<FilesModel> Files { get; set; }
}
