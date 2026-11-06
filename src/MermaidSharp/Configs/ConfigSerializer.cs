using MermaidSharp.Constants;
using MermaidSharp.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Provides the shared serialization logic used by configuration and theme variable objects.
    /// </summary>
    internal static class ConfigSerializer
    {
        /// <summary>
        /// Returns the non-null values of the public properties of an instance decorated with the specified attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute that marks the serialized properties.</typeparam>
        /// <param name="instance">The object whose properties are read.</param>
        /// <returns>The attribute and value of each decorated property that has a value.</returns>
        public static IEnumerable<KeyValuePair<TAttribute, object>> GetDecoratedValues<TAttribute>(object instance)
            where TAttribute : Attribute
        {
            foreach (var entry in PropertyCache<TAttribute>.Get(instance.GetType()))
            {
                var value = entry.Key.GetValue(instance);
                if (value != null)
                    yield return new KeyValuePair<TAttribute, object>(entry.Value, value);
            }
        }

        /// <summary>
        /// Formats a scalar value as a YAML value, or returns null when the value must not be written.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The YAML representation of the value, or null for null and empty values.</returns>
        public static string FormatValue(object value)
        {
            switch (value)
            {
                case null:
                    return null;
                case double dblVal:
                    return dblVal.ToString("G", CultureInfo.InvariantCulture);
                case float floatVal:
                    return floatVal.ToString("G", CultureInfo.InvariantCulture);
                case decimal decVal:
                    return decVal.ToString("G", CultureInfo.InvariantCulture);
                case bool boolVal:
                    return boolVal ? "true" : "false";
                case string strVal:
                    return string.IsNullOrEmpty(strVal) ? null : strVal.ToYamlQuoted();
                case Enum enumVal:
                    var enumString = enumVal.PrimaryString();
                    return string.IsNullOrEmpty(enumString) ? null : enumString;
                case IFormattable formattable:
                    return formattable.ToString(null, CultureInfo.InvariantCulture);
                default:
                    return value.ToString();
            }
        }

        /// <summary>
        /// Wraps configuration lines in a Mermaid front matter block.
        /// </summary>
        /// <param name="lines">The configuration lines to wrap.</param>
        /// <returns>The front matter block, or an empty string when there are no lines.</returns>
        public static string ToFrontMatter(IReadOnlyCollection<string> lines)
        {
            if (lines.Count == 0)
                return string.Empty;

            var block = new List<string> { FormattingConstants.TitleSeparator };
            block.AddRange(lines);
            block.Add(FormattingConstants.TitleSeparator);
            return string.Join(Environment.NewLine, block);
        }

        private static class PropertyCache<TAttribute>
            where TAttribute : Attribute
        {
            private static readonly ConcurrentDictionary<Type, KeyValuePair<PropertyInfo, TAttribute>[]> Properties
                = new ConcurrentDictionary<Type, KeyValuePair<PropertyInfo, TAttribute>[]>();

            public static KeyValuePair<PropertyInfo, TAttribute>[] Get(Type type)
            {
                return Properties.GetOrAdd(type, t => t
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Select(p => new KeyValuePair<PropertyInfo, TAttribute>(p, p.GetCustomAttribute<TAttribute>()))
                    .Where(entry => entry.Value != null)
                    .ToArray());
            }
        }
    }
}
