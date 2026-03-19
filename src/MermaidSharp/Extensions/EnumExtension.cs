using MermaidSharp.Attributes;
using System;

namespace MermaidSharp.Extensions
{
    internal static class EnumExtension
    {
        public static string StartString(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            var attr = (MermaidEnumAttribute)Attribute.GetCustomAttribute(field, typeof(MermaidEnumAttribute));
            if (attr == null)
            {
                return string.Empty;
            }

            return attr.Start;
        }

        public static string EndString(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            var attr = (MermaidEnumAttribute)Attribute.GetCustomAttribute(field, typeof(MermaidEnumAttribute));
            if (attr == null)
            {
                return string.Empty;
            }

            return attr.End;
        }
    }
}
