namespace MermaidSharp.Samples.Domain
{
    public class Member
    {
        public string Name { get; set; } = string.Empty;

        internal int CardNumber { get; set; }

        public bool CanBorrow() => true;
    }
}
