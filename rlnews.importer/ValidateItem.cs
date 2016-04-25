using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace rlnews.importer
{
    public class ValidateItem
    {
        /// <summary>
        /// Limits the lenght of the input string to 150 characters
        /// </summary>
        /// <param name="input"></param>
        public string TrimNewsLength(string input)
        {
            var trimmedNews = input.Substring(0, Math.Min(150, input.Length));
            return trimmedNews;
        }

        /// <summary>
        /// Removes and cleans any HTML characters from strings
        /// </summary>
        /// <param name="input"></param>
        public string TrimNewsHtml(string input)
        {
            var trimmedNewsHtml = Regex.Replace(input, @"<[^>]+>|•|&nbsp;", "").Trim();
            var removeExtraSpaces = Regex.Replace(trimmedNewsHtml, @"\s{2,}", " ");
            return removeExtraSpaces;
        }

    }
}
