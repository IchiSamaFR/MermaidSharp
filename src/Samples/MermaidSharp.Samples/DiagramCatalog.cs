using MermaidSharp.Samples.Examples;

namespace MermaidSharp.Samples
{
    /// <summary>
    /// Provides every sample diagram shown by the sample applications and checked by the sample tests.
    /// </summary>
    public static class DiagramCatalog
    {
        private static readonly Lazy<IReadOnlyList<DiagramExample>> LazyAll = new(Create);

        /// <summary>
        /// Gets all sample diagrams, ordered by diagram type. The list is built once and cached.
        /// </summary>
        public static IReadOnlyList<DiagramExample> All => LazyAll.Value;

        /// <summary>
        /// Gets the sample diagrams grouped by diagram type.
        /// </summary>
        public static IEnumerable<IGrouping<DiagramKind, DiagramExample>> ByKind => All.GroupBy(e => e.Kind);

        /// <summary>
        /// Builds a new list of all sample diagrams, generating every definition again.
        /// </summary>
        /// <returns>The sample diagrams, ordered by diagram type.</returns>
        public static IReadOnlyList<DiagramExample> Create()
        {
            return FlowchartExamples.Create()
                .Concat(ClassDiagramExamples.Create())
                .Concat(EntityRelationshipExamples.Create())
                .Concat(GitGraphExamples.Create())
                .Concat(PieChartExamples.Create())
                .Concat(QuadrantChartExamples.Create())
                .Concat(XYChartExamples.Create())
                .Concat(AutoDiagramExamples.Create())
                .ToList();
        }
    }
}
