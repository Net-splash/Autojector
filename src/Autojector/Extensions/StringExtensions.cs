using System.Text.RegularExpressions;

namespace Autojector.Extensions
{
    internal static class StringExtensions
    {
        public static string RemoveInterfacePrefix(this string name) => Regex.Replace(name, "^I", string.Empty);
    }
}
