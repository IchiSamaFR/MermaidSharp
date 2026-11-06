namespace MermaidSharp.Samples.Domain
{
    public abstract class LibraryItem : ILoanable
    {
        public string Title { get; set; } = string.Empty;

        public bool IsAvailable { get; protected set; } = true;

        public Loan Lend(Member member, int days)
        {
            IsAvailable = false;
            return new Loan { Item = this, Member = member, DueDate = DateTime.Today.AddDays(days) };
        }
    }
}
