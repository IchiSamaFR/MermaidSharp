using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;

namespace MermaidDotNet.Models
{
    public class ClassProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ClassPropertyVisibility Visibility { get; set; }

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
