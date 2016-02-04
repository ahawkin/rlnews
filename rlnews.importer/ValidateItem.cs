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
        /// Limits the lenght of the input string to 200 characters
        /// </summary>
        /// <param name="input"></param>
        public string LimitLength(string input)
        {
            string trimmedInput = input.Substring(0, Math.Min(150, input.Length));
            return trimmedInput;
        }

        /// <summary>
        /// Removes and cleans any HTML characters from strings
        /// </summary>
        /// <param name="input"></param>
        public string RemoveHtml(string input)
        {
            var removeHtml = Regex.Replace(input, @"<[^>]+>|•|&nbsp;", "").Trim();
            var removeExtraSpaces = Regex.Replace(removeHtml, @"\s{2,}", " ");
            return removeExtraSpaces;
        }

    }
}
