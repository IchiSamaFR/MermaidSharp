namespace MermaidSharp.Samples.Domain
{
    public class Author
    {
        public string Name { get; set; } = string.Empty;

        public List<Book> Books { get; } = new List<Book>();
    }
}
