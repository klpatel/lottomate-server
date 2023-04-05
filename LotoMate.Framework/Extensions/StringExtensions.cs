using System.Linq;

namespace LotoMate.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalName(this string source)
        {
            return source.Substring(0, 1).ToUpper() + source.Substring(1);
        }
        public static string ToAbbreviatedName(this string source)
        {
            return source==null? "" : new string(
                source.Split()
                    .Where(s => s.Length > 0 && char.IsLetter(s[0]) && char.IsUpper(s[0]))
                    .Take(3).Select(s => s[0])
                    .ToArray());
        }
        public static string ToLastNoOfChars(this string source, int length)
        {
            source = source?.Trim();
            if (string.IsNullOrEmpty(source) || source.Length <= length)
                return source;
            return source.Substring(source.Length - 4);
        }

    }
}
