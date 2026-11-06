namespace MermaidSharp.Samples.Domain
{
    public class Book : LibraryItem
    {
        public string Isbn { get; set; } = string.Empty;

        public Author Author { get; set; } = new Author();
    }
}
