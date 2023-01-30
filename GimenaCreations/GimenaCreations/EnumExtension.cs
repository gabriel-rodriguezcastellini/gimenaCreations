using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GimenaCreations;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue) => enumValue.GetType()?.GetMember(enumValue.ToString())?.First()?.GetCustomAttribute<DisplayAttribute>()?.Name!;
}
