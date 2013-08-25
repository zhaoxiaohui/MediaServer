using System;
using System.Text.RegularExpressions;

namespace ValueWebSocket.Infrastructure
{
    public class Common
    {
        public static MatchCollection GetRegexValue(String input, String pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern);
            return matches;
        }
    }
}
