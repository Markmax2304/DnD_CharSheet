using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMPro;

public static class UIExtensions
{
    public static void FillOptionsByEnum<T>(this TMP_Dropdown dropdown, Func<T, string> convertAction) where T : Enum
    {
        dropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach(T value in Enum.GetValues(typeof(T)))
            options.Add(convertAction(value));

        dropdown.AddOptions(options);
    }
}