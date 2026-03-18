using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Models
{
    public class ClassMethod
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public ClassPropertyVisibility Visibility { get; set; }
        public List<ClassMethodParam> Parameters { get; }

        public ClassMethod(string name, string returnType = "", ClassPropertyVisibility visibility = ClassPropertyVisibility.Public, List<ClassMethodParam> parameters = null)
        {
            Name = name;
            ReturnType = returnType;
            Visibility = visibility;
            Parameters = parameters ?? new List<ClassMethodParam>();
        }

        public override string ToString()
        {
            var visibilitySymbol = Visibility.StartString();
            var returnTypeString = ReturnType.FormatAngleBracket();
            var parametersString = string.Join(", ", Parameters.Select(p => p.ToString()));

            var returnedParts = new string[]
            {
                $"{visibilitySymbol}{Name}({parametersString})",
                returnTypeString
            };
            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
