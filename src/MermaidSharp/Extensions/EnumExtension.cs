using MermaidSharp.Attributes;
using System;
using System.Collections.Concurrent;

namespace MermaidSharp.Extensions
{
    internal static class EnumExtension
    {
        private static readonly ConcurrentDictionary<Enum, MermaidEnumAttribute> AttributeCache
            = new ConcurrentDictionary<Enum, MermaidEnumAttribute>();

        /// <summary>
        /// Returns the primary Mermaid syntax associated with the enum value, or an empty string when none is defined.
        /// </summary>
        public static string PrimaryString(this Enum value)
        {
            return GetMermaidAttribute(value)?.Primary ?? string.Empty;
        }

        /// <summary>
        /// Returns the secondary Mermaid syntax associated with the enum value, or an empty string when none is defined.
        /// </summary>
        public static string SecondaryString(this Enum value)
        {
            return GetMermaidAttribute(value)?.Secondary ?? string.Empty;
        }

        private static MermaidEnumAttribute GetMermaidAttribute(Enum value)
        {
            if (value == null)
                return null;

            return AttributeCache.GetOrAdd(value, v =>
            {
                var field = v.GetType().GetField(v.ToString());
                return field == null
                    ? null
                    : (MermaidEnumAttribute)Attribute.GetCustomAttribute(field, typeof(MermaidEnumAttribute));
            });
        }
    }
}
