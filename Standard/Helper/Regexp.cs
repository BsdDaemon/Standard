using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Standard.Helper
{
    public static class Regexp
    {
        public static string Replace(string expression, string text, string value) => Regex.Replace(text, expression, value);
        public static string Remove(string expression, string text) => Regex.Replace(text, expression, string.Empty);

        public static string[] Matches(string expression, string text, bool inverse = false)
        {
            Regex myRegex = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
            List<string> matches = new List<string>();

            foreach (Match token in myRegex.Matches(text))
            {
                if (inverse)
                {
                    if (!token.Success) matches.Add(token.Value);
                }
                else
                {
                    if (token.Success) matches.Add(token.Value);
                }
            }

            return matches.ToArray();
        }
    }
}
