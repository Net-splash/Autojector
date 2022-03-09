using System.Text.RegularExpressions;

namespace Autojector.Base;

internal static class StringExtensionMethods { 
    public static string RemoveInterfacePrefix(this string name) => Regex.Replace(name, "^I", string.Empty);
}
