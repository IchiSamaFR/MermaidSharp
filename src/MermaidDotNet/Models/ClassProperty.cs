using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;

namespace MermaidDotNet.Models
{
    /// <summary>
    /// Represents a property within a class, including its name, type, and visibility.
    /// </summary>
    /// <remarks>Use this class to model and describe properties when generating or analyzing class structures
    /// programmatically. The visibility is represented by the ClassPropertyVisibility enumeration, which determines how
    /// the property is exposed in the class definition.</remarks>
    public class ClassProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ClassPropertyVisibility Visibility { get; set; }

        /// <summary>
        /// Initializes a new instance of the ClassProperty class with the specified property name, type, and
        /// visibility.
        /// </summary>
        /// <param name="name">The name of the property to be represented.</param>
        /// <param name="type">The data type of the property.</param>
        /// <param name="visibility">The visibility level of the property, such as public, private, or protected.</param>
        public ClassProperty(string name, string type, ClassPropertyVisibility visibility)
        {
            Name = name;
            Type = type;
            Visibility = visibility;
        }

        public override string ToString()
        {
            var visibilitySymbol = Visibility.StartString();
            return $"{visibilitySymbol}{Type.FormatAngleBracket()} {Name}";
        }
    }
}
