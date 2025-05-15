using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LawyerAssistant.Application.Extentions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field.GetCustomAttribute<DisplayAttribute>();

        return attribute?.GetName() ?? value.ToString();
    }


    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<DisplayAttribute>();
        return attribute?.Description ?? "";
    }
}
