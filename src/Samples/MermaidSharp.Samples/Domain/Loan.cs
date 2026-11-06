namespace MermaidSharp.Samples.Domain
{
    public class Loan
    {
        public LibraryItem Item { get; set; } = null!;

        public Member Member { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public bool IsLate(DateTime today) => today > DueDate;
    }
}
