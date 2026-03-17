using MermaidDotNet.Extensions;

namespace MermaidDotNet.Models
{
    public class ClassMethodParam
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public ClassMethodParam(string name, string type = "")
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            var returnedParts = new string[]
            {
                Type.FormatAngleBracket(),
                Name
            };
            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
