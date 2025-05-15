namespace LawyerAssistant.Application.Objects;

//********************************************************************************************************************
public class DataTableResponse
{
    public int draw { get; set; }

    public int recordsTotal { get; set; }

    public int recordsFiltered { get; set; }

    public object data { get; set; }
}
//********************************************************************************************************************
