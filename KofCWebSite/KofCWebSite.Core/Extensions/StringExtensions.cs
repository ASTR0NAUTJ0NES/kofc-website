using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// IsTrimmedEquals - Performs a case-INsensitive stringcomparison
        /// </summary>
        /// <param name="s"></param>
        /// <param name="other"></param>
        /// <returns>true if matched and false if not matched or both or one are null</returns>
        public static bool IsTrimmedEquals(this string s, string other)
        {
            if (s == null && other == null) return true;
            if (s == null || other == null) return false;

            return s.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsTrimmedEqualsIgnoreCase(this string s, string other)
        {
            if (s == null && other == null) return true;
            if (s == null || other == null) return false;

            return s.Trim().Equals(other.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
