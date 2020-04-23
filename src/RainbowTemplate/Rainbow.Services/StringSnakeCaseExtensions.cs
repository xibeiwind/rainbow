using System.Linq;
using System.Text.RegularExpressions;

namespace Rainbow.Services
{
    public static class StringSnakeCaseExtensions
    {
        public static string SnakeCase(this string target, string separator = "_")
        {
            var pattern = "([A-Z]?[a-z]*)";
            if (Regex.IsMatch(target, pattern))
            {
                var matches = Regex.Matches(target, pattern, RegexOptions.IgnorePatternWhitespace);

                return string.Join(separator,
                    matches.Where(a => !string.IsNullOrEmpty(a.Value)).Select(a => a.Groups[1].Value.ToLower()));
            }

            return target;
        }
    }
}