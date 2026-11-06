namespace MermaidSharp.Samples.Domain
{
    public interface ILoanable
    {
        bool IsAvailable { get; }

        Loan Lend(Member member, int days);
    }
}
