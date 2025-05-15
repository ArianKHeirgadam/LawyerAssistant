using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Base;

public class IntegerIdentifierDTOModel
{
    public int Id { get; set; }
}

public class IntegerIdentifierStringDTOModel
{
    public string Id { get; set; }
}



public class IntegerIdentifierStringAppDTOModel
{
    [Required(ErrorMessage = "شناسه اپ استور الزامی می باشد.")]
    public string AppId { get; set; }
}

