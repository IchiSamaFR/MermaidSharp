using MermaidSharp.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Extensions
{
    internal static class StringExtension
    {
        private static readonly char[] MermaidLabelSpecialChars = { '"', '[', ']', '(', ')', '{', '}', '<', '>', '|', ';' };
        private static readonly char[] YamlSpecialChars = { '"', '\'', '#', '\\', '\r', '\n', '\t' };
        private const string YamlIndicatorChars = "-?:,[]{}&*!|>%@`";
        private static readonly string[] YamlReservedWords = { "true", "false", "null", "yes", "no", "on", "off", "~" };

        public static string JoinNonEmpty(this IEnumerable<string> lst, string separator)
        {
            return string.Join(separator, lst.Where(i => !string.IsNullOrEmpty(i)));
        }

        public static string Indent(this string str, int indent = 1)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            string indentString = string.Concat(Enumerable.Repeat(FormattingConstants.Indentation, indent));
            var lines = str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Select(line => indentString + line));
        }

        public static List<string> Indent(this IEnumerable<string> lst, int indent = 1)
        {
            return lst.Select(i => i.Indent(indent)).ToList();
        }

        public static List<string> ClearNewLines(this IEnumerable<string> lst)
        {
            return lst.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
        }

        public static string FormatAngleBracket(this string str)
        {
            return str?.Replace("<", "~").Replace(">", "~") ?? string.Empty;
        }

        /// <summary>
        /// Normalizes a Mermaid identifier by removing spaces. Throws when the identifier is null.
        /// </summary>
        public static string ToMermaidId(this string value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);

            return value.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Replaces double quotes with the Mermaid entity code so the text can be placed inside a quoted label.
        /// </summary>
        public static string EscapeMermaidQuotes(this string text)
        {
            return text?.Replace("\"", "#quot;") ?? string.Empty;
        }

        /// <summary>
        /// Wraps a Mermaid label in double quotes when it contains characters that would break the syntax.
        /// </summary>
        public static string ToMermaidLabel(this string text)
        {
            if (string.IsNullOrEmpty(text) || text.IndexOfAny(MermaidLabelSpecialChars) < 0)
                return text ?? string.Empty;

            return $"\"{text.EscapeMermaidQuotes()}\"";
        }

        /// <summary>
        /// Formats a value as a double-quoted YAML string, escaping backslashes, quotes and control characters.
        /// </summary>
        public static string ToYamlQuoted(this string text)
        {
            var escaped = (text ?? string.Empty)
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
            return $"\"{escaped}\"";
        }

        /// <summary>
        /// Formats a value as a YAML scalar, quoting it only when the plain form would be invalid or misinterpreted.
        /// </summary>
        public static string ToYamlScalar(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return "\"\"";

            var needsQuotes = text != text.Trim()
                || text.IndexOfAny(YamlSpecialChars) >= 0
                || YamlIndicatorChars.IndexOf(text[0]) >= 0
                || text.Contains(": ")
                || text.EndsWith(":", StringComparison.Ordinal)
                || YamlReservedWords.Contains(text.ToLowerInvariant());

            return needsQuotes ? text.ToYamlQuoted() : text;
        }
    }
}
