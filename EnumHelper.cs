using AspNetCore.Container.Models;

namespace AspNetCore.Container;

/// <summary>
/// 
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="en"></param>
    /// <returns></returns>
    public static SelectableItem ToDisplay<TEnum>(this TEnum en)
        where TEnum : Enum
    {
        return new()
        {
            Type = en.ToString(),
            DisplayName = en.ToString(),
            Id = Convert.ToInt32(en),
        };
    }
}
