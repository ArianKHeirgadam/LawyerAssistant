using Application.Enums;

namespace LawyerAssistant.Application.Extentions;

public static class BooleanExtension
{
    public static ActiveStatus ToActiveStatus(this bool Value)
    {
        return Value ? ActiveStatus.Active : ActiveStatus.Deactive;
    }
    public static bool ToBoolean(this ActiveStatus Value)
    {
        return Value == ActiveStatus.Active ? true : false;
    }
}
